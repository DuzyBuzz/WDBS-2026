namespace WDBS_2026.Services.Dashboard;

internal sealed record CashierDashboardPoint(string Label, decimal Value);

internal sealed record CashierRecentCollection(
    DateTime CollectionDate,
    string OrNumber,
    string Concessionaire,
    string PaymentType,
    decimal Amount);

internal sealed record CashierDashboardSnapshot(
    DateTime PeriodStart,
    DateTime PeriodEnd,
    decimal TotalCollectedAmount,
    int OfficialReceiptCount,
    decimal ScfCollectedAmount,
    int OutstandingAccountsCount,
    decimal MonthlyCollectionRate,
    IReadOnlyList<CashierDashboardPoint> CollectionTrend,
    IReadOnlyList<CashierDashboardPoint> UncollectedTrend,
    IReadOnlyList<CashierDashboardPoint> MonthlyCollectionTrend,
    IReadOnlyList<CashierDashboardPoint> MonthlyUncollectedTrend,
    IReadOnlyList<CashierDashboardPoint> PaymentTypeBreakdown,
    IReadOnlyList<CashierDashboardPoint> TopCollectedAccounts,
    IReadOnlyList<CashierRecentCollection> RecentCollections);
