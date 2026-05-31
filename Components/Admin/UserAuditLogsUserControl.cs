using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Auditing;
using WDBS_2026.Models;

namespace WDBS_2026.Components.Admin
{
    public partial class UserAuditLogsUserControl : UserControl
    {
        private readonly AuthenticatedUserDto _user;
        private bool _isLoading;
        private DataTable? _allLogs;
        private const int PageSize = 50;
        private int _currentPage = 1;
        private int _totalPages = 1;

        public UserAuditLogsUserControl() : this(new AuthenticatedUserDto
        {
            Role = UserRole.Admin
        })
        {
        }

        public UserAuditLogsUserControl(AuthenticatedUserDto user)
        {
            _user = user;
            InitializeComponent();
            ConfigureGrid();
            WireEvents();
            ApplyTheme();
            InitializeDatePickers();
        }

        private void InitializeDatePickers()
        {
            dateToPicker.Value = DateTime.Now;
            dateFromPicker.Value = DateTime.Now.AddMonths(-1);
        }

        private void ConfigureGrid()
        {
            auditLogsGrid.AutoGenerateColumns = false;

            AddTextColumn("log_id", "Log ID", 90, DataGridViewContentAlignment.MiddleCenter, format: "N0");
            AddTextColumn("logged_at", "Date/Time", 160);
            AddTextColumn("full_name", "User", 220);
            AddTextColumn("username", "Username", 140);
            AddTextColumn("actor_role", "Role", 120);
            AddTextColumn("action", "Action", 640);
        }

        private void AddTextColumn(string dataPropertyName, string headerText, int width, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft, string? format = null)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName,
                Width = width,
                MinimumWidth = 80,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = alignment,
                    Format = format ?? string.Empty
                }
            };

            auditLogsGrid.Columns.Add(column);
        }

        private void WireEvents()
        {
            Load += UserAuditLogsUserControl_Load;
            refreshButton.Click += refreshButton_Click;
            searchButton.Click += searchButton_Click;
            clearButton.Click += clearButton_Click;
            printButton.Click += printButton_Click;
            previousPageButton.Click += previousPageButton_Click;
            nextPageButton.Click += nextPageButton_Click;
            auditLogsGrid.CellDoubleClick += auditLogsGrid_CellDoubleClick;
            searchTextBox.KeyDown += searchTextBox_KeyDown;
        }

        private void ApplyTheme()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            BackColor = AppTheme.ShellBackgroundColor;
            rootLayout.BackColor = AppTheme.ShellBackgroundColor;
            filterLayout.BackColor = AppTheme.ShellBackgroundColor;
            footerLayout.BackColor = AppTheme.ShellBackgroundColor;
            paginationLayout.BackColor = AppTheme.ShellBackgroundColor;
            
            AppTheme.ApplyPageTitle(titleLabel);
            AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(searchButton, ButtonSeverity.Primary);
            AppTheme.ApplySeverityButton(clearButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(printButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(previousPageButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(nextPageButton, ButtonSeverity.Neutral);
            AppTheme.ApplyInput(searchTextBox);
            
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Font = AppTheme.BodyFont;
            pageInfoLabel.Font = AppTheme.BodyFont;
            pageInfoLabel.ForeColor = AppTheme.MutedTextColor;

            auditLogsGrid.BackgroundColor = Color.White;
            auditLogsGrid.BorderStyle = BorderStyle.FixedSingle;
            auditLogsGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            auditLogsGrid.GridColor = AppTheme.BorderColor;
            auditLogsGrid.EnableHeadersVisualStyles = false;
            auditLogsGrid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            auditLogsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            auditLogsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            auditLogsGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = AppTheme.PrimaryColor;
            auditLogsGrid.ColumnHeadersHeight = 36;
            auditLogsGrid.DefaultCellStyle.Font = AppTheme.BodyFont;
            auditLogsGrid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
            auditLogsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 236, 245);
            auditLogsGrid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
            auditLogsGrid.RowHeadersVisible = false;
            auditLogsGrid.RowTemplate.Height = 30;
        }

        private async void UserAuditLogsUserControl_Load(object? sender, EventArgs e)
        {
            await LoadAuditLogsAsync();
        }

        private async void refreshButton_Click(object? sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadAuditLogsAsync();
        }

        private async void searchButton_Click(object? sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadAuditLogsAsync();
        }

        private void clearButton_Click(object? sender, EventArgs e)
        {
            searchTextBox.Clear();
            dateFromPicker.Value = DateTime.Now.AddMonths(-1);
            dateToPicker.Value = DateTime.Now;
            _currentPage = 1;
        }

        private void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchButton_Click(null, EventArgs.Empty);
            }
        }

        private void previousPageButton_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                DisplayPage();
            }
        }

        private void nextPageButton_Click(object? sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                DisplayPage();
            }
        }

        private async Task LoadAuditLogsAsync()
        {
            if (_isLoading)
            {
                return;
            }

            try
            {
                _isLoading = true;
                SetBusyState(true, "Loading audit logs...");

                DBConfig.SetConnectionString(_user.Role == UserRole.Admin ? UserRole.Admin : _user.Role);
                await using var connection = DBConfig.GetConnection();
                await connection.OpenAsync();

                string searchTerm = searchTextBox.Text.Trim();
                DateTime dateFrom = dateFromPicker.Value.Date;
                DateTime dateTo = dateToPicker.Value.Date.AddDays(1);

                const string sql = @"
SELECT
    l.log_id,
    l.action,
    l.created_at,
    COALESCE(u.full_name, CONCAT('User #', l.user_id)) AS full_name,
    COALESCE(u.username, '') AS username,
    COALESCE(u.role, '') AS actor_role
FROM user_logs l
LEFT JOIN users u ON u.user_id = l.user_id
WHERE l.created_at >= @dateFrom
  AND l.created_at < @dateTo
  AND (l.action LIKE @searchTerm
       OR u.full_name LIKE @searchTerm
       OR u.username LIKE @searchTerm
       OR u.role LIKE @searchTerm
       OR CONCAT('User #', l.user_id) LIKE @searchTerm)
ORDER BY l.created_at DESC, l.log_id DESC;";

                await using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dateFrom", dateFrom);
                command.Parameters.AddWithValue("@dateTo", dateTo);
                command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                using var adapter = new MySqlDataAdapter(command);
                _allLogs = new DataTable();
                adapter.Fill(_allLogs);

                DataColumn? createdAtColumn = _allLogs.Columns["created_at"];
                if (createdAtColumn is not null)
                {
                    createdAtColumn.ColumnName = "logged_at";
                }

                _totalPages = (_allLogs.Rows.Count + PageSize - 1) / PageSize;
                if (_totalPages == 0)
                {
                    _totalPages = 1;
                }

                DisplayPage();
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = "Failed to load audit logs.";
                MessageBox.Show(this, ex.Message, "Audit Logs Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
                _isLoading = false;
            }
        }

        private void DisplayPage()
        {
            if (_allLogs is null || _allLogs.Rows.Count == 0)
            {
                auditLogsGrid.DataSource = _allLogs;
                statusLabel.ForeColor = AppTheme.MutedTextColor;
                statusLabel.Text = "No audit logs found.";
                pageInfoLabel.Text = "0/0";
                previousPageButton.Enabled = false;
                nextPageButton.Enabled = false;
                return;
            }

            int startRow = (_currentPage - 1) * PageSize;
            int endRow = Math.Min(startRow + PageSize, _allLogs.Rows.Count);
            int pageRowCount = endRow - startRow;

            var pageTable = _allLogs.Clone();
            for (int i = startRow; i < endRow; i++)
            {
                pageTable.ImportRow(_allLogs.Rows[i]);
            }

            auditLogsGrid.DataSource = pageTable;
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = $"Showing {pageRowCount:N0} of {_allLogs.Rows.Count:N0} audit logs.";
            pageInfoLabel.Text = $"{_currentPage}/{_totalPages}";
            previousPageButton.Enabled = _currentPage > 1;
            nextPageButton.Enabled = _currentPage < _totalPages;
        }

        private void SetBusyState(bool isBusy, string? status = null)
        {
            refreshButton.Enabled = !isBusy;
            searchButton.Enabled = !isBusy;
            clearButton.Enabled = !isBusy;
            searchTextBox.Enabled = !isBusy;
            dateFromPicker.Enabled = !isBusy;
            dateToPicker.Enabled = !isBusy;
            auditLogsGrid.Enabled = !isBusy;

            if (!string.IsNullOrWhiteSpace(status))
            {
                statusLabel.Text = status;
            }
        }

        private async void printButton_Click(object? sender, EventArgs e)
        {
            if (_allLogs is null || _allLogs.Rows.Count == 0)
            {
                MessageBox.Show(this, "No audit logs to print.", "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SetBusyState(true, "Generating print report...");

                var printData = new System.Text.StringBuilder();
                printData.AppendLine("========================================");
                printData.AppendLine("USER AUDIT LOGS REPORT");
                printData.AppendLine("========================================");
                printData.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                printData.AppendLine($"Period: {dateFromPicker.Value:yyyy-MM-dd} to {dateToPicker.Value:yyyy-MM-dd}");
                if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
                {
                    printData.AppendLine($"Search: {searchTextBox.Text}");
                }
                printData.AppendLine($"Total Records: {_allLogs.Rows.Count:N0}");
                printData.AppendLine("----------------------------------------");
                printData.AppendLine();

                foreach (DataRow row in _allLogs.Rows)
                {
                    printData.AppendLine($"Log ID: {row["log_id"]}");
                    printData.AppendLine($"Date/Time: {row["logged_at"]}");
                    printData.AppendLine($"User: {row["full_name"]} ({row["username"]})");
                    printData.AppendLine($"Role: {row["actor_role"]}");
                    printData.AppendLine($"Action: {row["action"]}");
                    printData.AppendLine();
                }

                string fileName = $"UserAuditLogs_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(Path.GetTempPath(), fileName);
                await File.WriteAllTextAsync(filePath, printData.ToString());

                if (File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = "Failed to generate report.";
                MessageBox.Show(this, ex.Message, "Print Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
            }
        }

        private void auditLogsGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= auditLogsGrid.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = auditLogsGrid.Rows[e.RowIndex];
            if (row.DataBoundItem is not DataRowView dataRowView)
            {
                return;
            }

            DataRow rowData = dataRowView.Row;
            int logId = rowData["log_id"] == DBNull.Value
                ? 0
                : Convert.ToInt32(rowData["log_id"], System.Globalization.CultureInfo.InvariantCulture);

            var detail = new AuditLogDetail
            {
                LogId = logId,
                LoggedAt = rowData.Field<DateTime?>("logged_at"),
                FullName = rowData.Field<string>("full_name") ?? string.Empty,
                Username = rowData.Field<string>("username") ?? string.Empty,
                ActorRole = rowData.Field<string>("actor_role") ?? string.Empty,
                Action = rowData.Field<string>("action") ?? string.Empty
            };

            using var auditingForm = new AuditingForm(detail);
            auditingForm.ShowDialog(this);
        }
    }
}
