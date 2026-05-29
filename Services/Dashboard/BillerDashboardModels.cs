namespace WDBS_2026.Services.Dashboard;

internal sealed record BillerDashboardPoint(string Label, decimal Value);

internal sealed record BillerDashboardSnapshot(
    DateTime PeriodStart,
    DateTime PeriodEnd,
    decimal TotalBilledAmount,
    int TotalBillCount,
    int UnpaidBillCount,
    int ActiveConcessionaireCount,
    IReadOnlyList<BillerDashboardPoint> BilledTrend,
    IReadOnlyList<BillerDashboardPoint> BillingStatusBreakdown,
    IReadOnlyList<BillerDashboardPoint> ConcessionairesByZone,
    IReadOnlyList<BillerDashboardPoint> ConcessionaireStatusMix,
    IReadOnlyList<BillerDashboardPoint> TopConcessionairesByAmount);
