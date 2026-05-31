using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Pickers;
using WDBS_2026.Forms.Report;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Components.Biller;

public partial class MeterReadingUserControl : UserControl
{
    private const string PenalizeAfterDaysKey = "penalize_after_days";
    private const int DefaultPenalizeAfterDays = 14;

    private static readonly Dictionary<string, string> FriendlyHeaderOverrides = new(StringComparer.OrdinalIgnoreCase)
    {
        ["row_number"] = "#",
        ["is_billed_today"] = "Billed This Month",
        ["concessionaire_code"] = "Account No",
        ["concessionaire_name"] = "Concessionaire Name",
        ["meter_number"] = "Meter No",
        ["previous_reading_date"] = "Previous Reading Date",
        ["current_reading_date"] = "Current Reading Date",
        ["previous_reading"] = "Previous Reading",
        ["present_reading"] = "Present Reading",
        ["free_water"] = "Free Water",
        ["bill_number"] = "Bill Number",
        ["is_initial"] = "Initial",
        ["scf_amount"] = "SCF",
        ["scf_status"] = "SCF Status",
        ["total_water_bill"] = "Total Water Bill",
        ["remaining_balance"] = "Remaining Balance",
        ["is_tax_exempt"] = "Tax Exempt",
        ["is_due_exempt"] = "Due Exempt",
        ["is_discounted"] = "Discounted",
        ["meter_no"] = "Meter No"
    };

    private readonly AuthenticatedUserDto _user;
    private readonly HashSet<string> _numericOnlyColumns = new(StringComparer.OrdinalIgnoreCase)
    {
        "present_reading",
        "free_water",
        "bill_number"
    };

    private readonly ContextMenuStrip _generatedBillsRowMenu = new();
    private readonly ToolStripMenuItem _deleteGeneratedBillMenuItem = new("Void Bill");

    private bool _isInitializing;
    private bool _isCompactLayout;
    private bool _layoutsConfigured;
    private bool _isSynchronizingDueDate;
    private int _penalizeAfterDays = DefaultPenalizeAfterDays;

    public MeterReadingUserControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        ConfigureGrids();
        ConfigureGeneratedBillsContextMenu();
        ApplyTheme();
        SizeChanged += MeterReadingUserControl_SizeChanged;
        UpdateResponsiveLayout();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        _isInitializing = true;

        try
        {
            readingDatePicker.Value = DateTime.Today;
            generatedBillsDatePicker.Value = DateTime.Today;

            SetBillingBusyState(true, "Loading zones and billing defaults...");
            SetGeneratedBillsBusyState(true, "Loading generated bills...");

            DBConfig.SetConnectionString(_user.Role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            await LoadZonesAsync(connection);
            _penalizeAfterDays = await GetPenalizeAfterDaysAsync(connection);
            RecalculateDueDate();

            if (zoneComboBox.Items.Count > 0 && zoneComboBox.SelectedIndex < 0)
            {
                zoneComboBox.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            leftStatusLabel.ForeColor = AppTheme.DangerColor;
            leftStatusLabel.Text = "Failed to initialize meter reading.";
            rightStatusLabel.ForeColor = AppTheme.DangerColor;
            rightStatusLabel.Text = "Failed to initialize generated bills.";
            MessageBox.Show(this, ex.Message, "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _isInitializing = false;
            SetBillingBusyState(false);
            SetGeneratedBillsBusyState(false);
        }

        await LoadMeterReadingsAsync();
        await LoadGeneratedBillsAsync();
    }

    private void ConfigureGrids()
    {
        ConfigureGridBase(meterReadingGrid, allowEditing: true, DataGridViewSelectionMode.CellSelect);
        ConfigureGridBase(generatedBillsGrid, allowEditing: false, DataGridViewSelectionMode.FullRowSelect);

        meterReadingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        meterReadingGrid.EditMode = DataGridViewEditMode.EditOnEnter;
        meterReadingGrid.EditingControlShowing += meterReadingGrid_EditingControlShowing;
        meterReadingGrid.DataBindingComplete += meterReadingGrid_DataBindingComplete;

        generatedBillsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        generatedBillsGrid.DataBindingComplete += generatedBillsGrid_DataBindingComplete;
    }

    private static void ConfigureGridBase(DataGridView grid, bool allowEditing, DataGridViewSelectionMode selectionMode)
    {
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.MultiSelect = false;
        grid.ReadOnly = !allowEditing;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = selectionMode;
        grid.EnableHeadersVisualStyles = false;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.BackgroundColor = AppTheme.SurfaceColor;
        grid.GridColor = AppTheme.BorderColor;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
        grid.DefaultCellStyle.Font = AppTheme.BodyFont;
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
        grid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;

        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(subtitleLabel);

        AppTheme.ApplyCard(leftCardPanel);
        AppTheme.ApplyCard(rightCardPanel);

        AppTheme.ApplyCardTitle(createBillsTitleLabel);
        AppTheme.ApplyCardBody(createBillsSubtitleLabel);
        AppTheme.ApplyCardTitle(generatedBillsTitleLabel);
        AppTheme.ApplyCardBody(generatedBillsSubtitleLabel);
        AppTheme.ApplySubtitle(leftStatusLabel);
        AppTheme.ApplySubtitle(rightStatusLabel);

        foreach (Label label in new[] { zoneLabel, readingDateLabel, dueDateLabel, generatedBillsDateLabel })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        zoneComboBox.Font = AppTheme.BodyFont;
        zoneComboBox.ForeColor = AppTheme.BodyTextColor;

        foreach (DateTimePicker picker in new[] { readingDatePicker, dueDatePicker, generatedBillsDatePicker })
        {
            picker.Font = AppTheme.BodyFont;
            picker.CalendarForeColor = AppTheme.BodyTextColor;
        }

        AppTheme.ApplyInput(meterSearchTextBox);
        AppTheme.ApplyInput(generatedBillsSearchTextBox);

        AppTheme.ApplySeverityButton(refreshMeterReadingsButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(generateBillsButton, ButtonSeverity.Success);
        AppTheme.ApplySeverityButton(printLatestReadingSheetButton, ButtonSeverity.Info);
        AppTheme.ApplySeverityButton(refreshGeneratedBillsButton, ButtonSeverity.Neutral);
    }

    private void ConfigureGeneratedBillsContextMenu()
    {
        _generatedBillsRowMenu.Items.Add(_deleteGeneratedBillMenuItem);
        _deleteGeneratedBillMenuItem.Click += deleteGeneratedBillMenuItem_Click;
        generatedBillsGrid.CellMouseDown += generatedBillsGrid_CellMouseDown;
    }

    private void MeterReadingUserControl_SizeChanged(object? sender, EventArgs e)
    {
        UpdateResponsiveLayout();
    }

    private void UpdateResponsiveLayout()
    {
        bool compactLayout = ClientSize.Width < 860;

        if (!_layoutsConfigured || _isCompactLayout != compactLayout)
        {
            ConfigureMeterEntryFiltersLayout(compactLayout);
            ConfigureGeneratedBillsFilterLayout(compactLayout);
            _isCompactLayout = compactLayout;
        }

        _layoutsConfigured = true;
        UpdateContentLayout();
    }

    private void ConfigureMeterEntryFiltersLayout(bool compactLayout)
    {
        meterEntryFiltersLayout.SuspendLayout();
        meterEntryFiltersLayout.Controls.Clear();
        meterEntryFiltersLayout.ColumnStyles.Clear();
        meterEntryFiltersLayout.RowStyles.Clear();

        if (compactLayout)
        {
            meterEntryFiltersLayout.ColumnCount = 1;
            meterEntryFiltersLayout.RowCount = 10;
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int index = 0; index < 10; index++)
            {
                meterEntryFiltersLayout.RowStyles.Add(new RowStyle());
            }

            zoneComboBox.Dock = DockStyle.Top;
            readingDatePicker.Dock = DockStyle.Top;
            dueDatePicker.Dock = DockStyle.Top;
            meterSearchTextBox.Dock = DockStyle.Top;
            refreshMeterReadingsButton.Dock = DockStyle.Fill;
            generateBillsButton.Dock = DockStyle.Fill;
            printLatestReadingSheetButton.Dock = DockStyle.Fill;

            zoneLabel.Margin = new Padding(0);
            zoneComboBox.Margin = new Padding(0, 6, 0, 0);
            readingDateLabel.Margin = new Padding(0, 12, 0, 0);
            readingDatePicker.Margin = new Padding(0, 6, 0, 0);
            dueDateLabel.Margin = new Padding(0, 12, 0, 0);
            dueDatePicker.Margin = new Padding(0, 6, 0, 0);
            meterSearchTextBox.Margin = new Padding(0, 12, 0, 0);
            refreshMeterReadingsButton.Margin = new Padding(0, 12, 0, 0);
            generateBillsButton.Margin = new Padding(0, 8, 0, 0);
            printLatestReadingSheetButton.Margin = new Padding(0, 8, 0, 0);

            meterEntryFiltersLayout.Controls.Add(zoneLabel, 0, 0);
            meterEntryFiltersLayout.Controls.Add(zoneComboBox, 0, 1);
            meterEntryFiltersLayout.Controls.Add(readingDateLabel, 0, 2);
            meterEntryFiltersLayout.Controls.Add(readingDatePicker, 0, 3);
            meterEntryFiltersLayout.Controls.Add(dueDateLabel, 0, 4);
            meterEntryFiltersLayout.Controls.Add(dueDatePicker, 0, 5);
            meterEntryFiltersLayout.Controls.Add(meterSearchTextBox, 0, 6);
            meterEntryFiltersLayout.Controls.Add(refreshMeterReadingsButton, 0, 7);
            meterEntryFiltersLayout.Controls.Add(generateBillsButton, 0, 8);
            meterEntryFiltersLayout.Controls.Add(printLatestReadingSheetButton, 0, 9);
        }
        else
        {
            meterEntryFiltersLayout.ColumnCount = 10;
            meterEntryFiltersLayout.RowCount = 1;
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
            meterEntryFiltersLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            zoneComboBox.Dock = DockStyle.None;
            readingDatePicker.Dock = DockStyle.None;
            dueDatePicker.Dock = DockStyle.None;
            meterSearchTextBox.Dock = DockStyle.Fill;
            refreshMeterReadingsButton.Dock = DockStyle.None;
            generateBillsButton.Dock = DockStyle.None;
            printLatestReadingSheetButton.Dock = DockStyle.None;

            zoneLabel.Margin = new Padding(0);
            zoneComboBox.Margin = new Padding(6, 3, 16, 3);
            readingDateLabel.Margin = new Padding(0);
            readingDatePicker.Margin = new Padding(6, 3, 16, 3);
            dueDateLabel.Margin = new Padding(0);
            dueDatePicker.Margin = new Padding(6, 3, 16, 3);
            meterSearchTextBox.Margin = new Padding(0, 3, 12, 3);
            refreshMeterReadingsButton.Margin = new Padding(0, 0, 8, 0);
            generateBillsButton.Margin = new Padding(0, 0, 8, 0);
            printLatestReadingSheetButton.Margin = new Padding(0);

            meterEntryFiltersLayout.Controls.Add(zoneLabel, 0, 0);
            meterEntryFiltersLayout.Controls.Add(zoneComboBox, 1, 0);
            meterEntryFiltersLayout.Controls.Add(readingDateLabel, 2, 0);
            meterEntryFiltersLayout.Controls.Add(readingDatePicker, 3, 0);
            meterEntryFiltersLayout.Controls.Add(dueDateLabel, 4, 0);
            meterEntryFiltersLayout.Controls.Add(dueDatePicker, 5, 0);
            meterEntryFiltersLayout.Controls.Add(meterSearchTextBox, 6, 0);
            meterEntryFiltersLayout.Controls.Add(refreshMeterReadingsButton, 7, 0);
            meterEntryFiltersLayout.Controls.Add(generateBillsButton, 8, 0);
            meterEntryFiltersLayout.Controls.Add(printLatestReadingSheetButton, 9, 0);
        }

        meterEntryFiltersLayout.ResumeLayout();
        meterEntryFiltersLayout.PerformLayout();
    }

    private void ConfigureGeneratedBillsFilterLayout(bool compactLayout)
    {
        generatedBillsFilterLayout.SuspendLayout();
        generatedBillsFilterLayout.Controls.Clear();
        generatedBillsFilterLayout.ColumnStyles.Clear();
        generatedBillsFilterLayout.RowStyles.Clear();

        if (compactLayout)
        {
            generatedBillsFilterLayout.ColumnCount = 1;
            generatedBillsFilterLayout.RowCount = 4;
            generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int index = 0; index < 4; index++)
            {
                generatedBillsFilterLayout.RowStyles.Add(new RowStyle());
            }

            generatedBillsDateLabel.Margin = new Padding(0);
            generatedBillsDatePicker.Margin = new Padding(0, 6, 0, 0);
            generatedBillsSearchTextBox.Margin = new Padding(0, 12, 0, 0);
            refreshGeneratedBillsButton.Margin = new Padding(0, 12, 0, 0);

            generatedBillsSearchTextBox.Dock = DockStyle.Top;

            generatedBillsFilterLayout.Controls.Add(generatedBillsDateLabel, 0, 0);
            generatedBillsFilterLayout.Controls.Add(generatedBillsDatePicker, 0, 1);
            generatedBillsFilterLayout.Controls.Add(generatedBillsSearchTextBox, 0, 2);
            generatedBillsFilterLayout.Controls.Add(refreshGeneratedBillsButton, 0, 3);
        }
        else
        {
            generatedBillsFilterLayout.ColumnCount = 4;
            generatedBillsFilterLayout.RowCount = 1;
            generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle());
            generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle());
            generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle());
            generatedBillsFilterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            generatedBillsDateLabel.Margin = new Padding(0);
            generatedBillsDatePicker.Margin = new Padding(3);
            generatedBillsSearchTextBox.Margin = new Padding(12, 3, 12, 3);
            refreshGeneratedBillsButton.Margin = new Padding(0);

            generatedBillsSearchTextBox.Dock = DockStyle.Fill;

            generatedBillsFilterLayout.Controls.Add(generatedBillsDateLabel, 0, 0);
            generatedBillsFilterLayout.Controls.Add(generatedBillsDatePicker, 1, 0);
            generatedBillsFilterLayout.Controls.Add(generatedBillsSearchTextBox, 2, 0);
            generatedBillsFilterLayout.Controls.Add(refreshGeneratedBillsButton, 3, 0);
        }

        generatedBillsFilterLayout.ResumeLayout();
        generatedBillsFilterLayout.PerformLayout();
    }

    private void UpdateContentLayout()
    {
        if (contentSplitContainer.Height <= 0)
        {
            return;
        }

        contentSplitContainer.Orientation = Orientation.Horizontal;
        contentSplitContainer.Panel1MinSize = 280;
        contentSplitContainer.Panel2MinSize = 220;

        int preferredTopHeight = ClientSize.Height < 760
            ? (int)Math.Round(contentSplitContainer.Height * 0.58)
            : (int)Math.Round(contentSplitContainer.Height * 0.52);
        int maxTopHeight = Math.Max(contentSplitContainer.Panel1MinSize, contentSplitContainer.Height - contentSplitContainer.Panel2MinSize - contentSplitContainer.SplitterWidth);

        if (maxTopHeight < contentSplitContainer.Panel1MinSize)
        {
            return;
        }

        contentSplitContainer.SplitterDistance = Math.Min(Math.Max(preferredTopHeight, contentSplitContainer.Panel1MinSize), maxTopHeight);
    }

    private async Task LoadZonesAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT zone_id, zone_name
FROM zone
ORDER BY zone_name;";

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();

        var zones = new List<LookupItem>();
        while (await reader.ReadAsync())
        {
            zones.Add(new LookupItem(reader.GetInt32("zone_id"), reader.GetString("zone_name")));
        }

        zoneComboBox.DataSource = zones;
        zoneComboBox.DisplayMember = nameof(LookupItem.Name);
        zoneComboBox.ValueMember = nameof(LookupItem.Id);
    }

    private static async Task<int> GetPenalizeAfterDaysAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT settings_value
FROM system_settings
WHERE settings_key = @settingsKey
LIMIT 1;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@settingsKey", PenalizeAfterDaysKey);

        object? result = await command.ExecuteScalarAsync();
        if (result is null || result == DBNull.Value)
        {
            return DefaultPenalizeAfterDays;
        }

        string? rawValue = Convert.ToString(result, CultureInfo.InvariantCulture);
        return int.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedValue) && parsedValue >= 0
            ? parsedValue
            : DefaultPenalizeAfterDays;
    }

    private async Task LoadMeterReadingsAsync()
    {
        if (zoneComboBox.SelectedItem is not LookupItem zone)
        {
            meterReadingGrid.DataSource = null;
            leftStatusLabel.ForeColor = AppTheme.WarningColor;
            leftStatusLabel.Text = "Select a zone to load meter reading entries.";
            return;
        }

        try
        {
            string searchTerm = meterSearchTextBox.Text.Trim();
            string loadScope = string.IsNullOrWhiteSpace(searchTerm)
                ? zone.Name
                : $"{zone.Name} (search: {searchTerm})";

            SetBillingBusyState(true, $"Loading meter reading rows for {loadScope}...");

            DBConfig.SetConnectionString(_user.Role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            DataTable table = await GetMeterReadingRowsAsync(connection, zone.Id, searchTerm);
            long nextBillNumber = await GetNextBillNumberAsync(connection);
            PrepareMeterReadingTable(table, nextBillNumber);

            meterReadingGrid.DataSource = table;

            leftStatusLabel.ForeColor = AppTheme.MutedTextColor;
            leftStatusLabel.Text = table.Rows.Count == 0
                ? (string.IsNullOrWhiteSpace(searchTerm)
                    ? $"No active concessionaires were found for {zone.Name}."
                    : $"No concessionaires matched \"{searchTerm}\" in {zone.Name}.")
                : $"Loaded {table.Rows.Count} account(s) for {loadScope}. Green rows are already billed this month and remain locked.";
        }
        catch (Exception ex)
        {
            meterReadingGrid.DataSource = null;
            leftStatusLabel.ForeColor = AppTheme.DangerColor;
            leftStatusLabel.Text = "Failed to load meter reading rows.";
            MessageBox.Show(this, ex.Message, "Meter Reading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBillingBusyState(false);
        }
    }

    private static async Task<DataTable> GetMeterReadingRowsAsync(MySqlConnection connection, int zoneId, string searchTerm)
    {
        const string sql = @"
SELECT
    v.Is_Billed_Today,
    v.Concessionaire_Id,
    v.Concessionaire_Code,
    v.Concessionaire_Name,
    v.Meter_Number,
    v.Previous_Reading_Date,
    v.Previous_Reading
FROM v_input_meter_reading v
WHERE v.Zone = @zoneId
  AND (@search = '' OR v.Concessionaire_Code LIKE @searchLike OR v.Concessionaire_Name LIKE @searchLike)
ORDER BY v.Concessionaire_Code ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@zoneId", zoneId);
        command.Parameters.AddWithValue("@search", searchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{searchTerm}%");

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private static async Task<DataTable> GetLatestReadingSheetRowsAsync(MySqlConnection connection, int zoneId)
    {
        const string sql = @"
SELECT
    v.Zone,
    v.Concessionaire_Code,
    v.Concessionaire_Name,
    v.Meter_Number,
    v.Previous_Reading_Date,
    v.Previous_Reading,
    v.Present_Reading
FROM v_meter_reading_sheet v
WHERE v.Zone = @zoneId
ORDER BY v.Concessionaire_Code ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@zoneId", zoneId);

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private static async Task<long> GetNextBillNumberAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT COALESCE(MAX(CAST(bill_number AS UNSIGNED)), 0)
FROM billing;";

        await using var command = new MySqlCommand(sql, connection);
        object? result = await command.ExecuteScalarAsync();
        long currentMax = result is null || result == DBNull.Value
            ? 0
            : Convert.ToInt64(result, CultureInfo.InvariantCulture);

        return currentMax + 1;
    }

    private static void PrepareMeterReadingTable(DataTable table, long startingBillNumber)
    {
        DataColumn rowNumberColumn = EnsureColumn(table, "row_number", typeof(int), 0, allowNull: false);
        DataColumn presentReadingColumn = EnsureColumn(table, "present_reading", typeof(int), DBNull.Value, allowNull: true);
        DataColumn freeWaterColumn = EnsureColumn(table, "free_water", typeof(int), 0, allowNull: true);
        DataColumn billNumberColumn = EnsureColumn(table, "bill_number", typeof(string), string.Empty, allowNull: true);
        DataColumn isInitialColumn = EnsureColumn(table, "is_initial", typeof(bool), false, allowNull: false);
        DataColumn previousReadingColumn = table.Columns["Previous_Reading"]!;

        rowNumberColumn.SetOrdinal(0);
        presentReadingColumn.SetOrdinal(previousReadingColumn.Ordinal + 1);
        freeWaterColumn.SetOrdinal(presentReadingColumn.Ordinal + 1);
        billNumberColumn.SetOrdinal(freeWaterColumn.Ordinal + 1);
        isInitialColumn.SetOrdinal(billNumberColumn.Ordinal + 1);

        foreach (DataRow row in table.Rows)
        {
            row["row_number"] = table.Rows.IndexOf(row) + 1;

            bool isBilledToday = GetBooleanValue(row["Is_Billed_Today"]);
            row["free_water"] = TryGetIntValue(row["free_water"], out int freeWater) ? Math.Max(freeWater, 0) : 0;
            row["is_initial"] = GetBooleanValue(row["is_initial"]);

            if (isBilledToday)
            {
                row["bill_number"] = string.Empty;
                continue;
            }

            row["bill_number"] = startingBillNumber.ToString(CultureInfo.InvariantCulture);
            startingBillNumber++;
        }
    }

    private static DataColumn EnsureColumn(DataTable table, string columnName, Type type, object defaultValue, bool allowNull)
    {
        if (table.Columns.Contains(columnName))
        {
            return table.Columns[columnName]!;
        }

        var column = new DataColumn(columnName, type)
        {
            AllowDBNull = allowNull,
            DefaultValue = defaultValue
        };

        table.Columns.Add(column);
        return column;
    }

    private async Task LoadGeneratedBillsAsync()
    {
        try
        {
            string selectedDate = generatedBillsDatePicker.Value.ToString("MMMM dd, yyyy", CultureInfo.CurrentCulture);
            string searchTerm = generatedBillsSearchTextBox.Text.Trim();
            SetGeneratedBillsBusyState(true, $"Loading generated bills for {selectedDate}...");

            DBConfig.SetConnectionString(_user.Role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            DataTable table = await GetGeneratedBillsRowsAsync(connection, generatedBillsDatePicker.Value.Date, searchTerm);
            generatedBillsGrid.DataSource = table;

            rightStatusLabel.ForeColor = AppTheme.MutedTextColor;
            rightStatusLabel.Text = table.Rows.Count == 0
                ? (string.IsNullOrWhiteSpace(searchTerm)
                    ? $"No generated bills were found for {selectedDate}."
                    : $"No generated bills matched \"{searchTerm}\" for {selectedDate}.")
                : (string.IsNullOrWhiteSpace(searchTerm)
                    ? $"Showing {table.Rows.Count} generated bill(s) for {selectedDate}. Select a row to delete when needed."
                    : $"Showing {table.Rows.Count} generated bill(s) matching \"{searchTerm}\" for {selectedDate}.");
        }
        catch (Exception ex)
        {
            generatedBillsGrid.DataSource = null;
            rightStatusLabel.ForeColor = AppTheme.DangerColor;
            rightStatusLabel.Text = "Failed to load generated bills.";
            MessageBox.Show(this, ex.Message, "Generated Bills Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetGeneratedBillsBusyState(false);
        }
    }

    private static async Task<DataTable> GetGeneratedBillsRowsAsync(MySqlConnection connection, DateTime billingDate, string searchTerm)
    {
        const string sql = @"
SELECT
    billing_id,
    bill_number,
    concessionaire_code,
    concessionaire_name,
    address,
    previous_reading_date,
    current_reading_date,
    previous_reading,
    present_reading,
    consumption,
    free_water,
    water_charge,
    discount_amount,
    tax_amount,
    total_water_bill,
    scf_amount,
    arrears_amount,
    penalty_amount,
    total_amount,
    remaining_balance,
    billing_date,
    due_date,
    status,
    scf_status,
    is_initial
FROM v_billing_details
WHERE billing_date = @billingDate
    AND (
            @search = ''
            OR CAST(bill_number AS CHAR) LIKE @searchLike
            OR concessionaire_code LIKE @searchLike
            OR concessionaire_name LIKE @searchLike
    )
ORDER BY created_at DESC, bill_number DESC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@billingDate", billingDate);
                command.Parameters.AddWithValue("@search", searchTerm);
                command.Parameters.AddWithValue("@searchLike", $"%{searchTerm}%");

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private async Task GenerateBillsAsync()
    {
        if (zoneComboBox.SelectedItem is not LookupItem zone)
        {
            leftStatusLabel.ForeColor = AppTheme.WarningColor;
            leftStatusLabel.Text = "Select a zone before generating bills.";
            return;
        }

        List<BillJob> jobs = BuildBillJobs(out int skippedCount, out List<string> skippedMessages);
        if (jobs.Count == 0)
        {
            string skipReason = skippedMessages.Count > 0 ? Environment.NewLine + string.Join(Environment.NewLine, skippedMessages.Take(5)) : string.Empty;
            MessageBox.Show(this, "No valid rows are ready for billing." + skipReason, "Nothing to Generate", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            this,
            $"Generate {jobs.Count} bill(s) for {zone.Name} on {readingDatePicker.Value:MMMM dd, yyyy}?\nSkipped rows: {skippedCount}",
            "Confirm Billing Generation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
        {
            return;
        }

        int successCount = 0;
        int failureCount = 0;
        var failures = new List<string>();

        try
        {
            SetBillingBusyState(true, "Generating bills...");

            DBConfig.SetConnectionString(_user.Role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            await UpsertPenalizeAfterDaysAsync(connection, GetSelectedPenalizeAfterDays());

            foreach (BillJob job in jobs)
            {
                try
                {
                    await ExecuteCreateBillProcedureAsync(connection, job);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failureCount++;
                    failures.Add($"{job.AccountNo} - {job.ConcessionaireName}: {ex.Message}");
                }
            }

            leftStatusLabel.ForeColor = failureCount == 0 ? AppTheme.SuccessColor : AppTheme.WarningColor;
            leftStatusLabel.Text = failureCount == 0
                ? $"Generated {successCount} bill(s) successfully."
                : $"Generated {successCount} bill(s). {failureCount} row(s) failed.";
        }
        catch (Exception ex)
        {
            leftStatusLabel.ForeColor = AppTheme.DangerColor;
            leftStatusLabel.Text = "Failed to generate bills.";
            MessageBox.Show(this, ex.Message, "Billing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        finally
        {
            SetBillingBusyState(false);
        }

        generatedBillsDatePicker.Value = readingDatePicker.Value.Date;
        await LoadMeterReadingsAsync();
        await LoadGeneratedBillsAsync();

        if (failureCount > 0 || skippedCount > 0)
        {
            string failureText = failures.Count == 0 ? string.Empty : Environment.NewLine + string.Join(Environment.NewLine, failures.Take(8));
            string skippedText = skippedMessages.Count == 0 ? string.Empty : Environment.NewLine + string.Join(Environment.NewLine, skippedMessages.Take(8));

            MessageBox.Show(
                this,
                $"Generated: {successCount}\nFailed: {failureCount}\nSkipped: {skippedCount}{failureText}{skippedText}",
                "Billing Generation Result",
                MessageBoxButtons.OK,
                failureCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }
    }

    private async Task DeleteSelectedGeneratedBillAsync()
    {
        if (!TryGetSelectedGeneratedBill(out int billingId, out string billNumber))
        {
            MessageBox.Show(
                this,
                "Select a generated bill first.",
                "Void Bill",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            this,
            $"Void bill {billNumber}?\n\nWarning: This action cannot be undone.",
            "Confirm Void Bill",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);

        if (confirmation != DialogResult.Yes)
        {
            return;
        }

        if (!VoidReasonPromptDialog.TryGetReason(
                this,
                "Void Bill",
                $"Enter reason for voiding bill {billNumber}:",
                out string voidReason))
        {
            return;
        }

        try
        {
            SetGeneratedBillsBusyState(true, $"Voiding bill {billNumber}...");

            DBConfig.SetConnectionString(_user.Role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            await ExecuteDeleteGeneratedBillAsync(connection, billingId, voidReason);

            rightStatusLabel.ForeColor = AppTheme.SuccessColor;
            rightStatusLabel.Text = $"Bill {billNumber} was voided successfully.";
        }
        catch (Exception ex)
        {
            rightStatusLabel.ForeColor = AppTheme.DangerColor;
            rightStatusLabel.Text = "Failed to void the selected bill.";
            MessageBox.Show(this, ex.Message, "Void Bill Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        finally
        {
            SetGeneratedBillsBusyState(false);
        }

        await LoadGeneratedBillsAsync();
        await LoadMeterReadingsAsync();
    }

    private async Task ExecuteDeleteGeneratedBillAsync(MySqlConnection connection, int billingId, string voidReason)
    {
        const string sql = @"
CALL sp_void_billing_v1(
    @billingId,
    @voidedByUserId,
    @voidRemarks
);";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@billingId", billingId);
        command.Parameters.AddWithValue("@voidedByUserId", _user.UserId);
        command.Parameters.AddWithValue("@voidRemarks", voidReason);

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            throw new InvalidOperationException("The selected bill could not be voided.");
        }
    }

    private bool TryGetSelectedGeneratedBill(out int billingId, out string billNumber)
    {
        billingId = 0;
        billNumber = string.Empty;

        if (generatedBillsGrid.CurrentRow is not { } row || row.IsNewRow)
        {
            return false;
        }

        if (!TryGetIntCellValue(row, "billing_id", out billingId))
        {
            return false;
        }

        billNumber = GetStringCellValue(row, "bill_number");
        return true;
    }

    private List<BillJob> BuildBillJobs(out int skippedCount, out List<string> skippedMessages)
    {
        var jobs = new List<BillJob>();
        skippedMessages = new List<string>();
        skippedCount = 0;
        var seenBillNumbers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (DataGridViewRow row in meterReadingGrid.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            string accountNo = GetStringCellValue(row, "Concessionaire_Code");
            string concessionaireName = GetStringCellValue(row, "Concessionaire_Name");
            bool isBilledToday = GetBooleanCellValue(row, "Is_Billed_Today");
            if (isBilledToday)
            {
                skippedCount++;
                continue;
            }

            if (!TryGetIntCellValue(row, "Concessionaire_Id", out int concessionaireId))
            {
                skippedCount++;
                skippedMessages.Add($"Skipped {accountNo}: missing concessionaire ID.");
                continue;
            }

            if (!TryGetIntCellValue(row, "Present_Reading", out int presentReading))
            {
                skippedCount++;
                skippedMessages.Add($"Skipped {accountNo}: enter a present reading.");
                continue;
            }

            _ = TryGetIntCellValue(row, "Previous_Reading", out int previousReading);
            if (presentReading < previousReading)
            {
                skippedCount++;
                skippedMessages.Add($"Skipped {accountNo}: present reading cannot be lower than previous reading.");
                continue;
            }

            int freeWater = TryGetIntCellValue(row, "Free_Water", out int parsedFreeWater) ? Math.Max(parsedFreeWater, 0) : 0;
            string billNumber = GetStringCellValue(row, "Bill_Number");
            if (string.IsNullOrWhiteSpace(billNumber))
            {
                skippedCount++;
                skippedMessages.Add($"Skipped {accountNo}: bill number is required.");
                continue;
            }

            if (!seenBillNumbers.Add(billNumber))
            {
                skippedCount++;
                skippedMessages.Add($"Skipped {accountNo}: duplicate bill number {billNumber} in the grid.");
                continue;
            }

            jobs.Add(new BillJob(
                concessionaireId,
                accountNo,
                concessionaireName,
                presentReading,
                billNumber,
                freeWater,
                GetBooleanCellValue(row, "Is_Initial") ? 1 : 0));
        }

        return jobs;
    }

    private async Task ExecuteCreateBillProcedureAsync(MySqlConnection connection, BillJob job)
    {
        const string sql = @"
CALL sp_create_bill_v46(
    @concessionaireId,
    @presentReading,
    @billNumber,
    @freeWater,
    @requestId,
    @userId,
    @forceInitial,
    @billDate
);";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@concessionaireId", job.ConcessionaireId);
        command.Parameters.AddWithValue("@presentReading", job.PresentReading);
        command.Parameters.AddWithValue("@billNumber", job.BillNumber);
        command.Parameters.AddWithValue("@freeWater", job.FreeWater);
        command.Parameters.AddWithValue("@requestId", BuildRequestId(job));
        command.Parameters.AddWithValue("@userId", _user.UserId);
        command.Parameters.AddWithValue("@forceInitial", job.ForceInitial);
        command.Parameters.AddWithValue("@billDate", readingDatePicker.Value.Date);

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return;
        }

        int isDuplicateRequest = reader.IsDBNull(reader.GetOrdinal("is_duplicate_request"))
            ? 0
            : Convert.ToInt32(reader["is_duplicate_request"], CultureInfo.InvariantCulture);

        if (isDuplicateRequest == 1)
        {
            return;
        }
    }

    private string BuildRequestId(BillJob job)
    {
        return $"MR-{readingDatePicker.Value:yyyyMMdd}-{_user.UserId}-{job.ConcessionaireId}-{Guid.NewGuid():N}";
    }

    private static async Task UpsertPenalizeAfterDaysAsync(MySqlConnection connection, int days)
    {
        const string sql = @"
INSERT INTO system_settings (settings_key, settings_value)
VALUES (@settingsKey, @settingsValue)
ON DUPLICATE KEY UPDATE settings_value = @settingsValue;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@settingsKey", PenalizeAfterDaysKey);
        command.Parameters.AddWithValue("@settingsValue", days.ToString(CultureInfo.InvariantCulture));
        await command.ExecuteNonQueryAsync();
    }

    private async Task UpdatePenalizeAfterDaysAsync(int days)
    {
        DBConfig.SetConnectionString(_user.Role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();
        await UpsertPenalizeAfterDaysAsync(connection, days);
    }

    private int GetSelectedPenalizeAfterDays()
    {
        return Math.Max((int)(dueDatePicker.Value.Date - readingDatePicker.Value.Date).TotalDays, 0);
    }

    private void RecalculateDueDate()
    {
        _isSynchronizingDueDate = true;
        dueDatePicker.Value = readingDatePicker.Value.Date.AddDays(Math.Max(_penalizeAfterDays, 0));
        _isSynchronizingDueDate = false;
    }

    private void ApplyFriendlyHeaders(DataGridView grid)
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            column.HeaderText = GetFriendlyHeader(column.Name);
        }
    }

    private static string GetFriendlyHeader(string rawName)
    {
        if (FriendlyHeaderOverrides.TryGetValue(rawName, out string? header))
        {
            return header;
        }

        string withSpaces = rawName.Replace('_', ' ').Trim();
        withSpaces = Regex.Replace(withSpaces, "(?<=[a-z0-9])([A-Z])", " $1");

        string title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(withSpaces.ToLower(CultureInfo.CurrentCulture));
        return title
            .Replace(" Id", " ID", StringComparison.Ordinal)
            .Replace(" Tin", " TIN", StringComparison.Ordinal)
            .Replace(" Scf", " SCF", StringComparison.Ordinal)
            .Trim();
    }

    private void ApplyMeterReadingGridLayout()
    {
        if (meterReadingGrid.Columns.Count == 0)
        {
            return;
        }

        ApplyFriendlyHeaders(meterReadingGrid);

        SetColumnWidth(meterReadingGrid, "row_number", 52);
        // SetColumnWidth(meterReadingGrid, "Is_Billed_Today", 1);
        SetColumnWidth(meterReadingGrid, "Concessionaire_Code", 120);
        SetColumnWidth(meterReadingGrid, "Concessionaire_Name", 220);
        SetColumnWidth(meterReadingGrid, "Meter_Number", 120);
        SetColumnWidth(meterReadingGrid, "Previous_Reading_Date", 140);
        SetColumnWidth(meterReadingGrid, "Previous_Reading", 120);
        SetColumnWidth(meterReadingGrid, "present_reading", 120);
        SetColumnWidth(meterReadingGrid, "free_water", 96);
        SetColumnWidth(meterReadingGrid, "bill_number", 112);
        SetColumnWidth(meterReadingGrid, "is_initial", 80);

        SetReadOnly(meterReadingGrid, true, "row_number", "Is_Billed_Today", "Concessionaire_Id", "Concessionaire_Code", "Concessionaire_Name", "Meter_Number", "Previous_Reading_Date", "Previous_Reading");
        SetAlignment(meterReadingGrid, DataGridViewContentAlignment.MiddleCenter, "row_number", "Is_Billed_Today", "Meter_Number", "Previous_Reading_Date", "Previous_Reading", "present_reading", "free_water", "bill_number", "is_initial");
        SetFormat(meterReadingGrid, "N0", "Previous_Reading", "present_reading", "free_water");

        if (meterReadingGrid.Columns["Concessionaire_Id"] is { } internalIdColumn)
        {
            internalIdColumn.Visible = false;
        }
        if (meterReadingGrid.Columns["Is_Billed_Today"] is { } billedThisMonthColumn)
{
    billedThisMonthColumn.Visible = false;
}

        if (meterReadingGrid.Columns["Concessionaire_Name"] is { } nameColumn)
        {
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumn.MinimumWidth = 220;
        }

        foreach (DataGridViewRow row in meterReadingGrid.Rows)
        {
            bool isBilledToday = GetBooleanCellValue(row, "Is_Billed_Today");
            row.ReadOnly = isBilledToday;
            row.DefaultCellStyle.BackColor = isBilledToday
                ? Color.FromArgb(198, 239, 206)
                : row.Index % 2 == 0
                    ? Color.White
                    : Color.FromArgb(246, 249, 251);
            row.DefaultCellStyle.SelectionBackColor = isBilledToday
                ? Color.FromArgb(157, 214, 171)
                : Color.FromArgb(219, 233, 241);
            row.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
        }

        meterReadingGrid.ClearSelection();
    }

    private void ApplyGeneratedBillsGridLayout()
    {
        if (generatedBillsGrid.Columns.Count == 0)
        {
            return;
        }

        ApplyFriendlyHeaders(generatedBillsGrid);

        if (generatedBillsGrid.Columns["billing_id"] is { } internalIdColumn)
        {
            internalIdColumn.Visible = false;
        }

        if (generatedBillsGrid.Columns["concessionaire_name"] is { } nameColumn)
        {
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        if (generatedBillsGrid.Columns["address"] is { } addressColumn)
        {
            addressColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addressColumn.MinimumWidth = 200;
        }

        SetFormat(generatedBillsGrid, "N0", "previous_reading", "present_reading", "consumption", "free_water");
        SetFormat(generatedBillsGrid, "N2", "water_charge", "discount_amount", "tax_amount", "total_water_bill", "scf_amount", "arrears_amount", "penalty_amount", "total_amount", "remaining_balance");
        SetFormat(generatedBillsGrid, "yyyy-MM-dd", "previous_reading_date", "current_reading_date", "billing_date", "due_date");
        SetAlignment(generatedBillsGrid, DataGridViewContentAlignment.MiddleCenter, "bill_number", "concessionaire_code", "previous_reading_date", "current_reading_date", "previous_reading", "present_reading", "consumption", "free_water", "billing_date", "due_date", "status", "scf_status", "is_initial");

        generatedBillsGrid.ClearSelection();
    }

    private static void SetColumnWidth(DataGridView grid, string columnName, int width)
    {
        if (grid.Columns[columnName] is { } column)
        {
            column.Width = width;
        }
    }

    private static void SetReadOnly(DataGridView grid, bool isReadOnly, params string[] columnNames)
    {
        foreach (string columnName in columnNames)
        {
            if (grid.Columns[columnName] is { } column)
            {
                column.ReadOnly = isReadOnly;
            }
        }
    }

    private static void SetAlignment(DataGridView grid, DataGridViewContentAlignment alignment, params string[] columnNames)
    {
        foreach (string columnName in columnNames)
        {
            if (grid.Columns[columnName] is not { } column)
            {
                continue;
            }

            column.DefaultCellStyle.Alignment = alignment;
            column.HeaderCell.Style.Alignment = alignment;
        }
    }

    private static void SetFormat(DataGridView grid, string format, params string[] columnNames)
    {
        foreach (string columnName in columnNames)
        {
            if (grid.Columns[columnName] is { } column)
            {
                column.DefaultCellStyle.Format = format;
            }
        }
    }

    private static bool GetBooleanValue(object? value)
    {
        if (value is null || value == DBNull.Value)
        {
            return false;
        }

        return value switch
        {
            bool booleanValue => booleanValue,
            sbyte signedByte => signedByte != 0,
            byte unsignedByte => unsignedByte != 0,
            short shortValue => shortValue != 0,
            int intValue => intValue != 0,
            long longValue => longValue != 0,
            string text when bool.TryParse(text, out bool parsedBool) => parsedBool,
            string text when int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedInt) => parsedInt != 0,
            _ => Convert.ToBoolean(value)
        };
    }

    private static bool TryGetIntValue(object? value, out int number)
    {
        switch (value)
        {
            case null:
            case DBNull:
                number = 0;
                return false;
            case int intValue:
                number = intValue;
                return true;
            case long longValue when longValue <= int.MaxValue && longValue >= int.MinValue:
                number = (int)longValue;
                return true;
            case short shortValue:
                number = shortValue;
                return true;
            case decimal decimalValue when decimal.Truncate(decimalValue) == decimalValue && decimalValue <= int.MaxValue && decimalValue >= int.MinValue:
                number = (int)decimalValue;
                return true;
            default:
                string? rawText = Convert.ToString(value, CultureInfo.InvariantCulture);
                return int.TryParse(rawText, NumberStyles.Integer, CultureInfo.InvariantCulture, out number);
        }
    }

    private static bool GetBooleanCellValue(DataGridViewRow row, string columnName)
    {
        return row.Cells[columnName].Value is { } value && GetBooleanValue(value);
    }

    private static bool TryGetIntCellValue(DataGridViewRow row, string columnName, out int value)
    {
        value = 0;
        return row.Cells[columnName].Value is { } cellValue && TryGetIntValue(cellValue, out value);
    }

    private static string GetStringCellValue(DataGridViewRow row, string columnName)
    {
        return row.Cells[columnName].Value is { } value
            ? Convert.ToString(value, CultureInfo.CurrentCulture)?.Trim() ?? string.Empty
            : string.Empty;
    }

    private void SetBillingBusyState(bool isBusy, string? busyMessage = null)
    {
        zoneComboBox.Enabled = !isBusy;
        readingDatePicker.Enabled = !isBusy;
        dueDatePicker.Enabled = !isBusy;
        meterSearchTextBox.Enabled = !isBusy;
        refreshMeterReadingsButton.Enabled = !isBusy;
        generateBillsButton.Enabled = !isBusy;
        printLatestReadingSheetButton.Enabled = !isBusy;
        meterReadingGrid.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            leftStatusLabel.ForeColor = AppTheme.MutedTextColor;
            leftStatusLabel.Text = busyMessage;
        }
    }

    private void SetGeneratedBillsBusyState(bool isBusy, string? busyMessage = null)
    {
        generatedBillsDatePicker.Enabled = !isBusy;
        generatedBillsSearchTextBox.Enabled = !isBusy;
        refreshGeneratedBillsButton.Enabled = !isBusy;
        _deleteGeneratedBillMenuItem.Enabled = !isBusy;
        generatedBillsGrid.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            rightStatusLabel.ForeColor = AppTheme.MutedTextColor;
            rightStatusLabel.Text = busyMessage;
        }
    }

    private async void zoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        await LoadMeterReadingsAsync();
    }

    private void readingDatePicker_ValueChanged(object sender, EventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        RecalculateDueDate();
    }

    private async void dueDatePicker_ValueChanged(object sender, EventArgs e)
    {
        if (_isInitializing || _isSynchronizingDueDate)
        {
            return;
        }

        int newPenalizeAfterDays = (int)(dueDatePicker.Value.Date - readingDatePicker.Value.Date).TotalDays;
        if (newPenalizeAfterDays < 0)
        {
            MessageBox.Show(this, "Due date cannot be earlier than the reading date.", "Invalid Due Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RecalculateDueDate();
            return;
        }

        int previousPenalizeAfterDays = _penalizeAfterDays;
        _penalizeAfterDays = newPenalizeAfterDays;

        try
        {
            await UpdatePenalizeAfterDaysAsync(newPenalizeAfterDays);
            leftStatusLabel.ForeColor = AppTheme.MutedTextColor;
            leftStatusLabel.Text = $"Due date rule updated to {newPenalizeAfterDays} day(s) after the reading date.";
        }
        catch (Exception ex)
        {
            _penalizeAfterDays = previousPenalizeAfterDays;
            RecalculateDueDate();
            leftStatusLabel.ForeColor = AppTheme.DangerColor;
            leftStatusLabel.Text = "Failed to update the due date rule.";
            MessageBox.Show(this, ex.Message, "Due Date Rule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void refreshMeterReadingsButton_Click(object sender, EventArgs e)
    {
        await LoadMeterReadingsAsync();
    }

    private async void meterSearchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        await LoadMeterReadingsAsync();
    }

    private async void generateBillsButton_Click(object sender, EventArgs e)
    {
        await GenerateBillsAsync();
    }

    private async void printLatestReadingSheetButton_Click(object sender, EventArgs e)
    {
        using var zonePicker = new ZonePickerForm(_user);
        if (zonePicker.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        DataTable rows;
        try
        {
            SetBillingBusyState(true, $"Loading latest reading sheet for {zonePicker.SelectedZoneName}...");

            DBConfig.SetConnectionString(_user.Role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            rows = await GetLatestReadingSheetRowsAsync(connection, zonePicker.SelectedZoneId);
        }
        catch (Exception ex)
        {
            leftStatusLabel.ForeColor = AppTheme.DangerColor;
            leftStatusLabel.Text = "Failed to load the latest reading sheet.";
            MessageBox.Show(this, ex.Message, "Print Reading Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        finally
        {
            SetBillingBusyState(false);
        }

        if (rows.Rows.Count == 0)
        {
            MessageBox.Show(
                this,
                $"There are no reading-sheet rows available for {zonePicker.SelectedZoneName}.",
                "Print Reading Sheet",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var documentData = new MeterReadingSheetDocumentData(
            "Latest Meter Reading Sheet",
            $"Zone: {zonePicker.SelectedZoneName}",
            _user.FullName,
            DateTime.Now,
            rows);

        var previewForm = new PrintPreviewForm(
            documentData.ReportTitle,
            documentData.PeriodCaption,
            new MeterReadingSheetPrintHelper(documentData),
            "Latest_Meter_Reading_Sheet");

        previewForm.Show(this);
    }

    private async void generatedBillsDatePicker_ValueChanged(object sender, EventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        await LoadGeneratedBillsAsync();
    }

    private async void refreshGeneratedBillsButton_Click(object sender, EventArgs e)
    {
        await LoadGeneratedBillsAsync();
    }

    private async void generatedBillsSearchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        await LoadGeneratedBillsAsync();
    }

    private async void deleteGeneratedBillButton_Click(object sender, EventArgs e)
    {
        await DeleteSelectedGeneratedBillAsync();
    }

    private async void deleteGeneratedBillMenuItem_Click(object? sender, EventArgs e)
    {
        await DeleteSelectedGeneratedBillAsync();
    }

    private void generatedBillsGrid_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.RowIndex >= generatedBillsGrid.Rows.Count)
        {
            return;
        }

        generatedBillsGrid.ClearSelection();
        DataGridViewRow row = generatedBillsGrid.Rows[e.RowIndex];
        row.Selected = true;

        // Avoid setting CurrentCell to hidden columns (for example billing_id), which throws InvalidOperationException.
        DataGridViewCell? firstVisibleCell = row.Cells
            .Cast<DataGridViewCell>()
            .FirstOrDefault(cell => cell.Visible);

        if (firstVisibleCell is not null)
        {
            generatedBillsGrid.CurrentCell = firstVisibleCell;
        }

        _deleteGeneratedBillMenuItem.Enabled = TryGetSelectedGeneratedBill(out _, out _);

        Point menuPosition = new(Cursor.Position.X + 10, Cursor.Position.Y);
        _generatedBillsRowMenu.Show(menuPosition);
    }

    private void meterReadingGrid_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (meterReadingGrid.CurrentCell?.OwningColumn is not { } column)
        {
            return;
        }

        if (e.Control is not TextBox textBox)
        {
            return;
        }

        textBox.KeyPress -= NumericOnly_KeyPress;
        if (_numericOnlyColumns.Contains(column.Name))
        {
            textBox.KeyPress += NumericOnly_KeyPress;
        }
    }

    private void NumericOnly_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void meterReadingGrid_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        ApplyMeterReadingGridLayout();
    }

    private void generatedBillsGrid_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        ApplyGeneratedBillsGridLayout();
    }

    private readonly record struct LookupItem(int Id, string Name)
    {
        public override string ToString() => Name;
    }

    private readonly record struct BillJob(
        int ConcessionaireId,
        string AccountNo,
        string ConcessionaireName,
        int PresentReading,
        string BillNumber,
        int FreeWater,
        int ForceInitial);
}
