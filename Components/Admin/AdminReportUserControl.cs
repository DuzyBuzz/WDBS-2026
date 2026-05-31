using System.ComponentModel;
using WDBS_2026.Components.Biller;
using WDBS_2026.Components.Cashier;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Models;

namespace WDBS_2026.Components.Admin
{
    public partial class AdminReportUserControl : UserControl
    {
        private readonly AuthenticatedUserDto _user;
        private bool _pagesLoaded;

        public AdminReportUserControl()
            : this(new AuthenticatedUserDto
            {
                UserId = 0,
                Username = "designer",
                FullName = "Dashboard Designer",
                Role = UserRole.Admin
            })
        {
        }

        public AdminReportUserControl(AuthenticatedUserDto user)
        {
            _user = user;
            InitializeComponent();
            ApplyTheme();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsDesignerHosted() || _pagesLoaded)
            {
                return;
            }

            LoadReportPages();
            _pagesLoaded = true;
        }

        private static bool IsDesignerHosted()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.ShellBackgroundColor;
            rootLayout.BackColor = AppTheme.ShellBackgroundColor;
            AppTheme.ApplyPageTitle(titleLabel);

            reportsTabControl.Font = AppTheme.BodyFont;
            reportsTabControl.BackColor = AppTheme.SurfaceColor;
            reportsTabControl.ForeColor = AppTheme.BodyTextColor;

            foreach (TabPage tab in new[]
                     {
                         billingTabPage,
                         collectionTabPage,
                         agingTabPage,
                         agingScfTabPage
                     })
            {
                tab.BackColor = AppTheme.ShellBackgroundColor;
                tab.ForeColor = AppTheme.BodyTextColor;
            }

            foreach (Panel host in new[]
                     {
                         billingHostPanel,
                         collectionHostPanel,
                         agingHostPanel,
                         agingScfHostPanel
                     })
            {
                host.BackColor = AppTheme.ShellBackgroundColor;
            }
        }

        private void LoadReportPages()
        {
            AddControlToHost(billingHostPanel, new BillingUserControl(CreateRoleScopedUser(UserRole.Biller)));
            AddControlToHost(collectionHostPanel, new CollectionUserControl(CreateRoleScopedUser(UserRole.Cashier)));
            AddControlToHost(agingHostPanel, new AgingOfAccountsUserControl(CreateRoleScopedUser(UserRole.Cashier)));
            AddControlToHost(agingScfHostPanel, new SCFAgingOfAccountsUserControl(CreateRoleScopedUser(UserRole.Cashier)));
        }

        private AuthenticatedUserDto CreateRoleScopedUser(UserRole role)
        {
            return new AuthenticatedUserDto
            {
                UserId = _user.UserId,
                Username = _user.Username,
                FullName = _user.FullName,
                Role = role
            };
        }

        private static void AddControlToHost(Panel host, Control control)
        {
            control.Dock = DockStyle.Fill;
            host.Controls.Clear();
            host.Controls.Add(control);
        }
    }
}
