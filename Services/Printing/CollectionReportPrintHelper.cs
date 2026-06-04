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

public sealed record CollectionReportDocumentData(
	string ReportTitle,
	string PeriodCaption,
	string PrintedBy,
	DateTime PrintedAt,
	DataTable Rows);

internal sealed class CollectionReportPrintHelper : IReportPreviewSource
{
	private const string WaterDistrictTitle = "IGBARAS WATER DISTRICT";
	private const float HeaderHeight = 60F;
	private const float TableHeaderHeight = 24F;
	private const float FooterHeight = 20F;
	private const float RowHeight = 18F;
	private const float CellPadding = 0F;
    private const float WrappedRowAllowance = 20F;

    private readonly CollectionReportDocumentData _document;
	private readonly object _previewPreparationSync = new();

	private static readonly object PdfFontResolverSync = new();
	private static bool _pdfFontResolverConfigured;

	private IReadOnlyList<ReportPage>? _preparedPages;
	private int _preparedPageCount = 1;

	private static readonly string[] MoneyColumns =
	[
		"Water Charge",
		"Tax",
		"Arrears",
		"Penalty",
		"SCF",
		"Others",
		"Collected",
		"Uncollected"
	];

	public CollectionReportPrintHelper(CollectionReportDocumentData document)
	{
		_document = document;
	}

	public Task PreparePreviewAsync(IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
	{
		return Task.Run(() => PreparePreviewInternal(progress, cancellationToken), cancellationToken);
	}

	public PrintDocument CreatePreviewDocument()
	{
		IReadOnlyList<ReportPage> pages = EnsurePreparedPages();
		var state = new PrintRenderState(pages.ToList());
		return CreatePrintDocument(state, preservePreparedPages: true);
	}

	public PrintDocument CreatePrintDocument()
	{
		return CreatePrintDocument(new PrintRenderState(), preservePreparedPages: false);
	}

	public int GetPreviewPageCount()
	{
		if (_preparedPages is null)
		{
			PreparePreviewInternal(progress: null, CancellationToken.None);
		}

		return _preparedPageCount;
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

	private PrintDocument CreatePrintDocument(PrintRenderState state, bool preservePreparedPages)
	{
		var document = new PrintDocument();
		document.DefaultPageSettings.Landscape = true;
		document.DefaultPageSettings.Margins = new Margins(24, 24, 30, 24);
		document.BeginPrint += (_, _) => state.Reset(preservePreparedPages);
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
		List<ReportPage> pages = state.Pages ??= BuildPages(graphics, bounds, state.Progress, state.CancellationToken, "Preparing preview pages", 15, 90);

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
			float rowHeight = page.RowHeights[rowOffset];

			if (rowOffset % 2 == 1)
			{
				graphics.FillRectangle(altBrush, new RectangleF(bounds.Left, currentTop, bounds.Width, rowHeight));
			}

			foreach (ColumnLayout column in columns)
			{
				var rect = new RectangleF(column.X, currentTop, column.Width, rowHeight);
				graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
				string text = GetCellDisplay(_document.Rows.Rows[rowIndex], column.Key);
				DrawCellText(graphics, text, bodyFont, textBrush, rect, column.Alignment, column.Wrap);
			}

			currentTop += rowHeight;
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

    private static void DrawCellText(
        Graphics graphics,
        string text,
        Font font,
        Brush brush,
        RectangleF rect,
        StringAlignment alignment,
        bool wrap)
    {
        RectangleF textBounds =
            RectangleF.Inflate(rect, -CellPadding, -CellPadding);

        using StringFormat format = new()
        {
            Alignment = alignment,
            Trimming = StringTrimming.Word
        };

        if (wrap)
        {
            format.LineAlignment = StringAlignment.Near;
        }
        else
        {
            format.LineAlignment = StringAlignment.Center;
            format.FormatFlags = StringFormatFlags.NoWrap;
            format.Trimming = StringTrimming.EllipsisCharacter;
        }

        graphics.DrawString(
            text,
            font,
            brush,
            textBounds,
            format);
    }

    private void ExportToPdfInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
	{
		EnsurePdfFontResolverConfigured();
		ReportProgress(progress, 5, "Preparing PDF export...");

		using var pdfDocument = new PdfDocument();
		pdfDocument.Info.Title = _document.ReportTitle;

		const float pageWidth = 841.89F;
		const float pageHeight = 595.28F;
		var bounds = new RectangleF(18F, 18F, pageWidth - 36F, pageHeight - 36F);

		using var bitmap = new Bitmap(1, 1);
		using var graphics = Graphics.FromImage(bitmap);
		graphics.PageUnit = GraphicsUnit.Point;

		List<ReportPage> pages = BuildPages(graphics, bounds, progress, cancellationToken, "Preparing PDF pages", 10, 55);
		IReadOnlyList<ColumnLayout> columns = BuildColumnLayouts(bounds);

		using XImage? leftLogo = TryLoadPdfImage(Path.Combine(AppContext.BaseDirectory, "Resources", "republika_ng_pilipinas.jpg"));
		using XImage? rightLogo = TryLoadPdfImage(Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg"));

		for (int pageIndex = 0; pageIndex < pages.Count; pageIndex++)
		{
			cancellationToken.ThrowIfCancellationRequested();

			PdfPage page = pdfDocument.AddPage();
			page.Size = PageSize.A4;
			page.Orientation = PageOrientation.Landscape;

			using XGraphics gfx = XGraphics.FromPdfPage(page);
			DrawPdfPage(gfx, bounds, pages[pageIndex], pageIndex + 1, pages.Count, columns, leftLogo, rightLogo);

			int percentage = pages.Count == 0
				? 95
				: 55 + (int)Math.Round(((pageIndex + 1D) / pages.Count) * 40D, MidpointRounding.AwayFromZero);
			ReportProgress(progress, percentage, $"Exporting PDF page {pageIndex + 1} of {pages.Count}...");
		}

		pdfDocument.Save(filePath);
		ReportProgress(progress, 100, "PDF export completed.");
	}

	private void DrawPdfPage(XGraphics gfx, RectangleF bounds, ReportPage page, int pageNumber, int totalPages, IReadOnlyList<ColumnLayout> columns, XImage? leftLogo, XImage? rightLogo)
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

		float logoSize = 44F;
		if (leftLogo is not null)
		{
			gfx.DrawImage(leftLogo, bounds.Left, bounds.Top, logoSize, logoSize);
		}

		if (rightLogo is not null)
		{
			gfx.DrawImage(rightLogo, bounds.Right - logoSize, bounds.Top, logoSize, logoSize);
		}

		float centerLeft = bounds.Left + logoSize + 12F;
		float centerWidth = bounds.Width - ((logoSize * 2F) + 24F);

		DrawCenteredPdfText(gfx, WaterDistrictTitle, titleFont, textBrush, centerLeft, bounds.Top + 1F, centerWidth);
		DrawCenteredPdfText(gfx, _document.ReportTitle, subtitleFont, textBrush, centerLeft, bounds.Top + 19F, centerWidth);
		DrawCenteredPdfText(gfx, _document.PeriodCaption, captionFont, textBrush, centerLeft, bounds.Top + 33F, centerWidth);

		float tableTop = bounds.Top + HeaderHeight;
		foreach (ColumnLayout column in columns)
		{
			var rect = new XRect(column.X, tableTop, column.Width, TableHeaderHeight);
			gfx.DrawRectangle(headerBrush, rect);
			gfx.DrawRectangle(borderPen, rect);
			DrawPdfText(gfx, column.Header, bodyBoldFont, headerTextBrush, rect, XStringAlignment.Center, wrap: false);
		}

		float currentTop = tableTop + TableHeaderHeight;
		for (int rowOffset = 0; rowOffset < page.RowCount; rowOffset++)
		{
			int rowIndex = page.StartIndex + rowOffset;
			float rowHeight = page.RowHeights[rowOffset];

			if (rowOffset % 2 == 1)
			{
				gfx.DrawRectangle(alternateBrush, bounds.Left, currentTop, bounds.Width, rowHeight);
			}

			foreach (ColumnLayout column in columns)
			{
				var rect = new XRect(column.X, currentTop, column.Width, rowHeight);
				gfx.DrawRectangle(borderPen, rect);
				DrawPdfText(gfx, GetCellDisplay(_document.Rows.Rows[rowIndex], column.Key), bodyFont, textBrush, rect, ToXAlignment(column.Alignment), column.Wrap);
			}

			currentTop += rowHeight;
		}

		float footerTop = bounds.Bottom - FooterHeight;
		gfx.DrawLine(borderPen, bounds.Left, footerTop, bounds.Right, footerTop);
		DrawPdfText(gfx, $"Printed: {_document.PrintedAt:MMMM dd, yyyy hh:mm tt} - {_document.PrintedBy}", captionFont, mutedBrush, new XRect(bounds.Left, footerTop + 3F, bounds.Width * 0.7F, FooterHeight), XStringAlignment.Near, wrap: false);
		DrawPdfText(gfx, $"Page {pageNumber} of {totalPages}", captionFont, mutedBrush, new XRect(bounds.Left + (bounds.Width * 0.7F), footerTop + 3F, bounds.Width * 0.3F, FooterHeight), XStringAlignment.Far, wrap: false);
	}

	private void ExportToExcelInternal(string filePath, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
	{
		ReportProgress(progress, 5, "Preparing Excel export...");

		ColumnDefinition[] definitions = GetColumnDefinitions();
		using var workbook = new XLWorkbook();
		IXLWorksheet sheet = workbook.Worksheets.Add("Collection Report");

		int totalColumns = definitions.Length;
		if (totalColumns <= 0)
		{
			totalColumns = 1;
		}

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
				ColumnDefinition definition = definitions[columnIndex];
				object value = GetExcelCellValue(row, definition.Key);
				SetExcelCellValue(sheet.Cell(rowIndex + 7, columnIndex + 1), value);
			}

			int percentage = totalRows == 0
				? 95
				: 10 + (int)Math.Round(((rowIndex + 1D) / totalRows) * 85D, MidpointRounding.AwayFromZero);
			ReportProgress(progress, percentage, $"Writing Excel rows {rowIndex + 1} of {totalRows}...");
		}

		if (definitions.Length > 0)
		{
			int lastRow = Math.Max(6, totalRows + 6);
			var tableRange = sheet.Range(6, 1, lastRow, definitions.Length);
			var table = tableRange.CreateTable("CollectionReportTable");
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
		ReportProgress(progress, 100, "Excel export completed.");
	}

	private List<ReportPage> BuildPages(Graphics graphics, RectangleF contentBounds, IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken, string progressMessage, int progressStart, int progressEnd)
	{
		int totalRows = _document.Rows.Rows.Count;
		float maxRowsHeightPerPage = contentBounds.Height - HeaderHeight - TableHeaderHeight - FooterHeight - 2F;
		IReadOnlyList<ColumnLayout> columns = BuildColumnLayouts(contentBounds);
		using var bodyFont = new Font("Segoe UI", 8F, FontStyle.Regular);

		var pages = new List<ReportPage>();
		if (totalRows == 0)
		{
			pages.Add(new ReportPage(0, 0, Array.Empty<float>()));
			ReportProgress(progress, progressEnd, progressMessage);
			return pages;
		}

		int processed = 0;
		while (processed < totalRows)
		{
			cancellationToken.ThrowIfCancellationRequested();

			int startIndex = processed;
			float consumedHeight = 0F;
			var rowHeights = new List<float>();

			while (processed < totalRows)
			{
				float rowHeight = GetRowHeight(graphics, bodyFont, _document.Rows.Rows[processed], columns);
				if (rowHeights.Count > 0 && consumedHeight + rowHeight > maxRowsHeightPerPage)
				{
					break;
				}

				rowHeights.Add(rowHeight);
				consumedHeight += rowHeight;
				processed++;
			}

			pages.Add(new ReportPage(startIndex, rowHeights.Count, rowHeights.ToArray()));

			int percentage = progressStart + (int)Math.Round(((double)processed / totalRows) * Math.Max(progressEnd - progressStart, 1), MidpointRounding.AwayFromZero);
			ReportProgress(progress, percentage, $"{progressMessage} ({processed} of {totalRows} rows)...");
		}

		return pages;
	}

	private IReadOnlyList<ReportPage> EnsurePreparedPages()
	{
		if (_preparedPages is null)
		{
			PreparePreviewInternal(progress: null, CancellationToken.None);
		}

		return _preparedPages!;
	}

	private void PreparePreviewInternal(IProgress<ReportOperationProgress>? progress, CancellationToken cancellationToken)
	{
		if (_preparedPages is not null)
		{
			ReportProgress(progress, 100, "Preview is ready.");
			return;
		}

		lock (_previewPreparationSync)
		{
			if (_preparedPages is not null)
			{
				ReportProgress(progress, 100, "Preview is ready.");
				return;
			}

			cancellationToken.ThrowIfCancellationRequested();
			ReportProgress(progress, 10, "Preparing preview document...");

			using var bitmap = new Bitmap(1, 1);
			using var graphics = Graphics.FromImage(bitmap);
			graphics.PageUnit = GraphicsUnit.Point;

			using PrintDocument tempDocument = CreatePrintDocument();
			RectangleF bounds = GetDocumentPageBounds(tempDocument);

			_preparedPages = BuildPages(graphics, bounds, progress, cancellationToken, "Preparing preview pages", 20, 95).ToArray();
			_preparedPageCount = Math.Max(1, _preparedPages.Count);
			ReportProgress(progress, 100, $"Prepared {_preparedPageCount} preview page(s).");
		}
	}

    private static float GetRowHeight(
       Graphics graphics,
       Font bodyFont,
       DataRow row,
       IReadOnlyList<ColumnLayout> columns)
    {
        float rowHeight = RowHeight;

        foreach (ColumnLayout column in columns)
        {
            if (!column.Wrap)
            {
                continue;
            }

            string text = GetCellDisplay(row, column.Key);

            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            float availableWidth = Math.Max(1F, column.Width - (CellPadding * 2F));

            using StringFormat format = new()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };

            SizeF size = graphics.MeasureString(
                text,
                bodyFont,
                new SizeF(availableWidth, 10000F),
                format);

            float requiredHeight =
                size.Height +
                (CellPadding * 2F) +
                WrappedRowAllowance;

            rowHeight = Math.Max(rowHeight, requiredHeight);
        }

        return (float)Math.Ceiling(rowHeight);
    }

 //   private static float GetWrappedHeightAllowance(string columnKey)
	//{
	//	return columnKey switch
	//	{
	//		"Payor" or "Invoice #" or "Remarks" => WrappedRowAllowance,
	//		_ => 2F
	//	};
	//}

	//private static float MeasureWrappedTextHeight(Graphics graphics, string text, Font font, float width)
	//{
	//	using var format = new StringFormat
	//	{
	//		Alignment = StringAlignment.Near,
	//		LineAlignment = StringAlignment.Near,
	//		Trimming = StringTrimming.None
	//	};

	//	SizeF measured = graphics.MeasureString(text, font, new SizeF(Math.Max(1F, width), 1000F), format);
	//	return measured.Height;
	//}

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
			new("Date", "Date", 10F, StringAlignment.Center),
			new("OR Number", "OR #", 8F, StringAlignment.Center),
			new("Payor", "Payor", 14F, StringAlignment.Near, true),
			new("Invoice Number", "Invoice #", 7F, StringAlignment.Near, true),
			new("Remarks", "Remarks", 15F, StringAlignment.Near, true),
            new("Water Charge", "Water Charge", 10F, StringAlignment.Far),
			new("Tax", "Tax", 8F, StringAlignment.Far),
			new("Arrears", "Arrears", 8F, StringAlignment.Far),
			new("Penalty", "Penalty", 8F, StringAlignment.Far),
			new("SCF", "SCF", 8F, StringAlignment.Far),
                    new("Others", "Others", 8F, StringAlignment.Far),
            new("Collected", "Collected", 10F, StringAlignment.Far),
			new("Uncollected", "Uncollected", 10F, StringAlignment.Far)
		];
	}

	private static string GetCellDisplay(DataRow row, string columnName)
	{
		if (!row.Table.Columns.Contains(columnName) || row[columnName] is null || row[columnName] == DBNull.Value)
		{
			return string.Empty;
		}

		object value = row[columnName];

		if (string.Equals(columnName, "Date", StringComparison.OrdinalIgnoreCase))
		{
			if (value is DateTime date)
			{
				return date.ToString("MMM dd, yyyy", CultureInfo.CurrentCulture);
			}

			if (DateTime.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime parsedDate))
			{
				return parsedDate.ToString("MMM dd, yyyy", CultureInfo.CurrentCulture);
			}
		}

		if (MoneyColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase) && decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
		{
			return amount.ToString("N2", CultureInfo.CurrentCulture);
		}

		return Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty;
	}

	private static object GetExcelCellValue(DataRow row, string columnName)
	{
		if (!row.Table.Columns.Contains(columnName) || row[columnName] is null || row[columnName] == DBNull.Value)
		{
			return string.Empty;
		}

		object value = row[columnName];

		if (string.Equals(columnName, "Date", StringComparison.OrdinalIgnoreCase))
		{
			if (value is DateTime date)
			{
				return date;
			}

			if (DateTime.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime parsedDate))
			{
				return parsedDate;
			}
		}

		if (MoneyColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase) && decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
		{
			return amount;
		}

		return value;
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
			case string text:
				cell.Value = text;
				return;
			case DateTime dateTime:
				cell.Value = dateTime;
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

	private static void DrawCenteredPdfText(XGraphics gfx, string text, XFont font, XBrush brush, float x, float y, float width)
	{
		XSize size = gfx.MeasureString(text, font);
		double left = x + ((width - (float)size.Width) / 2F);
		gfx.DrawString(text, font, brush, new XPoint(left, y + size.Height));
	}

	private static void DrawPdfText(XGraphics gfx, string text, XFont font, XBrush brush, XRect rect, XStringAlignment alignment, bool wrap)
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
			XStringAlignment.Center => innerRect.X + ((innerRect.Width - size.Width) / 2D),
			XStringAlignment.Far => innerRect.Right - size.Width,
			_ => innerRect.X
		};

		double drawY = innerRect.Y + ((innerRect.Height - size.Height) / 2D) + size.Height;
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

	private static XImage? TryLoadPdfImage(string filePath)
	{
		return File.Exists(filePath) ? XImage.FromFile(filePath) : null;
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

	private static void ReportProgress(IProgress<ReportOperationProgress>? progress, int percentage, string message)
	{
		progress?.Report(new ReportOperationProgress(Math.Clamp(percentage, 0, 100), message));
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
		public PrintRenderState(List<ReportPage>? pages = null, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default)
		{
			Pages = pages;
			Progress = progress;
			CancellationToken = cancellationToken;
		}

		public int PageIndex { get; private set; }

		public List<ReportPage>? Pages { get; set; }

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

	private sealed record ReportPage(int StartIndex, int RowCount, IReadOnlyList<float> RowHeights);

	private sealed record ColumnDefinition(string Key, string HeaderText, float Weight, StringAlignment Alignment, bool Wrap = false);

	private sealed record ColumnLayout(string Key, string Header, StringAlignment Alignment, bool Wrap, float X, float Width);
}
