using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Forms.Concessionaire;
using WDBS_2026.Models;

namespace WDBS_2026.Components.Biller;

public partial class ConcessionaireUserControl : UserControl
{
    private const int PageSize = 100;

    private readonly UserRole _role;

    private int _currentPage = 1;
    private int _totalRecords;
    private string _searchTerm = string.Empty;
    private string _statusFilter = string.Empty;
    private int? _contextConcessionaireId;

    private readonly ContextMenuStrip _rowActionsMenu = new();
    private readonly ToolStripMenuItem _updateConcessionaireMenuItem = new("Update Concessionaire");
    private readonly ToolStripMenuItem _initializeScfMenuItem = new("Initialize SCF");

    public ConcessionaireUserControl(UserRole role)
    {
        _role = role;
        InitializeComponent();
        ConfigureGridContextMenu();
        ApplyTheme();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        statusFilterComboBox.SelectedIndex = 0;
        await LoadConcessionairesAsync();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        AppTheme.ApplyPageTitle(titleLabel);
        statusFilterLabel.ForeColor = AppTheme.BodyTextColor;
        statusFilterComboBox.Font = AppTheme.BodyFont;
        AppTheme.ApplyInput(searchTextBox);
        AppTheme.ApplyPrimaryButton(searchButton);
        AppTheme.ApplySeverityButton(clearButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(addConcessionaireButton, ButtonSeverity.Success);
        AppTheme.ApplySeverityButton(previousPageButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(nextPageButton, ButtonSeverity.Neutral);

        concessionaireGrid.BackgroundColor = AppTheme.SurfaceColor;
        concessionaireGrid.GridColor = AppTheme.BorderColor;
        concessionaireGrid.BorderStyle = BorderStyle.FixedSingle;
        concessionaireGrid.RowHeadersVisible = false;
        concessionaireGrid.EnableHeadersVisualStyles = false;
        concessionaireGrid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        concessionaireGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        concessionaireGrid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
        concessionaireGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
        concessionaireGrid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
    }

    private void ConfigureGridContextMenu()
    {
        _rowActionsMenu.Items.AddRange(new ToolStripItem[]
        {
            _updateConcessionaireMenuItem,
            _initializeScfMenuItem
        });

        _updateConcessionaireMenuItem.Click += updateConcessionaireMenuItem_Click;
        _initializeScfMenuItem.Click += initializeScfMenuItem_Click;

        concessionaireGrid.CellMouseDown += concessionaireGrid_CellMouseDown;
    }

    private async Task LoadConcessionairesAsync()
    {
        try
        {
            SetBusyState(true, "Loading concessionaires...");

            DBConfig.SetConnectionString(_role);
            await using MySqlConnection connection = DBConfig.GetConnection();
            await connection.OpenAsync();

            _totalRecords = await GetTotalRecordsAsync(connection, _searchTerm, _statusFilter);
            int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
            _currentPage = Math.Max(1, Math.Min(_currentPage, totalPages));

            int offset = (_currentPage - 1) * PageSize;
            DataTable rows = await GetRowsAsync(connection, _searchTerm, _statusFilter, offset);

            concessionaireGrid.DataSource = rows;
            ApplyGridHeaders();

            pageInfoLabel.Text = $"Page {_currentPage} of {totalPages}  •  {_totalRecords} record(s)";
            previousPageButton.Enabled = _currentPage > 1;
            nextPageButton.Enabled = _currentPage < totalPages;

            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = _totalRecords == 0
                ? "No concessionaire records found for the current filter."
                : "Concessionaire records loaded. Right-click a row to update concessionaire or initialize SCF.";
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Failed to load concessionaire records.";
            MessageBox.Show(this, ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private static async Task<int> GetTotalRecordsAsync(MySqlConnection connection, string searchTerm, string statusFilter)
    {
        const string sql = @"
SELECT COUNT(*)
FROM v_concessionaire_details v
WHERE (@search = '' OR v.Account_No LIKE @searchLike OR v.Concessionaire_Name LIKE @searchLike)
  AND (@statusFilter = '' OR LOWER(TRIM(v.Status)) = LOWER(@statusFilter));";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@search", searchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{searchTerm}%");
        command.Parameters.AddWithValue("@statusFilter", statusFilter);

        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0);
    }

    private static async Task<DataTable> GetRowsAsync(MySqlConnection connection, string searchTerm, string statusFilter, int offset)
    {
        const string sql = @"
SELECT
    v.Concessionaire_ID,
    v.Account_No,
    v.Concessionaire_Name,
    v.Address,
    v.Tin,
    v.Status,
    v.Meter_Number,
    v.FRD,
    v.Tax_Exempted,
    v.Due_Exempted,
    v.Discounted,
    v.Zone_Name,
    v.Service_Type,
    v.Pipe_Size,
    v.SCF_Total_Amount,
    v.SCF_Balance,
    v.SCF_Monthly
FROM v_concessionaire_details v
WHERE (@search = '' OR v.Account_No LIKE @searchLike OR v.Concessionaire_Name LIKE @searchLike)
  AND (@statusFilter = '' OR LOWER(TRIM(v.Status)) = LOWER(@statusFilter))
ORDER BY v.Concessionaire_ID DESC
LIMIT @limit OFFSET @offset;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@search", searchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{searchTerm}%");
        command.Parameters.AddWithValue("@statusFilter", statusFilter);
        command.Parameters.AddWithValue("@limit", PageSize);
        command.Parameters.AddWithValue("@offset", offset);

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private void ApplyGridHeaders()
    {
        if (concessionaireGrid.Columns.Count == 0)
        {
            return;
        }

        foreach (DataGridViewColumn column in concessionaireGrid.Columns)
        {
            column.HeaderText = GetFriendlyHeader(column.Name);
        }
    }

    private static string GetFriendlyHeader(string rawName)
    {
        var namedOverrides = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Concessionaire_ID"] = "ID",
            ["Tin"] = "TIN",
            ["FRD"] = "First Reading Date",
            ["SCF_Total_Amount"] = "SCF Total",
            ["SCF_Balance"] = "SCF Balance",
            ["SCF_Monthly"] = "SCF Monthly",
            ["Meter_Number"] = "Meter No"
        };

        if (namedOverrides.TryGetValue(rawName, out string? mapped))
        {
            return mapped;
        }

        string withSpaces = rawName.Replace('_', ' ').Trim();
        withSpaces = Regex.Replace(withSpaces, "(?<=[a-z])([A-Z])", " $1");

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        string title = textInfo.ToTitleCase(withSpaces.ToLower(CultureInfo.CurrentCulture));

        return title
            .Replace(" Id", " ID", StringComparison.Ordinal)
            .Replace(" Tin", " TIN", StringComparison.Ordinal)
            .Replace(" Scf", " SCF", StringComparison.Ordinal)
            .Trim();
    }

    private async void searchButton_Click(object sender, EventArgs e)
    {
        _searchTerm = searchTextBox.Text.Trim();
        _statusFilter = GetSelectedStatusFilter();
        _currentPage = 1;
        await LoadConcessionairesAsync();
    }

    private async void clearButton_Click(object sender, EventArgs e)
    {
        searchTextBox.Clear();
        _searchTerm = string.Empty;
        statusFilterComboBox.SelectedIndex = 0;
        _statusFilter = string.Empty;
        _currentPage = 1;
        await LoadConcessionairesAsync();
    }

    private async void addConcessionaireButton_Click(object sender, EventArgs e)
    {
        await OpenUpsertConcessionaireFormAsync();
    }

    private void concessionaireGrid_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.RowIndex >= concessionaireGrid.Rows.Count)
        {
            return;
        }

        concessionaireGrid.ClearSelection();
        concessionaireGrid.Rows[e.RowIndex].Selected = true;
        concessionaireGrid.CurrentCell = concessionaireGrid.Rows[e.RowIndex].Cells[0];

        _contextConcessionaireId = TryGetConcessionaireId(e.RowIndex, out int concessionaireId)
            ? concessionaireId
            : null;

        _updateConcessionaireMenuItem.Enabled = _contextConcessionaireId.HasValue;
        _initializeScfMenuItem.Enabled = _contextConcessionaireId.HasValue;

        Point menuPosition = new(Cursor.Position.X + 10, Cursor.Position.Y);
        _rowActionsMenu.Show(menuPosition);
    }

    private async Task OpenUpsertConcessionaireFormAsync(int? concessionaireId = null)
    {
        using var form = new UpsertConcessionaireForm(_role, concessionaireId);
        DialogResult result = form.ShowDialog(this);
        if (result != DialogResult.OK)
        {
            return;
        }

        _currentPage = 1;
        await LoadConcessionairesAsync();
    }

    private async Task OpenInitializeScfFormAsync(int concessionaireId)
    {
        using var form = new InitializeSCFForm(_role, concessionaireId);
        DialogResult result = form.ShowDialog(this);
        if (result != DialogResult.OK)
        {
            return;
        }

        await LoadConcessionairesAsync();
    }

    private async void updateConcessionaireMenuItem_Click(object? sender, EventArgs e)
    {
        if (!_contextConcessionaireId.HasValue)
        {
            return;
        }

        await OpenUpsertConcessionaireFormAsync(_contextConcessionaireId.Value);
    }

    private async void initializeScfMenuItem_Click(object? sender, EventArgs e)
    {
        if (!_contextConcessionaireId.HasValue)
        {
            return;
        }

        await OpenInitializeScfFormAsync(_contextConcessionaireId.Value);
    }

    private bool TryGetConcessionaireId(int rowIndex, out int concessionaireId)
    {
        concessionaireId = 0;

        if (rowIndex < 0 || rowIndex >= concessionaireGrid.Rows.Count)
        {
            return false;
        }

        object? rawValue = concessionaireGrid.Rows[rowIndex].Cells["Concessionaire_ID"].Value;
        if (rawValue is null || rawValue == DBNull.Value)
        {
            return false;
        }

        string? textValue = Convert.ToString(rawValue, CultureInfo.InvariantCulture);
        return int.TryParse(textValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out concessionaireId);
    }

    private async void previousPageButton_Click(object sender, EventArgs e)
    {
        if (_currentPage <= 1)
        {
            return;
        }

        _currentPage--;
        await LoadConcessionairesAsync();
    }

    private async void nextPageButton_Click(object sender, EventArgs e)
    {
        int totalPages = Math.Max(1, (int)Math.Ceiling(_totalRecords / (double)PageSize));
        if (_currentPage >= totalPages)
        {
            return;
        }

        _currentPage++;
        await LoadConcessionairesAsync();
    }

    private async void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        _searchTerm = searchTextBox.Text.Trim();
        _statusFilter = GetSelectedStatusFilter();
        _currentPage = 1;
        await LoadConcessionairesAsync();
    }

    private async void statusFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        _statusFilter = GetSelectedStatusFilter();
        _currentPage = 1;
        await LoadConcessionairesAsync();
    }

    private string GetSelectedStatusFilter()
    {
        return statusFilterComboBox.SelectedItem?.ToString() switch
        {
            "Active" => "active",
            "Disconnected" => "disconnected",
            _ => string.Empty
        };
    }

    private void SetBusyState(bool isBusy, string? busyMessage = null)
    {
        searchTextBox.Enabled = !isBusy;
        statusFilterComboBox.Enabled = !isBusy;
        searchButton.Enabled = !isBusy;
        clearButton.Enabled = !isBusy;
        previousPageButton.Enabled = !isBusy;
        nextPageButton.Enabled = !isBusy;
        concessionaireGrid.Enabled = !isBusy;

        if (isBusy && !string.IsNullOrWhiteSpace(busyMessage))
        {
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = busyMessage;
        }
    }
}
