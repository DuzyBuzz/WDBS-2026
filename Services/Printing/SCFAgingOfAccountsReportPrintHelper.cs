using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using ClosedXML.Excel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace WDBS_2026.Services.Printing;

public sealed record SCFAgingOfAccountsReportDocumentData(
    string ReportTitle,
    string PeriodCaption,
    string PrintedBy,
    DateTime PrintedAt,
    DataTable Rows);

internal sealed class SCFAgingOfAccountsReportPrintHelper : IReportPreviewSource
{
    private const string WaterDistrictTitle = "TUBUNGAN WATER DISTRICT";
    private const float HeaderHeight = 58F;
    private const float TableHeaderHeight = 24F;
    private const float FooterHeight = 20F;
    private const float RowHeight = 22F;
    private const float CellPadding = 4F;

    private readonly SCFAgingOfAccountsReportDocumentData _document;
    private IReadOnlyList<ReportPage>? _preparedPages;

    private static readonly string[] MoneyColumns =
    [
        "Current",
        "1_30_Days",
        "31_60_Days",
        "61_90_Days",
        "Over_90_Days",
        "Total"
    ];

    public SCFAgingOfAccountsReportPrintHelper(SCFAgingOfAccountsReportDocumentData document)
    {
        _document = document;
    }

    public Task PreparePreviewAsync(IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            using var bitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bitmap);
            using PrintDocument tempDocument = CreatePrintDocument();
            RectangleF bounds = GetDocumentPageBounds(tempDocument);
            _preparedPages = BuildPages(bounds);
            progress?.Report(new ReportOperationProgress(100, "Preview is ready."));
        }, cancellationToken);
    }

    public PrintDocument CreatePreviewDocument()
    {
        return CreatePrintDocument(new PrintRenderState(_preparedPages?.ToList()));
    }

    public PrintDocument CreatePrintDocument()
    {
        return CreatePrintDocument(new PrintRenderState());
    }

    public int GetPreviewPageCount()
    {
        _preparedPages ??= BuildPages(GetDocumentPageBounds(CreatePrintDocument()));
        return Math.Max(1, _preparedPages.Count);
    }

    public Task ExportToPdfAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => ExportToPdfInternal(filePath, progress, cancellationToken), cancellationToken);
    }

    public void ExportToPdf(string filePath)
    {
        ExportToPdfAsync(filePath).GetAwaiter().GetResult();
    }

    public Task ExportToExcelAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => ExportToExcelInternal(filePath, progress, cancellationToken), cancellationToken);
    }

    public void ExportToExcel(string filePath)
    {
        ExportToExcelAsync(filePath).GetAwaiter().GetResult();
    }

    private PrintDocument CreatePrintDocument(PrintRenderState state)
    {
        var document = new PrintDocument();
        document.DefaultPageSettings.Landscape = false;
        document.DefaultPageSettings.Margins = new Margins(24, 24, 30, 24);
        document.BeginPrint += (_, _) => state.Reset();
        document.PrintPage += (_, e) => PrintPage(e, state);
        return document;
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

        var bounds = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
        List<ReportPage> pages = state.Pages ??= BuildPages(bounds).ToList();

        if (state.PageIndex >= pages.Count)
        {
            e.HasMorePages = false;
            return;
        }

        DrawPrintPage(graphics, bounds, pages[state.PageIndex], state.PageIndex + 1, pages.Count);
        state.Advance();
        e.HasMorePages = state.PageIndex < pages.Count;
    }

    private void DrawPrintPage(Graphics graphics, RectangleF bounds, ReportPage page, int pageNumber, int totalPages)
    {
        using var titleFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        using var subtitleFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        using var captionFont = new Font("Segoe UI", 8F, FontStyle.Regular);
        using var bodyFont = new Font("Segoe UI", 8F, FontStyle.Regular);
        using var bodyBoldFont = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
        using var textBrush = new SolidBrush(AppTheme.BodyTextColor);
        using var mutedBrush = new SolidBrush(AppTheme.MutedTextColor);
        using var headerBrush = new SolidBrush(AppTheme.PrimaryDarkColor);
        using var headerTextBrush = new SolidBrush(Color.White);
        using var altBrush = new SolidBrush(Color.FromArgb(246, 249, 251));
        using var borderPen = new Pen(AppTheme.BorderColor);

        DrawHeader(graphics, bounds, titleFont, subtitleFont, captionFont, textBrush);

        IReadOnlyList<ColumnLayout> columns = BuildColumnLayouts(bounds);
        float tableTop = bounds.Top + HeaderHeight;

        foreach (ColumnLayout column in columns)
        {
            var rect = new RectangleF(column.X, tableTop, column.Width, TableHeaderHeight);
            graphics.FillRectangle(headerBrush, rect);
            graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
            DrawCellText(graphics, column.Header, bodyBoldFont, headerTextBrush, rect, StringAlignment.Center, wrap: false);
        }

        float currentTop = tableTop + TableHeaderHeight;
        for (int rowOffset = 0; rowOffset < page.RowCount; rowOffset++)
        {
            int rowIndex = page.StartIndex + rowOffset;

            if (rowOffset % 2 == 1)
            {
                graphics.FillRectangle(altBrush, new RectangleF(bounds.Left, currentTop, bounds.Width, RowHeight));
            }

            foreach (ColumnLayout column in columns)
            {
                var rect = new RectangleF(column.X, currentTop, column.Width, RowHeight);
                graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
                string text = GetCellDisplay(_document.Rows.Rows[rowIndex], column.Key);
                DrawCellText(graphics, text, bodyFont, textBrush, rect, column.Alignment, column.Wrap);
            }

            currentTop += RowHeight;
        }

        float footerTop = bounds.Bottom - FooterHeight;
        graphics.DrawLine(borderPen, bounds.Left, footerTop, bounds.Right, footerTop);
        graphics.DrawString($"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}", captionFont, mutedBrush, new RectangleF(bounds.Left, footerTop + 3F, bounds.Width * 0.7F, FooterHeight));

        using var rightFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
        graphics.DrawString($"Page {pageNumber} of {totalPages}", captionFont, mutedBrush, new RectangleF(bounds.Left + (bounds.Width * 0.7F), footerTop + 3F, bounds.Width * 0.3F, FooterHeight), rightFormat);
    }

    private void DrawHeader(Graphics graphics, RectangleF bounds, Font titleFont, Font subtitleFont, Font captionFont, Brush textBrush)
    {
        float logoSize = 44F;

        using Image? leftLogo = TryLoadImage(Path.Combine(AppContext.BaseDirectory, "Resources", "republika_ng_pilipinas.jpg"));
        using Image? rightLogo = TryLoadImage(Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg"));

        if (leftLogo is not null)
        {
            graphics.DrawImage(leftLogo, bounds.Left, bounds.Top, logoSize, logoSize);
        }

        if (rightLogo is not null)
        {
            graphics.DrawImage(rightLogo, bounds.Right - logoSize, bounds.Top, logoSize, logoSize);
        }

        float centerLeft = bounds.Left + logoSize + 12F;
        float centerWidth = bounds.Width - ((logoSize * 2F) + 24F);

        using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        graphics.DrawString(WaterDistrictTitle, titleFont, textBrush, new RectangleF(centerLeft, bounds.Top + 1F, centerWidth, 18F), format);
        graphics.DrawString(_document.ReportTitle, subtitleFont, textBrush, new RectangleF(centerLeft, bounds.Top + 19F, centerWidth, 14F), format);
        graphics.DrawString(_document.PeriodCaption, captionFont, textBrush, new RectangleF(centerLeft, bounds.Top + 33F, centerWidth, 12F), format);
    }

    private static void DrawCellText(Graphics graphics, string text, Font font, Brush brush, RectangleF rect, StringAlignment alignment, bool wrap)
    {
        RectangleF textBounds = RectangleF.Inflate(rect, -CellPadding, -CellPadding);
        using var format = new StringFormat
        {
            Alignment = alignment,
            LineAlignment = wrap ? StringAlignment.Near : StringAlignment.Center,
            Trimming = wrap ? StringTrimming.None : StringTrimming.EllipsisCharacter
        };

        if (!wrap)
        {
            format.FormatFlags = StringFormatFlags.NoWrap;
        }

        graphics.DrawString(text, font, brush, textBounds, format);
    }

    private void ExportToPdfInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        using var pdfDocument = new PdfDocument();
        pdfDocument.Info.Title = _document.ReportTitle;

        const float pageWidth = 595.28F;
        const float pageHeight = 841.89F;
        var bounds = new RectangleF(18F, 18F, pageWidth - 36F, pageHeight - 36F);

        IReadOnlyList<ReportPage> pages = BuildPages(bounds);
        IReadOnlyList<ColumnLayout> columns = BuildColumnLayouts(bounds);

        for (int pageIndex = 0; pageIndex < pages.Count; pageIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            PdfPage page = pdfDocument.AddPage();
            page.Size = PageSize.A4;
            page.Orientation = PageOrientation.Portrait;

            using XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawPdfPage(gfx, bounds, pages[pageIndex], pageIndex + 1, pages.Count, columns);

            int percentage = pages.Count == 0 ? 100 : (int)Math.Round(((pageIndex + 1D) / pages.Count) * 100D, MidpointRounding.AwayFromZero);
            progress?.Report(new ReportOperationProgress(Math.Clamp(percentage, 0, 100), $"Exporting PDF page {pageIndex + 1} of {pages.Count}..."));
        }

        pdfDocument.Save(filePath);
    }

    private void DrawPdfPage(XGraphics gfx, RectangleF bounds, ReportPage page, int pageNumber, int totalPages, IReadOnlyList<ColumnLayout> columns)
    {
        var titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
        var subtitleFont = new XFont("Arial", 10, XFontStyleEx.Bold);
        var captionFont = new XFont("Arial", 8, XFontStyleEx.Regular);
        var bodyFont = new XFont("Arial", 8, XFontStyleEx.Regular);
        var bodyBoldFont = new XFont("Arial", 8, XFontStyleEx.Bold);
        var textBrush = new XSolidBrush(ToXColor(AppTheme.BodyTextColor));
        var mutedBrush = new XSolidBrush(ToXColor(AppTheme.MutedTextColor));
        var headerBrush = new XSolidBrush(ToXColor(AppTheme.PrimaryDarkColor));
        var headerTextBrush = new XSolidBrush(XColors.White);
        var alternateBrush = new XSolidBrush(ToXColor(Color.FromArgb(246, 249, 251)));
        var borderPen = new XPen(ToXColor(AppTheme.BorderColor), 0.5);

        float centerLeft = bounds.Left + 56F;
        float centerWidth = bounds.Width - 112F;

        DrawCenteredPdfText(gfx, WaterDistrictTitle, titleFont, textBrush, centerLeft, bounds.Top + 1F, centerWidth);
        DrawCenteredPdfText(gfx, _document.ReportTitle, subtitleFont, textBrush, centerLeft, bounds.Top + 19F, centerWidth);
        DrawCenteredPdfText(gfx, _document.PeriodCaption, captionFont, textBrush, centerLeft, bounds.Top + 33F, centerWidth);

        float tableTop = bounds.Top + HeaderHeight;
        foreach (ColumnLayout column in columns)
        {
            var rect = new XRect(column.X, tableTop, column.Width, TableHeaderHeight);
            gfx.DrawRectangle(headerBrush, rect);
            gfx.DrawRectangle(borderPen, rect);
            DrawPdfText(gfx, column.Header, bodyBoldFont, headerTextBrush, rect, XStringAlignment.Center);
        }

        float currentTop = tableTop + TableHeaderHeight;
        for (int rowOffset = 0; rowOffset < page.RowCount; rowOffset++)
        {
            int rowIndex = page.StartIndex + rowOffset;

            if (rowOffset % 2 == 1)
            {
                gfx.DrawRectangle(alternateBrush, bounds.Left, currentTop, bounds.Width, RowHeight);
            }

            foreach (ColumnLayout column in columns)
            {
                var rect = new XRect(column.X, currentTop, column.Width, RowHeight);
                gfx.DrawRectangle(borderPen, rect);
                DrawPdfText(gfx, GetCellDisplay(_document.Rows.Rows[rowIndex], column.Key), bodyFont, textBrush, rect, ToXAlignment(column.Alignment));
            }

            currentTop += RowHeight;
        }

        float footerTop = bounds.Bottom - FooterHeight;
        gfx.DrawLine(borderPen, bounds.Left, footerTop, bounds.Right, footerTop);
        DrawPdfText(gfx, $"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}", captionFont, mutedBrush, new XRect(bounds.Left, footerTop + 3F, bounds.Width * 0.7F, FooterHeight), XStringAlignment.Near);
        DrawPdfText(gfx, $"Page {pageNumber} of {totalPages}", captionFont, mutedBrush, new XRect(bounds.Left + (bounds.Width * 0.7F), footerTop + 3F, bounds.Width * 0.3F, FooterHeight), XStringAlignment.Far);
    }

    private void ExportToExcelInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        ColumnDefinition[] definitions = GetColumnDefinitions();
        using var workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("SCF Aging Report");

        int totalColumns = definitions.Length;

        sheet.Cell(1, 1).Value = WaterDistrictTitle;
        sheet.Cell(2, 1).Value = _document.ReportTitle;
        sheet.Cell(3, 1).Value = _document.PeriodCaption;
        sheet.Cell(4, 1).Value = $"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}";

        foreach (int row in new[] { 1, 2, 3, 4 })
        {
            var range = sheet.Range(row, 1, row, totalColumns);
            range.Merge();
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        for (int columnIndex = 0; columnIndex < definitions.Length; columnIndex++)
        {
            sheet.Cell(6, columnIndex + 1).Value = definitions[columnIndex].HeaderText;
        }

        int totalRows = _document.Rows.Rows.Count;
        for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            DataRow row = _document.Rows.Rows[rowIndex];
            for (int columnIndex = 0; columnIndex < definitions.Length; columnIndex++)
            {
                string key = definitions[columnIndex].Key;
                object value = row.Table.Columns.Contains(key) ? row[key] : string.Empty;
                sheet.Cell(rowIndex + 7, columnIndex + 1).Value = value?.ToString() ?? string.Empty;
            }

            int percentage = totalRows == 0 ? 100 : (int)Math.Round(((rowIndex + 1D) / totalRows) * 100D, MidpointRounding.AwayFromZero);
            progress?.Report(new ReportOperationProgress(Math.Clamp(percentage, 0, 100), $"Writing Excel rows {rowIndex + 1} of {totalRows}..."));
        }

        if (definitions.Length > 0)
        {
            int lastRow = Math.Max(6, totalRows + 6);
            var tableRange = sheet.Range(6, 1, lastRow, definitions.Length);
            var table = tableRange.CreateTable("SCFAgingReportTable");
            table.Theme = XLTableTheme.TableStyleMedium9;
        }

        for (int i = 0; i < definitions.Length; i++)
        {
            if (MoneyColumns.Contains(definitions[i].Key, StringComparer.OrdinalIgnoreCase))
            {
                sheet.Column(i + 1).Style.NumberFormat.Format = "#,##0.00";
            }
        }

        sheet.Columns().AdjustToContents();
        sheet.SheetView.FreezeRows(6);
        workbook.SaveAs(filePath);
    }

    private IReadOnlyList<ReportPage> BuildPages(RectangleF contentBounds)
    {
        int totalRows = _document.Rows.Rows.Count;
        float maxRowsHeightPerPage = contentBounds.Height - HeaderHeight - TableHeaderHeight - FooterHeight - 2F;
        int rowsPerPage = Math.Max(1, (int)Math.Floor(maxRowsHeightPerPage / RowHeight));

        if (totalRows == 0)
        {
            return [new ReportPage(0, 0)];
        }

        var pages = new List<ReportPage>();
        for (int start = 0; start < totalRows; start += rowsPerPage)
        {
            pages.Add(new ReportPage(start, Math.Min(rowsPerPage, totalRows - start)));
        }

        return pages;
    }

    private static RectangleF GetDocumentPageBounds(PrintDocument document)
    {
        PaperSize paperSize = document.DefaultPageSettings.PaperSize;
        Margins margins = document.DefaultPageSettings.Margins;
        bool isLandscape = document.DefaultPageSettings.Landscape;

        float width = isLandscape ? paperSize.Height : paperSize.Width;
        float height = isLandscape ? paperSize.Width : paperSize.Height;

        return new RectangleF(
            margins.Left,
            margins.Top,
            Math.Max(1F, width - margins.Left - margins.Right),
            Math.Max(1F, height - margins.Top - margins.Bottom));
    }

    private IReadOnlyList<ColumnLayout> BuildColumnLayouts(RectangleF bounds)
    {
        ColumnDefinition[] definitions = GetColumnDefinitions();
        float totalWeight = definitions.Sum(item => item.Weight);
        float x = bounds.Left;
        var columns = new List<ColumnLayout>(definitions.Length);

        for (int index = 0; index < definitions.Length; index++)
        {
            ColumnDefinition item = definitions[index];
            float width = index == definitions.Length - 1
                ? bounds.Right - x
                : bounds.Width * (item.Weight / totalWeight);

            columns.Add(new ColumnLayout(item.Key, item.HeaderText, item.Alignment, item.Wrap, x, width));
            x += width;
        }

        return columns;
    }

    private static ColumnDefinition[] GetColumnDefinitions()
    {
        return
        [
            new("Account_No", "Account No.", 14F, StringAlignment.Center),
            new("Concessionaire_Name", "Concessionaire Name", 28F, StringAlignment.Near),
            new("Current", "Current", 9.5F, StringAlignment.Far),
            new("1_30_Days", "1-30 Days", 9.5F, StringAlignment.Far),
            new("31_60_Days", "31-60 Days", 9.5F, StringAlignment.Far),
            new("61_90_Days", "61-90 Days", 9.5F, StringAlignment.Far),
            new("Over_90_Days", "Over 90 Days", 10F, StringAlignment.Far),
            new("Total", "Total", 10.5F, StringAlignment.Far)
        ];
    }

    private static string GetCellDisplay(DataRow row, string columnName)
    {
        if (!row.Table.Columns.Contains(columnName) || row[columnName] is null || row[columnName] == DBNull.Value)
        {
            return string.Empty;
        }

        object value = row[columnName];

        if (MoneyColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase)
            && decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
        {
            return amount.ToString("N2", CultureInfo.CurrentCulture);
        }

        return Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
    }

    private static void DrawCenteredPdfText(XGraphics gfx, string text, XFont font, XBrush brush, float x, float y, float width)
    {
        XSize size = gfx.MeasureString(text, font);
        double left = x + ((width - (float)size.Width) / 2F);
        gfx.DrawString(text, font, brush, new XPoint(left, y + size.Height));
    }

    private static void DrawPdfText(XGraphics gfx, string text, XFont font, XBrush brush, XRect rect, XStringAlignment alignment)
    {
        XSize size = gfx.MeasureString(text, font);
        double drawX = alignment switch
        {
            XStringAlignment.Center => rect.X + ((rect.Width - size.Width) / 2D),
            XStringAlignment.Far => rect.Right - size.Width - CellPadding,
            _ => rect.X + CellPadding
        };

        double drawY = rect.Y + ((rect.Height - size.Height) / 2D) + size.Height;
        gfx.DrawString(text, font, brush, new XPoint(drawX, drawY));
    }

    private static XColor ToXColor(Color color)
    {
        return XColor.FromArgb(color.A, color.R, color.G, color.B);
    }

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

    private static XStringAlignment ToXAlignment(StringAlignment alignment)
    {
        return alignment switch
        {
            StringAlignment.Center => XStringAlignment.Center,
            StringAlignment.Far => XStringAlignment.Far,
            _ => XStringAlignment.Near
        };
    }

    private sealed class PrintRenderState
    {
        public PrintRenderState(List<ReportPage>? pages = null)
        {
            Pages = pages;
        }

        public int PageIndex { get; private set; }

        public List<ReportPage>? Pages { get; set; }

        public void Reset()
        {
            PageIndex = 0;
            if (Pages is not null && Pages.Count == 0)
            {
                Pages = null;
            }
        }

        public void Advance()
        {
            PageIndex++;
        }
    }

    private sealed record ReportPage(int StartIndex, int RowCount);

    private sealed record ColumnDefinition(string Key, string HeaderText, float Weight, StringAlignment Alignment, bool Wrap = false);

    private sealed record ColumnLayout(string Key, string Header, StringAlignment Alignment, bool Wrap, float X, float Width);
}