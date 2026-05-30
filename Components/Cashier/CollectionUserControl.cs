using System.Data;
using System.Globalization;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Report;
using WDBS_2026.Services.Cashier;
using WDBS_2026.Services.Printing;
using WDBS_2026.Services.Reports;

namespace WDBS_2026.Components.Cashier;

public partial class CollectionUserControl : UserControl
{
    private const int PageSize = 100;

    private enum CollectionViewMode
    {
        Daily,
        Monthly
    }

    private static readonly Dictionary<string, string> FriendlyHeaderOverrides = new(StringComparer.OrdinalIgnoreCase)
    {
        ["collection_date"] = "Date",
        ["or_number"] = "OR Number",
        ["bill_numbers"] = "Invoice Number",
        ["concessionaire_code"] = "Account No.",
        ["payor_name"] = "Payor",
        ["concessionaire_name"] = "Concessionaire Name",
        ["address"] = "Address",
        ["remarks"] = "Remarks",
        ["total_current_bill"] = "Water Charge",
        ["total_arrears"] = "Arrears",
        ["total_penalty"] = "Penalty",
        ["total_tax"] = "Tax",
        ["total_scf"] = "SCF",
        ["total_others"] = "Others",
        ["amount_received"] = "Amount Received",
        ["change_amount"] = "Change",
        ["total_paid_amount"] = "Collected",
        ["uncollected"] = "Uncollected",
        ["payment_type"] = "Payment Type",
        ["payment_reference"] = "Reference No.",
        ["created_at"] = "Created At",
        ["billing_count"] = "Billing Count",
        ["payment_count"] = "Payment Count",
        ["created_by_full_name"] = "Collected By"
    };

    private readonly AuthenticatedUserDto _user;

    private int _currentPage = 1;
    private int _totalRecords;
    private bool _isAdjustingPeriodPicker;

    public CollectionUserControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        ApplyTheme();
        ConfigureGrid();
    }

    public CollectionUserControl()
        : this(new AuthenticatedUserDto())
    {
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        modeComboBox.SelectedIndex = (int)CollectionViewMode.Monthly;
        periodPicker.Value = DateTime.Today;
        ApplySelectedMode();

        await LoadCollectionsAsync();
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
        collectionGrid.AllowUserToAddRows = false;
        collectionGrid.AllowUserToDeleteRows = false;
        collectionGrid.AllowUserToResizeRows = false;
        collectionGrid.BackgroundColor = AppTheme.SurfaceColor;
        collectionGrid.BorderStyle = BorderStyle.FixedSingle;
        collectionGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        collectionGrid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        collectionGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        collectionGrid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
        collectionGrid.DefaultCellStyle.Font = AppTheme.BodyFont;
        collectionGrid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
        collectionGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
        collectionGrid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
        collectionGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
        collectionGrid.EnableHeadersVisualStyles = false;
        collectionGrid.GridColor = AppTheme.BorderColor;
        collectionGrid.MultiSelect = false;
        collectionGrid.ReadOnly = true;
        collectionGrid.RowHeadersVisible = false;
        collectionGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        collectionGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        collectionGrid.DataBindingComplete += collectionGrid_DataBindingComplete;
    }

    private async Task LoadCollectionsAsync()
    {
        try
        {
            SetBusyState(true, "Loading collections...");

            (DateTime dateFrom, DateTime dateTo) = GetSelectedDateRange();
            string searchTerm = searchTextBox.Text.Trim();

            _totalRecords = await CollectionService.GetCollectionHistoryTotalRecordsAsync(_user.Role, dateFrom, dateTo, searchTerm);

            int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
            _currentPage = Math.Max(1, Math.Min(_currentPage, totalPages));

            int offset = (_currentPage - 1) * PageSize;
            DataTable rows = await CollectionService.GetCollectionHistoryPagedAsync(_user.Role, dateFrom, dateTo, offset, PageSize, searchTerm);

            collectionGrid.DataSource = rows;
            ApplyGridLayout();

            pageInfoLabel.Text = $"Page {_currentPage} of {totalPages}  •  {_totalRecords} record(s)";
            previousPageButton.Enabled = _currentPage > 1;
            nextPageButton.Enabled = _currentPage < totalPages;

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = _totalRecords == 0
                ? "No collection records found for the selected filters."
                : "Collection records loaded successfully.";
        }
        catch (Exception ex)
        {
            collectionGrid.DataSource = null;
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load collection records.";
            MessageBox.Show(this, ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private CollectionReportQueryOptions BuildQueryOptions()
    {
        (DateTime dateFrom, DateTime dateTo) = GetSelectedDateRange();

        return new CollectionReportQueryOptions(
            dateFrom,
            dateTo,
            searchTextBox.Text.Trim());
    }

    private void ApplyGridLayout()
    {
        if (collectionGrid.Columns.Count == 0)
        {
            return;
        }

        foreach (DataGridViewColumn column in collectionGrid.Columns)
        {
            column.HeaderText = GetFriendlyHeader(column.Name);
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Width = Math.Max(column.Width, 120);
        }

        HideColumn("collection_id");
        HideColumn("grand_total");

        SetColumnWidth("collection_date", 130);
        SetColumnWidth("or_number", 105);
        SetColumnWidth("bill_numbers", 180);
        SetColumnWidth("concessionaire_code", 115);
        SetColumnWidth("payor_name", 210);
        SetColumnWidth("concessionaire_name", 220);
        SetColumnWidth("address", 240);
        SetColumnWidth("remarks", 220);
        SetColumnWidth("total_current_bill", 120);
        SetColumnWidth("total_arrears", 120);
        SetColumnWidth("total_penalty", 120);
        SetColumnWidth("total_tax", 120);
        SetColumnWidth("total_scf", 120);
        SetColumnWidth("total_others", 120);
        SetColumnWidth("amount_received", 120);
        SetColumnWidth("change_amount", 120);
        SetColumnWidth("total_paid_amount", 120);
        SetColumnWidth("uncollected", 120);
        SetColumnWidth("payment_type", 120);
        SetColumnWidth("payment_reference", 140);
        SetColumnWidth("created_at", 150);
        SetColumnWidth("billing_count", 110);
        SetColumnWidth("payment_count", 110);
        SetColumnWidth("created_by_full_name", 180);

        if (collectionGrid.Columns["collection_date"] is { } collectionDateColumn)
        {
            collectionDateColumn.DefaultCellStyle.Format = "MMM dd, yyyy";
            collectionDateColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        if (collectionGrid.Columns["created_at"] is { } createdAtColumn)
        {
            createdAtColumn.DefaultCellStyle.Format = "MMM dd, yyyy";
            createdAtColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        foreach (string numericColumn in new[]
                 {
                     "total_current_bill",
                     "total_arrears",
                     "total_penalty",
                     "total_tax",
                     "total_scf",
                     "total_others",
                     "amount_received",
                     "change_amount",
                     "total_paid_amount",
                     "uncollected"
                 })
        {
            if (collectionGrid.Columns[numericColumn] is { } column)
            {
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        foreach (string countColumn in new[] { "billing_count", "payment_count" })
        {
            if (collectionGrid.Columns[countColumn] is { } column)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        if (collectionGrid.Columns["payor_name"] is { } payorColumn)
        {
            payorColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (collectionGrid.Columns["bill_numbers"] is { } billNumbersColumn)
        {
            billNumbersColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (collectionGrid.Columns["address"] is { } addressColumn)
        {
            addressColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        if (collectionGrid.Columns["remarks"] is { } remarksColumn)
        {
            remarksColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        collectionGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        SetColumnDisplayIndex("collection_date", 0);
        SetColumnDisplayIndex("or_number", 1);
        SetColumnDisplayIndex("bill_numbers", 2);
        SetColumnDisplayIndex("concessionaire_code", 3);
        SetColumnDisplayIndex("concessionaire_name", 4);
        SetColumnDisplayIndex("address", 5);
        SetColumnDisplayIndex("total_paid_amount", 6);
        SetColumnDisplayIndex("uncollected", 7);
        SetColumnDisplayIndex("total_current_bill", 8);
        SetColumnDisplayIndex("total_arrears", 9);
        SetColumnDisplayIndex("total_penalty", 10);
        SetColumnDisplayIndex("total_tax", 11);
        SetColumnDisplayIndex("total_scf", 12);
        SetColumnDisplayIndex("total_others", 13);
        SetColumnDisplayIndex("amount_received", 14);
        SetColumnDisplayIndex("change_amount", 15);
        SetColumnDisplayIndex("payment_type", 16);
        SetColumnDisplayIndex("payment_reference", 17);
        SetColumnDisplayIndex("remarks", 18);
        SetColumnDisplayIndex("payor_name", 19);
        SetColumnDisplayIndex("created_at", 20);
        SetColumnDisplayIndex("billing_count", 21);
        SetColumnDisplayIndex("payment_count", 22);
        SetColumnDisplayIndex("created_by_full_name", 23);
    }

    private static string GetFriendlyHeader(string columnName)
    {
        if (FriendlyHeaderOverrides.TryGetValue(columnName, out string? header))
        {
            return header;
        }

        return ToProfessionalTitle(columnName);
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
                "or" => "OR",
                "scf" => "SCF",
                "no" => "No",
                _ => textInfo.ToTitleCase(lower)
            };
        }

        return string.Join(" ", parts);
    }

    private void SetColumnWidth(string columnName, int width)
    {
        if (collectionGrid.Columns[columnName] is { } column)
        {
            column.Width = width;
        }
    }

    private void HideColumn(string columnName)
    {
        if (collectionGrid.Columns[columnName] is { } column)
        {
            column.Visible = false;
        }
    }

    private void SetColumnDisplayIndex(string columnName, int displayIndex)
    {
        if (collectionGrid.Columns[columnName] is { Visible: true } column)
        {
            column.DisplayIndex = Math.Min(displayIndex, collectionGrid.Columns.Count - 1);
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
        collectionGrid.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = busyMessage;
        }
    }

    private async void searchButton_Click(object sender, EventArgs e)
    {
        _currentPage = 1;
        await LoadCollectionsAsync();
    }

    private async void clearButton_Click(object sender, EventArgs e)
    {
        periodPicker.Value = GetSelectedMode() == CollectionViewMode.Monthly
            ? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            : DateTime.Today;

        ApplySelectedMode();
        searchTextBox.Clear();
        _currentPage = 1;
        await LoadCollectionsAsync();
    }

    private async void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        _currentPage = 1;
        await LoadCollectionsAsync();
    }

    private async void previousPageButton_Click(object sender, EventArgs e)
    {
        if (_currentPage <= 1)
        {
            return;
        }

        _currentPage--;
        await LoadCollectionsAsync();
    }

    private async void nextPageButton_Click(object sender, EventArgs e)
    {
        int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
        if (_currentPage >= totalPages)
        {
            return;
        }

        _currentPage++;
        await LoadCollectionsAsync();
    }

    private void collectionGrid_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        collectionGrid.ClearSelection();
    }

    private async void printReportButton_Click(object sender, EventArgs e)
    {
        CollectionReportQueryOptions options = BuildQueryOptions();

        await OpenReportPreviewAsync(BuildReportTitle(), BuildPeriodCaption(), options);
    }

    private async Task OpenReportPreviewAsync(string reportTitle, string periodCaption, CollectionReportQueryOptions options)
    {
        int totalRecords = 0;
        DataTable reportRows = new();

        try
        {
            ReportOperationProgressForm.Run(
                this,
                "Loading Collection Report",
                "Counting collection records...",
                async progress =>
                {
                    progress.Report(new ReportOperationProgress(5, "Counting collection records..."));
                    totalRecords = await CollectionReportService.GetTotalRecordsAsync(_user.Role, options);

                    if (totalRecords == 0)
                    {
                        progress.Report(new ReportOperationProgress(100, "No collection records found for the selected filters."));
                        reportRows = new DataTable();
                        return;
                    }

                    progress.Report(new ReportOperationProgress(8, $"Found {totalRecords} collection record(s)."));
                    reportRows = await CollectionReportService.GetReportRowsWithProgressAsync(_user.Role, options, totalRecords, progress);
                });
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load collection report.";
            MessageBox.Show(this, ex.Message, "Collection Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (totalRecords == 0 || reportRows.Rows.Count == 0)
        {
            MessageBox.Show(this, "There are no collection records for the selected filters to print.", "Collection Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        await LoadCollectionsAsync();
    }

    private async void periodPicker_ValueChanged(object sender, EventArgs e)
    {
        if (!IsHandleCreated || _isAdjustingPeriodPicker)
        {
            return;
        }

        if (GetSelectedMode() == CollectionViewMode.Monthly && NormalizeMonthlyPickerValue())
        {
            _currentPage = 1;
            await LoadCollectionsAsync();
            return;
        }

        _currentPage = 1;
        await LoadCollectionsAsync();
    }

    private void OpenReportPreview(string reportTitle, string periodCaption, DataTable rows)
    {
        try
        {
            var document = new CollectionReportDocumentData(
                reportTitle,
                periodCaption,
                _user.FullName,
                DateTime.Now,
                rows);

            var previewForm = new PrintPreviewForm(
                document.ReportTitle,
                document.PeriodCaption,
                new CollectionReportPrintHelper(document));
            previewForm.Show(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Collection Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplySelectedMode()
    {
        bool isDaily = GetSelectedMode() == CollectionViewMode.Daily;
        periodLabel.Text = isDaily ? "Collection Date" : "Collection Month";
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

    private CollectionViewMode GetSelectedMode()
    {
        return modeComboBox.SelectedIndex == (int)CollectionViewMode.Daily
            ? CollectionViewMode.Daily
            : CollectionViewMode.Monthly;
    }

    private (DateTime DateFrom, DateTime DateTo) GetSelectedDateRange()
    {
        DateTime selectedDate = periodPicker.Value.Date;
        return GetSelectedMode() switch
        {
            CollectionViewMode.Daily => (selectedDate, selectedDate),
            CollectionViewMode.Monthly => (new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1)),
            _ => (selectedDate, selectedDate)
        };
    }

    private string BuildReportTitle()
    {
        return GetSelectedMode() == CollectionViewMode.Daily ? "Daily Collection Report" : "Monthly Collection Report";
    }

    private string BuildPeriodCaption()
    {
        DateTime selectedDate = periodPicker.Value.Date;
        return GetSelectedMode() == CollectionViewMode.Daily
            ? $"Collection Date: {selectedDate:MMMM dd, yyyy}"
            : $"Collection Month: {selectedDate:MMMM yyyy}";
    }

    private void filterLayout_Paint(object sender, PaintEventArgs e)
    {
    }
}
