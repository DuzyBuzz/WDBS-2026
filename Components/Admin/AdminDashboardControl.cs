using System.ComponentModel;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Models;

namespace WDBS_2026.Components.Admin;

public partial class AdminDashboardControl : UserControl
{
    private readonly AuthenticatedUserDto _user;
    private bool _dashboardsLoaded;

    public AdminDashboardControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        ApplyTheme();
    }

    protected override void OnLoad(EventArgs e)
    { 
        base.OnLoad(e);

        if (IsDesignerHosted() || _dashboardsLoaded)
        {
            return;
        }

        LoadRoleDashboards();
        _dashboardsLoaded = true;
    }

    private static bool IsDesignerHosted()
    {
        return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }

    private void LoadRoleDashboards()
    {
        var billerDashboard = new Biller.BillerDashboardControl(CreateDashboardUser(UserRole.Biller))
        {
            Dock = DockStyle.Fill
        };

        var cashierDashboard = new Cashier.CashierDashboardControl(CreateDashboardUser(UserRole.Cashier))
        {
            Dock = DockStyle.Fill
        };

        billerDashboardHostPanel.Controls.Clear();
        billerDashboardHostPanel.Controls.Add(billerDashboard);

        cashierDashboardHostPanel.Controls.Clear();
        cashierDashboardHostPanel.Controls.Add(cashierDashboard);
    }

    private AuthenticatedUserDto CreateDashboardUser(UserRole role)
    {
        return new AuthenticatedUserDto
        {
            UserId = _user.UserId,
            Username = _user.Username,
            FullName = _user.FullName,
            Role = role
        };
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;

        roleDashboardsTabControl.Font = AppTheme.BodyFont;
        roleDashboardsTabControl.BackColor = AppTheme.SurfaceColor;
        roleDashboardsTabControl.ForeColor = AppTheme.BodyTextColor;

        foreach (TabPage tab in new[] { billerTabPage, cashierTabPage })
        {
            tab.BackColor = AppTheme.ShellBackgroundColor;
            tab.ForeColor = AppTheme.BodyTextColor;
        }

        billerDashboardHostPanel.BackColor = AppTheme.ShellBackgroundColor;
        cashierDashboardHostPanel.BackColor = AppTheme.ShellBackgroundColor;
    }
}