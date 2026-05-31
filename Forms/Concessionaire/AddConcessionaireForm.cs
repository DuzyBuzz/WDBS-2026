using System.Globalization;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Forms.Concessionaire;

public partial class UpsertConcessionaireForm : Form
{
    private readonly UserRole _role;
    private readonly int _actingUserId;
    private readonly int? _concessionaireId;

    private decimal _existingScfTotalAmount;
    private decimal _existingScfBalanceAmount;

    private bool IsEditMode => _concessionaireId.HasValue;

    public UpsertConcessionaireForm(UserRole role, int actingUserId = 0, int? concessionaireId = null)
    {
        _role = role;
        _actingUserId = actingUserId;
        _concessionaireId = concessionaireId;
        InitializeComponent();
        InitializeStatusOptions();
        WireInputEnhancements();
        ApplyTheme();
        UpdateFormCaption();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadLookupsAsync();

        if (IsEditMode)
        {
            await LoadExistingConcessionaireAsync();
        }
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);

        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(statusLabel);

        foreach (Label label in new[]
                 {
                     accountNoLabel, nameLabel, tinLabel, addressLabel, zoneLabel, serviceLabel,
                     collectionStatusFieldLabel, meterLabel, firstReadingDateLabel, scfTotalLabel, scfMonthlyLabel
                 })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        foreach (TextBox textBox in new[] { accountNoTextBox, nameTextBox, tinTextBox, addressTextBox, meterNumberTextBox, scfTotalTextBox, scfMonthlyTextBox })
        {
            AppTheme.ApplyInput(textBox);
        }

        zoneComboBox.Font = AppTheme.BodyFont;
        serviceComboBox.Font = AppTheme.BodyFont;
        statusComboBox.Font = AppTheme.BodyFont;

        taxExemptedCheckBox.Font = AppTheme.BodyFont;
        dueExemptedCheckBox.Font = AppTheme.BodyFont;
        discountedCheckBox.Font = AppTheme.BodyFont;
        notBillableCheckBox.Font = AppTheme.BodyFont;

        AppTheme.ApplyPrimaryButton(saveButton);
        AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);
    }

    private void InitializeStatusOptions()
    {
        statusComboBox.Items.Clear();
        statusComboBox.Items.AddRange(new object[]
        {
            "ACTIVE",
            "PENDING",
            "DISCONNECTED"
        });
        statusComboBox.SelectedItem = "PENDING";
    }

    private void UpdateFormCaption()
    {
        titleLabel.Text = IsEditMode ? "Edit Concessionaire" : "Add Concessionaire";
        Text = "Upsert Concessionaire";
        saveButton.Text = IsEditMode ? "Update" : "Save";
    }

    private async Task LoadLookupsAsync()
    {
        try
        {
            SetBusyState(true, "Loading zone and service references...");

            DBConfig.SetConnectionString(_role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            await LoadZonesAsync(connection);
            await LoadServicesAsync(connection);
            await LoadAddressSuggestionsAsync(connection);

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = "Ready.";
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load lookup values.";
            MessageBox.Show(this, ex.Message, "Lookup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private async Task LoadExistingConcessionaireAsync()
    {
        if (!IsEditMode || !_concessionaireId.HasValue)
        {
            return;
        }

        try
        {
            SetBusyState(true, "Loading concessionaire details...");

            DBConfig.SetConnectionString(_role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            ExistingConcessionaireRecord record = await GetExistingConcessionaireAsync(connection, _concessionaireId.Value);
            PopulateForm(record);

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = "Ready to update concessionaire details.";
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load concessionaire details.";
            MessageBox.Show(this, ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Cancel;
            Close();
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private static async Task<ExistingConcessionaireRecord> GetExistingConcessionaireAsync(MySqlConnection connection, int concessionaireId)
    {
        const string sql = @"
SELECT
    c.concessionaire_code,
    c.concessionaire_name,
    c.tin_number,
    c.address,
    COALESCE(c.status, 'Pending') AS concessionaire_status,
    c.zone_id,
    c.service_id,
    c.meter_no,
    c.first_reading_date,
    c.is_tax_exempt,
    c.is_due_exempt,
    c.is_discounted,
    c.is_not_billable,
    COALESCE(sb.total_amount, 0) AS scf_total_amount,
    COALESCE(sb.balance, 0) AS scf_balance_amount,
    COALESCE(sb.monthly, 0) AS scf_monthly_amount
FROM concessionaire c
LEFT JOIN scf_balance sb ON sb.concessionaire_id = c.concessionaire_id
WHERE c.concessionaire_id = @concessionaireId
LIMIT 1;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@concessionaireId", concessionaireId);

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            throw new InvalidOperationException("The selected concessionaire record no longer exists.");
        }

        return new ExistingConcessionaireRecord(
            Convert.ToString(reader["concessionaire_code"], CultureInfo.CurrentCulture) ?? string.Empty,
            Convert.ToString(reader["concessionaire_name"], CultureInfo.CurrentCulture) ?? string.Empty,
            Convert.ToString(reader["tin_number"], CultureInfo.CurrentCulture) ?? string.Empty,
            Convert.ToString(reader["address"], CultureInfo.CurrentCulture) ?? string.Empty,
            Convert.ToString(reader["concessionaire_status"], CultureInfo.CurrentCulture) ?? "Pending",
            reader.IsDBNull(reader.GetOrdinal("zone_id")) ? 0 : reader.GetInt32("zone_id"),
            reader.IsDBNull(reader.GetOrdinal("service_id")) ? 0 : reader.GetInt32("service_id"),
            Convert.ToString(reader["meter_no"], CultureInfo.CurrentCulture) ?? string.Empty,
            reader.IsDBNull(reader.GetOrdinal("first_reading_date")) ? null : reader.GetDateTime("first_reading_date"),
            reader.GetDecimal("scf_total_amount"),
            reader.GetDecimal("scf_balance_amount"),
            reader.GetDecimal("scf_monthly_amount"),
            reader.GetBoolean("is_tax_exempt"),
            reader.GetBoolean("is_due_exempt"),
            reader.GetBoolean("is_discounted"),
            reader.GetBoolean("is_not_billable"));
    }

    private void PopulateForm(ExistingConcessionaireRecord record)
    {
        accountNoTextBox.Text = record.AccountNo.ToUpper(CultureInfo.CurrentCulture);
        nameTextBox.Text = ToTitleCase(record.ConcessionaireName);
        tinTextBox.Text = record.TinNumber;
        addressTextBox.Text = ToTitleCase(record.Address);
        SetSelectedStatus(record.Status);
        meterNumberTextBox.Text = record.MeterNumber;
        firstReadingDatePicker.Value = record.FirstReadingDate?.Date ?? DateTime.Today;
        scfTotalTextBox.Text = FormatAmount(record.ScfTotalAmount);
        scfMonthlyTextBox.Text = FormatAmount(record.ScfMonthlyAmount);
        taxExemptedCheckBox.Checked = record.IsTaxExempt;
        dueExemptedCheckBox.Checked = record.IsDueExempt;
        discountedCheckBox.Checked = record.IsDiscounted;
        notBillableCheckBox.Checked = record.IsNotBillable;

        if (record.ZoneId > 0)
        {
            zoneComboBox.SelectedValue = record.ZoneId;
        }

        if (record.ServiceId > 0)
        {
            serviceComboBox.SelectedValue = record.ServiceId;
        }

        _existingScfTotalAmount = record.ScfTotalAmount;
        _existingScfBalanceAmount = record.ScfBalanceAmount;
    }

    private void SetSelectedStatus(string? status)
    {
        string normalized = string.IsNullOrWhiteSpace(status) ? "Pending" : status.Trim();

        foreach (object item in statusComboBox.Items)
        {
            if (string.Equals(item.ToString(), normalized, StringComparison.OrdinalIgnoreCase))
            {
                statusComboBox.SelectedItem = item;
                return;
            }
        }

        statusComboBox.SelectedItem = "Pending";
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

    private async Task LoadServicesAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT service_id, service_type, pipe_size
FROM services
ORDER BY service_id ASC;";

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();

        var services = new List<LookupItem>();
        while (await reader.ReadAsync())
        {
            int serviceId = reader.GetInt32("service_id");
            string serviceName = $"{reader.GetString("service_type")} ({reader.GetString("pipe_size")})";
            services.Add(new LookupItem(serviceId, serviceName));
        }

        serviceComboBox.DataSource = services;
        serviceComboBox.DisplayMember = nameof(LookupItem.Name);
        serviceComboBox.ValueMember = nameof(LookupItem.Id);
    }

    private async Task LoadAddressSuggestionsAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT DISTINCT address
FROM concessionaire
WHERE address IS NOT NULL AND TRIM(address) <> ''
ORDER BY address
LIMIT 500;";

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();

        var source = new AutoCompleteStringCollection();
        while (await reader.ReadAsync())
        {
            source.Add(ToTitleCase(reader.GetString("address")));
        }

        addressTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        addressTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        addressTextBox.AutoCompleteCustomSource = source;
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        if (!TryBuildRequest(out UpsertConcessionaireRequest request, out string validationMessage))
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = validationMessage;
            return;
        }

        try
        {
            SetBusyState(true, IsEditMode ? "Updating concessionaire..." : "Saving concessionaire...");

            DBConfig.SetConnectionString(_role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();
            await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

            int concessionaireId = _concessionaireId ?? await InsertConcessionaireAsync(connection, transaction, request, _actingUserId);
            if (IsEditMode)
            {
                await UpdateConcessionaireAsync(connection, transaction, concessionaireId, request, _actingUserId);
            }

            decimal updatedScfBalance = CalculateScfBalanceForSave(request.ScfTotalAmount);
            await UpsertScfBalanceAsync(connection, transaction, concessionaireId, request, updatedScfBalance, _actingUserId);

            await transaction.CommitAsync();

            _existingScfTotalAmount = request.ScfTotalAmount;
            _existingScfBalanceAmount = updatedScfBalance;

            statusLabel.ForeColor = AppTheme.SuccessColor;
            statusLabel.Text = IsEditMode
                ? "Concessionaire updated successfully."
                : "Concessionaire created successfully.";

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to save concessionaire.";
            MessageBox.Show(this, ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private static async Task<int> InsertConcessionaireAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        UpsertConcessionaireRequest request,
        int actingUserId)
    {
        const string sql = @"
INSERT INTO concessionaire
(
    concessionaire_code,
    concessionaire_name,
    address,
    zone_id,
    service_id,
    meter_no,
    first_reading_date,
    is_tax_exempt,
    is_due_exempt,
    is_discounted,
    is_not_billable,
    status,
    tin_number,
    user_id
)
VALUES
(
    @accountNo,
    @name,
    @address,
    @zoneId,
    @serviceId,
    @meterNumber,
    @firstReadingDate,
    @isTaxExempt,
    @isDueExempt,
    @isDiscounted,
    @isNotBillable,
    @status,
    @tin,
    @userId
);";

        await using var command = new MySqlCommand(sql, connection, transaction);
        command.Parameters.AddWithValue("@accountNo", request.AccountNo);
        command.Parameters.AddWithValue("@name", request.ConcessionaireName);
        command.Parameters.AddWithValue("@address", request.Address);
        command.Parameters.AddWithValue("@zoneId", request.ZoneId);
        command.Parameters.AddWithValue("@serviceId", request.ServiceId);
        command.Parameters.AddWithValue("@meterNumber", request.MeterNumber);
        command.Parameters.AddWithValue("@firstReadingDate", request.FirstReadingDate.Date);
        command.Parameters.AddWithValue("@isTaxExempt", request.IsTaxExempt);
        command.Parameters.AddWithValue("@isDueExempt", request.IsDueExempt);
        command.Parameters.AddWithValue("@isDiscounted", request.IsDiscounted);
        command.Parameters.AddWithValue("@isNotBillable", request.IsNotBillable);
        command.Parameters.AddWithValue("@status", request.Status);
        command.Parameters.AddWithValue("@tin", request.TinNumber);
        command.Parameters.AddWithValue("@userId", actingUserId > 0 ? actingUserId : DBNull.Value);

        await command.ExecuteNonQueryAsync();
        return Convert.ToInt32(command.LastInsertedId);
    }

    private static async Task UpdateConcessionaireAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        int concessionaireId,
        UpsertConcessionaireRequest request,
        int actingUserId)
    {
        const string sql = @"
UPDATE concessionaire
SET
    concessionaire_code = @accountNo,
    concessionaire_name = @name,
    address = @address,
    status = @status,
    zone_id = @zoneId,
    service_id = @serviceId,
    meter_no = @meterNumber,
    first_reading_date = @firstReadingDate,
    is_tax_exempt = @isTaxExempt,
    is_due_exempt = @isDueExempt,
    is_discounted = @isDiscounted,
    is_not_billable = @isNotBillable,
    tin_number = @tin,
    user_id = @userId
WHERE concessionaire_id = @concessionaireId;";

        await using var command = new MySqlCommand(sql, connection, transaction);
        command.Parameters.AddWithValue("@concessionaireId", concessionaireId);
        command.Parameters.AddWithValue("@accountNo", request.AccountNo);
        command.Parameters.AddWithValue("@name", request.ConcessionaireName);
        command.Parameters.AddWithValue("@address", request.Address);
        command.Parameters.AddWithValue("@status", request.Status);
        command.Parameters.AddWithValue("@zoneId", request.ZoneId);
        command.Parameters.AddWithValue("@serviceId", request.ServiceId);
        command.Parameters.AddWithValue("@meterNumber", request.MeterNumber);
        command.Parameters.AddWithValue("@firstReadingDate", request.FirstReadingDate.Date);
        command.Parameters.AddWithValue("@isTaxExempt", request.IsTaxExempt);
        command.Parameters.AddWithValue("@isDueExempt", request.IsDueExempt);
        command.Parameters.AddWithValue("@isDiscounted", request.IsDiscounted);
        command.Parameters.AddWithValue("@isNotBillable", request.IsNotBillable);
        command.Parameters.AddWithValue("@tin", request.TinNumber);
        command.Parameters.AddWithValue("@userId", actingUserId > 0 ? actingUserId : DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    private static async Task UpsertScfBalanceAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        int concessionaireId,
        UpsertConcessionaireRequest request,
        decimal balance,
        int actingUserId)
    {
        const string sql = @"
INSERT INTO scf_balance
(
    concessionaire_id,
    total_amount,
    balance,
    monthly,
    user_id,
    updated_at
)
VALUES
(
    @concessionaireId,
    @totalAmount,
    @balance,
    @monthly,
    @userId,
    NOW()
)
ON DUPLICATE KEY UPDATE
    total_amount = @totalAmount,
    balance = @balance,
    monthly = @monthly,
    user_id = @userId,
    updated_at = NOW();";

        await using var command = new MySqlCommand(sql, connection, transaction);
        command.Parameters.AddWithValue("@concessionaireId", concessionaireId);
        command.Parameters.AddWithValue("@totalAmount", request.ScfTotalAmount);
        command.Parameters.AddWithValue("@balance", balance);
        command.Parameters.AddWithValue("@monthly", request.ScfMonthlyAmount);
        command.Parameters.AddWithValue("@userId", actingUserId > 0 ? actingUserId : DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    private decimal CalculateScfBalanceForSave(decimal newTotalAmount)
    {
        decimal amountAlreadyApplied = Math.Max(_existingScfTotalAmount - _existingScfBalanceAmount, 0M);
        decimal recalculatedBalance = Math.Max(newTotalAmount - amountAlreadyApplied, 0M);
        return Math.Min(recalculatedBalance, newTotalAmount);
    }

    private bool TryBuildRequest(out UpsertConcessionaireRequest request, out string validationMessage)
    {
        request = default;
        validationMessage = string.Empty;

        string accountNo = accountNoTextBox.Text.Trim().ToUpper(CultureInfo.CurrentCulture);
        string name = ToTitleCase(nameTextBox.Text);
        string address = ToTitleCase(addressTextBox.Text);

        if (string.IsNullOrWhiteSpace(accountNo))
        {
            validationMessage = "Account no is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            validationMessage = "Concessionaire name is required.";
            return false;
        }

        if (zoneComboBox.SelectedItem is not LookupItem zone)
        {
            validationMessage = "Select a zone.";
            return false;
        }

        if (serviceComboBox.SelectedItem is not LookupItem service)
        {
            validationMessage = "Select a service type.";
            return false;
        }

        if (!TryParseAmount(scfTotalTextBox.Text, out decimal scfTotal))
        {
            validationMessage = "SCF total amount must be a valid number.";
            return false;
        }

        if (!TryParseAmount(scfMonthlyTextBox.Text, out decimal scfMonthly))
        {
            validationMessage = "SCF monthly amount must be a valid number.";
            return false;
        }

        request = new UpsertConcessionaireRequest(
            accountNo,
            name,
            tinTextBox.Text.Trim(),
            address,
            statusComboBox.SelectedItem?.ToString() ?? "Pending",
            zone.Id,
            service.Id,
            meterNumberTextBox.Text.Trim(),
            firstReadingDatePicker.Value,
            scfTotal,
            scfMonthly,
            taxExemptedCheckBox.Checked,
            dueExemptedCheckBox.Checked,
            discountedCheckBox.Checked,
            notBillableCheckBox.Checked);

        return true;
    }

    private static bool TryParseAmount(string input, out decimal amount)
    {
        return decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out amount)
               || decimal.TryParse(input.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out amount);
    }

    private static string ToTitleCase(string input)
    {
        string trimmed = input.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return string.Empty;
        }

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(trimmed.ToLower(CultureInfo.CurrentCulture));
    }

    private static string FormatAmount(decimal amount)
    {
        return amount.ToString("0.##", CultureInfo.CurrentCulture);
    }

    private void WireInputEnhancements()
    {
        accountNoTextBox.Leave += (_, _) => accountNoTextBox.Text = accountNoTextBox.Text.Trim().ToUpper(CultureInfo.CurrentCulture);
        nameTextBox.Leave += (_, _) => nameTextBox.Text = ToTitleCase(nameTextBox.Text);
        addressTextBox.Leave += (_, _) => addressTextBox.Text = ToTitleCase(addressTextBox.Text);
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void SetBusyState(bool isBusy, string? busyMessage = null)
    {
        accountNoTextBox.Enabled = !isBusy;
        nameTextBox.Enabled = !isBusy;
        tinTextBox.Enabled = !isBusy;
        addressTextBox.Enabled = !isBusy;
        zoneComboBox.Enabled = !isBusy;
        serviceComboBox.Enabled = !isBusy;
        statusComboBox.Enabled = !isBusy;
        meterNumberTextBox.Enabled = !isBusy;
        firstReadingDatePicker.Enabled = !isBusy;
        scfTotalTextBox.Enabled = !isBusy;
        scfMonthlyTextBox.Enabled = !isBusy;
        taxExemptedCheckBox.Enabled = !isBusy;
        dueExemptedCheckBox.Enabled = !isBusy;
        discountedCheckBox.Enabled = !isBusy;
        notBillableCheckBox.Enabled = !isBusy;
        saveButton.Enabled = !isBusy;
        cancelButton.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = busyMessage;
        }
    }

    private readonly record struct LookupItem(int Id, string Name)
    {
        public override string ToString() => Name;
    }

    private readonly record struct ExistingConcessionaireRecord(
        string AccountNo,
        string ConcessionaireName,
        string TinNumber,
        string Address,
        string Status,
        int ZoneId,
        int ServiceId,
        string MeterNumber,
        DateTime? FirstReadingDate,
        decimal ScfTotalAmount,
        decimal ScfBalanceAmount,
        decimal ScfMonthlyAmount,
        bool IsTaxExempt,
        bool IsDueExempt,
        bool IsDiscounted,
        bool IsNotBillable);

    private readonly record struct UpsertConcessionaireRequest(
        string AccountNo,
        string ConcessionaireName,
        string TinNumber,
        string Address,
        string Status,
        int ZoneId,
        int ServiceId,
        string MeterNumber,
        DateTime FirstReadingDate,
        decimal ScfTotalAmount,
        decimal ScfMonthlyAmount,
        bool IsTaxExempt,
        bool IsDueExempt,
        bool IsDiscounted,
        bool IsNotBillable);
}
