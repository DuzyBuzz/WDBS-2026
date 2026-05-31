namespace WDBS_2026.Services.Dashboard;

internal sealed record BillerDashboardPoint(string Label, decimal Value);

internal sealed record BillerServiceSummaryItem(
    int ServiceId,
    string ServiceType,
    string PipeSize,
    int TotalConcessionaires,
    int TaxExemptCount,
    int DueExemptCount,
    int DiscountedCount,
    int ActiveCount,
    int InactiveCount,
    int MetersAssigned,
    int MetersUnassigned);

internal sealed record BillerBillingStatusMonthly(
    string BillingMonth,
    int TotalBills,
    int PaidCount,
    int PartiallyPaidCount,
    int OverdueCount,
    int UnpaidCount,
    decimal PaidPercent,
    decimal PartiallyPaidPercent,
    decimal OverduePercent,
    decimal UnpaidPercent);

internal sealed record BillerServiceBillingMonthly(
    string BillingMonth,
    int ServiceId,
    string ServiceType,
    string PipeSize,
    int TotalBills,
    decimal TotalConsumption,
    decimal TotalWaterCharge,
    decimal TotalTaxAmount,
    decimal TotalPenaltyAmount,
    decimal TotalBilledAmount,
    decimal TotalRemainingBalance,
    decimal BillingSharePercent);

internal sealed record BillerDashboardSnapshot(
    DateTime PeriodStart,
    DateTime PeriodEnd,
    decimal TotalBilledAmount,
    int TotalBillCount,
    int UnpaidBillCount,
    int ActiveConcessionaireCount,
    IReadOnlyList<BillerDashboardPoint> BilledTrend,
    IReadOnlyList<BillerBillingStatusMonthly> BillingStatusMonthly,
    IReadOnlyList<BillerDashboardPoint> ConcessionairesByZone,
    IReadOnlyList<BillerDashboardPoint> ConcessionaireStatusMix,
    IReadOnlyList<BillerDashboardPoint> TopConcessionairesByAmount,
    IReadOnlyList<BillerServiceBillingMonthly> ServiceBillingMonthly);
