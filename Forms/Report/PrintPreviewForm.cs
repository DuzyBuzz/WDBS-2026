using System.Drawing.Printing;
using System.Globalization;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Forms.Report
{
    public partial class PrintPreviewForm : Form
    {
        private const double ZoomStep = 0.10D;
        private const double MinimumZoom = 0.25D;
        private const double MaximumZoom = 3.00D;

        private readonly IReportPreviewSource? _previewSource;
        private readonly string _defaultFileNamePrefix = "Report";

        private PrintDocument? _previewDocument;
        private int _previewPageCount = 1;
        private int _currentPreviewPage;
        private bool _previewLoaded;

        public PrintPreviewForm()
        {
            InitializeComponent();
            ApplyTheme();
            previewControl.UseAntiAlias = true;
            previewControl.TabStop = true;
            previewControl.MouseEnter += previewControl_FocusRequested;
            previewControl.MouseDown += previewControl_FocusRequested;
            previewControl.MouseWheel += previewControl_MouseWheel;
        }

        internal PrintPreviewForm(string reportTitle, string periodCaption, IReportPreviewSource previewSource, string? defaultFileNamePrefix = null)
            : this()
        {
            _previewSource = previewSource;
            _defaultFileNamePrefix = string.IsNullOrWhiteSpace(defaultFileNamePrefix) ? reportTitle : defaultFileNamePrefix;

            titleLabel.Text = reportTitle;
            subtitleLabel.Text = periodCaption;
            Text = reportTitle;

            LoadButtonImages();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_previewLoaded || _previewSource is null)
            {
                return;
            }

            try
            {
                ReportOperationProgressForm.Run(
                    this,
                    "Preparing Report Preview",
                    "Preparing preview document...",
                    progress => _previewSource.PreparePreviewAsync(progress));

                LoadPreview();
                _previewLoaded = true;
            }
            catch (Exception ex)
            {
                SetActionAvailability(false);
                pageInfoLabel.Text = "Preview unavailable";
                zoomInfoLabel.Text = "Zoom: --";
                MessageBox.Show(this, ex.Message, "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            previewControl.Document = null;
            _previewDocument?.Dispose();
            _previewDocument = null;

            base.OnFormClosed(e);
        }

        private void ApplyTheme()
        {
            AppTheme.ApplyFormSurface(this);
            AppTheme.ApplyPageTitle(titleLabel);
            AppTheme.ApplySubtitle(subtitleLabel);
            AppTheme.ApplySurfacePanel(previewHostPanel);

            foreach (Label label in new[] { pageInfoLabel, zoomInfoLabel })
            {
                label.ForeColor = AppTheme.BodyTextColor;
            }

            AppTheme.ApplySubtitle(zoomHintLabel);
            AppTheme.ApplyInput(pageNumberTextBox);

            AppTheme.ApplySeverityButton(printButton, ButtonSeverity.Primary);
            AppTheme.ApplySeverityButton(pdfButton, ButtonSeverity.Danger);
            AppTheme.ApplySeverityButton(excelButton, ButtonSeverity.Success);
            AppTheme.ApplySeverityButton(previousPageButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(nextPageButton, ButtonSeverity.Neutral);
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

        private void LoadPreview()
        {
            SetActionAvailability(false);
            pageInfoLabel.Text = "Loading preview...";
            zoomInfoLabel.Text = "Zoom: Auto";

            _previewPageCount = Math.Max(1, _previewSource!.GetPreviewPageCount());

            previewControl.Document = null;
            _previewDocument?.Dispose();
            _previewDocument = _previewSource.CreatePreviewDocument();

            previewControl.Rows = 1;
            previewControl.Columns = 1;
            previewControl.AutoZoom = true;
            previewControl.Document = _previewDocument;

            _currentPreviewPage = 0;
            ApplyPreviewPage();
            UpdateZoomState();
            SetActionAvailability(true);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if (_previewSource is null)
            {
                return;
            }

            using var printDialog = new PrintDialog
            {
                AllowSomePages = false,
                UseEXDialog = true,
                Document = _previewSource.CreatePrintDocument()
            };

            if (printDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            printDialog.Document.Print();
        }

        private void pdfButton_Click(object sender, EventArgs e)
        {
            if (_previewSource is null)
            {
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = BuildDefaultFileName(_defaultFileNamePrefix, "pdf")
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            ReportOperationProgressForm.Run(
                this,
                "Exporting PDF",
                "Preparing PDF export...",
                progress => _previewSource.ExportToPdfAsync(dialog.FileName, progress));
            MessageBox.Show(this, "PDF report saved successfully.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void excelButton_Click(object sender, EventArgs e)
        {
            if (_previewSource is null)
            {
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = BuildDefaultFileName(_defaultFileNamePrefix, "xlsx")
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            ReportOperationProgressForm.Run(
                this,
                "Exporting Excel",
                "Preparing Excel export...",
                progress => _previewSource.ExportToExcelAsync(dialog.FileName, progress));
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

        private void AdjustZoom(double delta)
        {
            if (previewControl.Document is null)
            {
                return;
            }

            double currentZoom = GetCurrentZoom();
            previewControl.AutoZoom = false;
            previewControl.Zoom = Math.Clamp(currentZoom + delta, MinimumZoom, MaximumZoom);
            UpdateZoomState();
        }

        private void ApplyPreviewPage()
        {
            if (previewControl.Document is null)
            {
                return;
            }

            previewControl.StartPage = _currentPreviewPage;
            // Do not force a full preview regeneration on page navigation.
            previewControl.Invalidate();
            previewControl.Update();

            previousPageButton.Enabled = _currentPreviewPage > 0;
            nextPageButton.Enabled = _currentPreviewPage < _previewPageCount - 1;
            pageNumberTextBox.Text = (_currentPreviewPage + 1).ToString(CultureInfo.InvariantCulture);
            pageInfoLabel.Text = $"of {_previewPageCount}";
        }

        private void UpdateZoomState()
        {
            double zoom = GetCurrentZoom();
            zoomInfoLabel.Text = previewControl.AutoZoom
                ? "Zoom: Auto"
                : $"Zoom: {Math.Round(zoom * 100D):0}%";
        }

        private double GetCurrentZoom()
        {
            double zoom = previewControl.Zoom;
            if (zoom <= 0D)
            {
                return 1D;
            }

            return Math.Clamp(zoom, MinimumZoom, MaximumZoom);
        }

        private void SetActionAvailability(bool isEnabled)
        {
            printButton.Enabled = isEnabled;
            pdfButton.Enabled = isEnabled;
            excelButton.Enabled = isEnabled;
            previousPageButton.Enabled = isEnabled;
            nextPageButton.Enabled = isEnabled;
            pageNumberTextBox.Enabled = isEnabled;
        }

        private void pageNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            NavigateToTypedPage();
        }

        private void pageNumberTextBox_Leave(object sender, EventArgs e)
        {
            NavigateToTypedPage();
        }

        private void NavigateToTypedPage()
        {
            if (previewControl.Document is null || _previewPageCount <= 0)
            {
                return;
            }

            if (!int.TryParse(pageNumberTextBox.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int pageNumber))
            {
                pageNumberTextBox.Text = (_currentPreviewPage + 1).ToString(CultureInfo.InvariantCulture);
                return;
            }

            int targetPage = Math.Clamp(pageNumber, 1, _previewPageCount) - 1;
            if (targetPage == _currentPreviewPage)
            {
                pageNumberTextBox.Text = (_currentPreviewPage + 1).ToString(CultureInfo.InvariantCulture);
                return;
            }

            _currentPreviewPage = targetPage;
            ApplyPreviewPage();
        }

        private void previewControl_FocusRequested(object? sender, EventArgs e)
        {
            if (previewControl.CanFocus && !previewControl.Focused)
            {
                previewControl.Focus();
            }
        }

        private void previewControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != Keys.Control)
            {
                return;
            }

            AdjustZoom(e.Delta > 0 ? ZoomStep : -ZoomStep);
        }

        private static string BuildDefaultFileName(string reportTitle, string extension)
        {
            string safeTitle = string.Concat(reportTitle.Select(character => Path.GetInvalidFileNameChars().Contains(character) ? '_' : character));
            return $"{safeTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
        }
    }
}
