using System.ComponentModel;
using System.Data;
using System.Globalization;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Report;
using WDBS_2026.Models;
using WDBS_2026.Services.Printing;
using WDBS_2026.Services.Reports;

namespace WDBS_2026.Components.Cashier;

public partial class SCFAgingOfAccountsUserControl : UserControl
{
    private static readonly Dictionary<string, string> FriendlyHeaderOverrides = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Account_No"] = "Account No.",
        ["Concessionaire_Name"] = "Concessionaire Name",
        ["Current"] = "Current",
        ["1_30_Days"] = "1-30 Days",
        ["31_60_Days"] = "31-60 Days",
        ["61_90_Days"] = "61-90 Days",
        ["Over_90_Days"] = "Over 90 Days",
        ["Total"] = "Total"
    };

    private readonly AuthenticatedUserDto _user;
    private bool _isLoading;
    private DataTable _currentRows = new();

    public SCFAgingOfAccountsUserControl()
        : this(new AuthenticatedUserDto
        {
            UserId = 0,
            Username = "designer",
            FullName = "Dashboard Designer",
            Role = UserRole.Cashier
        })
    {
    }

    public SCFAgingOfAccountsUserControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        ApplyTheme();
        ConfigureGrid();
        ConfigureSearchAutoComplete();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (IsDesignerHosted())
        {
            return;
        }

        await LoadSummaryAsync();
    }

    private static bool IsDesignerHosted()
    {
        return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        rootLayout.BackColor = AppTheme.ShellBackgroundColor;

        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplyInput(searchTextBox);
        AppTheme.ApplySeverityButton(searchButton, ButtonSeverity.Primary);
        AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(printReportButton, ButtonSeverity.Info);

        foreach (Label label in new[] { searchLabel, statusLabel })
        {
            label.ForeColor = AppTheme.BodyTextColor;
        }

        AppTheme.ApplySubtitle(statusLabel);
    }

    private void ConfigureGrid()
    {
        agingGrid.AllowUserToAddRows = false;
        agingGrid.AllowUserToDeleteRows = false;
        agingGrid.AllowUserToResizeRows = false;
        agingGrid.BackgroundColor = AppTheme.SurfaceColor;
        agingGrid.BorderStyle = BorderStyle.FixedSingle;
        agingGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        agingGrid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        agingGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        agingGrid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
        agingGrid.DefaultCellStyle.Font = AppTheme.BodyFont;
        agingGrid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
        agingGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
        agingGrid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
        agingGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
        agingGrid.EnableHeadersVisualStyles = false;
        agingGrid.GridColor = AppTheme.BorderColor;
        agingGrid.MultiSelect = false;
        agingGrid.ReadOnly = true;
        agingGrid.RowHeadersVisible = false;
        agingGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        agingGrid.AutoGenerateColumns = true;
        agingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        agingGrid.DataBindingComplete += agingGrid_DataBindingComplete;
    }

    private SCFAgingOfAccountsQueryOptions BuildQueryOptions()
    {
        return new SCFAgingOfAccountsQueryOptions(searchTextBox.Text.Trim());
    }

    private async Task LoadSummaryAsync()
    {
        if (_isLoading)
        {
            return;
        }

        try
        {
            _isLoading = true;
            SetBusyState(true, "Loading SCF aging summary...");

            SCFAgingOfAccountsQueryOptions options = BuildQueryOptions();
            _currentRows = await SCFAgingOfAccountsReportService.GetSummaryRowsAsync(_user.Role, options);

            agingGrid.DataSource = _currentRows;
            RefreshSearchAutoComplete(_currentRows);
            ApplyGridLayout();

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = _currentRows.Rows.Count == 0
                ? "No SCF aging rows found."
                : $"Loaded {_currentRows.Rows.Count} SCF aging row(s).";
        }
        catch (Exception ex)
        {
            agingGrid.DataSource = null;
            _currentRows = new DataTable();
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load SCF aging rows.";
            MessageBox.Show(this, ex.Message, "SCF Aging of Accounts", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
            _isLoading = false;
        }
    }

    private void ApplyGridLayout()
    {
        if (agingGrid.Columns.Count == 0)
        {
            return;
        }

        foreach (DataGridViewColumn column in agingGrid.Columns)
        {
            column.HeaderText = GetFriendlyHeader(column.Name);
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        HideColumn("Concessionaire_ID");
        HideColumn("Address");
        HideColumn("Max_Total_Allowed");

        SetColumnWidth("Account_No", 120);
        SetColumnWidth("Concessionaire_Name", 260);
        SetColumnWidth("Current", 120);
        SetColumnWidth("1_30_Days", 120);
        SetColumnWidth("31_60_Days", 120);
        SetColumnWidth("61_90_Days", 120);
        SetColumnWidth("Over_90_Days", 120);
        SetColumnWidth("Total", 130);

        if (agingGrid.Columns["Account_No"] is { } accountColumn)
        {
            accountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        if (agingGrid.Columns["Concessionaire_Name"] is { } nameColumn)
        {
            nameColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        CultureInfo phpCulture = CultureInfo.GetCultureInfo("en-PH");
        foreach (string moneyColumn in new[]
                 {
                     "Current",
                     "1_30_Days",
                     "31_60_Days",
                     "61_90_Days",
                     "Over_90_Days",
                     "Total"
                 })
        {
            if (agingGrid.Columns[moneyColumn] is { } column)
            {
                column.DefaultCellStyle.Format = "C2";
                column.DefaultCellStyle.FormatProvider = phpCulture;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        agingGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    }

    private static string GetFriendlyHeader(string columnName)
    {
        if (FriendlyHeaderOverrides.TryGetValue(columnName, out string? header))
        {
            return header;
        }

        return columnName.Replace("_", " ", StringComparison.Ordinal);
    }

    private void HideColumn(string columnName)
    {
        if (agingGrid.Columns[columnName] is { } column)
        {
            column.Visible = false;
        }
    }

    private void SetColumnWidth(string columnName, int width)
    {
        if (agingGrid.Columns[columnName] is { } column)
        {
            if (agingGrid.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
            {
                column.FillWeight = Math.Max(20, width);
            }
            else
            {
                column.Width = width;
            }
        }
    }

    private void SetBusyState(bool isBusy, string? message = null)
    {
        searchTextBox.Enabled = !isBusy;
        searchButton.Enabled = !isBusy;
        refreshButton.Enabled = !isBusy;
        printReportButton.Enabled = !isBusy;
        agingGrid.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(message))
        {
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = message;
        }
    }

    private void ConfigureSearchAutoComplete()
    {
        searchTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        searchTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        searchTextBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
    }

    private void RefreshSearchAutoComplete(DataTable rows)
    {
        searchTextBox.AutoCompleteCustomSource = BuildAutoCompleteSource(rows, "Account_No", "Concessionaire_Name");
    }

    private static AutoCompleteStringCollection BuildAutoCompleteSource(DataTable table, params string[] columnNames)
    {
        var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (DataRow row in table.Rows)
        {
            foreach (string columnName in columnNames)
            {
                if (!table.Columns.Contains(columnName))
                {
                    continue;
                }

                string value = Convert.ToString(row[columnName], CultureInfo.CurrentCulture)?.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    values.Add(value);
                }
            }
        }

        var source = new AutoCompleteStringCollection();
        source.AddRange(values.OrderBy(static value => value, StringComparer.CurrentCultureIgnoreCase).ToArray());
        return source;
    }

    private void agingGrid_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        agingGrid.ClearSelection();
    }

    private async void searchButton_Click(object sender, EventArgs e)
    {
        await LoadSummaryAsync();
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        await LoadSummaryAsync();
    }

    private async void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        await LoadSummaryAsync();
    }

    private void printReportButton_Click(object sender, EventArgs e)
    {
        if (_currentRows.Rows.Count == 0)
        {
            MessageBox.Show(this, "There are no SCF aging rows to print.", "SCF Aging of Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var document = new SCFAgingOfAccountsReportDocumentData(
            "SCF Aging of Accounts Summary",
            $"As of: {DateTime.Today:MMMM dd, yyyy}",
            _user.FullName,
            DateTime.Now,
            _currentRows.Copy());

        var previewForm = new PrintPreviewForm(
            document.ReportTitle,
            document.PeriodCaption,
            new SCFAgingOfAccountsReportPrintHelper(document));
        previewForm.Show(this);
    }
}
