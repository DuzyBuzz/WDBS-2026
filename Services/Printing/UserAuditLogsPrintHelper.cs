using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using ClosedXML.Excel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace WDBS_2026.Services.Printing;

public sealed record UserAuditLogsReportDocumentData(
    string ReportTitle,
    string PeriodCaption,
    string FilterCaption,
    string PrintedBy,
    DateTime PrintedAt,
    DataTable Rows);

internal sealed class UserAuditLogsPrintHelper : IReportPreviewSource
{
    private const int RowsPerPage = 28;

    private readonly UserAuditLogsReportDocumentData _document;
    private readonly object _stateSync = new();

    private List<int> _pageStartRows = new() { 0 };

    public UserAuditLogsPrintHelper(UserAuditLogsReportDocumentData document)
    {
        _document = document;
    }

    public Task PreparePreviewAsync(IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            ReportProgress(progress, 10, "Preparing audit report pages...");
            BuildPageStarts(cancellationToken);
            ReportProgress(progress, 100, "Preview ready.");
        }, cancellationToken);
    }

    public PrintDocument CreatePreviewDocument()
    {
        return CreateDocument();
    }

    public PrintDocument CreatePrintDocument()
    {
        return CreateDocument();
    }

    public int GetPreviewPageCount()
    {
        BuildPageStarts(CancellationToken.None);
        lock (_stateSync)
        {
            return Math.Max(1, _pageStartRows.Count);
        }
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

    private PrintDocument CreateDocument()
    {
        BuildPageStarts(CancellationToken.None);

        var state = new PrintState();
        var document = new PrintDocument();
        document.DefaultPageSettings.Landscape = true;
        document.DefaultPageSettings.Margins = new Margins(30, 30, 34, 30);

        document.BeginPrint += (_, _) => state.Reset();
        document.PrintPage += (_, args) => PrintPage(args, state);

        return document;
    }

    private void PrintPage(PrintPageEventArgs e, PrintState state)
    {
        if (e.Graphics is null)
        {
            e.HasMorePages = false;
            return;
        }

        BuildPageStarts(CancellationToken.None);

        List<int> pageStarts;
        lock (_stateSync)
        {
            pageStarts = _pageStartRows.ToList();
        }

        int pageIndex = state.PageIndex;
        if (pageIndex >= pageStarts.Count)
        {
            e.HasMorePages = false;
            return;
        }

        using var titleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        using var subtitleFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        using var headerFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        using var bodyFont = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        using var borderPen = new Pen(Color.FromArgb(199, 209, 216));
        using var headerBrush = new SolidBrush(AppTheme.PrimaryDarkColor);
        using var headerTextBrush = new SolidBrush(Color.White);
        using var textBrush = new SolidBrush(Color.Black);

        Rectangle margin = e.MarginBounds;

        float y = margin.Top;
        e.Graphics.DrawString(_document.ReportTitle, titleFont, textBrush, margin.Left, y);
        y += 22F;
        e.Graphics.DrawString(_document.PeriodCaption, subtitleFont, textBrush, margin.Left, y);
        y += 18F;
        e.Graphics.DrawString(_document.FilterCaption, subtitleFont, textBrush, margin.Left, y);
        y += 18F;
        e.Graphics.DrawString($"Printed: {_document.PrintedAt:yyyy-MM-dd hh:mm tt}  |  By: {_document.PrintedBy}", subtitleFont, textBrush, margin.Left, y);
        y += 22F;

        float tableLeft = margin.Left;
        float tableWidth = margin.Width;
        float headerHeight = 24F;
        float rowHeight = 22F;

        float[] columnWidths = BuildColumnWidths(tableWidth, _document.Rows.Columns.Count);

        float x = tableLeft;
        for (int col = 0; col < _document.Rows.Columns.Count; col++)
        {
            float width = columnWidths[col];
            var cellBounds = new RectangleF(x, y, width, headerHeight);
            e.Graphics.FillRectangle(headerBrush, cellBounds);
            e.Graphics.DrawRectangle(borderPen, x, y, width, headerHeight);
            e.Graphics.DrawString(_document.Rows.Columns[col].ColumnName, headerFont, headerTextBrush, new RectangleF(x + 3F, y + 4F, width - 6F, headerHeight - 8F));
            x += width;
        }

        y += headerHeight;
        int startRow = pageStarts[pageIndex];
        int endRow = Math.Min(startRow + RowsPerPage, _document.Rows.Rows.Count);

        for (int rowIndex = startRow; rowIndex < endRow; rowIndex++)
        {
            DataRow row = _document.Rows.Rows[rowIndex];
            x = tableLeft;
            for (int col = 0; col < _document.Rows.Columns.Count; col++)
            {
                float width = columnWidths[col];
                e.Graphics.DrawRectangle(borderPen, x, y, width, rowHeight);
                string value = Convert.ToString(row[col]) ?? string.Empty;
                e.Graphics.DrawString(value, bodyFont, textBrush, new RectangleF(x + 3F, y + 3F, width - 6F, rowHeight - 6F));
                x += width;
            }

            y += rowHeight;
        }

        string pageText = $"Page {pageIndex + 1} of {pageStarts.Count}";
        e.Graphics.DrawString(pageText, subtitleFont, textBrush, margin.Right - 110F, margin.Bottom + 8F);

        state.PageIndex++;
        e.HasMorePages = state.PageIndex < pageStarts.Count;
    }

    private void ExportToPdfInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        BuildPageStarts(cancellationToken);

        List<int> pageStarts;
        lock (_stateSync)
        {
            pageStarts = _pageStartRows.ToList();
        }

        using var pdfDocument = new PdfDocument();
        pdfDocument.Info.Title = _document.ReportTitle;

        var titleFont = new XFont("Segoe UI", 12, XFontStyleEx.Bold);
        var subtitleFont = new XFont("Segoe UI", 9, XFontStyleEx.Regular);
        var headerFont = new XFont("Segoe UI", 8, XFontStyleEx.Bold);
        var bodyFont = new XFont("Segoe UI", 8, XFontStyleEx.Regular);

        for (int pageIndex = 0; pageIndex < pageStarts.Count; pageIndex++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            PdfPage page = pdfDocument.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Landscape;

            using XGraphics gfx = XGraphics.FromPdfPage(page);
            var margin = new XRect(24, 20, page.Width.Point - 48, page.Height.Point - 40);

            double y = margin.Top;
            gfx.DrawString(_document.ReportTitle, titleFont, XBrushes.Black, new XRect(margin.Left, y, margin.Width, 18), XStringFormats.TopLeft);
            y += 20;
            gfx.DrawString(_document.PeriodCaption, subtitleFont, XBrushes.Black, new XRect(margin.Left, y, margin.Width, 14), XStringFormats.TopLeft);
            y += 14;
            gfx.DrawString(_document.FilterCaption, subtitleFont, XBrushes.Black, new XRect(margin.Left, y, margin.Width, 14), XStringFormats.TopLeft);
            y += 14;
            gfx.DrawString($"Printed: {_document.PrintedAt:yyyy-MM-dd hh:mm tt}  |  By: {_document.PrintedBy}", subtitleFont, XBrushes.Black, new XRect(margin.Left, y, margin.Width, 14), XStringFormats.TopLeft);
            y += 18;

            double[] columnWidths = BuildPdfColumnWidths(margin.Width, _document.Rows.Columns.Count);

            double x = margin.Left;
            for (int col = 0; col < _document.Rows.Columns.Count; col++)
            {
                double width = columnWidths[col];
                var headerRect = new XRect(x, y, width, 18);
                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(AppTheme.PrimaryDarkColor.R, AppTheme.PrimaryDarkColor.G, AppTheme.PrimaryDarkColor.B)), headerRect);
                gfx.DrawRectangle(XPens.LightGray, headerRect);
                gfx.DrawString(_document.Rows.Columns[col].ColumnName, headerFont, XBrushes.White, new XRect(x + 2, y + 3, width - 4, 12), XStringFormats.TopLeft);
                x += width;
            }

            y += 18;
            int startRow = pageStarts[pageIndex];
            int endRow = Math.Min(startRow + RowsPerPage, _document.Rows.Rows.Count);

            for (int rowIndex = startRow; rowIndex < endRow; rowIndex++)
            {
                x = margin.Left;
                DataRow row = _document.Rows.Rows[rowIndex];

                for (int col = 0; col < _document.Rows.Columns.Count; col++)
                {
                    double width = columnWidths[col];
                    var cellRect = new XRect(x, y, width, 16);
                    gfx.DrawRectangle(XPens.LightGray, cellRect);
                    string value = Convert.ToString(row[col]) ?? string.Empty;
                    gfx.DrawString(value, bodyFont, XBrushes.Black, new XRect(x + 2, y + 2, width - 4, 12), XStringFormats.TopLeft);
                    x += width;
                }

                y += 16;
            }

            gfx.DrawString($"Page {pageIndex + 1} of {pageStarts.Count}", subtitleFont, XBrushes.Black, new XRect(margin.Right - 120, margin.Bottom + 6, 120, 14), XStringFormats.TopLeft);

            int percentage = pageStarts.Count == 0
                ? 100
                : 10 + (int)Math.Round(((pageIndex + 1D) / pageStarts.Count) * 90D, MidpointRounding.AwayFromZero);
            ReportProgress(progress, percentage, $"Exporting PDF page {pageIndex + 1} of {pageStarts.Count}...");
        }

        pdfDocument.Save(filePath);
        ReportProgress(progress, 100, "PDF export completed.");
    }

    private void ExportToExcelInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
    {
        using var workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Audit Logs");

        int totalColumns = Math.Max(1, _document.Rows.Columns.Count);

        sheet.Cell(1, 1).Value = _document.ReportTitle;
        sheet.Cell(2, 1).Value = _document.PeriodCaption;
        sheet.Cell(3, 1).Value = _document.FilterCaption;
        sheet.Cell(4, 1).Value = $"Printed: {_document.PrintedAt:yyyy-MM-dd hh:mm tt}  |  By: {_document.PrintedBy}";

        foreach (int rowIndex in new[] { 1, 2, 3, 4 })
        {
            var merged = sheet.Range(rowIndex, 1, rowIndex, totalColumns);
            merged.Merge();
            merged.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            merged.Style.Font.Bold = rowIndex == 1;
        }

        for (int col = 0; col < _document.Rows.Columns.Count; col++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            sheet.Cell(6, col + 1).Value = _document.Rows.Columns[col].ColumnName;
            sheet.Cell(6, col + 1).Style.Font.Bold = true;
        }

        for (int row = 0; row < _document.Rows.Rows.Count; row++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            DataRow data = _document.Rows.Rows[row];
            for (int col = 0; col < _document.Rows.Columns.Count; col++)
            {
                sheet.Cell(row + 7, col + 1).Value = Convert.ToString(data[col]) ?? string.Empty;
            }

            int percentage = _document.Rows.Rows.Count == 0
                ? 100
                : 10 + (int)Math.Round(((row + 1D) / _document.Rows.Rows.Count) * 90D, MidpointRounding.AwayFromZero);
            ReportProgress(progress, percentage, $"Writing Excel rows {row + 1} of {_document.Rows.Rows.Count}...");
        }

        if (_document.Rows.Columns.Count > 0)
        {
            int lastRow = Math.Max(6, _document.Rows.Rows.Count + 6);
            sheet.Range(6, 1, lastRow, _document.Rows.Columns.Count).CreateTable("UserAuditLogsTable");
        }

        sheet.Columns().AdjustToContents();
        sheet.SheetView.FreezeRows(6);

        workbook.SaveAs(filePath);
        ReportProgress(progress, 100, "Excel export completed.");
    }

    private void BuildPageStarts(CancellationToken cancellationToken)
    {
        lock (_stateSync)
        {
            int rowCount = _document.Rows.Rows.Count;
            var starts = new List<int>();

            if (rowCount == 0)
            {
                starts.Add(0);
                _pageStartRows = starts;
                return;
            }

            for (int index = 0; index < rowCount; index += RowsPerPage)
            {
                cancellationToken.ThrowIfCancellationRequested();
                starts.Add(index);
            }

            _pageStartRows = starts;
        }
    }

    private static float[] BuildColumnWidths(float totalWidth, int columnCount)
    {
        int safeCount = Math.Max(1, columnCount);
        float[] widths = new float[safeCount];

        float baseWidth = totalWidth / safeCount;
        for (int i = 0; i < safeCount; i++)
        {
            widths[i] = baseWidth;
        }

        return widths;
    }

    private static double[] BuildPdfColumnWidths(double totalWidth, int columnCount)
    {
        int safeCount = Math.Max(1, columnCount);
        double[] widths = new double[safeCount];

        double baseWidth = totalWidth / safeCount;
        for (int i = 0; i < safeCount; i++)
        {
            widths[i] = baseWidth;
        }

        return widths;
    }

    private static void ReportProgress(IProgress<ReportOperationProgress>? progress, int percentage, string message)
    {
        progress?.Report(new ReportOperationProgress(Math.Clamp(percentage, 0, 100), message));
    }

    private sealed class PrintState
    {
        public int PageIndex { get; set; }

        public void Reset()
        {
            PageIndex = 0;
        }
    }
}
