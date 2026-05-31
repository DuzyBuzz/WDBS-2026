using System.ComponentModel;

namespace WDBS_2026.Forms.Auditing
{


    public partial class AuditingForm : Form
    {
        private readonly AuditLogDetail _detail;

        public AuditingForm()
            : this(new AuditLogDetail())
        {
        }

        public AuditingForm(AuditLogDetail detail)
        {
            _detail = detail;
            InitializeComponent();
            
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ApplyTheme();
                BindDetails();
            }
        }

        private void ApplyTheme()
        {
            AppTheme.ApplyFormSurface(this);
            detailsCard.BackColor = AppTheme.SurfaceColor;
            detailsCard.BorderStyle = BorderStyle.FixedSingle;

            titleLabel.ForeColor = AppTheme.BodyTextColor;
            titleLabel.Font = AppTheme.HeadingFont;
            subtitleLabel.ForeColor = AppTheme.MutedTextColor;
            subtitleLabel.Font = AppTheme.BodyFont;

            foreach (Control control in detailsTable.Controls)
            {
                if (control is Label label && label.Name.EndsWith("CaptionLabel", StringComparison.Ordinal))
                {
                    label.ForeColor = AppTheme.MutedTextColor;
                    label.Font = AppTheme.SectionFont;
                }

                if (control is TextBox textBox)
                {
                    AppTheme.ApplyInput(textBox);
                    textBox.ReadOnly = true;
                }
            }
        }

        private void BindDetails()
        {
            logIdTextBox.Text = _detail.LogId > 0 ? _detail.LogId.ToString("N0") : "-";
            loggedAtTextBox.Text = _detail.LoggedAt?.ToString("yyyy-MM-dd hh:mm:ss tt") ?? "-";
            fullNameTextBox.Text = string.IsNullOrWhiteSpace(_detail.FullName) ? "-" : _detail.FullName;
            usernameTextBox.Text = string.IsNullOrWhiteSpace(_detail.Username) ? "-" : _detail.Username;
            roleTextBox.Text = string.IsNullOrWhiteSpace(_detail.ActorRole) ? "-" : _detail.ActorRole;
            actionTextBox.Text = string.IsNullOrWhiteSpace(_detail.Action) ? "-" : _detail.Action;
        }
    }
    public sealed class AuditLogDetail
    {
        public int LogId { get; init; }
        public DateTime? LoggedAt { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string ActorRole { get; init; } = string.Empty;
        public string Action { get; init; } = string.Empty;
    }
}
