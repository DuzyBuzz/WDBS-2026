using System.Data;
using System.Data.Common;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Printing;

internal sealed record InvoiceTierRow(string Label, decimal Quantity, decimal Rate, decimal Amount);

internal sealed record BillingInvoiceDocumentData(
    int BillingId,
    string BillNumber,
    DateTime BillingDate,
    DateTime? DueDate,
    string ConcessionaireName,
    string ConcessionaireCode,
    string TinNumber,
    string Address,
    DateTime ReadingFrom,
    DateTime ReadingTo,
    int PreviousReading,
    int PresentReading,
    int Consumption,
    decimal MinimumCharge,
    decimal WaterCharge,
    decimal DiscountPercent,
    decimal DiscountAmount,
    decimal TaxPercent,
    decimal TaxAmount,
    decimal ArrearsAmount,
    decimal ScfAmount,
    decimal PenaltyPercent,
    decimal PenaltyAmount,
    decimal TotalAmount,
    IReadOnlyList<InvoiceTierRow> TierRows,
    string PrintedBy,
    DateTime PrintedAt);

internal static class BillingInvoiceDataService
{
    public static async Task<BillingInvoiceDocumentData> LoadAsync(UserRole role, int billingId, string printedBy)
    {
        const string sql = @"
SELECT
    b.billing_id,
    b.bill_number,
    b.billing_date,
    b.due_date,
    c.concessionaire_name,
    c.concessionaire_code,
    c.tin_number,
    c.address,
    COALESCE(prev_reading.reading_date, c.first_reading_date) AS reading_from,
    r.reading_date AS reading_to,
    COALESCE(r.previous_reading, 0) AS previous_reading,
    COALESCE(r.present_reading, 0) AS present_reading,
    COALESCE(b.consumption, 0) AS consumption,
    COALESCE(s.min_rate, 0) AS min_rate,
    COALESCE(s.rate_11_20, 0) AS rate_11_20,
    COALESCE(s.rate_21_30, 0) AS rate_21_30,
    COALESCE(s.rate_31_40, 0) AS rate_31_40,
    COALESCE(s.rate_41_above, 0) AS rate_41_above,
    COALESCE(b.water_charge, 0) AS water_charge,
    COALESCE(b.discount_percent_used, 0) AS discount_percent,
    COALESCE(b.discount_amount, 0) AS discount_amount,
    COALESCE(b.tax_percent_used, 0) AS tax_percent,
    COALESCE(b.tax_amount, 0) AS tax_amount,
    COALESCE(b.arrears_amount, 0) AS arrears_amount,
    COALESCE(b.scf_amount, 0) AS scf_amount,
    COALESCE(b.penalty_percent_used, 0) AS penalty_percent,
    COALESCE(b.penalty_amount, 0) AS penalty_amount,
    COALESCE(b.total_amount, 0) AS total_amount
FROM billing b
INNER JOIN concessionaire c ON c.concessionaire_id = b.concessionaire_id
LEFT JOIN services s ON s.service_id = c.service_id
LEFT JOIN reading r ON r.reading_id = b.reading_id
LEFT JOIN reading prev_reading ON prev_reading.reading_id = (
    SELECT r2.reading_id
    FROM reading r2
    WHERE r2.concessionaire_id = b.concessionaire_id
      AND r2.reading_id < COALESCE(b.reading_id, 0)
    ORDER BY r2.reading_id DESC
    LIMIT 1
)
WHERE b.billing_id = @billingId
LIMIT 1;";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@billingId", billingId);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            throw new InvalidOperationException("Billing row was not found for printing.");
        }

        string billNumber = GetString(reader, "bill_number");
        DateTime billingDate = GetDate(reader, "billing_date");
        DateTime? dueDate = GetNullableDate(reader, "due_date");
        string concessionaireName = GetString(reader, "concessionaire_name");
        string concessionaireCode = GetString(reader, "concessionaire_code");
        string tinNumber = GetString(reader, "tin_number");
        string address = GetString(reader, "address");
        DateTime readingFrom = GetDate(reader, "reading_from", fallback: billingDate);
        DateTime readingTo = GetDate(reader, "reading_to", fallback: billingDate);
        int previousReading = GetInt(reader, "previous_reading");
        int presentReading = GetInt(reader, "present_reading");
        int consumption = GetInt(reader, "consumption");

        decimal minRate = GetDecimal(reader, "min_rate");
        decimal rate1120 = GetDecimal(reader, "rate_11_20");
        decimal rate2130 = GetDecimal(reader, "rate_21_30");
        decimal rate3140 = GetDecimal(reader, "rate_31_40");
        decimal rate41Above = GetDecimal(reader, "rate_41_above");

        decimal waterCharge = GetDecimal(reader, "water_charge");
        decimal discountPercent = GetDecimal(reader, "discount_percent");
        decimal discountAmount = GetDecimal(reader, "discount_amount");
        decimal taxPercent = GetDecimal(reader, "tax_percent");
        decimal taxAmount = GetDecimal(reader, "tax_amount");
        decimal arrearsAmount = GetDecimal(reader, "arrears_amount");
        decimal scfAmount = GetDecimal(reader, "scf_amount");
        decimal penaltyPercent = GetDecimal(reader, "penalty_percent");
        decimal penaltyAmount = GetDecimal(reader, "penalty_amount");
        decimal totalAmount = GetDecimal(reader, "total_amount");

        List<InvoiceTierRow> tierRows = BuildTierRows(consumption, minRate, rate1120, rate2130, rate3140, rate41Above);

        return new BillingInvoiceDocumentData(
            billingId,
            billNumber,
            billingDate,
            dueDate,
            concessionaireName,
            concessionaireCode,
            tinNumber,
            address,
            readingFrom,
            readingTo,
            previousReading,
            presentReading,
            consumption,
            minRate,
            waterCharge,
            discountPercent,
            discountAmount,
            taxPercent,
            taxAmount,
            arrearsAmount,
            scfAmount,
            penaltyPercent,
            penaltyAmount,
            totalAmount,
            tierRows,
            printedBy,
            DateTime.Now);
    }

    private static List<InvoiceTierRow> BuildTierRows(int consumption, decimal minRate, decimal rate1120, decimal rate2130, decimal rate3140, decimal rate41Above)
    {
        int billable = Math.Max(consumption, 0);

        decimal inferredFirstTierRate = minRate > 0M ? decimal.Round(minRate / 10M, 2, MidpointRounding.AwayFromZero) : rate1120;
        int q0to10 = Math.Min(10, billable);
        int q11to20 = Math.Min(10, Math.Max(billable - 10, 0));
        int q21to30 = Math.Min(10, Math.Max(billable - 20, 0));
        int q31to40 = Math.Min(10, Math.Max(billable - 30, 0));
        int q41up = Math.Max(billable - 40, 0);

        return
        [
            new InvoiceTierRow("0-10 m³", q0to10, inferredFirstTierRate, q0to10 * inferredFirstTierRate),
            new InvoiceTierRow("11-20 m³", q11to20, rate1120, q11to20 * rate1120),
            new InvoiceTierRow("21-30 m³", q21to30, rate2130, q21to30 * rate2130),
            new InvoiceTierRow("31-40 m³", q31to40, rate3140, q31to40 * rate3140),
            new InvoiceTierRow("41 & Up m³", q41up, rate41Above, q41up * rate41Above)
        ];
    }

    private static string GetString(DbDataReader reader, string column)
    {
        return Convert.ToString(reader[column], CultureInfo.CurrentCulture) ?? string.Empty;
    }

    private static int GetInt(DbDataReader reader, string column)
    {
        return Convert.ToInt32(reader[column] == DBNull.Value ? 0 : reader[column], CultureInfo.InvariantCulture);
    }

    private static decimal GetDecimal(DbDataReader reader, string column)
    {
        return Convert.ToDecimal(reader[column] == DBNull.Value ? 0M : reader[column], CultureInfo.InvariantCulture);
    }

    private static DateTime GetDate(DbDataReader reader, string column, DateTime? fallback = null)
    {
        if (reader[column] == DBNull.Value)
        {
            return fallback ?? DateTime.Today;
        }

        return Convert.ToDateTime(reader[column], CultureInfo.InvariantCulture);
    }

    private static DateTime? GetNullableDate(DbDataReader reader, string column)
    {
        if (reader[column] == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDateTime(reader[column], CultureInfo.InvariantCulture);
    }
}

internal sealed class PrintBillInvoice : IReportPreviewSource
{
    private readonly BillingInvoiceDocumentData _document;
    private readonly Font _titleFont = new("Arial", 16F, FontStyle.Bold);
    private readonly Font _subtitleFont = new("Arial", 9F, FontStyle.Bold);
    private readonly Font _bodyFont = new("Arial", 8.5F, FontStyle.Regular);
    private readonly Font _bodyBoldFont = new("Arial", 8.5F, FontStyle.Bold);
    private readonly Font _smallFont = new("Arial", 7.5F, FontStyle.Regular);
    private readonly Pen _gridPen = new(Color.Black, 1F);

    private readonly string[] _copyTitles =
    [
        "Concessionaire's Copy",
        "Office Copy",
        "File Copy"
    ];

    public PrintBillInvoice(BillingInvoiceDocumentData document)
    {
        _document = document;
    }

    public Task PreparePreviewAsync(IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        progress?.Report(new ReportOperationProgress(100, "Invoice preview ready."));
        return Task.CompletedTask;
    }

    public PrintDocument CreatePreviewDocument()
    {
        return CreatePrintDocument();
    }

    public PrintDocument CreatePrintDocument()
    {
        var printDocument = new PrintDocument();
        printDocument.DefaultPageSettings.Landscape = false;
        printDocument.DefaultPageSettings.Margins = new Margins(18, 18, 18, 18);

        printDocument.PrintPage += (_, e) =>
        {
            if (e.Graphics is null)
            {
                e.HasMorePages = false;
                return;
            }

            DrawPage(e.Graphics, e.MarginBounds);
            e.HasMorePages = false;
        };

        return printDocument;
    }

    public int GetPreviewPageCount()
    {
        return 1;
    }

    public Task ExportToPdfAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => ExportToPdf(filePath), cancellationToken);
    }

    public void ExportToPdf(string filePath)
    {
        using var pdf = new PdfDocument();
        PdfPage page = pdf.AddPage();
        page.Size = PageSize.A4;
        page.Orientation = PageOrientation.Portrait;

        using XGraphics gfx = XGraphics.FromPdfPage(page);

        const double pageWidth = 595.28;
        const double pageHeight = 841.89;
        var bounds = new RectangleF(18F, 18F, (float)pageWidth - 36F, (float)pageHeight - 36F);

        using var bmp = new Bitmap((int)Math.Ceiling(bounds.Width), (int)Math.Ceiling(bounds.Height));
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        DrawPage(g, new Rectangle(0, 0, bmp.Width, bmp.Height));

        string tempPngPath = Path.Combine(Path.GetTempPath(), $"wdbs_invoice_{Guid.NewGuid():N}.png");
        bmp.Save(tempPngPath, System.Drawing.Imaging.ImageFormat.Png);

        using XImage img = XImage.FromFile(tempPngPath);
        gfx.DrawImage(img, bounds.X, bounds.Y, bounds.Width, bounds.Height);

        try
        {
            File.Delete(tempPngPath);
        }
        catch
        {
            // Best effort temporary cleanup only.
        }

        pdf.Save(filePath);
    }

    public Task ExportToExcelAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => ExportToExcel(filePath), cancellationToken);
    }

    public void ExportToExcel(string filePath)
    {
        using var workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Billing Invoice");

        sheet.Cell(1, 1).Value = "Billing Invoice";
        sheet.Cell(2, 1).Value = "Invoice No.";
        sheet.Cell(2, 2).Value = _document.BillNumber;
        sheet.Cell(3, 1).Value = "Billing Date";
        sheet.Cell(3, 2).Value = _document.BillingDate;
        sheet.Cell(4, 1).Value = "Due Date";
        sheet.Cell(4, 2).Value = _document.DueDate;
        sheet.Cell(5, 1).Value = "Concessionaire";
        sheet.Cell(5, 2).Value = _document.ConcessionaireName;
        sheet.Cell(6, 1).Value = "Account No.";
        sheet.Cell(6, 2).Value = _document.ConcessionaireCode;
        sheet.Cell(7, 1).Value = "Address";
        sheet.Cell(7, 2).Value = _document.Address;

        int row = 9;
        sheet.Cell(row, 1).Value = "Tier";
        sheet.Cell(row, 2).Value = "Qty";
        sheet.Cell(row, 3).Value = "Rate";
        sheet.Cell(row, 4).Value = "Amount";
        row++;

        foreach (InvoiceTierRow tier in _document.TierRows)
        {
            sheet.Cell(row, 1).Value = tier.Label;
            sheet.Cell(row, 2).Value = tier.Quantity;
            sheet.Cell(row, 3).Value = tier.Rate;
            sheet.Cell(row, 4).Value = tier.Amount;
            row++;
        }

        row += 1;
        sheet.Cell(row, 1).Value = "Minimum Charge";
        sheet.Cell(row, 4).Value = _document.MinimumCharge;
        row++;
        sheet.Cell(row, 1).Value = "Water Consumption";
        sheet.Cell(row, 4).Value = _document.WaterCharge;
        row++;
        sheet.Cell(row, 1).Value = "Less: Discount";
        sheet.Cell(row, 4).Value = _document.DiscountAmount;
        row++;
        sheet.Cell(row, 1).Value = "Add: Franchise Tax";
        sheet.Cell(row, 4).Value = _document.TaxAmount;
        row++;
        sheet.Cell(row, 1).Value = "Add: Arrears";
        sheet.Cell(row, 4).Value = _document.ArrearsAmount;
        row++;
        sheet.Cell(row, 1).Value = "Add: SCF Installment";
        sheet.Cell(row, 4).Value = _document.ScfAmount;
        row++;
        sheet.Cell(row, 1).Value = "Penalty";
        sheet.Cell(row, 4).Value = _document.PenaltyAmount;
        row++;
        sheet.Cell(row, 1).Value = "Total Amount Due";
        sheet.Cell(row, 4).Value = _document.TotalAmount;

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(filePath);
    }

    private void DrawPage(Graphics graphics, Rectangle bounds)
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        graphics.Clear(Color.White);

        const int copyGap = 10;
        int copyHeight = (bounds.Height - copyGap * 2) / 3;

        for (int copyIndex = 0; copyIndex < 3; copyIndex++)
        {
            int y = bounds.Top + copyIndex * (copyHeight + copyGap);
            var copyRect = new Rectangle(bounds.Left, y, bounds.Width, copyHeight);
            DrawSingleCopy(graphics, copyRect, _copyTitles[copyIndex]);
        }
    }

    private void DrawSingleCopy(Graphics g, Rectangle copyRect, string copyTitle)
    {
        g.DrawRectangle(_gridPen, copyRect);

        const int headerHeight = 52;
        const int footerHeight = 32;

        var headerRect = new Rectangle(copyRect.Left, copyRect.Top, copyRect.Width, headerHeight);
        g.DrawRectangle(_gridPen, headerRect);

        DrawHeader(g, headerRect, copyTitle);

        int bodyTop = headerRect.Bottom;
        int leftWidth = (int)Math.Round(copyRect.Width * 0.58);
        int rightWidth = copyRect.Width - leftWidth;

        var leftRect = new Rectangle(copyRect.Left, bodyTop, leftWidth, copyRect.Bottom - bodyTop - footerHeight);
        var rightRect = new Rectangle(leftRect.Right, bodyTop, rightWidth, leftRect.Height);
        g.DrawRectangle(_gridPen, leftRect);
        g.DrawRectangle(_gridPen, rightRect);

        DrawLeftPanel(g, leftRect);
        DrawRightPanel(g, rightRect);

        var footerRect = new Rectangle(copyRect.Left, copyRect.Bottom - footerHeight, copyRect.Width, footerHeight);
        g.DrawRectangle(_gridPen, footerRect);

        int footerRow1Y = footerRect.Top + 11;
        int footerRow2Y = footerRect.Top + 21;
        g.DrawLine(_gridPen, footerRect.Left, footerRow1Y, footerRect.Right, footerRow1Y);
        g.DrawLine(_gridPen, footerRect.Left, footerRow2Y, footerRect.Right, footerRow2Y);

        int footerCol1X = footerRect.Left + (int)Math.Round(footerRect.Width * 0.27);
        int footerCol2X = footerRect.Left + (int)Math.Round(footerRect.Width * 0.64);
        g.DrawLine(_gridPen, footerCol1X, footerRect.Top, footerCol1X, footerRect.Bottom);
        g.DrawLine(_gridPen, footerCol2X, footerRect.Top, footerCol2X, footerRect.Bottom);

        g.DrawString("PERMIT TO USE LOOSE LEAF NO.:", _smallFont, Brushes.Black, new PointF(footerRect.Left + 6, footerRect.Top + 1));
        g.DrawString("BIR AUTHORITY TO PRINT NO. 000000000000000", _smallFont, Brushes.Black, new PointF(footerCol1X + 6, footerRect.Top + 1));
        g.DrawString("PRINTER", _smallFont, Brushes.Black, new PointF(footerCol2X + 6, footerRect.Top + 1));

        g.DrawString("DATE ISSUED:", _smallFont, Brushes.Black, new PointF(footerRect.Left + 6, footerRow1Y + 1));
        g.DrawString("DATE ISSUED:", _smallFont, Brushes.Black, new PointF(footerCol1X + 6, footerRow1Y + 1));
        g.DrawString("TIN:", _smallFont, Brushes.Black, new PointF(footerCol2X + 6, footerRow1Y + 1));

        g.DrawString("APPROVED SERIES: 0000000001-00080000", _smallFont, Brushes.Black, new PointF(footerCol1X + 6, footerRow2Y + 1));
        g.DrawString("BIR Accreditation No. 0000000000", _smallFont, Brushes.Black, new PointF(footerCol2X + 6, footerRow2Y + 1));
    }

    private void DrawHeader(Graphics g, Rectangle headerRect, string copyTitle)
    {
        string republicLogoPath = Path.Combine(AppContext.BaseDirectory, "Resources", "republika_ng_pilipinas.jpg");
        string districtLogoPath = Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg");

        if (File.Exists(republicLogoPath))
        {
            using Image logo = Image.FromFile(republicLogoPath);
            g.DrawImage(logo, new Rectangle(headerRect.Left + 6, headerRect.Top + 6, 36, 36));
        }

        if (File.Exists(districtLogoPath))
        {
            using Image logo = Image.FromFile(districtLogoPath);
            g.DrawImage(logo, new Rectangle(headerRect.Left + 320, headerRect.Top + 5, 40, 40));
        }

        g.DrawString("TUBUNGAN WATER DISTRICT", _subtitleFont, Brushes.Black, new PointF(headerRect.Left + 48, headerRect.Top + 4));
        g.DrawString("Tubungan Municipal Hall Annex Bldg.", _smallFont, Brushes.Black, new PointF(headerRect.Left + 48, headerRect.Top + 18));
        g.DrawString("Paz St., Iloilo City", _smallFont, Brushes.Black, new PointF(headerRect.Left + 48, headerRect.Top + 29));
        g.DrawString("CCC No. 595 TIN No. 006-281-163-000", _bodyBoldFont, Brushes.Black, new PointF(headerRect.Left + 48, headerRect.Top + 40));

        SizeF copySize = g.MeasureString(copyTitle, _subtitleFont);
        g.DrawString(copyTitle, _subtitleFont, Brushes.Black, new PointF(headerRect.Right - copySize.Width - 8, headerRect.Top + 4));

        g.DrawString("DUE DATE:", _bodyBoldFont, Brushes.Black, new PointF(headerRect.Right - 170, headerRect.Top + 26));
        g.DrawString(_document.DueDate?.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture) ?? "-", _bodyBoldFont, Brushes.Black, new PointF(headerRect.Right - 98, headerRect.Top + 26));
    }

    private void DrawLeftPanel(Graphics g, Rectangle rect)
    {
        int y = rect.Top + 2;

        float billMetaLabelX = rect.Right - 194F;
        float billNumberX = rect.Right - 104F;

        g.DrawString("BILLING INVOICE", _titleFont, Brushes.Black, new PointF(rect.Left + 4, y));
        g.DrawString("Invoice No.", _bodyBoldFont, Brushes.Black, new PointF(billMetaLabelX, y + 5));
        g.DrawString(_document.BillNumber.PadLeft(8, '0'), new Font("Arial", 14F, FontStyle.Bold), Brushes.Red, new PointF(billNumberX, y + 1));

        int billingDateY = y + 23;
        g.DrawString("Billing Date:", _bodyBoldFont, Brushes.Black, new PointF(billMetaLabelX + 2, billingDateY));
        g.DrawString(_document.BillingDate.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture), _bodyFont, Brushes.Black, new PointF(rect.Right - 120, billingDateY));
        y = billingDateY + 16;

        g.DrawString("□ Cash Sales", _bodyFont, Brushes.Black, new PointF(rect.Left + 14, y));
        g.DrawString("☑ Charge Sales", _bodyFont, Brushes.Black, new PointF(rect.Left + 14, y + 14));
        y += 30;

        g.DrawString("SOLD TO:", _subtitleFont, Brushes.Black, new PointF(rect.Left + 6, y));
        y += 14;
        g.DrawString("Registered Name:", _bodyFont, Brushes.Black, new PointF(rect.Left + 6, y));
        g.DrawString(_document.ConcessionaireName, _bodyBoldFont, Brushes.Black, new PointF(rect.Left + 122, y));
        y += 14;
        g.DrawString("TIN:", _bodyFont, Brushes.Black, new PointF(rect.Left + 6, y));
        g.DrawString(string.IsNullOrWhiteSpace(_document.TinNumber) ? "-" : _document.TinNumber, _bodyBoldFont, Brushes.Black, new PointF(rect.Left + 122, y));
        y += 14;
        g.DrawString("Business Address:", _bodyFont, Brushes.Black, new PointF(rect.Left + 6, y));
        g.DrawString(_document.Address, _bodyBoldFont, Brushes.Black, new RectangleF(rect.Left + 122, y, rect.Width - 126, 22));
        y += 14;
        g.DrawString("Account No.:", _bodyFont, Brushes.Black, new PointF(rect.Left + 6, y));
        g.DrawString(_document.ConcessionaireCode, _bodyBoldFont, Brushes.Black, new PointF(rect.Left + 122, y));
        y += 12;

        const int meterHeight = 84;
        int footerLineY = rect.Bottom - 14;
        int noteY = footerLineY - 16;
        int meterTop = y + 4;
        if (meterTop + meterHeight > noteY - 4)
        {
            meterTop = noteY - meterHeight - 4;
        }

        var signatureRect = new Rectangle(rect.Left + 6, meterTop, 170, meterHeight);
        g.DrawRectangle(_gridPen, signatureRect);

        var meterRect = new Rectangle(rect.Left + 176, meterTop, rect.Width - 180, meterHeight);
        g.DrawRectangle(_gridPen, meterRect);

        int signatureColDividerX = signatureRect.Left + 92;
        int signatureRowDividerY = signatureRect.Top + signatureRect.Height / 2;

        g.DrawLine(_gridPen, signatureColDividerX, signatureRect.Top, signatureColDividerX, signatureRect.Bottom);
        g.DrawLine(_gridPen, signatureRect.Left, signatureRowDividerY, signatureRect.Right, signatureRowDividerY);

        var labelColTopRect = new RectangleF(
            signatureRect.Left + 3,
            signatureRect.Top + 3,
            signatureColDividerX - signatureRect.Left - 6,
            signatureRowDividerY - signatureRect.Top - 5);
        var labelColBottomRect = new RectangleF(
            signatureRect.Left + 3,
            signatureRowDividerY + 2,
            signatureColDividerX - signatureRect.Left - 6,
            signatureRect.Bottom - signatureRowDividerY - 5);

        g.DrawString("SC/PWD/NAAC/MOV/SP ID No.:", _smallFont, Brushes.Black, labelColTopRect);
        g.DrawString("SC/PWD/NAAC/MOV/SP Sig:", _smallFont, Brushes.Black, labelColBottomRect);

        int disclaimerTop = noteY - 20;
        const int disclaimerLineHeight = 10;
        g.DrawString("\"THIS DOCUMENT IS NOT VALID", _bodyBoldFont, Brushes.Red, new PointF(rect.Left + 10, disclaimerTop));
        g.DrawString("FOR CLAIM OF INPUT TAX\"", _bodyBoldFont, Brushes.Red, new PointF(rect.Left + 28, disclaimerTop + disclaimerLineHeight));

        int topRowBottom = meterRect.Top + 24;
        int meterTitleBottom = meterRect.Top + 42;
        int headingBottom = meterRect.Top + 70;

        g.DrawLine(_gridPen, meterRect.Left, topRowBottom, meterRect.Right, topRowBottom);
        g.DrawLine(_gridPen, meterRect.Left, meterTitleBottom, meterRect.Right, meterTitleBottom);
        g.DrawLine(_gridPen, meterRect.Left, headingBottom, meterRect.Right, headingBottom);

        int col1X = meterRect.Left + meterRect.Width / 3;
        int col2X = meterRect.Left + (meterRect.Width * 2) / 3;
        g.DrawLine(_gridPen, col1X, meterTitleBottom, col1X, meterRect.Bottom);
        g.DrawLine(_gridPen, col2X, meterTitleBottom, col2X, meterRect.Bottom);

        g.DrawString("Billing Period:", _smallFont, Brushes.Black, new PointF(meterRect.Left + 4, meterRect.Top + 3));
        g.DrawString(_document.ReadingFrom.ToString("M/d/yyyy", CultureInfo.InvariantCulture), _smallFont, Brushes.Black, new PointF(meterRect.Left + 80, meterRect.Top + 3));
        g.DrawString(_document.ReadingTo.ToString("M/d/yyyy", CultureInfo.InvariantCulture), _smallFont, Brushes.Black, new PointF(meterRect.Left + 154, meterRect.Top + 3));

        SizeF meterTitleSize = g.MeasureString("METER READING", _smallFont);
        g.DrawString("METER READING", _smallFont, Brushes.Black, new PointF(meterRect.Left + (meterRect.Width - meterTitleSize.Width) / 2F, topRowBottom + 1));

        g.DrawString("PREVIOUS", _smallFont, Brushes.Black, new PointF(meterRect.Left + 18, meterTitleBottom + 2));
        g.DrawString("PRESENT", _smallFont, Brushes.Black, new PointF(col1X + 18, meterTitleBottom + 2));
        g.DrawString("Cu. M. Consumed", _smallFont, Brushes.Black, new PointF(col2X + 2, meterTitleBottom + 2));

        g.DrawString(_document.PreviousReading.ToString(CultureInfo.InvariantCulture), _bodyBoldFont, Brushes.Black, new PointF(meterRect.Left + 35, headingBottom -1));
        g.DrawString(_document.PresentReading.ToString(CultureInfo.InvariantCulture), _bodyBoldFont, Brushes.Black, new PointF(col1X + 35, headingBottom - 1));
        g.DrawString(_document.Consumption.ToString(CultureInfo.InvariantCulture), _bodyBoldFont, Brushes.Black, new PointF(col2X + 45, headingBottom - 1));

        g.DrawString("Note: 10% Penalty for late payments.", new Font("Arial", 9.5F, FontStyle.Bold), Brushes.Black, new PointF(meterRect.Left + 35, noteY - 16));
        g.DrawString("ID No.", new Font("Arial", 9.5F, FontStyle.Bold), Brushes.Black, new PointF(rect.Left + 6, footerLineY - 5));
        g.DrawString(_document.BillingId.ToString("00000000", CultureInfo.InvariantCulture), _bodyBoldFont, Brushes.Black, new PointF(rect.Left + 60, footerLineY - 5));
        g.DrawString("Signature:", new Font("Arial", 9.5F, FontStyle.Bold), Brushes.Black, new PointF(rect.Left + 182, footerLineY - 5));
        g.DrawLine(Pens.Black, rect.Left + 250, footerLineY + 10, rect.Right - 12, footerLineY + 10);
    }

    private void DrawRightPanel(Graphics g, Rectangle rect)
    {
        int y = rect.Top;

        const int totalRows = 15;
        int headerHeight = 18;
        int rowHeight = Math.Max(12, (rect.Height - headerHeight - 2) / totalRows);

        var headerRect = new Rectangle(rect.Left, y, rect.Width, headerHeight);
        g.DrawRectangle(_gridPen, headerRect);
        g.DrawString("Item Description / Nature of Service", _bodyBoldFont, Brushes.Black, new RectangleF(rect.Left + 4, y + 2, rect.Width * 0.48F, headerHeight - 3));
        g.DrawString("Qty", _bodyFont, Brushes.Black, new PointF(rect.Left + rect.Width * 0.55F, y + 2));
        g.DrawString("Unit Price", _bodyFont, Brushes.Black, new PointF(rect.Left + rect.Width * 0.68F, y + 2));
        g.DrawString("Amount", _bodyFont, Brushes.Black, new PointF(rect.Left + rect.Width * 0.86F, y + 2));
        y += headerHeight;

        DrawChargeRow(g, rect, ref y, rowHeight, "WATER CONSUMPTION", _document.Consumption.ToString(CultureInfo.InvariantCulture), string.Empty, _document.WaterCharge, bold: true);
        DrawChargeRow(g, rect, ref y, rowHeight, "Minimum Charge", string.Empty, FormatCurrency(_document.MinimumCharge), _document.MinimumCharge);

        foreach (InvoiceTierRow tier in _document.TierRows)
        {
            DrawChargeRow(g, rect, ref y, rowHeight, tier.Label, tier.Quantity.ToString("0", CultureInfo.InvariantCulture), FormatCurrency(tier.Rate), tier.Amount);
        }

        DrawChargeRow(g, rect, ref y, rowHeight, "Less: Discount", FormatPercent(_document.DiscountPercent), string.Empty, -_document.DiscountAmount);
        DrawChargeRow(g, rect, ref y, rowHeight, "Add: Franchise Tax", FormatPercent(_document.TaxPercent), string.Empty, _document.TaxAmount);
        DrawChargeRow(g, rect, ref y, rowHeight, "Less: Withholding Tax", string.Empty, string.Empty, 0M);
        DrawChargeRow(g, rect, ref y, rowHeight, "Add: Arrears", string.Empty, string.Empty, _document.ArrearsAmount);

        decimal totalBeforePenalty = Math.Max(_document.TotalAmount - _document.PenaltyAmount, 0M);
        DrawChargeRow(g, rect, ref y, rowHeight, "TOTAL AMOUNT DUE", string.Empty, string.Empty, totalBeforePenalty, bold: true);
        DrawChargeRow(g, rect, ref y, rowHeight, "Penalty", FormatPercent(_document.PenaltyPercent), string.Empty, _document.PenaltyAmount);
        DrawChargeRow(g, rect, ref y, rowHeight, "TOTAL AMOUNT DUE", string.Empty, string.Empty, _document.TotalAmount, bold: true);
        DrawChargeRow(g, rect, ref y, rowHeight, "Add: SCF Installment", string.Empty, string.Empty, _document.ScfAmount, bold: true);
    }

    private void DrawChargeRow(Graphics g, Rectangle rightRect, ref int y, int rowHeight, string label, string qty, string unitPrice, decimal amount, bool bold = false)
    {
        var rowRect = new Rectangle(rightRect.Left, y, rightRect.Width, rowHeight);
        g.DrawRectangle(_gridPen, rowRect);

        float qtyX = rightRect.Left + rightRect.Width * 0.55F;
        float unitX = rightRect.Left + rightRect.Width * 0.70F;
        float amountX = rightRect.Left + rightRect.Width * 0.96F;

        Font font = bold ? _bodyBoldFont : _bodyFont;

        g.DrawString(label, font, Brushes.Black, new RectangleF(rightRect.Left + 4, y + 1, rightRect.Width * 0.52F, rowHeight - 2));
        if (!string.IsNullOrWhiteSpace(qty))
        {
            g.DrawString(qty, font, Brushes.Black, new PointF(qtyX, y + 1));
        }

        if (!string.IsNullOrWhiteSpace(unitPrice))
        {
            g.DrawString(unitPrice, font, Brushes.Black, new PointF(unitX, y + 1));
        }

        string amountText = amount < 0M ? $"- {FormatCurrency(Math.Abs(amount))}" : FormatCurrency(amount);
        SizeF amountSize = g.MeasureString(amountText, font);
        g.DrawString(amountText, font, Brushes.Black, new PointF(amountX - amountSize.Width, y + 1));

        y += rowHeight;
    }

    private static string FormatCurrency(decimal value)
    {
        return value.ToString("N2", CultureInfo.InvariantCulture);
    }

    private static string FormatPercent(decimal value)
    {
        if (value <= 0M)
        {
            return string.Empty;
        }

        return value.ToString("0.##", CultureInfo.InvariantCulture) + "%";
    }
}
