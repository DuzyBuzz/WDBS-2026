using System.Data;

namespace WDBS_2026.Services.Cashier;

internal sealed record CollectionPreviewRequest(
    string OrNumber,
    DateTime PaymentDate,
    string PaymentType,
    string ReferenceNumber,
    string Remarks,
    string PayorName,
    decimal Others,
    IReadOnlyList<int> ConcessionaireIds);

internal sealed record CollectionPaymentRequest(
    string OrNumber,
    DateTime PaymentDate,
    string PaymentType,
    string ReferenceNumber,
    string Remarks,
    string PayorName,
    decimal AmountReceived,
    decimal Others,
    int UserId,
    IReadOnlyList<int> ConcessionaireIds);

internal sealed record CollectionScfPaymentRequest(
    string OrNumber,
    DateTime PaymentDate,
    string PaymentType,
    string ReferenceNumber,
    string Remarks,
    string PayorName,
    decimal ScfAmount,
    decimal Others,
    int UserId,
    int ConcessionaireId);

internal sealed record CollectionLedgerRequest(
    string ConcessionaireCode,
    string ConcessionaireName,
    string PrintedBy);

internal sealed record CollectionSummaryResult(
    DataTable Concessionaires,
    DataTable Collections,
    int NextOrNumber);
