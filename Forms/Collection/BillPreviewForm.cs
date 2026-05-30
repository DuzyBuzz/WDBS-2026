using System.Data;

namespace WDBS_2026.Forms.Collection
{
    public partial class BillPreviewForm : Form
    {
        public bool IsConfirmed { get; private set; }

        public BillPreviewForm(DataTable previewData)
        {
            InitializeComponent();
            ApplyTheme();
            previewTextBox.Text = BuildPreviewText(previewData);
        }

        private void ApplyTheme()
        {
            AppTheme.ApplyFormSurface(this);
            AppTheme.ApplyPageTitle(titleLabel);
            AppTheme.ApplySubtitle(subtitleLabel);

            previewTextBox.BackColor = Color.White;
            previewTextBox.ForeColor = AppTheme.BodyTextColor;
            previewTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular);

            AppTheme.ApplySeverityButton(confirmButton, ButtonSeverity.Success);
            AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);
        }

        private static string BuildPreviewText(DataTable previewData)
        {
            if (previewData.Rows.Count == 0)
            {
                return "No preview data returned.";
            }

            DataRow row = previewData.Rows[0];
            if (previewData.Columns.Contains("preview_text"))
            {
                return Convert.ToString(row["preview_text"]) ?? "No preview data returned.";
            }

            return string.Join(
                Environment.NewLine,
                previewData.Columns.Cast<DataColumn>().Select(column => $"{column.ColumnName}: {Convert.ToString(row[column])}"));
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
