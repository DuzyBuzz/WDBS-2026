using System.Globalization;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Forms.Concessionaire;

public partial class InitializeSCFForm : Form
{
    private readonly UserRole _role;
    private readonly int _actingUserId;
    private readonly int _concessionaireId;

    public InitializeSCFForm(UserRole role, int concessionaireId, int actingUserId = 0)
    {
        _role = role;
        _concessionaireId = concessionaireId;
        _actingUserId = actingUserId;

        InitializeComponent();
        ApplyTheme();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadScfAsync();
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);
        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySurfacePanel(contentPanel);
        AppTheme.ApplySubtitle(statusLabel);

        foreach (Label label in new[]
                 {
                     accountNoLabel, concessionaireNameLabel, totalAmountLabel, monthlyAmountLabel, balanceAmountLabel
                 })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        foreach (TextBox textBox in new[]
                 {
                     accountNoTextBox, concessionaireNameTextBox, totalAmountTextBox, monthlyAmountTextBox, balanceAmountTextBox
                 })
        {
            AppTheme.ApplyInput(textBox);
        }

        accountNoTextBox.ReadOnly = true;
        concessionaireNameTextBox.ReadOnly = true;
        accountNoTextBox.BackColor = Color.FromArgb(247, 250, 252);
        concessionaireNameTextBox.BackColor = Color.FromArgb(247, 250, 252);

        totalAmountTextBox.TextAlign = HorizontalAlignment.Right;
        monthlyAmountTextBox.TextAlign = HorizontalAlignment.Right;
        balanceAmountTextBox.TextAlign = HorizontalAlignment.Right;

        AppTheme.ApplyPrimaryButton(saveButton);
        AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);
    }

    private async Task LoadScfAsync()
    {
        SetBusyState(true, "Loading SCF details...");

        try
        {
            DBConfig.SetConnectionString(_role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            ConcessionaireScfRecord record = await GetConcessionaireScfRecordAsync(connection, _concessionaireId);
            PopulateForm(record);

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = "Review the SCF amounts, then save.";
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load SCF details.";
            MessageBox.Show(this, ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private static async Task<ConcessionaireScfRecord> GetConcessionaireScfRecordAsync(MySqlConnection connection, int concessionaireId)
    {
        const string sql = @"
SELECT
    c.concessionaire_code,
    c.concessionaire_name,
    COALESCE(sb.total_amount, 0) AS scf_total_amount,
    COALESCE(sb.monthly, 0) AS scf_monthly_amount,
    COALESCE(sb.balance, 0) AS scf_balance_amount
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

        return new ConcessionaireScfRecord(
            Convert.ToString(reader["concessionaire_code"], CultureInfo.CurrentCulture) ?? string.Empty,
            Convert.ToString(reader["concessionaire_name"], CultureInfo.CurrentCulture) ?? string.Empty,
            reader.GetDecimal("scf_total_amount"),
            reader.GetDecimal("scf_monthly_amount"),
            reader.GetDecimal("scf_balance_amount"));
    }

    private void PopulateForm(ConcessionaireScfRecord record)
    {
        accountNoTextBox.Text = record.AccountNo;
        concessionaireNameTextBox.Text = record.ConcessionaireName;
        totalAmountTextBox.Text = FormatAmount(record.TotalAmount);
        monthlyAmountTextBox.Text = FormatAmount(record.MonthlyAmount);
        balanceAmountTextBox.Text = FormatAmount(record.BalanceAmount);
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        if (!TryBuildRequest(out ScfUpsertRequest request, out string validationMessage))
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = validationMessage;
            return;
        }

        SetBusyState(true, "Saving SCF...");

        try
        {
            DBConfig.SetConnectionString(_role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            await UpsertScfAsync(connection, _concessionaireId, request, _actingUserId);

            statusLabel.ForeColor = AppTheme.SuccessColor;
            statusLabel.Text = "SCF saved successfully.";

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to save SCF.";
            MessageBox.Show(this, ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private static async Task UpsertScfAsync(MySqlConnection connection, int concessionaireId, ScfUpsertRequest request, int actingUserId)
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

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@concessionaireId", concessionaireId);
        command.Parameters.AddWithValue("@totalAmount", request.TotalAmount);
        command.Parameters.AddWithValue("@balance", request.BalanceAmount);
        command.Parameters.AddWithValue("@monthly", request.MonthlyAmount);
        command.Parameters.AddWithValue("@userId", actingUserId > 0 ? actingUserId : DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    private bool TryBuildRequest(out ScfUpsertRequest request, out string validationMessage)
    {
        request = default;
        validationMessage = string.Empty;

        if (!TryParseAmount(totalAmountTextBox.Text, out decimal totalAmount))
        {
            validationMessage = "Enter a valid SCF total amount.";
            return false;
        }

        if (!TryParseAmount(monthlyAmountTextBox.Text, out decimal monthlyAmount))
        {
            validationMessage = "Enter a valid SCF monthly amount.";
            return false;
        }

        if (!TryParseAmount(balanceAmountTextBox.Text, out decimal balanceAmount))
        {
            validationMessage = "Enter a valid SCF balance amount.";
            return false;
        }

        if (balanceAmount > totalAmount)
        {
            validationMessage = "SCF balance cannot be greater than the total amount.";
            return false;
        }

        request = new ScfUpsertRequest(totalAmount, monthlyAmount, balanceAmount);

        totalAmountTextBox.Text = FormatAmount(totalAmount);
        monthlyAmountTextBox.Text = FormatAmount(monthlyAmount);
        balanceAmountTextBox.Text = FormatAmount(balanceAmount);
        return true;
    }

    private static bool TryParseAmount(string rawValue, out decimal amount)
    {
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            amount = 0M;
            return true;
        }

        return decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.CurrentCulture, out amount) && amount >= 0M;
    }

    private static string FormatAmount(decimal amount)
    {
        return amount.ToString("N2", CultureInfo.CurrentCulture);
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void SetBusyState(bool isBusy, string? busyMessage = null)
    {
        totalAmountTextBox.Enabled = !isBusy;
        monthlyAmountTextBox.Enabled = !isBusy;
        balanceAmountTextBox.Enabled = !isBusy;
        saveButton.Enabled = !isBusy;
        cancelButton.Enabled = !isBusy;
        UseWaitCursor = isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = busyMessage;
        }
    }

    private readonly record struct ScfUpsertRequest(decimal TotalAmount, decimal MonthlyAmount, decimal BalanceAmount);

    private readonly record struct ConcessionaireScfRecord(
        string AccountNo,
        string ConcessionaireName,
        decimal TotalAmount,
        decimal MonthlyAmount,
        decimal BalanceAmount);
}
