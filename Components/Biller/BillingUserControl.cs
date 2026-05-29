using System.Data;
using System.Globalization;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Report;
using WDBS_2026.Services.Printing;
using WDBS_2026.Services.Reports;

namespace WDBS_2026.Components.Biller;

public partial class BillingUserControl : UserControl
{
    private const int PageSize = 100;

    private enum BillingViewMode
    {
        Daily,
        Monthly
    }

    private readonly AuthenticatedUserDto _user;

    private int _currentPage = 1;
    private int _totalRecords;

    public BillingUserControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        ApplyTheme();
        ConfigureGrid();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        modeComboBox.SelectedIndex = (int)BillingViewMode.Monthly;
        periodPicker.Value = DateTime.Today;
        ApplySelectedMode();

        await LoadBillingAsync();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplyInput(searchTextBox);
        AppTheme.ApplySeverityButton(searchButton, ButtonSeverity.Primary);
        AppTheme.ApplySeverityButton(clearButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(printReportButton, ButtonSeverity.Info);
        AppTheme.ApplySeverityButton(previousPageButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(nextPageButton, ButtonSeverity.Neutral);

        foreach (Label label in new[] { modeLabel, periodLabel, statusLabel, pageInfoLabel })
        {
            label.ForeColor = AppTheme.BodyTextColor;
        }

        AppTheme.ApplySubtitle(statusLabel);
        modeComboBox.Font = AppTheme.BodyFont;

        periodPicker.Font = AppTheme.BodyFont;
        periodPicker.CalendarForeColor = AppTheme.BodyTextColor;
    }

    private void ConfigureGrid()
    {
        billingGrid.AllowUserToAddRows = false;
        billingGrid.AllowUserToDeleteRows = false;
        billingGrid.AllowUserToResizeRows = false;
        billingGrid.BackgroundColor = AppTheme.SurfaceColor;
        billingGrid.BorderStyle = BorderStyle.FixedSingle;
        billingGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        billingGrid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        billingGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        billingGrid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
        billingGrid.DefaultCellStyle.Font = AppTheme.BodyFont;
        billingGrid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
        billingGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
        billingGrid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
        billingGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
        billingGrid.EnableHeadersVisualStyles = false;
        billingGrid.GridColor = AppTheme.BorderColor;
        billingGrid.MultiSelect = false;
        billingGrid.ReadOnly = true;
        billingGrid.RowHeadersVisible = false;
        billingGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        billingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        billingGrid.DataBindingComplete += billingGrid_DataBindingComplete;
    }

    private async Task LoadBillingAsync()
    {
        try
        {
            SetBusyState(true, "Loading billings...");

            BillingReportQueryOptions options = BuildQueryOptions();
            _totalRecords = await BillingReportService.GetTotalRecordsAsync(_user.Role, options);

            int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
            _currentPage = Math.Max(1, Math.Min(_currentPage, totalPages));

            int offset = (_currentPage - 1) * PageSize;
            DataTable rows = await BillingReportService.GetPagedRowsAsync(_user.Role, options, offset, PageSize);

            billingGrid.DataSource = rows;
            ApplyGridLayout();

            pageInfoLabel.Text = $"Page {_currentPage} of {totalPages}  •  {_totalRecords} record(s)";
            previousPageButton.Enabled = _currentPage > 1;
            nextPageButton.Enabled = _currentPage < totalPages;

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = _totalRecords == 0
                ? "No billing records found for the selected filters."
                : "Billing records loaded successfully.";
        }
        catch (Exception ex)
        {
            billingGrid.DataSource = null;
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load billing records.";
            MessageBox.Show(this, ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private BillingReportQueryOptions BuildQueryOptions()
    {
        (DateTime dateFrom, DateTime dateTo) = GetSelectedDateRange();

        return new BillingReportQueryOptions(
            dateFrom,
            dateTo,
            searchTextBox.Text.Trim());
    }

    private void ApplyGridLayout()
    {
        if (billingGrid.Columns.Count == 0)
        {
            return;
        }

        foreach (DataGridViewColumn column in billingGrid.Columns)
        {
            column.HeaderText = GetFriendlyHeader(column.Name);
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Width = Math.Max(column.Width, 120);
        }

        if (billingGrid.Columns["billing_id"] is { } billingIdColumn)
        {
            billingIdColumn.Visible = false;
        }

        if (billingGrid.Columns["zone_id"] is { } zoneIdColumn)
        {
            zoneIdColumn.Visible = false;
        }

        SetColumnWidth("concessionaire_name", 260);
        SetColumnWidth("address", 260);
        SetColumnWidth("meter_no", 140);
        SetColumnWidth("status", 120);
        SetColumnWidth("scf_status", 120);

        // Backward compatibility in case legacy projected columns are used.
        SetColumnWidth("Date", 108);
        SetColumnWidth("Concessionaire_Name", 260);
        SetColumnWidth("Invoice_Number", 120);
        SetColumnWidth("Remaining_Balance", 140);

        if (billingGrid.Columns["concessionaire_name"] is { } viewNameColumn)
        {
            viewNameColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (billingGrid.Columns["Concessionaire_Name"] is { } reportNameColumn)
        {
            reportNameColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (billingGrid.Columns["address"] is { } addressColumn)
        {
            addressColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        billingGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        foreach (string dateOnlyColumn in new[] { "Date", "billing_date", "due_date", "last_payment_date", "first_reading_date", "paid_at", "previous_reading_date", "current_reading_date" })
        {
            if (billingGrid.Columns[dateOnlyColumn] is { } column)
            {
                column.DefaultCellStyle.Format = "MMM dd, yyyy";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        foreach (string wholeNumberColumn in new[] { "consumption", "free_water" })
        {
            if (billingGrid.Columns[wholeNumberColumn] is { } column)
            {
                column.DefaultCellStyle.Format = "N0";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        foreach (string dateTimeColumn in new[] { "created_at", "updated_at", "penalty_applied_at" })
        {
            if (billingGrid.Columns[dateTimeColumn] is { } column)
            {
                column.DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        foreach (string numericColumn in new[]
                 {
                     "Cu_m³", "Un_m³", "Water_Bill", "Tax", "Discount", "Total_Water_Bill", "Arrears", "SCF", "Remaining_Balance", "Total_Amount_Billed",
                     "water_charge", "discount_amount", "tax_amount", "total_water_bill",
                     "scf_amount", "arrears_amount", "penalty_amount", "total_amount", "remaining_water_charge",
                     "remaining_tax_amount", "remaining_penalty_amount", "remaining_scf_amount", "remaining_balance",
                     "tax_percent_used", "discount_percent_used", "penalty_percent_used", "scf_monthly_used", "scf_total_cap_used"
                 })
        {
            if (billingGrid.Columns[numericColumn] is { } column)
            {
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        foreach (string integerColumn in new[]
                 {
                     "billing_id", "bill_number", "concessionaire_id", "reading_id", "zone_id", "service_id",
                     "payment_count", "last_collection_id", "created_by_user_id", "updated_by_user_id", "request_id"
                 })
        {
            if (billingGrid.Columns[integerColumn] is { } column)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        foreach (string boolColumn in new[] { "is_initial", "is_penalty_applied", "is_tax_exempt", "is_due_exempt", "is_discounted" })
        {
            if (billingGrid.Columns[boolColumn] is { } column)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }

    private static string GetFriendlyHeader(string columnName)
    {
        return columnName switch
        {
            "Concessionaire_Name" => "Concessionaire Name",
            "Invoice_Number" => "Invoice Number",
            "Water_Bill" => "Water Bill",
            "Total_Water_Bill" => "Total Water Bill",
            "Remaining_Balance" => "Remaining Balance",
            "Total_Amount_Billed" => "Total Amount Billed",
            "Cu_m³" => "Cu m³",
            "Un_m³" => "Un m³",
            "billing_id" => "Billing ID",
            "bill_number" => "Bill Number",
            "concessionaire_code" => "Concessionaire Code",
            "concessionaire_name" => "Concessionaire Name",
            "previous_reading_date" => "Previous Reading Date",
            "current_reading_date" => "Current Reading Date",
            "previous_reading" => "Previous Reading",
            "present_reading" => "Present Reading",
            "consumption" => "Consumption",
            "free_water" => "Free Water",
            "water_charge" => "Water Charge",
            "discount_amount" => "Discount Amount",
            "tax_amount" => "Tax Amount",
            "total_water_bill" => "Total Water Bill",
            "scf_amount" => "SCF Amount",
            "arrears_amount" => "Arrears Amount",
            "penalty_amount" => "Penalty Amount",
            "total_amount" => "Total Amount",
            "remaining_balance" => "Remaining Balance",
            "billing_date" => "Billing Date",
            "due_date" => "Due Date",
            "scf_status" => "SCF Status",
            "is_initial" => "Is Initial",
            _ => ToProfessionalTitle(columnName)
        };
    }

    private static string ToProfessionalTitle(string rawHeader)
    {
        if (string.IsNullOrWhiteSpace(rawHeader))
        {
            return rawHeader;
        }

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        string[] parts = rawHeader.Split('_', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < parts.Length; i++)
        {
            string token = parts[i].Trim();
            string lower = token.ToLowerInvariant();

            parts[i] = lower switch
            {
                "id" => "ID",
                "scf" => "SCF",
                "tin" => "TIN",
                "no" => "No",
                _ => textInfo.ToTitleCase(lower)
            };
        }

        return string.Join(" ", parts);
    }

    private void SetColumnWidth(string columnName, int width)
    {
        if (billingGrid.Columns[columnName] is { } column)
        {
            column.Width = width;
        }
    }

    private void SetBusyState(bool isBusy, string? busyMessage = null)
    {
        modeComboBox.Enabled = !isBusy;
        periodPicker.Enabled = !isBusy;
        searchTextBox.Enabled = !isBusy;
        searchButton.Enabled = !isBusy;
        clearButton.Enabled = !isBusy;
        printReportButton.Enabled = !isBusy;
        previousPageButton.Enabled = !isBusy;
        nextPageButton.Enabled = !isBusy;
        billingGrid.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = busyMessage;
        }
    }

    private async void searchButton_Click(object sender, EventArgs e)
    {
        _currentPage = 1;
        await LoadBillingAsync();
    }

    private async void clearButton_Click(object sender, EventArgs e)
    {
        periodPicker.Value = DateTime.Today;
        ApplySelectedMode();
        searchTextBox.Clear();
        _currentPage = 1;
        await LoadBillingAsync();
    }

    private async void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        _currentPage = 1;
        await LoadBillingAsync();
    }

    private async void previousPageButton_Click(object sender, EventArgs e)
    {
        if (_currentPage <= 1)
        {
            return;
        }

        _currentPage--;
        await LoadBillingAsync();
    }

    private async void nextPageButton_Click(object sender, EventArgs e)
    {
        int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
        if (_currentPage >= totalPages)
        {
            return;
        }

        _currentPage++;
        await LoadBillingAsync();
    }

    private void billingGrid_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        billingGrid.ClearSelection();
    }

    private async void printReportButton_Click(object sender, EventArgs e)
    {
        BillingReportQueryOptions options = BuildQueryOptions();

        await OpenReportPreviewAsync(BuildReportTitle(), BuildPeriodCaption(), options);
    }

    private async Task OpenReportPreviewAsync(string reportTitle, string periodCaption, BillingReportQueryOptions options)
    {
        int totalRecords = 0;
        DataTable reportRows = new();

        try
        {
            ReportOperationProgressForm.Run(
                this,
                "Loading Billing Report",
                "Counting billing records...",
                async progress =>
                {
                    progress.Report(new ReportOperationProgress(5, "Counting billing records..."));
                    totalRecords = await BillingReportService.GetTotalRecordsAsync(_user.Role, options);

                    if (totalRecords == 0)
                    {
                        progress.Report(new ReportOperationProgress(100, "No billing records found for the selected filters."));
                        reportRows = new DataTable();
                        return;
                    }

                    progress.Report(new ReportOperationProgress(8, $"Found {totalRecords} billing record(s)."));
                    reportRows = await BillingReportService.GetReportRowsWithProgressAsync(_user.Role, options, totalRecords, progress);
                });
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load billing report.";
            MessageBox.Show(this, ex.Message, "Billing Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (totalRecords == 0 || reportRows.Rows.Count == 0)
        {
            MessageBox.Show(this, "There are no billing records for the selected filters to print.", "Billing Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        OpenReportPreview(reportTitle, periodCaption, reportRows);
    }

    private async void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        ApplySelectedMode();
        _currentPage = 1;
        await LoadBillingAsync();
    }

    private async void periodPicker_ValueChanged(object sender, EventArgs e)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        _currentPage = 1;
        await LoadBillingAsync();
    }

    private void OpenReportPreview(string reportTitle, string periodCaption, DataTable rows)
    {
        try
        {
            var document = new BillingReportDocumentData(
                reportTitle,
                periodCaption,
                _user.FullName,
                DateTime.Now,
                rows);

            using var previewForm = new PrintPreviewForm(
                document.ReportTitle,
                document.PeriodCaption,
                new BillingReportPrintHelper(document));
            previewForm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Billing Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplySelectedMode()
    {
        bool isDaily = GetSelectedMode() == BillingViewMode.Daily;
        periodLabel.Text = isDaily ? "Billing Date" : "Billing Month";
        printReportButton.Text = isDaily ? "Print Daily Report" : "Print Monthly Report";

        if (isDaily)
        {
            periodPicker.ShowUpDown = false;
            periodPicker.Format = DateTimePickerFormat.Short;
        }
        else
        {
            periodPicker.ShowUpDown = true;
            periodPicker.Format = DateTimePickerFormat.Custom;
            periodPicker.CustomFormat = "MMMM yyyy";
        }
    }

    private BillingViewMode GetSelectedMode()
    {
        return modeComboBox.SelectedIndex == (int)BillingViewMode.Daily
            ? BillingViewMode.Daily
            : BillingViewMode.Monthly;
    }

    private (DateTime DateFrom, DateTime DateTo) GetSelectedDateRange()
    {
        DateTime selectedDate = periodPicker.Value.Date;
        return GetSelectedMode() switch
        {
            BillingViewMode.Daily => (selectedDate, selectedDate),
            BillingViewMode.Monthly => (new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1)),
            _ => (selectedDate, selectedDate)
        };
    }

    private string BuildReportTitle()
    {
        return GetSelectedMode() == BillingViewMode.Daily ? "Daily Billing Report" : "Monthly Billing Report";
    }

    private string BuildPeriodCaption()
    {
        DateTime selectedDate = periodPicker.Value.Date;
        return GetSelectedMode() == BillingViewMode.Daily
            ? $"Billing Date: {selectedDate:MMMM dd, yyyy}"
            : $"Billing Month: {selectedDate:MMMM yyyy}";
    }

    private void filterLayout_Paint(object sender, PaintEventArgs e)
    {

    }
}
