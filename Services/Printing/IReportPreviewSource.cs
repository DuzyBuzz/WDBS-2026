using System.Drawing.Printing;

namespace WDBS_2026.Services.Printing;

internal readonly record struct ReportOperationProgress(
    int Percentage,
    string Message);

internal interface IReportPreviewSource
{
    Task PreparePreviewAsync(IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default);

    PrintDocument CreatePreviewDocument();

    PrintDocument CreatePrintDocument();

    int GetPreviewPageCount();

    Task ExportToPdfAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default);

    void ExportToPdf(string filePath);

    Task ExportToExcelAsync(string filePath, IProgress<ReportOperationProgress>? progress = null, CancellationToken cancellationToken = default);

    void ExportToExcel(string filePath);
}