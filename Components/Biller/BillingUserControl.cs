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
    private bool _isAdjustingPeriodPicker;

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
        billingGrid.CellFormatting += billingGrid_CellFormatting;
        billingGrid.CellDoubleClick += billingGrid_CellDoubleClick;
    }

    private async Task LoadBillingAsync()
    {
        try
        {
            SetBusyState(true, "Loading billings...");

            BillingReportQueryOptions options = BuildQueryOptions();
            _totalRecords = await BillingReportService.GetTableTotalRecordsAsync(_user.Role, options);

            int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
            _currentPage = Math.Max(1, Math.Min(_currentPage, totalPages));

            int offset = (_currentPage - 1) * PageSize;
            DataTable rows = await BillingReportService.GetTablePagedRowsAsync(_user.Role, options, offset, PageSize);

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

        foreach (DataGridViewColumn column in billingGrid.Columns)
        {
            if (column.Name.EndsWith("_id", StringComparison.OrdinalIgnoreCase))
            {
                column.Visible = false;
            }
        }

        foreach (string hiddenColumn in new[]
                 {
                     "updated_at", "scf_status", "last_payment_date", "created_at", "penalty_applied_at",
                     "scf_monthly_used", "scf_total_cap_used", "paid_at", "created_by_username", "created_by_role",
                     "updated_by_username", "updated_by_full_name", "updated_by_role"
                 })
        {
            HideColumn(hiddenColumn);
        }

        SetColumnWidth("billing_date", 124);
        SetColumnWidth("concessionaire_code", 122);
        SetColumnWidth("concessionaire_name", 260);
        SetColumnWidth("zone", 130);
        SetColumnWidth("bill_number", 120);
        SetColumnWidth("address", 260);
        SetColumnWidth("meter_number", 140);
        SetColumnWidth("status", 120);
        SetColumnWidth("scf_status", 120);
        SetColumnWidth("created_by_username", 140);
        SetColumnWidth("created_by_full_name", 200);
        SetColumnWidth("created_by_role", 120);
        SetColumnWidth("updated_by_username", 140);
        SetColumnWidth("updated_by_full_name", 200);
        SetColumnWidth("updated_by_role", 120);
        SetColumnWidth("created_at", 150);
        SetColumnWidth("updated_at", 150);
        SetColumnWidth("paid_at", 150);

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

        foreach (string dateOnlyColumn in new[] { "Date", "billing_date", "due_date", "last_payment_date", "first_reading_date", "previous_reading_date", "current_reading_date" })
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

        foreach (string dateTimeColumn in new[] { "created_at", "updated_at", "penalty_applied_at", "paid_at" })
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
                EnsureCheckBoxColumn(boolColumn);
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        foreach (string textCenterColumn in new[] { "created_by_role", "updated_by_role", "status", "scf_status" })
        {
            if (billingGrid.Columns[textCenterColumn] is { } column)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        SetColumnDisplayIndex("billing_date", 0);
        SetColumnDisplayIndex("bill_number", 1);
        SetColumnDisplayIndex("status", 2);
        SetColumnDisplayIndex("is_initial", 3);
        SetColumnDisplayIndex("concessionaire_code", 4);
        SetColumnDisplayIndex("concessionaire_name", 5);
        SetColumnDisplayIndex("zone", 6);
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
            "bill_number" => "Invoice Number",
            "concessionaire_code" => "Account No",
            "concessionaire_name" => "Concessionaire Name",
            "zone" => "Zone",
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
            "created_by_username" => "Created By Username",
            "created_by_full_name" => "Created By",
            "created_by_role" => "Created By Role",
            "updated_by_username" => "Updated By Username",
            "updated_by_full_name" => "Updated By Full Name",
            "updated_by_role" => "Updated By Role",
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

    private void HideColumn(string columnName)
    {
        if (billingGrid.Columns[columnName] is { } column)
        {
            column.Visible = false;
        }
    }

    private void EnsureCheckBoxColumn(string columnName)
    {
        if (billingGrid.Columns[columnName] is not { } existingColumn)
        {
            return;
        }

        if (existingColumn is DataGridViewCheckBoxColumn checkColumn)
        {
            checkColumn.ThreeState = false;
            checkColumn.ReadOnly = true;
            return;
        }

        int insertIndex = existingColumn.Index;
        int displayIndex = existingColumn.DisplayIndex;

        var replacement = new DataGridViewCheckBoxColumn
        {
            Name = existingColumn.Name,
            DataPropertyName = existingColumn.DataPropertyName,
            HeaderText = existingColumn.HeaderText,
            Width = existingColumn.Width,
            Visible = existingColumn.Visible,
            Frozen = existingColumn.Frozen,
            ReadOnly = true,
            SortMode = DataGridViewColumnSortMode.NotSortable,
            ThreeState = false,
            TrueValue = true,
            FalseValue = false,
            IndeterminateValue = DBNull.Value
        };

        billingGrid.Columns.RemoveAt(insertIndex);
        billingGrid.Columns.Insert(insertIndex, replacement);

        if (replacement.Visible)
        {
            replacement.DisplayIndex = displayIndex;
        }

        replacement.ThreeState = false;
        replacement.ReadOnly = true;
    }

    private void SetColumnDisplayIndex(string columnName, int displayIndex)
    {
        if (billingGrid.Columns[columnName] is { Visible: true } column)
        {
            column.DisplayIndex = Math.Min(displayIndex, billingGrid.Columns.Count - 1);
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
        periodPicker.Value = GetSelectedMode() == BillingViewMode.Monthly
            ? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            : DateTime.Today;
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

    private async void billingGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= billingGrid.Rows.Count)
        {
            return;
        }

        billingGrid.ClearSelection();
        DataGridViewRow row = billingGrid.Rows[e.RowIndex];
        row.Selected = true;

        DataGridViewCell? firstVisibleCell = row.Cells
            .Cast<DataGridViewCell>()
            .FirstOrDefault(cell => cell.Visible);

        if (firstVisibleCell is not null)
        {
            billingGrid.CurrentCell = firstVisibleCell;
        }

        await PrintSelectedBillAsync();
    }

    private async Task PrintSelectedBillAsync()
    {
        if (!TryGetSelectedBilling(out int billingId, out string billNumber))
        {
            MessageBox.Show(
                this,
                "Select a billing row first.",
                "Print Bill",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        try
        {
            SetBusyState(true, $"Loading invoice for bill {billNumber}...");

            BillingInvoiceDocumentData document = await BillingInvoiceDataService.LoadAsync(_user.Role, billingId, _user.FullName);

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = $"Invoice preview ready for bill {billNumber}.";

            var previewForm = new PrintPreviewForm(
                "Billing Invoice",
                $"Invoice No. {document.BillNumber}",
                new PrintBillInvoice(document),
                $"Billing_Invoice_{document.BillNumber}");

            previewForm.Show(this);
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load invoice preview.";
            MessageBox.Show(this, ex.Message, "Print Bill Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private bool TryGetSelectedBilling(out int billingId, out string billNumber)
    {
        billingId = 0;
        billNumber = string.Empty;

        if (billingGrid.CurrentRow is not { } row || row.IsNewRow)
        {
            return false;
        }

        if (!TryGetIntCellValue(row, out billingId, "billing_id", "Billing_Id"))
        {
            return false;
        }

        billNumber =
            GetStringCellValue(row, "bill_number") ??
            GetStringCellValue(row, "Invoice_Number") ??
            GetStringCellValue(row, "Bill_Number") ??
            billingId.ToString(CultureInfo.InvariantCulture);

        return true;
    }

    private static bool TryGetIntCellValue(DataGridViewRow row, out int value, params string[] columnNames)
    {
        value = 0;

        foreach (string columnName in columnNames)
        {
            if (row.DataGridView?.Columns.Contains(columnName) != true)
            {
                continue;
            }

            object? cellValue = row.Cells[columnName].Value;
            if (cellValue is null || cellValue == DBNull.Value)
            {
                continue;
            }

            if (int.TryParse(Convert.ToString(cellValue, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed))
            {
                value = parsed;
                return true;
            }
        }

        return false;
    }

    private static string? GetStringCellValue(DataGridViewRow row, string columnName)
    {
        if (row.DataGridView?.Columns.Contains(columnName) != true)
        {
            return null;
        }

        return Convert.ToString(row.Cells[columnName].Value, CultureInfo.CurrentCulture);
    }

    private void billingGrid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
        {
            return;
        }

        string columnName = billingGrid.Columns[e.ColumnIndex].Name;
        if (columnName is not ("tax_percent_used" or "discount_percent_used" or "penalty_percent_used"))
        {
            return;
        }

        if (e.Value is null || e.Value == DBNull.Value)
        {
            e.Value = string.Empty;
            e.FormattingApplied = true;
            return;
        }

        if (decimal.TryParse(Convert.ToString(e.Value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsed))
        {
            e.Value = $"{Math.Round(parsed, MidpointRounding.AwayFromZero):N0}%";
            e.FormattingApplied = true;
        }
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
        if (!IsHandleCreated || _isAdjustingPeriodPicker)
        {
            return;
        }

        if (GetSelectedMode() == BillingViewMode.Monthly && NormalizeMonthlyPickerValue())
        {
            _currentPage = 1;
            await LoadBillingAsync();
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

            var previewForm = new PrintPreviewForm(
                document.ReportTitle,
                document.PeriodCaption,
                new BillingReportPrintHelper(document));
            previewForm.Show(this);
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
            NormalizeMonthlyPickerValue();
        }
    }

    private bool NormalizeMonthlyPickerValue()
    {
        DateTime currentValue = periodPicker.Value.Date;
        DateTime normalizedValue = new(currentValue.Year, currentValue.Month, 1);
        if (currentValue == normalizedValue)
        {
            return false;
        }

        _isAdjustingPeriodPicker = true;
        try
        {
            periodPicker.Value = normalizedValue;
        }
        finally
        {
            _isAdjustingPeriodPicker = false;
        }

        return true;
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
