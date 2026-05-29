using WDBS_2026.DTOs.Auth;

namespace WDBS_2026.Components.Cashier;

public partial class CashierDashboardControl : UserControl
{
    private readonly AuthenticatedUserDto _user;

    public CashierDashboardControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        subtitleLabel.Text = $"Signed in as {_user.FullName}. This role preserves the collection and cashier posting flow.";
        ApplyTheme();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        AppTheme.ApplyPageTitle(headingLabel);
        AppTheme.ApplySubtitle(subtitleLabel);

        foreach (Panel card in new[] { collectionCardPanel, scfCollectionCardPanel, recordsCardPanel })
        {
            AppTheme.ApplyCard(card);
        }

        foreach (Label label in new[] { collectionTitleLabel, scfCollectionTitleLabel, recordsTitleLabel })
        {
            AppTheme.ApplyCardTitle(label);
        }

        foreach (Label label in new[] { collectionBodyLabel, scfCollectionBodyLabel, recordsBodyLabel })
        {
            AppTheme.ApplyCardBody(label);
        }
    }
}