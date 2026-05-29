using System.Drawing.Printing;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Forms.Billing;

public partial class BillingReportPreviewForm : Form
{
    private BillingReportDocumentData? _documentData;
    private BillingReportPrintHelper? _printHelper;
    private int _previewPageCount = 1;
    private int _currentPreviewPage;

    public BillingReportPreviewForm()
    {
        InitializeComponent();
        ApplyTheme();
    }

    public BillingReportPreviewForm(BillingReportDocumentData documentData)
        : this()
    {
        _documentData = documentData;
        _printHelper = new BillingReportPrintHelper(documentData);
        titleLabel.Text = documentData.ReportTitle;
        subtitleLabel.Text = documentData.PeriodCaption;
        Text = documentData.ReportTitle;
        LoadButtonImages();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (_printHelper is null)
        {
            return;
        }

        previewControl.Document = _printHelper.CreatePrintDocument();
        previewControl.AutoZoom = true;
        _previewPageCount = Math.Max(1, _printHelper.GetPreviewPageCount());
        _currentPreviewPage = 0;
        ApplyPreviewPage();
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);
        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(subtitleLabel);
        AppTheme.ApplySurfacePanel(previewHostPanel);
        AppTheme.ApplySeverityButton(previousPageButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(nextPageButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(printButton, ButtonSeverity.Primary);
        AppTheme.ApplySeverityButton(pdfButton, ButtonSeverity.Danger);
        AppTheme.ApplySeverityButton(excelButton, ButtonSeverity.Success);
        AppTheme.ApplySeverityButton(closeButton, ButtonSeverity.Neutral);
    }

    private void LoadButtonImages()
    {
        ApplyButtonIcon(printButton, Path.Combine(AppContext.BaseDirectory, "Resources", "print.ico"));
        ApplyButtonIcon(pdfButton, Path.Combine(AppContext.BaseDirectory, "Resources", "pdf.ico"));
        ApplyButtonIcon(excelButton, Path.Combine(AppContext.BaseDirectory, "Resources", "excel.ico"));
    }

    private static void ApplyButtonIcon(Button button, string iconPath)
    {
        if (!File.Exists(iconPath))
        {
            return;
        }

        using var icon = new Icon(iconPath);
        button.Image = new Bitmap(icon.ToBitmap(), new Size(18, 18));
        button.ImageAlign = ContentAlignment.MiddleLeft;
        button.TextImageRelation = TextImageRelation.ImageBeforeText;
        button.Padding = new Padding(12, 0, 12, 0);
    }

    private void printButton_Click(object sender, EventArgs e)
    {
        if (_printHelper is null)
        {
            return;
        }

        using var printDialog = new PrintDialog
        {
            AllowSomePages = false,
            UseEXDialog = true,
            Document = _printHelper.CreatePrintDocument()
        };

        if (printDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        printDialog.Document.Print();
    }

    private void pdfButton_Click(object sender, EventArgs e)
    {
        if (_printHelper is null || _documentData is null)
        {
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "PDF Files (*.pdf)|*.pdf",
            FileName = BuildDefaultFileName(_documentData.ReportTitle, "pdf")
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _printHelper.ExportToPdf(dialog.FileName);
        MessageBox.Show(this, "PDF report saved successfully.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void excelButton_Click(object sender, EventArgs e)
    {
        if (_printHelper is null || _documentData is null)
        {
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "Excel Workbook (*.xlsx)|*.xlsx",
            FileName = BuildDefaultFileName(_documentData.ReportTitle, "xlsx")
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _printHelper.ExportToExcel(dialog.FileName);
        MessageBox.Show(this, "Excel report saved successfully.", "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void previousPageButton_Click(object sender, EventArgs e)
    {
        if (_currentPreviewPage <= 0)
        {
            return;
        }

        _currentPreviewPage--;
        ApplyPreviewPage();
    }

    private void nextPageButton_Click(object sender, EventArgs e)
    {
        if (_currentPreviewPage >= _previewPageCount - 1)
        {
            return;
        }

        _currentPreviewPage++;
        ApplyPreviewPage();
    }

    private void ApplyPreviewPage()
    {
        previewControl.StartPage = _currentPreviewPage;
        previewControl.InvalidatePreview();

        previousPageButton.Enabled = _currentPreviewPage > 0;
        nextPageButton.Enabled = _currentPreviewPage < _previewPageCount - 1;
        pageInfoLabel.Text = $"Page {_currentPreviewPage + 1} of {_previewPageCount}";
    }

    private static string BuildDefaultFileName(string reportTitle, string extension)
    {
        string safeTitle = string.Concat(reportTitle.Select(character => Path.GetInvalidFileNameChars().Contains(character) ? '_' : character));
        return $"{safeTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}