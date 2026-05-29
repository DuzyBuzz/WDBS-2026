using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using ClosedXML.Excel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace WDBS_2026.Services.Printing;

public sealed record BillingReportDocumentData(
    string ReportTitle,
    string PeriodCaption,
    string PrintedBy,
    DateTime PrintedAt,
    DataTable Rows);

internal sealed class BillingReportPrintHelper : IReportPreviewSource
{
    private const string WaterDistrictTitle = "TUBUNGAN WATER DISTRICT";
    private const float HeaderHeight = 60F;
    private const float TableHeaderHeight = 24F;
    private const float FooterHeight = 20F;
    private const float CellPadding = 4F;
    private const float MinimumRowHeight = 22F;

    private static readonly Color HeaderFillColor = AppTheme.PrimaryDarkColor;
    private static readonly Color HeaderTextColor = Color.White;
    private static readonly Color BorderDrawColor = AppTheme.BorderColor;
    private static readonly Color AlternateRowColor = Color.FromArgb(246, 249, 251);
    private static readonly Color SummaryRowColor = Color.FromArgb(255, 245, 204);

    private readonly BillingReportDocumentData _document;
    private readonly bool _isMonthlyReport;
    private readonly bool _isDailyReport;
    private readonly object _rowsSync = new();
    private readonly object _previewPreparationSync = new();

    private IReadOnlyList<BillingReportRowData>? _rows;
    private IReadOnlyList<ReportPageLayout>? _preparedPreviewPages;
    private int _preparedPreviewPageCount = 1;

    private static readonly object PdfFontResolverSync = new();
    private static bool _pdfFontResolverConfigured;

    public BillingReportPrintHelper(BillingReportDocumentData document)
    {
        _document = document;
        _isMonthlyReport = document.ReportTitle.Contains("Monthly", StringComparison.OrdinalIgnoreCase);
        _isDailyReport = document.ReportTitle.Contains("Daily", StringComparison.OrdinalIgnoreCase);
    }

    public Task PreparePreviewAsync(IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => PreparePreviewInternal(progress, cancellationToken), cancellationToken);
    }

    public PrintDocument CreatePreviewDocument()
    {
        IReadOnlyList<ReportPageLayout> preparedPages = EnsurePreparedPreviewPages();
        var state = new PrintRenderState(preparedPages.ToList());
        return CreatePrintDocument(state, preservePreparedPages: true);
    }

    public PrintDocument CreatePrintDocument()
    {
        return CreatePrintDocument(new PrintRenderState(), preservePreparedPages: false);
    }

    private PrintDocument CreatePrintDocument(PrintRenderState state, bool preservePreparedPages)
    {
        var document = new PrintDocument();
        document.DefaultPageSettings.Landscape = true;
        document.DefaultPageSettings.Margins = new Margins(30, 30, 42, 34);
        document.BeginPrint += (_, _) => state.Reset(preservePreparedPages);
        document.PrintPage += (_, e) => PrintPage(e, state);
        return document;
    }

    public int GetPreviewPageCount()
    {
        if (_preparedPreviewPages is null)
        {
            PreparePreviewInternal(progress: null, CancellationToken.None);
        }

        return _preparedPreviewPageCount;
    }

    public Task ExportToPdfAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => ExportToPdfInternal(filePath, progress, cancellationToken), cancellationToken);
    }

    public void ExportToPdf(string filePath)
    {
        ExportToPdfAsync(filePath).GetAwaiter().GetResult();
    }

    private void ExportToPdfInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        EnsurePdfFontResolverConfigured();
        ReportProgress(progress, 5, "Preparing PDF export...");

        using var pdfDocument = new PdfDocument();
        pdfDocument.Info.Title = _document.ReportTitle;

        const float pageWidth = 841.89F;
        const float pageHeight = 595.28F;
        var contentBounds = new RectangleF(24F, 24F, pageWidth - 48F, pageHeight - 48F);

        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.PageUnit = GraphicsUnit.Point;
        List<ReportPageLayout> pages = BuildPages(graphics, contentBounds, progress, cancellationToken, "Preparing PDF pages", 10, 55);

        using XImage? leftLogo = TryLoadPdfImage(Path.Combine(AppContext.BaseDirectory, "Resources", "republika_ng_pilipinas.jpg"));
        using XImage? rightLogo = TryLoadPdfImage(Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg"));

        for (int pageIndex = 0; pageIndex < pages.Count; pageIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            PdfPage page = pdfDocument.AddPage();
            page.Size = PageSize.A4;
            page.Orientation = PageOrientation.Landscape;

            using XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawPdfPage(gfx, contentBounds, pages[pageIndex], pageIndex + 1, pages.Count, leftLogo, rightLogo);

            int percentage = pages.Count == 0
                ? 95
                : 55 + (int)Math.Round(((pageIndex + 1D) / pages.Count) * 40D, MidpointRounding.AwayFromZero);
            ReportProgress(progress, percentage, $"Exporting PDF page {pageIndex + 1} of {pages.Count}...");
        }

        pdfDocument.Save(filePath);
        ReportProgress(progress, 100, "PDF export completed.");
    }

    public Task ExportToExcelAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => ExportToExcelInternal(filePath, progress, cancellationToken), cancellationToken);
    }

    public void ExportToExcel(string filePath)
    {
        ExportToExcelAsync(filePath).GetAwaiter().GetResult();
    }

    private void ExportToExcelInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        ReportProgress(progress, 5, "Preparing Excel export...");

        using var workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Billing Report");
        int totalColumns = Math.Max(1, _document.Rows.Columns.Count);

        sheet.Cell(1, 1).Value = WaterDistrictTitle;
        sheet.Cell(2, 1).Value = _document.ReportTitle;
        sheet.Cell(3, 1).Value = _document.PeriodCaption;
        sheet.Cell(4, 1).Value = $"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}";

        foreach (int rowIndex in new[] { 1, 2, 3, 4 })
        {
            var rowRange = sheet.Range(rowIndex, 1, rowIndex, totalColumns);
            rowRange.Merge();
            rowRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        sheet.Cell(1, 1).Style.Font.Bold = true;
        sheet.Cell(1, 1).Style.Font.FontSize = 16;
        sheet.Cell(2, 1).Style.Font.Bold = true;
        sheet.Cell(2, 1).Style.Font.FontSize = 12;
        sheet.Cell(3, 1).Style.Font.FontSize = 10;
        sheet.Cell(4, 1).Style.Font.FontSize = 10;

        for (int columnIndex = 0; columnIndex < _document.Rows.Columns.Count; columnIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            sheet.Cell(6, columnIndex + 1).Value = _document.Rows.Columns[columnIndex].ColumnName;
        }

        int totalRows = _document.Rows.Rows.Count;
        for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            DataRow row = _document.Rows.Rows[rowIndex];
            for (int columnIndex = 0; columnIndex < _document.Rows.Columns.Count; columnIndex++)
            {
                SetExcelCellValue(sheet.Cell(rowIndex + 7, columnIndex + 1), row[columnIndex]);
            }

            int percentage = totalRows == 0
                ? 95
                : 10 + (int)Math.Round(((rowIndex + 1D) / totalRows) * 85D, MidpointRounding.AwayFromZero);
            ReportProgress(progress, percentage, $"Writing Excel rows {rowIndex + 1} of {totalRows}...");
        }

        if (totalColumns > 0)
        {
            int lastRow = Math.Max(6, totalRows + 6);
            var range = sheet.Range(6, 1, lastRow, totalColumns);
            var table = range.CreateTable("BillingReportTable");
            table.Theme = XLTableTheme.TableStyleMedium9;
        }

        foreach (string columnName in new[]
                 {
                     "Cu_m³", "Un_m³", "Water_Bill", "Tax", "Discount", "Total_Water_Bill", "Arrears", "SCF", "Remaining_Balance", "Total_Amount_Billed",
                     "consumption", "free_water", "water_charge", "tax_amount", "discount_amount", "total_water_bill", "arrears_amount", "scf_amount", "remaining_balance", "total_amount"
                 })
        {
            int columnIndex = _document.Rows.Columns.IndexOf(columnName) + 1;
            if (columnIndex > 0)
            {
                sheet.Column(columnIndex).Style.NumberFormat.Format = "#,##0.00";
            }
        }

        sheet.Columns().AdjustToContents();
        sheet.SheetView.FreezeRows(6);

        workbook.SaveAs(filePath);
        ReportProgress(progress, 100, "Excel export completed.");
    }

    private void PrintPage(PrintPageEventArgs e, PrintRenderState state)
    {
        if (e.Graphics is null)
        {
            e.HasMorePages = false;
            return;
        }

        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        var contentBounds = new RectangleF(
            e.MarginBounds.Left,
            e.MarginBounds.Top,
            e.MarginBounds.Width,
            e.MarginBounds.Height);

        List<ReportPageLayout> pages = state.Pages ??= BuildPages(
            graphics,
            contentBounds,
            state.Progress,
            state.CancellationToken,
            "Preparing preview pages",
            15,
            90);
        if (state.PageIndex >= pages.Count)
        {
            e.HasMorePages = false;
            return;
        }

        DrawPrintPage(graphics, contentBounds, pages[state.PageIndex], state.PageIndex + 1, pages.Count);
        state.Advance();
        e.HasMorePages = state.PageIndex < pages.Count;
    }

    private List<ReportPageLayout> BuildPages(
        Graphics graphics,
        RectangleF contentBounds,
        IProgress<ReportOperationProgress>? progress = null,
        CancellationToken cancellationToken = default,
        string progressMessage = "Preparing preview pages",
        int progressStart = 10,
        int progressEnd = 85)
    {
        IReadOnlyList<BillingReportRowData> rows = GetRows();
        IReadOnlyList<ReportColumnLayout> columns = GetColumnLayouts(contentBounds);
        float availableHeight = contentBounds.Height - HeaderHeight - TableHeaderHeight - FooterHeight - 10F;

        var pages = new List<ReportPageLayout>();
        var currentRows = new List<ReportRowLayout>();
        float usedHeight = 0F;

        if (rows.Count == 0)
        {
            ReportProgress(progress, progressEnd, progressMessage);
        }

        for (int index = 0; index < rows.Count; index++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            BillingReportRowData row = rows[index];
            float rowHeight = GetRowHeight(graphics, row, columns);
            if (currentRows.Count > 0 && usedHeight + rowHeight > availableHeight)
            {
                pages.Add(new ReportPageLayout(currentRows.ToArray()));
                currentRows.Clear();
                usedHeight = 0F;
            }

            currentRows.Add(new ReportRowLayout(row, rowHeight));
            usedHeight += rowHeight;

            if (rows.Count <= 100 || index == rows.Count - 1 || index % 10 == 0)
            {
                int percentage = progressStart + (int)Math.Round(((index + 1D) / rows.Count) * Math.Max(progressEnd - progressStart, 1), MidpointRounding.AwayFromZero);
                ReportProgress(progress, percentage, $"{progressMessage} ({index + 1} of {rows.Count} rows)...");
            }
        }

        if (currentRows.Count == 0)
        {
            pages.Add(new ReportPageLayout(Array.Empty<ReportRowLayout>()));
        }
        else
        {
            pages.Add(new ReportPageLayout(currentRows.ToArray()));
        }

        return pages;
    }

    private IReadOnlyList<BillingReportRowData> GetRows()
    {
        if (_rows is not null)
        {
            return _rows;
        }

        lock (_rowsSync)
        {
            _rows ??= MapRows(_document.Rows, _isMonthlyReport);
            return _rows;
        }
    }

    private IReadOnlyList<ReportPageLayout> EnsurePreparedPreviewPages()
    {
        if (_preparedPreviewPages is null)
        {
            PreparePreviewInternal(progress: null, CancellationToken.None);
        }

        return _preparedPreviewPages!;
    }

    private void PreparePreviewInternal(IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        if (_preparedPreviewPages is not null)
        {
            ReportProgress(progress, 100, "Preview is ready.");
            return;
        }

        lock (_previewPreparationSync)
        {
            if (_preparedPreviewPages is not null)
            {
                ReportProgress(progress, 100, "Preview is ready.");
                return;
            }

            cancellationToken.ThrowIfCancellationRequested();
            ReportProgress(progress, 5, "Preparing preview document...");
            _ = GetRows();
            ReportProgress(progress, 10, "Formatting report rows...");

            var state = new PrintRenderState(progress: progress, cancellationToken: cancellationToken);
            using PrintDocument document = CreatePrintDocument(state, preservePreparedPages: true);
            var previewController = new PreviewPrintController();
            document.PrintController = previewController;
            document.Print();

            _preparedPreviewPages = (state.Pages ?? new List<ReportPageLayout> { new(Array.Empty<ReportRowLayout>()) }).ToArray();
            _preparedPreviewPageCount = Math.Max(1, _preparedPreviewPages.Count);
            ReportProgress(progress, 100, $"Prepared {_preparedPreviewPageCount} preview page(s).");
        }
    }

    private static void ReportProgress(IProgress<ReportOperationProgress>? progress, int percentage, string message)
    {
        progress?.Report(new ReportOperationProgress(Math.Clamp(percentage, 0, 100), message));
    }

    private float GetRowHeight(Graphics graphics, BillingReportRowData row, IReadOnlyList<ReportColumnLayout> columns)
    {
        ReportColumnLayout nameColumn = columns.First(column => column.Definition.Key == nameof(BillingReportRowData.ConcessionaireName));
        using var font = CreateBodyFont();
        float nameHeight = MeasureWrappedTextHeight(graphics, row.ConcessionaireName, font, nameColumn.Width - (CellPadding * 2F));
        return Math.Max(MinimumRowHeight, nameHeight + (CellPadding * 2F));
    }

    private void DrawPrintPage(Graphics graphics, RectangleF contentBounds, ReportPageLayout page, int pageNumber, int totalPages)
    {
        using var headerFillBrush = new SolidBrush(HeaderFillColor);
        using var headerTextBrush = new SolidBrush(HeaderTextColor);
        using var borderPen = new Pen(BorderDrawColor);
        using var alternateRowBrush = new SolidBrush(AlternateRowColor);
        using var summaryRowBrush = new SolidBrush(SummaryRowColor);
        using var primaryTextBrush = new SolidBrush(AppTheme.BodyTextColor);
        using var mutedTextBrush = new SolidBrush(AppTheme.MutedTextColor);
        using var titleFont = CreateTitleFont();
        using var reportFont = CreateReportTitleFont();
        using var captionFont = CreateCaptionFont();
        using var bodyFont = CreateBodyFont();
        using var bodyBoldFont = CreateBodyBoldFont();

        DrawHeader(graphics, contentBounds, titleFont, reportFont, captionFont, primaryTextBrush);

        IReadOnlyList<ReportColumnLayout> columns = GetColumnLayouts(contentBounds);
        float headerTop = contentBounds.Top + HeaderHeight;

        foreach (ReportColumnLayout column in columns)
        {
            var rect = new RectangleF(column.X, headerTop, column.Width, TableHeaderHeight);
            graphics.FillRectangle(headerFillBrush, rect);
            graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
            DrawCellText(graphics, column.Definition.HeaderText, bodyBoldFont, headerTextBrush, rect, CellTextAlignment.Center, false);
        }

        float currentTop = headerTop + TableHeaderHeight;
        for (int index = 0; index < page.Rows.Count; index++)
        {
            ReportRowLayout rowLayout = page.Rows[index];

            if (rowLayout.Row.IsSummaryRow)
            {
                graphics.FillRectangle(summaryRowBrush, new RectangleF(contentBounds.Left, currentTop, contentBounds.Width, rowLayout.Height));
            }
            else if (index % 2 == 1)
            {
                graphics.FillRectangle(alternateRowBrush, new RectangleF(contentBounds.Left, currentTop, contentBounds.Width, rowLayout.Height));
            }

            foreach (ReportColumnLayout column in columns)
            {
                var rect = new RectangleF(column.X, currentTop, column.Width, rowLayout.Height);
                graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
                string text = column.Definition.GetValue(rowLayout.Row);
                Font cellFont = rowLayout.Row.IsSummaryRow ? bodyBoldFont : bodyFont;
                DrawCellText(
                    graphics,
                    text,
                    cellFont,
                    primaryTextBrush,
                    rect,
                    column.Definition.Alignment,
                    column.Definition.Wrap,
                    column.Definition.PreventEllipsis);
            }

            currentTop += rowLayout.Height;
        }

        DrawFooter(graphics, contentBounds, captionFont, mutedTextBrush, pageNumber, totalPages);
    }

    private void DrawHeader(Graphics graphics, RectangleF contentBounds, Font titleFont, Font reportFont, Font captionFont, Brush textBrush)
    {
        float logoSize = 44F;
        float logoTop = contentBounds.Top;

        using Image? leftLogo = TryLoadImage(Path.Combine(AppContext.BaseDirectory, "Resources", "republika_ng_pilipinas.jpg"));
        using Image? rightLogo = TryLoadImage(Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg"));

        if (leftLogo is not null)
        {
            graphics.DrawImage(leftLogo, contentBounds.Left, logoTop, logoSize, logoSize);
        }

        if (rightLogo is not null)
        {
            graphics.DrawImage(rightLogo, contentBounds.Right - logoSize, logoTop, logoSize, logoSize);
        }

        float centerLeft = contentBounds.Left + logoSize + 12F;
        float centerWidth = contentBounds.Width - ((logoSize * 2F) + 24F);

        using var centerFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };

        graphics.DrawString(WaterDistrictTitle, titleFont, textBrush, new RectangleF(centerLeft, contentBounds.Top + 1F, centerWidth, 20F), centerFormat);
        graphics.DrawString(_document.ReportTitle, reportFont, textBrush, new RectangleF(centerLeft, contentBounds.Top + 22F, centerWidth, 16F), centerFormat);
        graphics.DrawString(_document.PeriodCaption, captionFont, textBrush, new RectangleF(centerLeft, contentBounds.Top + 39F, centerWidth, 14F), centerFormat);
    }

    private void DrawFooter(Graphics graphics, RectangleF contentBounds, Font footerFont, Brush textBrush, int pageNumber, int totalPages)
    {
        float footerTop = contentBounds.Bottom - FooterHeight;
        using var pen = new Pen(BorderDrawColor);
        graphics.DrawLine(pen, contentBounds.Left, footerTop, contentBounds.Right, footerTop);

        string leftText = $"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}";
        string rightText = $"Page {pageNumber} of {totalPages}";

        graphics.DrawString(leftText, footerFont, textBrush, new RectangleF(contentBounds.Left, footerTop + 3F, contentBounds.Width * 0.7F, FooterHeight), new StringFormat());

        using var rightFormat = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Near
        };

        graphics.DrawString(rightText, footerFont, textBrush, new RectangleF(contentBounds.Left + (contentBounds.Width * 0.7F), footerTop + 3F, contentBounds.Width * 0.3F, FooterHeight), rightFormat);
    }

    private void DrawCellText(Graphics graphics, string text, Font font, Brush brush, RectangleF rect, CellTextAlignment alignment, bool wrap, bool preventEllipsis = false)
    {
        RectangleF textBounds = RectangleF.Inflate(rect, -CellPadding, -CellPadding);
        using var format = new StringFormat
        {
            Alignment = alignment switch
            {
                CellTextAlignment.Left => StringAlignment.Near,
                CellTextAlignment.Center => StringAlignment.Center,
                CellTextAlignment.Right => StringAlignment.Far,
                _ => StringAlignment.Near
            },
            LineAlignment = wrap ? StringAlignment.Near : StringAlignment.Center,
            Trimming = preventEllipsis ? StringTrimming.None : StringTrimming.EllipsisCharacter
        };

        if (!wrap)
        {
            format.FormatFlags = StringFormatFlags.NoWrap;
        }

        graphics.DrawString(text, font, brush, textBounds, format);
    }

    private void DrawPdfPage(XGraphics gfx, RectangleF contentBounds, ReportPageLayout page, int pageNumber, int totalPages, XImage? leftLogo, XImage? rightLogo)
    {
        IReadOnlyList<ReportColumnLayout> columns = GetColumnLayouts(contentBounds);
        var titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
        var reportFont = new XFont("Arial", 10, XFontStyleEx.Bold);
        var captionFont = new XFont("Arial", 8, XFontStyleEx.Regular);
        var bodyFont = new XFont("Arial", 8, XFontStyleEx.Regular);
        var bodyBoldFont = new XFont("Arial", 8, XFontStyleEx.Bold);
        var headerBrush = new XSolidBrush(ToXColor(HeaderFillColor));
        var headerTextBrush = new XSolidBrush(ToXColor(HeaderTextColor));
        var bodyBrush = new XSolidBrush(ToXColor(AppTheme.BodyTextColor));
        var mutedBrush = new XSolidBrush(ToXColor(AppTheme.MutedTextColor));
        var borderPen = new XPen(ToXColor(BorderDrawColor), 0.5);
        var alternateBrush = new XSolidBrush(ToXColor(AlternateRowColor));
        var summaryBrush = new XSolidBrush(ToXColor(SummaryRowColor));

        float logoSize = 44F;
        if (leftLogo is not null)
        {
            gfx.DrawImage(leftLogo, contentBounds.Left, contentBounds.Top, logoSize, logoSize);
        }

        if (rightLogo is not null)
        {
            gfx.DrawImage(rightLogo, contentBounds.Right - logoSize, contentBounds.Top, logoSize, logoSize);
        }

        float centerLeft = contentBounds.Left + logoSize + 12F;
        float centerWidth = contentBounds.Width - ((logoSize * 2F) + 24F);
        DrawCenteredPdfText(gfx, WaterDistrictTitle, titleFont, bodyBrush, centerLeft, contentBounds.Top + 1F, centerWidth);
        DrawCenteredPdfText(gfx, _document.ReportTitle, reportFont, bodyBrush, centerLeft, contentBounds.Top + 22F, centerWidth);
        DrawCenteredPdfText(gfx, _document.PeriodCaption, captionFont, bodyBrush, centerLeft, contentBounds.Top + 39F, centerWidth);

        float headerTop = contentBounds.Top + HeaderHeight;
        foreach (ReportColumnLayout column in columns)
        {
            var rect = new XRect(column.X, headerTop, column.Width, TableHeaderHeight);
            gfx.DrawRectangle(headerBrush, rect);
            gfx.DrawRectangle(borderPen, rect);
            DrawPdfText(gfx, column.Definition.HeaderText, bodyBoldFont, headerTextBrush, rect, CellTextAlignment.Center, false);
        }

        float currentTop = headerTop + TableHeaderHeight;
        for (int index = 0; index < page.Rows.Count; index++)
        {
            ReportRowLayout rowLayout = page.Rows[index];

            if (rowLayout.Row.IsSummaryRow)
            {
                gfx.DrawRectangle(summaryBrush, contentBounds.Left, currentTop, contentBounds.Width, rowLayout.Height);
            }
            else if (index % 2 == 1)
            {
                gfx.DrawRectangle(alternateBrush, contentBounds.Left, currentTop, contentBounds.Width, rowLayout.Height);
            }

            foreach (ReportColumnLayout column in columns)
            {
                var rect = new XRect(column.X, currentTop, column.Width, rowLayout.Height);
                gfx.DrawRectangle(borderPen, rect);
                string text = column.Definition.GetValue(rowLayout.Row);
                XFont cellFont = rowLayout.Row.IsSummaryRow ? bodyBoldFont : bodyFont;
                DrawPdfText(gfx, text, cellFont, bodyBrush, rect, column.Definition.Alignment, column.Definition.Wrap);
            }

            currentTop += rowLayout.Height;
        }

        float footerTop = contentBounds.Bottom - FooterHeight;
        gfx.DrawLine(borderPen, contentBounds.Left, footerTop, contentBounds.Right, footerTop);
        DrawPdfText(gfx, $"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}", captionFont, mutedBrush, new XRect(contentBounds.Left, footerTop + 3F, contentBounds.Width * 0.7F, FooterHeight), CellTextAlignment.Left, false);
        DrawPdfText(gfx, $"Page {pageNumber} of {totalPages}", captionFont, mutedBrush, new XRect(contentBounds.Left + (contentBounds.Width * 0.7F), footerTop + 3F, contentBounds.Width * 0.3F, FooterHeight), CellTextAlignment.Right, false);
    }

    private static void DrawCenteredPdfText(XGraphics gfx, string text, XFont font, XBrush brush, float x, float y, float width)
    {
        XSize size = gfx.MeasureString(text, font);
        double left = x + ((width - (float)size.Width) / 2F);
        gfx.DrawString(text, font, brush, new XPoint(left, y + size.Height));
    }

    private static void DrawPdfText(XGraphics gfx, string text, XFont font, XBrush brush, XRect rect, CellTextAlignment alignment, bool wrap)
    {
        var innerRect = new XRect(rect.X + CellPadding, rect.Y + CellPadding, Math.Max(rect.Width - (CellPadding * 2F), 2F), Math.Max(rect.Height - (CellPadding * 2F), 2F));

        if (wrap)
        {
            var formatter = new XTextFormatter(gfx)
            {
                Alignment = XParagraphAlignment.Left
            };
            formatter.DrawString(text, font, brush, innerRect, XStringFormats.TopLeft);
            return;
        }

        XSize size = gfx.MeasureString(text, font);
        double drawX = alignment switch
        {
            CellTextAlignment.Center => innerRect.X + ((innerRect.Width - size.Width) / 2D),
            CellTextAlignment.Right => innerRect.Right - size.Width,
            _ => innerRect.X
        };

        double drawY = innerRect.Y + ((innerRect.Height - size.Height) / 2D) + size.Height;
        gfx.DrawString(text, font, brush, new XPoint(drawX, drawY));
    }

    private IReadOnlyList<ReportColumnLayout> GetColumnLayouts(RectangleF contentBounds)
    {
        ColumnDefinition[] definitions = GetColumnDefinitions();
        float totalWeight = definitions.Sum(definition => definition.Weight);
        float currentX = contentBounds.Left;
        var columns = new List<ReportColumnLayout>(definitions.Length);

        for (int index = 0; index < definitions.Length; index++)
        {
            ColumnDefinition definition = definitions[index];
            float width = index == definitions.Length - 1
                ? contentBounds.Right - currentX
                : contentBounds.Width * (definition.Weight / totalWeight);

            columns.Add(new ReportColumnLayout(definition, currentX, width));
            currentX += width;
        }

        return columns;
    }

    private ColumnDefinition[] GetColumnDefinitions()
    {
        var definitions = new List<ColumnDefinition>();

        if (!_isDailyReport)
        {
            definitions.Add(new ColumnDefinition(nameof(BillingReportRowData.Date), "Date", 7F, CellTextAlignment.Center));
        }

        definitions.AddRange(new[]
        {
            new ColumnDefinition(nameof(BillingReportRowData.ConcessionaireName), "Concessionaire Name", 17F, CellTextAlignment.Left, true),
            new ColumnDefinition(nameof(BillingReportRowData.InvoiceNumber), "Invoice #", 6F, CellTextAlignment.Center, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.CuM3), "Cu m³", 4F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.UnM3), "Un m³", 4F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.WaterBill), "Water Bill", 9F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.Tax), "Tax", 7F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.Discount), "Discount", 7F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.TotalWaterBill), "Total Water Bill", 11F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.Arrears), "Arrears", 7F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.Scf), "SCF", 7F, CellTextAlignment.Right, PreventEllipsis: true),
            new ColumnDefinition(nameof(BillingReportRowData.TotalAmountBilled), "Total Amount Billed", 11F, CellTextAlignment.Right, PreventEllipsis: true)
        });

        return definitions.ToArray();
    }

    private static float MeasureWrappedTextHeight(Graphics graphics, string text, Font font, float width)
    {
        var bounds = new RectangleF(0F, 0F, Math.Max(width, 12F), 1000F);
        using var format = new StringFormat();
        return graphics.MeasureString(text, font, bounds.Size, format).Height;
    }

    private static Font CreateTitleFont() => new("Segoe UI Semibold", 12F, FontStyle.Bold);

    private static Font CreateReportTitleFont() => new("Segoe UI Semibold", 9F, FontStyle.Bold);

    private static Font CreateCaptionFont() => new("Segoe UI", 8F, FontStyle.Regular);

    private static Font CreateBodyFont() => new("Segoe UI", 8F, FontStyle.Regular);

    private static Font CreateBodyBoldFont() => new("Segoe UI Semibold", 8F, FontStyle.Bold);

    private static Image? TryLoadImage(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var image = Image.FromStream(stream);
        return new Bitmap(image);
    }

    private static XImage? TryLoadPdfImage(string filePath)
    {
        return File.Exists(filePath) ? XImage.FromFile(filePath) : null;
    }

    private static XColor ToXColor(Color color)
    {
        return XColor.FromArgb(color.A, color.R, color.G, color.B);
    }

    private static IReadOnlyList<BillingReportRowData> MapRows(DataTable table, bool isMonthlyReport)
    {
        IEnumerable<DataRow> orderedRows = table.Rows.Cast<DataRow>();
        if (isMonthlyReport)
        {
            orderedRows = orderedRows
                .OrderBy(GetZoneSortKey, StringComparer.OrdinalIgnoreCase)
                .ThenBy(GetInvoiceSortNumber)
                .ThenBy(GetInvoiceSortText, StringComparer.OrdinalIgnoreCase)
                .ThenBy(row => GetString(row, "concessionaire_name", "Concessionaire_Name"), StringComparer.OrdinalIgnoreCase);
        }

        if (!isMonthlyReport)
        {
            return orderedRows.Select(MapDetailRow).ToArray();
        }

        var result = new List<BillingReportRowData>();
        decimal grandCu = 0m;
        decimal grandUn = 0m;
        decimal grandWaterBill = 0m;
        decimal grandTax = 0m;
        decimal grandDiscount = 0m;
        decimal grandTotalWater = 0m;
        decimal grandArrears = 0m;
        decimal grandScf = 0m;
        decimal grandTotalAmount = 0m;

        foreach (IGrouping<string, DataRow> zoneGroup in orderedRows.GroupBy(row => GetZoneLabel(row), StringComparer.OrdinalIgnoreCase))
        {
            decimal zoneCu = 0m;
            decimal zoneUn = 0m;
            decimal zoneWaterBill = 0m;
            decimal zoneTax = 0m;
            decimal zoneDiscount = 0m;
            decimal zoneTotalWater = 0m;
            decimal zoneArrears = 0m;
            decimal zoneScf = 0m;
            decimal zoneTotalAmount = 0m;

            foreach (DataRow row in zoneGroup)
            {
                result.Add(MapDetailRow(row));

                zoneCu += GetDecimal(row, "Cu_m³", "consumption");
                zoneUn += GetDecimal(row, "Un_m³", "free_water");
                zoneWaterBill += GetDecimal(row, "Water_Bill", "water_charge");
                zoneTax += GetDecimal(row, "Tax", "tax_amount");
                zoneDiscount += GetDecimal(row, "Discount", "discount_amount");
                zoneTotalWater += GetDecimal(row, "Total_Water_Bill", "total_water_bill");
                zoneArrears += GetDecimal(row, "Arrears", "arrears_amount");
                zoneScf += GetDecimal(row, "SCF", "scf_amount");
                zoneTotalAmount += GetDecimal(row, "Total_Amount_Billed", "total_amount");
            }

            result.Add(new BillingReportRowData(
                zoneGroup.Key,
                $"{zoneGroup.Key}",
                string.Empty,
                string.Empty,
                FormatWholeNumber(zoneCu),
                FormatWholeNumber(zoneUn),
                FormatDecimal(zoneWaterBill),
                FormatDecimal(zoneTax),
                FormatDecimal(zoneDiscount),
                FormatDecimal(zoneTotalWater),
                FormatDecimal(zoneArrears),
                FormatDecimal(zoneScf),
                FormatDecimal(zoneTotalAmount),
                true));

            grandCu += zoneCu;
            grandUn += zoneUn;
            grandWaterBill += zoneWaterBill;
            grandTax += zoneTax;
            grandDiscount += zoneDiscount;
            grandTotalWater += zoneTotalWater;
            grandArrears += zoneArrears;
            grandScf += zoneScf;
            grandTotalAmount += zoneTotalAmount;
        }

        result.Add(new BillingReportRowData(
            "All Zones",
            "Total",
            string.Empty,
            string.Empty,
            FormatWholeNumber(grandCu),
            FormatWholeNumber(grandUn),
            FormatDecimal(grandWaterBill),
            FormatDecimal(grandTax),
            FormatDecimal(grandDiscount),
            FormatDecimal(grandTotalWater),
            FormatDecimal(grandArrears),
            FormatDecimal(grandScf),
            FormatDecimal(grandTotalAmount),
            true));

        return result;
    }

    private static BillingReportRowData MapDetailRow(DataRow row)
    {
        return new BillingReportRowData(
            GetZoneLabel(row),
            FormatDate(row, "Date", "billing_date"),
            GetString(row, "Concessionaire_Name", "concessionaire_name"),
            GetString(row, "Invoice_Number", "bill_number"),
            FormatWholeNumber(row, "Cu_m³", "consumption"),
            FormatWholeNumber(row, "Un_m³", "free_water"),
            FormatDecimal(row, "Water_Bill", "water_charge"),
            FormatDecimal(row, "Tax", "tax_amount"),
            FormatDecimal(row, "Discount", "discount_amount"),
            FormatDecimal(row, "Total_Water_Bill", "total_water_bill"),
            FormatDecimal(row, "Arrears", "arrears_amount"),
            FormatDecimal(row, "SCF", "scf_amount"),
            FormatDecimal(row, "Total_Amount_Billed", "total_amount"),
            false);
    }

    private static string GetZoneLabel(DataRow row)
    {
        string zone = GetString(row, "zone_id", "zone", "Zone", "zone_name", "Zone_Name");
        if (string.IsNullOrWhiteSpace(zone))
        {
            return "Zone N/A";
        }

        zone = zone.Trim();
        return zone.StartsWith("zone", StringComparison.OrdinalIgnoreCase) ? zone : $"Zone {zone}";
    }

    private static string GetZoneSortKey(DataRow row)
    {
        string zone = GetString(row, "zone_id", "zone", "Zone", "zone_name", "Zone_Name").Trim();
        return string.IsNullOrWhiteSpace(zone) ? "zzzz" : zone;
    }

    private static long GetInvoiceSortNumber(DataRow row)
    {
        string invoice = GetString(row, "Invoice_Number", "bill_number").Trim();
        return long.TryParse(invoice, NumberStyles.Integer, CultureInfo.InvariantCulture, out long parsed)
            ? parsed
            : long.MaxValue;
    }

    private static string GetInvoiceSortText(DataRow row)
    {
        return GetString(row, "Invoice_Number", "bill_number").Trim();
    }

    private static string GetString(DataRow row, params string[] columnNames)
    {
        if (!TryGetValue(row, out object? value, columnNames) || value is DBNull)
        {
            return string.Empty;
        }

        return Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
    }

    private static string FormatDate(DataRow row, params string[] columnNames)
    {
        if (!TryGetValue(row, out object? value, columnNames) || value is DBNull)
        {
            return string.Empty;
        }

        if (value is DateTime dateValue)
        {
            return dateValue.ToString("MMM dd, yyyy", CultureInfo.CurrentCulture);
        }

        return Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
    }

    private static string FormatDecimal(DataRow row, params string[] columnNames)
    {
        if (!TryGetValue(row, out object? value, columnNames) || value is DBNull)
        {
            return 0m.ToString("N2", CultureInfo.CurrentCulture);
        }

        decimal decimalValue = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        return decimalValue.ToString("N2", CultureInfo.CurrentCulture);
    }

    private static decimal GetDecimal(DataRow row, params string[] columnNames)
    {
        if (!TryGetValue(row, out object? value, columnNames) || value is DBNull)
        {
            return 0m;
        }

        return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
    }

    private static string FormatWholeNumber(DataRow row, params string[] columnNames)
    {
        return FormatWholeNumber(GetDecimal(row, columnNames));
    }

    private static string FormatWholeNumber(decimal value)
    {
        return Math.Round(value, MidpointRounding.AwayFromZero).ToString("N0", CultureInfo.CurrentCulture);
    }

    private static string FormatDecimal(decimal value)
    {
        return value.ToString("N2", CultureInfo.CurrentCulture);
    }

    private static void SetExcelCellValue(IXLCell cell, object value)
    {
        if (value is null || value == DBNull.Value)
        {
            cell.Value = string.Empty;
            return;
        }

        switch (value)
        {
            case string stringValue:
                cell.Value = stringValue;
                return;
            case DateTime dateTimeValue:
                cell.Value = dateTimeValue;
                return;
            case bool boolValue:
                cell.Value = boolValue;
                return;
            case byte byteValue:
                cell.Value = byteValue;
                return;
            case short shortValue:
                cell.Value = shortValue;
                return;
            case int intValue:
                cell.Value = intValue;
                return;
            case long longValue:
                cell.Value = longValue;
                return;
            case float floatValue:
                cell.Value = floatValue;
                return;
            case double doubleValue:
                cell.Value = doubleValue;
                return;
            case decimal decimalValue:
                cell.Value = decimalValue;
                return;
            default:
                cell.Value = Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
                return;
        }
    }

    private static bool TryGetValue(DataRow row, out object? value, params string[] columnNames)
    {
        foreach (string columnName in columnNames)
        {
            if (!row.Table.Columns.Contains(columnName))
            {
                continue;
            }

            value = row[columnName];
            return true;
        }

        value = null;
        return false;
    }

    private static void EnsurePdfFontResolverConfigured()
    {
        if (_pdfFontResolverConfigured)
        {
            return;
        }

        lock (PdfFontResolverSync)
        {
            if (_pdfFontResolverConfigured)
            {
                return;
            }

            GlobalFontSettings.FontResolver ??= new WindowsFontResolver();
            _pdfFontResolverConfigured = true;
        }
    }

    private sealed class WindowsFontResolver : IFontResolver
    {
        private const string ArialRegularFace = "arial#regular";
        private const string ArialBoldFace = "arial#bold";
        private const string ArialItalicFace = "arial#italic";
        private const string ArialBoldItalicFace = "arial#bolditalic";

        public byte[]? GetFont(string faceName)
        {
            string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            string fileName = faceName switch
            {
                ArialBoldFace => "arialbd.ttf",
                ArialItalicFace => "ariali.ttf",
                ArialBoldItalicFace => "arialbi.ttf",
                _ => "arial.ttf"
            };

            string path = Path.Combine(fontsFolder, fileName);
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }

            string fallback = Path.Combine(fontsFolder, "segoeui.ttf");
            return File.Exists(fallback) ? File.ReadAllBytes(fallback) : null;
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Resolve all requested families to local Windows TrueType files.
            string normalized = (familyName ?? string.Empty).Trim().ToLowerInvariant();
            if (normalized is "" or "helvetica" or "arial" or "segoe ui" or "segoe ui semibold")
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo(ArialBoldItalicFace);
                }

                if (isBold)
                {
                    return new FontResolverInfo(ArialBoldFace);
                }

                if (isItalic)
                {
                    return new FontResolverInfo(ArialItalicFace);
                }

                return new FontResolverInfo(ArialRegularFace);
            }

            return new FontResolverInfo(ArialRegularFace);
        }
    }

    private sealed class PrintRenderState
    {
        public PrintRenderState(
            List<ReportPageLayout>? pages = null,
            IProgress<ReportOperationProgress>? progress = null,
            CancellationToken cancellationToken = default)
        {
            Pages = pages;
            Progress = progress;
            CancellationToken = cancellationToken;
        }

        public int PageIndex { get; private set; }

        public List<ReportPageLayout>? Pages { get; set; }

        public IProgress<ReportOperationProgress>? Progress { get; }

        public CancellationToken CancellationToken { get; }

        public void Reset(bool preservePreparedPages)
        {
            PageIndex = 0;
            if (!preservePreparedPages)
            {
                Pages = null;
            }
        }

        public void Advance()
        {
            PageIndex++;
        }
    }

    private sealed record ReportPageLayout(IReadOnlyList<ReportRowLayout> Rows);

    private sealed record ReportRowLayout(BillingReportRowData Row, float Height);

    private sealed record ReportColumnLayout(ColumnDefinition Definition, float X, float Width);

    private sealed record ColumnDefinition(
        string Key,
        string HeaderText,
        float Weight,
        CellTextAlignment Alignment,
        bool Wrap = false,
        bool PreventEllipsis = false)
    {
        public string GetValue(BillingReportRowData row)
        {
            return Key switch
            {
                nameof(BillingReportRowData.Zone) => row.Zone,
                nameof(BillingReportRowData.Date) => row.Date,
                nameof(BillingReportRowData.ConcessionaireName) => row.ConcessionaireName,
                nameof(BillingReportRowData.InvoiceNumber) => row.InvoiceNumber,
                nameof(BillingReportRowData.CuM3) => row.CuM3,
                nameof(BillingReportRowData.UnM3) => row.UnM3,
                nameof(BillingReportRowData.WaterBill) => row.WaterBill,
                nameof(BillingReportRowData.Tax) => row.Tax,
                nameof(BillingReportRowData.Discount) => row.Discount,
                nameof(BillingReportRowData.TotalWaterBill) => row.TotalWaterBill,
                nameof(BillingReportRowData.Arrears) => row.Arrears,
                nameof(BillingReportRowData.Scf) => row.Scf,
                nameof(BillingReportRowData.TotalAmountBilled) => row.TotalAmountBilled,
                _ => string.Empty
            };
        }
    }

    private sealed record BillingReportRowData(
        string Zone,
        string Date,
        string ConcessionaireName,
        string InvoiceNumber,
        string CuM3,
        string UnM3,
        string WaterBill,
        string Tax,
        string Discount,
        string TotalWaterBill,
        string Arrears,
        string Scf,
        string TotalAmountBilled,
        bool IsSummaryRow = false);

    private enum CellTextAlignment
    {
        Left,
        Center,
        Right
    }
}