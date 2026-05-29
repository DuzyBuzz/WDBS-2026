using System.Configuration;

namespace WDBS_2026.Properties;

internal sealed class UserInterfaceSettings : ApplicationSettingsBase
{
    private static readonly UserInterfaceSettings DefaultInstance =
        (UserInterfaceSettings)Synchronized(new UserInterfaceSettings());

    public static UserInterfaceSettings Default => DefaultInstance;

    [UserScopedSetting]
    [DefaultSettingValue("245")]
    public int BillerDashboardBilledTrendSplitterDistance
    {
        get => (int)this[nameof(BillerDashboardBilledTrendSplitterDistance)];
        set => this[nameof(BillerDashboardBilledTrendSplitterDistance)] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("245")]
    public int BillerDashboardBillingStatusSplitterDistance
    {
        get => (int)this[nameof(BillerDashboardBillingStatusSplitterDistance)];
        set => this[nameof(BillerDashboardBillingStatusSplitterDistance)] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("245")]
    public int BillerDashboardConcessionairesByZoneSplitterDistance
    {
        get => (int)this[nameof(BillerDashboardConcessionairesByZoneSplitterDistance)];
        set => this[nameof(BillerDashboardConcessionairesByZoneSplitterDistance)] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("245")]
    public int BillerDashboardConcessionaireStatusSplitterDistance
    {
        get => (int)this[nameof(BillerDashboardConcessionaireStatusSplitterDistance)];
        set => this[nameof(BillerDashboardConcessionaireStatusSplitterDistance)] = value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("218")]
    public int BillerDashboardTopConcessionairesSplitterDistance
    {
        get => (int)this[nameof(BillerDashboardTopConcessionairesSplitterDistance)];
        set => this[nameof(BillerDashboardTopConcessionairesSplitterDistance)] = value;
    }
}
