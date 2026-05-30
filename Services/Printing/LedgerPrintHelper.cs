using System.Data;
using WDBS_2026.Forms.Report;
using WDBS_2026.Models;
using WDBS_2026.Services.Cashier;

namespace WDBS_2026.Services.Printing;

internal static class LedgerPrintHelper
{
    public static async Task ShowPreviewAsync(IWin32Window owner, UserRole role, CollectionLedgerRequest request)
    {
        DataTable rows = await CollectionService.GetLedgerRowsAsync(role, request.ConcessionaireCode);

        // Compute cumulative running balance (debit − credit) row by row
        rows.Columns.Add("running_balance", typeof(decimal));
        decimal runningBalance = 0m;
        foreach (DataRow r in rows.Rows)
        {
            decimal debit  = r["debit"]  is not DBNull ? Convert.ToDecimal(r["debit"])  : 0m;
            decimal credit = r["credit"] is not DBNull ? Convert.ToDecimal(r["credit"]) : 0m;
            runningBalance += debit - credit;
            if (runningBalance < 0m)
            {
                runningBalance = 0m;
            }

            r["running_balance"] = runningBalance;
        }

        var document = new LedgerReportDocumentData(
            "Concessionaire Ledger",
            $"Account: {request.ConcessionaireCode} - {request.ConcessionaireName}",
            request.PrintedBy,
            DateTime.Now,
            rows,
            ConcessionaireCode: request.ConcessionaireCode,
            ConcessionaireName: request.ConcessionaireName,
            ConcessionaireAddress: GetConcessionaireAddress(rows));

        var preview = new PrintPreviewForm(
            "Concessionaire Ledger",
            $"Account: {request.ConcessionaireCode} - {request.ConcessionaireName}",
            new LedgerReportPrintHelper(document),
            defaultFileNamePrefix: $"ledger_{request.ConcessionaireCode}");

        preview.Show(owner);
    }

    private static string GetConcessionaireAddress(DataTable rows)
    {
        if (rows.Rows.Count == 0 || !rows.Columns.Contains("address"))
        {
            return string.Empty;
        }

        object val = rows.Rows[0]["address"];
        return val is not DBNull ? Convert.ToString(val) ?? string.Empty : string.Empty;
    }
}
