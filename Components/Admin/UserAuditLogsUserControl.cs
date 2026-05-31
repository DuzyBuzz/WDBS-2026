using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Auditing;
using WDBS_2026.Forms.Report;
using WDBS_2026.Models;
using WDBS_2026.Services.Printing;

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
            InitializeFilterOptions();
            ConfigureGrid();
            WireEvents();
            ApplyTheme();
            InitializeDatePickers();
        }

        private void InitializeFilterOptions()
        {
            InitializeFilterCombo(roleFilterComboBox);
            InitializeFilterCombo(actionTypeFilterComboBox);
            InitializeFilterCombo(moduleFilterComboBox);
            InitializeFilterCombo(severityFilterComboBox);
        }

        private static void InitializeFilterCombo(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("All");
            comboBox.SelectedIndex = 0;
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
            AddTextColumn("formatted_date", "Date/Time", 150);
            AddTextColumn("full_name", "User", 220);
            AddTextColumn("username", "Username", 140);
            AddTextColumn("actor_role", "Role", 120);
            AddTextColumn("action_type", "Action Type", 120);
            AddTextColumn("module", "Module", 120);
            AddTextColumn("entity_id", "Entity ID", 110);
            AddTextColumn("severity", "Severity", 100, DataGridViewContentAlignment.MiddleCenter);
            AddTextColumn("description", "Description", 320);
            AddTextColumn("activity_summary", "Activity Summary", 420);
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
            roleFilterComboBox.SelectedIndexChanged += filterComboBox_SelectedIndexChanged;
            actionTypeFilterComboBox.SelectedIndexChanged += filterComboBox_SelectedIndexChanged;
            moduleFilterComboBox.SelectedIndexChanged += filterComboBox_SelectedIndexChanged;
            severityFilterComboBox.SelectedIndexChanged += filterComboBox_SelectedIndexChanged;
            dateFromPicker.ValueChanged += filterComboBox_SelectedIndexChanged;
            dateToPicker.ValueChanged += filterComboBox_SelectedIndexChanged;
            printButton.Click += printButton_Click;
            previousPageButton.Click += previousPageButton_Click;
            nextPageButton.Click += nextPageButton_Click;
            auditLogsGrid.CellDoubleClick += auditLogsGrid_CellDoubleClick;
            userSearchTextBox.KeyDown += userSearchTextBox_KeyDown;
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
            AppTheme.ApplyInput(userSearchTextBox);
            roleFilterComboBox.Font = AppTheme.BodyFont;
            actionTypeFilterComboBox.Font = AppTheme.BodyFont;
            moduleFilterComboBox.Font = AppTheme.BodyFont;
            severityFilterComboBox.Font = AppTheme.BodyFont;
            userSearchLabel.ForeColor = AppTheme.BodyTextColor;
            userSearchLabel.Font = AppTheme.BodyFont;
            roleFilterLabel.ForeColor = AppTheme.BodyTextColor;
            roleFilterLabel.Font = AppTheme.BodyFont;
            actionTypeFilterLabel.ForeColor = AppTheme.BodyTextColor;
            actionTypeFilterLabel.Font = AppTheme.BodyFont;
            moduleFilterLabel.ForeColor = AppTheme.BodyTextColor;
            moduleFilterLabel.Font = AppTheme.BodyFont;
            severityFilterLabel.ForeColor = AppTheme.BodyTextColor;
            severityFilterLabel.Font = AppTheme.BodyFont;
            dateFromLabel.ForeColor = AppTheme.BodyTextColor;
            dateFromLabel.Font = AppTheme.BodyFont;
            dateToLabel.ForeColor = AppTheme.BodyTextColor;
            dateToLabel.Font = AppTheme.BodyFont;
            
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

        private async void clearButton_Click(object? sender, EventArgs e)
        {
            userSearchTextBox.Clear();
            roleFilterComboBox.SelectedIndex = 0;
            actionTypeFilterComboBox.SelectedIndex = 0;
            moduleFilterComboBox.SelectedIndex = 0;
            severityFilterComboBox.SelectedIndex = 0;
            dateFromPicker.Value = DateTime.Now.AddMonths(-1);
            dateToPicker.Value = DateTime.Now;
            _currentPage = 1;
            await LoadAuditLogsAsync();
        }

        private async void filterComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!IsHandleCreated || _isLoading)
            {
                return;
            }

            _currentPage = 1;
            await LoadAuditLogsAsync();
        }

        private void userSearchTextBox_KeyDown(object? sender, KeyEventArgs e)
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

                string userSearchTerm = userSearchTextBox.Text.Trim();
                string roleFilter = GetSelectedFilterValue(roleFilterComboBox);
                string actionTypeFilter = GetSelectedFilterValue(actionTypeFilterComboBox);
                string moduleFilter = GetSelectedFilterValue(moduleFilterComboBox);
                string severityFilter = GetSelectedFilterValue(severityFilterComboBox);
                DateTime dateFrom = dateFromPicker.Value.Date;
                DateTime dateTo = dateToPicker.Value.Date.AddDays(1);

                string sql = $@"
SELECT
        v.log_id,
        v.created_at AS logged_at,
    v.formatted_date,
        COALESCE(v.full_name, CONCAT('User #', v.user_id)) AS full_name,
        COALESCE(v.username, '') AS username,
        COALESCE(v.role, '') AS actor_role,
    COALESCE(v.action_type, '') AS action_type,
    COALESCE(v.module, '') AS module,
    COALESCE(v.entity_name, '') AS entity_name,
    COALESCE(v.entity_id, '') AS entity_id,
    COALESCE(v.severity, '') AS severity,
    COALESCE(v.description, '') AS description,
    COALESCE(v.activity_summary, '') AS activity_summary
FROM v_user_logs v
WHERE v.created_at >= @dateFrom
    AND v.created_at < @dateTo
    AND (@userSearchTerm = '' OR COALESCE(v.full_name, '') LIKE @userSearchLike OR COALESCE(v.username, '') LIKE @userSearchLike)
    AND (@roleFilter = 'All' OR LOWER(COALESCE(v.role, '')) = LOWER(@roleFilter))
    AND (@actionTypeFilter = 'All' OR LOWER(COALESCE(v.action_type, '')) = LOWER(@actionTypeFilter))
    AND (@moduleFilter = 'All' OR LOWER(COALESCE(v.module, '')) = LOWER(@moduleFilter))
    AND (@severityFilter = 'All' OR LOWER(COALESCE(v.severity, '')) = LOWER(@severityFilter))
ORDER BY v.created_at DESC, v.log_id DESC;";

                await using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dateFrom", dateFrom);
                command.Parameters.AddWithValue("@dateTo", dateTo);
                command.Parameters.AddWithValue("@userSearchTerm", userSearchTerm);
                command.Parameters.AddWithValue("@userSearchLike", $"%{userSearchTerm}%");
                command.Parameters.AddWithValue("@roleFilter", roleFilter);
                command.Parameters.AddWithValue("@actionTypeFilter", actionTypeFilter);
                command.Parameters.AddWithValue("@moduleFilter", moduleFilter);
                command.Parameters.AddWithValue("@severityFilter", severityFilter);

                using var adapter = new MySqlDataAdapter(command);
                _allLogs = new DataTable();
                adapter.Fill(_allLogs);

                RefreshFilterCollections();

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
            ApplySeverityRowStyles();
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = $"Showing {pageRowCount:N0} of {_allLogs.Rows.Count:N0} audit logs.";
            pageInfoLabel.Text = $"{_currentPage}/{_totalPages}";
            previousPageButton.Enabled = _currentPage > 1;
            nextPageButton.Enabled = _currentPage < _totalPages;
        }

        private void ApplySeverityRowStyles()
        {
            if (auditLogsGrid.Rows.Count == 0)
            {
                return;
            }

            int severityColumnIndex = auditLogsGrid.Columns["severity"]?.Index ?? -1;
            if (severityColumnIndex < 0)
            {
                return;
            }

            foreach (DataGridViewRow row in auditLogsGrid.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string severity = Convert.ToString(row.Cells[severityColumnIndex].Value)?.Trim() ?? string.Empty;
                (Color backColor, Color selectionBackColor) = GetSeverityRowColors(severity);

                row.DefaultCellStyle.BackColor = backColor;
                row.DefaultCellStyle.SelectionBackColor = selectionBackColor;
            }
        }

        private static (Color BackColor, Color SelectionBackColor) GetSeverityRowColors(string severity)
        {
            return severity.ToUpperInvariant() switch
            {
                "CRITICAL" => (Color.FromArgb(253, 230, 230), Color.FromArgb(244, 197, 197)),
                "HIGH" => (Color.FromArgb(255, 239, 230), Color.FromArgb(248, 215, 191)),
                "MEDIUM" => (Color.FromArgb(255, 249, 230), Color.FromArgb(247, 236, 189)),
                "LOW" => (Color.FromArgb(236, 248, 236), Color.FromArgb(211, 233, 211)),
                "INFO" => (Color.FromArgb(236, 245, 255), Color.FromArgb(205, 224, 245)),
                _ => (Color.White, Color.FromArgb(216, 236, 245))
            };
        }

        private void SetBusyState(bool isBusy, string? status = null)
        {
            refreshButton.Enabled = !isBusy;
            searchButton.Enabled = !isBusy;
            clearButton.Enabled = !isBusy;
            userSearchTextBox.Enabled = !isBusy;
            roleFilterComboBox.Enabled = !isBusy;
            actionTypeFilterComboBox.Enabled = !isBusy;
            moduleFilterComboBox.Enabled = !isBusy;
            severityFilterComboBox.Enabled = !isBusy;
            dateFromPicker.Enabled = !isBusy;
            dateToPicker.Enabled = !isBusy;
            auditLogsGrid.Enabled = !isBusy;

            if (!string.IsNullOrWhiteSpace(status))
            {
                statusLabel.Text = status;
            }
        }

        private void RefreshFilterCollections()
        {
            if (_allLogs is null || _allLogs.Rows.Count == 0)
            {
                InitializeFilterOptions();
                return;
            }

            RefreshSingleFilterCombo(roleFilterComboBox, "actor_role");
            RefreshSingleFilterCombo(actionTypeFilterComboBox, "action_type");
            RefreshSingleFilterCombo(moduleFilterComboBox, "module");
            RefreshSingleFilterCombo(severityFilterComboBox, "severity");
        }

        private void RefreshSingleFilterCombo(ComboBox comboBox, string columnName)
        {
            string selected = GetSelectedFilterValue(comboBox);
            var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (_allLogs is not null && _allLogs.Columns.Contains(columnName))
            {
                foreach (DataRow row in _allLogs.Rows)
                {
                    string? value = row[columnName]?.ToString()?.Trim();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        values.Add(value);
                    }

                    if (values.Count >= 200)
                    {
                        break;
                    }
                }
            }

            comboBox.BeginUpdate();
            comboBox.Items.Clear();
            comboBox.Items.Add("All");
            comboBox.Items.AddRange(values
                .OrderBy(static value => value, StringComparer.CurrentCultureIgnoreCase)
                .Cast<object>()
                .ToArray());

            comboBox.SelectedItem = comboBox.Items.Cast<object>()
                .FirstOrDefault(item => string.Equals(item.ToString(), selected, StringComparison.CurrentCultureIgnoreCase))
                ?? "All";
            comboBox.EndUpdate();
        }

        private void printButton_Click(object? sender, EventArgs e)
        {
            if (_allLogs is null || _allLogs.Rows.Count == 0)
            {
                MessageBox.Show(this, "No audit logs to print.", "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SetBusyState(true, "Preparing report preview...");

                string userSearch = userSearchTextBox.Text.Trim();
                string roleFilter = GetSelectedFilterValue(roleFilterComboBox);
                string actionTypeFilter = GetSelectedFilterValue(actionTypeFilterComboBox);
                string moduleFilter = GetSelectedFilterValue(moduleFilterComboBox);
                string severityFilter = GetSelectedFilterValue(severityFilterComboBox);

                string periodCaption = $"Period: {dateFromPicker.Value:yyyy-MM-dd} to {dateToPicker.Value:yyyy-MM-dd}";
                string filterCaption = $"Filters: User={(string.IsNullOrWhiteSpace(userSearch) ? "All" : userSearch)} | Role={roleFilter} | Action={actionTypeFilter} | Module={moduleFilter} | Severity={severityFilter}";
                string printedBy = string.IsNullOrWhiteSpace(_user.FullName)
                    ? _user.Username
                    : $"{_user.FullName} ({_user.Username})";

                DataTable printableRows = BuildPrintableAuditRows(_allLogs);
                var document = new UserAuditLogsReportDocumentData(
                    "User Audit Logs Report",
                    periodCaption,
                    filterCaption,
                    printedBy,
                    DateTime.Now,
                    printableRows);

                var helper = new UserAuditLogsPrintHelper(document);
                using var previewForm = new PrintPreviewForm(
                    "User Audit Logs Report",
                    periodCaption,
                    helper,
                    "UserAuditLogs");
                previewForm.ShowDialog(this);
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

        private static DataTable BuildPrintableAuditRows(DataTable source)
        {
            var table = new DataTable();
            table.Columns.Add("Log ID", typeof(string));
            table.Columns.Add("Date/Time", typeof(string));
            table.Columns.Add("User", typeof(string));
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("Role", typeof(string));
            table.Columns.Add("Action", typeof(string));
            table.Columns.Add("Module", typeof(string));
            table.Columns.Add("Entity ID", typeof(string));
            table.Columns.Add("Severity", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Activity Summary", typeof(string));

            foreach (DataRow row in source.Rows)
            {
                table.Rows.Add(
                    Convert.ToString(row["log_id"]) ?? string.Empty,
                    Convert.ToString(row["formatted_date"]) ?? string.Empty,
                    Convert.ToString(row["full_name"]) ?? string.Empty,
                    Convert.ToString(row["username"]) ?? string.Empty,
                    Convert.ToString(row["actor_role"]) ?? string.Empty,
                    Convert.ToString(row["action_type"]) ?? string.Empty,
                    Convert.ToString(row["module"]) ?? string.Empty,
                    Convert.ToString(row["entity_id"]) ?? string.Empty,
                    Convert.ToString(row["severity"]) ?? string.Empty,
                    Convert.ToString(row["description"]) ?? string.Empty,
                    Convert.ToString(row["activity_summary"]) ?? string.Empty);
            }

            return table;
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
                ActionType = rowData.Field<string>("action_type") ?? string.Empty,
                Module = rowData.Field<string>("module") ?? string.Empty,
                EntityName = rowData.Field<string>("entity_name") ?? string.Empty,
                EntityId = rowData.Field<string>("entity_id") ?? string.Empty,
                Severity = rowData.Field<string>("severity") ?? string.Empty,
                Description = rowData.Field<string>("description") ?? string.Empty,
                ActivitySummary = rowData.Field<string>("activity_summary") ?? string.Empty
            };

            using var auditingForm = new AuditingForm(detail);
            auditingForm.ShowDialog(this);
        }

        private static string GetSelectedFilterValue(ComboBox comboBox)
        {
            string value = comboBox.SelectedItem?.ToString()?.Trim() ?? "All";
            return string.IsNullOrWhiteSpace(value) ? "All" : value;
        }
    }
}
