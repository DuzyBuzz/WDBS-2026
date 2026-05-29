using WDBS_2026.DTOs.Auth;

namespace WDBS_2026.Components.Admin;

public partial class AdminDashboardControl : UserControl
{
    private readonly AuthenticatedUserDto _user;

    public AdminDashboardControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        subtitleLabel.Text = $"Signed in as {_user.FullName}. This area manages users, settings, service rates, and zones.";
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        AppTheme.ApplyPageTitle(headingLabel);
        AppTheme.ApplySubtitle(subtitleLabel);

        foreach (Panel card in new[] { usersCardPanel, settingsCardPanel, servicesCardPanel, zonesCardPanel })
        {
            AppTheme.ApplyCard(card);
        }

        foreach (Label label in new[] { usersTitleLabel, settingsTitleLabel, servicesTitleLabel, zonesTitleLabel })
        {
            AppTheme.ApplyCardTitle(label);
        }

        foreach (Label label in new[] { usersBodyLabel, settingsBodyLabel, servicesBodyLabel, zonesBodyLabel })
        {
            AppTheme.ApplyCardBody(label);
        }
    }
}