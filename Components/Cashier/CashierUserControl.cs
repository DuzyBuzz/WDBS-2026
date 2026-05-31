using System.Data;
using System.ComponentModel;
using System.Globalization;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.Collection;
using WDBS_2026.Forms.Pickers;
using WDBS_2026.Services.Cashier;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Components.Cashier
{
    public partial class CashierUserControl : UserControl
    {
        private static readonly Dictionary<string, string> FriendlyHeaderOverrides = new(StringComparer.OrdinalIgnoreCase)
        {
            ["concessionaire_id"] = "ID",
            ["concessionaire_code"] = "Account No",
            ["concessionaire_name"] = "Concessionaire Name",
            ["zone_id"] = "Zone",
            ["meter_no"] = "Meter No",
            ["service_type"] = "Service Type",
            ["outstanding_balance"] = "Outstanding",
            ["remaining_scf"] = "SCF Balance",
            ["unpaid_bill_count"] = "Unpaid Bills",
            ["last_billing_date"] = "Last Billing Date",
            ["collection_id"] = "Collection ID",
            ["or_number"] = "OR #",
            ["collection_date"] = "Collection Date",
            ["bill_numbers"] = "Bill Nos",
            ["total_current_bill"] = "Current Bill",
            ["total_arrears"] = "Arrears",
            ["total_penalty"] = "Penalty",
            ["total_tax"] = "Tax",
            ["total_scf"] = "SCF",
            ["total_others"] = "Others",
            ["grand_total"] = "Grand Total",
            ["amount_received"] = "Amount Received",
            ["change_amount"] = "Change",
            ["total_paid_amount"] = "Collected",
            ["payment_type"] = "Payment Type",
            ["payment_reference"] = "Reference",
            ["payor_name"] = "Payor",
            ["created_by_full_name"] = "Created By",
            ["created_by_username"] = "Created By (User)",
            ["voided_by_full_name"] = "Voided By",
            ["voided_by_username"] = "Voided By (User)",
            ["created_at"] = "Created At",
            ["updated_at"] = "Updated At",
            ["voided_at"] = "Voided At",
            ["is_discounted"] = "Discounted",
            ["is_tax_exempt"] = "Tax Exempt",
            ["is_due_exempt"] = "Due Exempt",
            ["remarks"] = "Remarks"
        };

        private readonly AuthenticatedUserDto _user;
        private readonly DataTable _selectedConcessionaires = CreateSelectedConcessionairesTable();
        private readonly ContextMenuStrip _selectedGridMenu = new();
        private readonly ToolStripMenuItem _selectedGridDeleteMenuItem = new("Delete Selected");
        private readonly ContextMenuStrip _concessionaireGridMenu = new();
        private readonly ToolStripMenuItem _printLedgerMenuItem = new("Print Ledger");
        private readonly ContextMenuStrip _collectionGridMenu = new();
        private readonly ToolStripMenuItem _voidCollectionMenuItem = new("Void Collection");

        private bool _isInitializing;
        private bool _isLoadingConcessionaires;
        private bool _isLoadingCollections;


        public CashierUserControl(AuthenticatedUserDto user)
        {
            _user = user;
            InitializeComponent();
            ConfigureGrids();
            ConfigureContextMenus();
            WireEvents();
            ApplyTheme();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            if (_isInitializing)
            {
                return;
            }

            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            _isInitializing = true;

            try
            {
                SetBusyState(true, "Loading cashier workspace...");

                if (!await TryEnsureCashierConnectionAsync())
                {
                    return;
                }

                paymentTypeComboBox.Items.Clear();
                paymentTypeComboBox.Items.AddRange(new object[] { "Cash", "GCash", "Check", "Bank Transfer", "Others" });
                paymentTypeComboBox.SelectedIndex = 0;

                billingDatePicker.Value = DateTime.Today;
                collectionDateTimePicker.Value = DateTime.Now;
                amountReceivedNUD.Value = 0;
                paymentForOthersNUD.Value = 0;

                await LoadNextOrNumberAsync();
                await LoadConcessionairesAsync();
                await LoadCollectionsAsync();


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isInitializing = false;
                SetBusyState(false);
            }
        }

        private async Task<bool> TryEnsureCashierConnectionAsync()
        {
            try
            {
                DBConfig.SetConnectionString(_user.Role);
                await using MySqlConnection connection = DBConfig.GetConnection();
                await connection.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    this,
                    "Unable to connect using CashierConnection.\n\n" +
                    "Please verify host, user, password, and database in db_connection.txt.\n\n" +
                    ex.Message,
                    "Cashier Database Connection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.ShellBackgroundColor;

            AppTheme.ApplyPageTitle(titleLabel);
            AppTheme.ApplySubtitle(subtitleLabel);

            AppTheme.ApplyCardTitle(concessionaireHeaderLabel);
            AppTheme.ApplyCardTitle(collectionHeaderLabel);
            AppTheme.ApplyCardTitle(selectedHeaderLabel);
            AppTheme.ApplyCardTitle(paymentHeaderLabel);

            foreach (Label label in new[] { collectionDateLabel, orLabel, dateLabel, paymentTypeLabel, payorLabel, referenceLabel, amountLabel, othersLabel, remarksLabel })
            {
                label.Font = AppTheme.SectionFont;
                label.ForeColor = AppTheme.BodyTextColor;
            }

            foreach (TextBox textBox in new[] { searchConcessionaireTextBox, collectionSearchTextBox, ORTextBox, payorNameTextbox, paymentReferenceTextBox, remarksTextBox })
            {
                AppTheme.ApplyInput(textBox);
            }

            AppTheme.ApplySeverityButton(searchConcessionaireButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(refreshConcessionaireButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(searchCollectionButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(refreshCollectionButton, ButtonSeverity.Neutral);
            AppTheme.ApplySeverityButton(previewBillButton, ButtonSeverity.Primary);
            AppTheme.ApplySeverityButton(paymentForSCFButton, ButtonSeverity.Warning);

            paymentTypeComboBox.Font = AppTheme.BodyFont;
            paymentTypeComboBox.ForeColor = AppTheme.BodyTextColor;
        }

        private void ConfigureGrids()
        {
            ConfigureGridBase(concessionaireGrid, readOnly: true, DataGridViewSelectionMode.FullRowSelect, multiSelect: false);
            ConfigureGridBase(collectionGrid, readOnly: true, DataGridViewSelectionMode.FullRowSelect, multiSelect: false);
            ConfigureGridBase(selectedGrid, readOnly: true, DataGridViewSelectionMode.FullRowSelect, multiSelect: true);

            concessionaireGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            collectionGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            selectedGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            selectedGrid.DataSource = _selectedConcessionaires;
            ApplyFriendlyHeaders(selectedGrid);
            ApplySelectedGridLayout();
        }

        private static void ConfigureGridBase(DataGridView grid, bool readOnly, DataGridViewSelectionMode selectionMode, bool multiSelect)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.MultiSelect = multiSelect;
            grid.ReadOnly = readOnly;
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

        private void ConfigureContextMenus()
        {
            _selectedGridMenu.Items.Add(_selectedGridDeleteMenuItem);
            _selectedGridDeleteMenuItem.Click += selectedGridDeleteMenuItem_Click;

            _concessionaireGridMenu.Items.Add(_printLedgerMenuItem);
            _printLedgerMenuItem.Click += printLedgerMenuItem_Click;

            _collectionGridMenu.Items.Add(_voidCollectionMenuItem);
            _voidCollectionMenuItem.Click += voidCollectionMenuItem_Click;
        }

        private void WireEvents()
        {
            searchConcessionaireButton.Click += searchConcessionaireButton_Click;
            refreshConcessionaireButton.Click += refreshConcessionaireButton_Click;
            searchConcessionaireTextBox.KeyDown += searchConcessionaireTextBox_KeyDown;
            concessionaireGrid.CellMouseDown += concessionaireGrid_CellMouseDown;
            concessionaireGrid.CellDoubleClick += concessionaireGrid_CellDoubleClick;
            concessionaireGrid.KeyDown += concessionaireGrid_KeyDown;

            searchCollectionButton.Click += searchCollectionButton_Click;
            refreshCollectionButton.Click += refreshCollectionButton_Click;
            collectionSearchTextBox.KeyDown += collectionSearchTextBox_KeyDown;
            billingDatePicker.ValueChanged += billingDatePicker_ValueChanged;

            selectedGrid.CellMouseDown += selectedGrid_CellMouseDown;
            selectedGrid.KeyDown += selectedGrid_KeyDown;

            collectionGrid.CellMouseDown += collectionGrid_CellMouseDown;

            previewBillButton.Click += previewBillButton_Click;
            paymentForSCFButton.Click += paymentForSCFButton_Click;
        }

        private async Task LoadConcessionairesAsync()
        {
            if (_isLoadingConcessionaires)
            {
                return;
            }

            _isLoadingConcessionaires = true;
            try
            {
                string search = searchConcessionaireTextBox.Text.Trim();
                DataTable rows = await CollectionService.GetConcessionaireLookupAsync(_user.Role, search);
                concessionaireGrid.DataSource = rows;

                ApplyFriendlyHeaders(concessionaireGrid);
                ApplyConcessionaireGridLayout();


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Concessionaire Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingConcessionaires = false;
            }
        }

        private async Task LoadCollectionsAsync()
        {
            if (_isLoadingCollections)
            {
                return;
            }

            _isLoadingCollections = true;
            try
            {
                string search = collectionSearchTextBox.Text.Trim();
                DataTable rows = await CollectionService.GetCollectionHistoryByDateAsync(_user.Role, billingDatePicker.Value.Date, search);
                collectionGrid.DataSource = rows;

                ApplyFriendlyHeaders(collectionGrid);
                ApplyCollectionGridLayout();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Collection Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingCollections = false;
            }
        }

        private async Task LoadNextOrNumberAsync()
        {
            try
            {
                ORTextBox.Text = await CollectionService.GetNextOrNumberAsync(_user.Role);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "OR Number Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static DataTable CreateSelectedConcessionairesTable()
        {
            var table = new DataTable();
            table.Columns.Add("concessionaire_id", typeof(int));
            table.Columns.Add("concessionaire_code", typeof(string));
            table.Columns.Add("concessionaire_name", typeof(string));
            table.Columns.Add("address", typeof(string));
            table.Columns.Add("outstanding_balance", typeof(decimal));
            table.Columns.Add("remaining_scf", typeof(decimal));
            table.Columns.Add("unpaid_bill_count", typeof(int));
            return table;
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
            if (FriendlyHeaderOverrides.TryGetValue(rawName, out string? label))
            {
                return label;
            }

            string withSpaces = rawName.Replace('_', ' ').Trim();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(withSpaces.ToLowerInvariant());
        }

        private void ApplyConcessionaireGridLayout()
        {
            HideColumns(concessionaireGrid, "concessionaire_id", "service_type", "unpaid_bill_count");

            EnsureCheckBoxColumn(concessionaireGrid, "is_discounted");
            EnsureCheckBoxColumn(concessionaireGrid, "is_tax_exempt");
            EnsureCheckBoxColumn(concessionaireGrid, "is_due_exempt");

            SetAlignment(concessionaireGrid, DataGridViewContentAlignment.MiddleCenter, "zone_id", "is_discounted", "is_tax_exempt", "is_due_exempt");
            SetFormat(concessionaireGrid, "N2", "outstanding_balance", "remaining_scf");
            SetFormat(concessionaireGrid, "yyyy-MM-dd", "last_billing_date");
            SetAlignment(concessionaireGrid, DataGridViewContentAlignment.MiddleRight, "outstanding_balance", "remaining_scf");

            SetColumnDisplayIndex(concessionaireGrid, "concessionaire_code", 0);
            SetColumnDisplayIndex(concessionaireGrid, "concessionaire_name", 1);
            SetColumnDisplayIndex(concessionaireGrid, "outstanding_balance", 2);
            SetColumnDisplayIndex(concessionaireGrid, "remaining_scf", 3);
            SetColumnDisplayIndex(concessionaireGrid, "zone_id", 4);
            SetColumnDisplayIndex(concessionaireGrid, "address", 5);
            SetColumnDisplayIndex(concessionaireGrid, "meter_no", 6);
            SetColumnDisplayIndex(concessionaireGrid, "is_discounted", 7);
            SetColumnDisplayIndex(concessionaireGrid, "is_tax_exempt", 8);
            SetColumnDisplayIndex(concessionaireGrid, "is_due_exempt", 9);
            SetColumnDisplayIndex(concessionaireGrid, "last_billing_date", 10);
        }

        private void ApplyCollectionGridLayout()
        {
            HideColumns(
                collectionGrid,
                "collection_id",
                "created_by_user_id",
                "voided_by_user_id",
                "created_by_role",
                "created_by_is_active");

            SetFormat(
                collectionGrid,
                "N2",
                "total_current_bill",
                "total_arrears",
                "total_penalty",
                "total_tax",
                "total_scf",
                "total_others",
                "grand_total",
                "amount_received",
                "change_amount",
                "total_paid_amount",
                "uncollected",
                "total_discount");

            SetFormat(collectionGrid, "yyyy-MM-dd HH:mm", "collection_date", "created_at", "updated_at", "voided_at");
            SetAlignment(collectionGrid, DataGridViewContentAlignment.MiddleRight, "total_current_bill", "total_arrears", "total_penalty", "total_tax", "total_scf", "total_others", "grand_total", "amount_received", "change_amount", "total_paid_amount", "uncollected", "total_discount");

            SetColumnDisplayIndex(collectionGrid, "collection_date", 0);
            SetColumnDisplayIndex(collectionGrid, "payor_name", 1);
            SetColumnDisplayIndex(collectionGrid, "total_paid_amount", 2);
        }

        private void ApplySelectedGridLayout()
        {
            HideColumns(selectedGrid, "concessionaire_id", "address");
            SetFormat(selectedGrid, "N2", "outstanding_balance", "remaining_scf");
            SetAlignment(selectedGrid, DataGridViewContentAlignment.MiddleRight, "outstanding_balance", "remaining_scf", "unpaid_bill_count");
        }

        private static void SetColumnDisplayIndex(DataGridView grid, string columnName, int displayIndex)
        {
            if (grid.Columns[columnName] is { Visible: true } column)
            {
                column.DisplayIndex = Math.Min(displayIndex, grid.Columns.Count - 1);
            }
        }

        private static void SetFormat(DataGridView grid, string format, params string[] columns)
        {
            foreach (string name in columns)
            {
                if (grid.Columns[name] is not { } column)
                {
                    continue;
                }

                column.DefaultCellStyle.Format = format;
            }
        }

        private static void SetAlignment(DataGridView grid, DataGridViewContentAlignment alignment, params string[] columns)
        {
            foreach (string name in columns)
            {
                if (grid.Columns[name] is not { } column)
                {
                    continue;
                }

                column.DefaultCellStyle.Alignment = alignment;
            }
        }

        private static void HideColumns(DataGridView grid, params string[] columns)
        {
            foreach (string name in columns)
            {
                if (grid.Columns[name] is { } column)
                {
                    column.Visible = false;
                }
            }
        }

        private static void EnsureCheckBoxColumn(DataGridView grid, string columnName)
        {
            if (grid.Columns[columnName] is not { } existingColumn)
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

            grid.Columns.RemoveAt(insertIndex);
            grid.Columns.Insert(insertIndex, replacement);

            if (replacement.Visible)
            {
                replacement.DisplayIndex = displayIndex;
            }

            replacement.ThreeState = false;
            replacement.ReadOnly = true;
        }

        private static void MoveColumnsToRight(DataGridView grid, params string[] columns)
        {
            var targetColumns = columns
                .Where(name => grid.Columns[name] is { Visible: true })
                .Select(name => grid.Columns[name])
                .Where(column => column is not null)
                .Cast<DataGridViewColumn>()
                .ToList();

            if (targetColumns.Count == 0)
            {
                return;
            }

            int firstRightIndex = grid.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible) - targetColumns.Count;
            for (int i = 0; i < targetColumns.Count; i++)
            {
                targetColumns[i].DisplayIndex = firstRightIndex + i;
            }
        }

        private void SetBusyState(bool busy, string? message = null)
        {
            UseWaitCursor = busy;

            foreach (Control control in new Control[]
                     {
                         searchConcessionaireButton,
                         refreshConcessionaireButton,
                         searchCollectionButton,
                         refreshCollectionButton,
                         previewBillButton,
                         paymentForSCFButton,
                     })
            {
                control.Enabled = !busy;
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
            }
        }

        private void AddConcessionaireToSelected(DataGridViewRow row)
        {
            if (!TryGetIntCellValue(row, "concessionaire_id", out int concessionaireId) || concessionaireId <= 0)
            {
                return;
            }

            bool alreadySelected = _selectedConcessionaires.AsEnumerable().Any(r => Convert.ToInt32(r["concessionaire_id"], CultureInfo.InvariantCulture) == concessionaireId);
            if (alreadySelected)
            {
                return;
            }

            DataRow newRow = _selectedConcessionaires.NewRow();
            newRow["concessionaire_id"] = concessionaireId;
            newRow["concessionaire_code"] = GetCellString(row, "concessionaire_code");
            newRow["concessionaire_name"] = GetCellString(row, "concessionaire_name");
            newRow["address"] = GetCellString(row, "address");
            newRow["outstanding_balance"] = GetCellDecimal(row, "outstanding_balance");
            newRow["remaining_scf"] = GetCellDecimal(row, "remaining_scf");
            newRow["unpaid_bill_count"] = GetCellInt(row, "unpaid_bill_count");
            _selectedConcessionaires.Rows.Add(newRow);

            UpdatePayorNameFromSelection();
        }

        private void RemoveSelectedConcessionaireRows()
        {
            if (selectedGrid.SelectedRows.Count == 0)
            {
                return;
            }

            List<DataGridViewRow> rowsToDelete = selectedGrid.SelectedRows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToList();
            foreach (DataGridViewRow row in rowsToDelete)
            {
                selectedGrid.Rows.Remove(row);
            }

            UpdatePayorNameFromSelection();
        }

        private bool TryBuildPreviewRequest(out CollectionPreviewRequest request, out string validationMessage)
        {
            request = new CollectionPreviewRequest(string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0m, Array.Empty<int>());
            validationMessage = string.Empty;

            IReadOnlyList<int> concessionaireIds = GetSelectedConcessionaireIds();
            if (concessionaireIds.Count == 0)
            {
                validationMessage = "Select at least one concessionaire before previewing.";
                return false;
            }

            if (!TryNormalizeOrNumber(ORTextBox.Text, out string normalizedOrNumber))
            {
                validationMessage = "OR number must be numeric.";
                return false;
            }

            string paymentType = Convert.ToString(paymentTypeComboBox.SelectedItem, CultureInfo.CurrentCulture) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(paymentType))
            {
                validationMessage = "Select payment type.";
                return false;
            }

            string payorName = payorNameTextbox.Text.Trim();
            if (string.IsNullOrWhiteSpace(payorName))
            {
                validationMessage = "Payor name is required.";
                return false;
            }

            request = new CollectionPreviewRequest(
                normalizedOrNumber,
                collectionDateTimePicker.Value,
                paymentType,
                paymentReferenceTextBox.Text.Trim(),
                remarksTextBox.Text.Trim(),
                payorName,
                paymentForOthersNUD.Value,
                concessionaireIds);

            ORTextBox.Text = normalizedOrNumber;
            return true;
        }

        private bool TryBuildCollectionPaymentRequest(out CollectionPaymentRequest request, out string validationMessage)
        {
            request = new CollectionPaymentRequest(string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0m, 0m, 0, Array.Empty<int>());
            validationMessage = string.Empty;

            if (!TryBuildPreviewRequest(out CollectionPreviewRequest previewRequest, out validationMessage))
            {
                return false;
            }

            if (amountReceivedNUD.Value <= 0)
            {
                validationMessage = "Amount received must be greater than zero.";
                return false;
            }

            request = new CollectionPaymentRequest(
                previewRequest.OrNumber,
                previewRequest.PaymentDate,
                previewRequest.PaymentType,
                previewRequest.ReferenceNumber,
                previewRequest.Remarks,
                previewRequest.PayorName,
                amountReceivedNUD.Value,
                previewRequest.Others,
                _user.UserId,
                previewRequest.ConcessionaireIds);

            return true;
        }

        private bool TryBuildScfPaymentRequest(out CollectionScfPaymentRequest request, out string validationMessage)
        {
            request = new CollectionScfPaymentRequest(string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0m, 0m, 0, 0);
            validationMessage = string.Empty;

            if (!TryNormalizeOrNumber(ORTextBox.Text, out string normalizedOrNumber))
            {
                validationMessage = "OR number must be numeric.";
                return false;
            }

            IReadOnlyList<int> concessionaireIds = GetSelectedConcessionaireIds();
            if (concessionaireIds.Count != 1)
            {
                validationMessage = "Select exactly one concessionaire for SCF/Others posting.";
                return false;
            }

            string paymentType = Convert.ToString(paymentTypeComboBox.SelectedItem, CultureInfo.CurrentCulture) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(paymentType))
            {
                validationMessage = "Select payment type.";
                return false;
            }

            string payorName = payorNameTextbox.Text.Trim();
            if (string.IsNullOrWhiteSpace(payorName))
            {
                validationMessage = "Payor name is required.";
                return false;
            }

            decimal scfAmount = amountReceivedNUD.Value;
            decimal others = paymentForOthersNUD.Value;
            if (scfAmount <= 0 && others <= 0)
            {
                validationMessage = "Provide SCF amount and/or Others amount.";
                return false;
            }

            request = new CollectionScfPaymentRequest(
                normalizedOrNumber,
                collectionDateTimePicker.Value,
                paymentType,
                paymentReferenceTextBox.Text.Trim(),
                remarksTextBox.Text.Trim(),
                payorName,
                scfAmount,
                others,
                _user.UserId,
                concessionaireIds[0]);

            ORTextBox.Text = normalizedOrNumber;
            return true;
        }

        private IReadOnlyList<int> GetSelectedConcessionaireIds()
        {
            return _selectedConcessionaires
                .AsEnumerable()
                .Select(row => Convert.ToInt32(row["concessionaire_id"], CultureInfo.InvariantCulture))
                .Where(id => id > 0)
                .Distinct()
                .ToArray();
        }

        private void UpdatePayorNameFromSelection()
        {
            int selectedCount = _selectedConcessionaires.Rows.Count;

            if (selectedCount == 1)
            {
                string concessionaireName = Convert.ToString(_selectedConcessionaires.Rows[0]["concessionaire_name"], CultureInfo.CurrentCulture) ?? string.Empty;
                payorNameTextbox.Text = concessionaireName;
                return;
            }

            payorNameTextbox.Clear();
        }

        private static bool TryNormalizeOrNumber(string rawValue, out string normalized)
        {
            normalized = string.Empty;
            string digits = new(rawValue.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(digits))
            {
                return false;
            }

            normalized = digits.PadLeft(7, '0');
            return true;
        }

        private static bool TryGetIntCellValue(DataGridViewRow row, string columnName, out int value)
        {
            value = 0;
            if (row.DataGridView is null || !row.DataGridView.Columns.Contains(columnName))
            {
                return false;
            }

            object? raw = row.Cells[columnName].Value;
            if (raw is null || raw == DBNull.Value)
            {
                return false;
            }

            return int.TryParse(Convert.ToString(raw, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        }

        private static string GetCellString(DataGridViewRow row, string columnName)
        {
            if (row.DataGridView is null || !row.DataGridView.Columns.Contains(columnName))
            {
                return string.Empty;
            }

            return Convert.ToString(row.Cells[columnName].Value, CultureInfo.CurrentCulture) ?? string.Empty;
        }

        private static int GetCellInt(DataGridViewRow row, string columnName)
        {
            if (!TryGetIntCellValue(row, columnName, out int value))
            {
                return 0;
            }

            return value;
        }

        private static decimal GetCellDecimal(DataGridViewRow row, string columnName)
        {
            if (row.DataGridView is null || !row.DataGridView.Columns.Contains(columnName))
            {
                return 0m;
            }

            object? raw = row.Cells[columnName].Value;
            if (raw is null || raw == DBNull.Value)
            {
                return 0m;
            }

            return decimal.TryParse(Convert.ToString(raw, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                ? parsed
                : 0m;
        }

        private static bool IsVoidableStatus(DataGridViewRow row)
        {
            if (row.DataGridView is null || !row.DataGridView.Columns.Contains("status"))
            {
                return true;
            }

            string status = Convert.ToString(row.Cells["status"].Value, CultureInfo.InvariantCulture) ?? string.Empty;
            return !status.Equals("VOIDED", StringComparison.OrdinalIgnoreCase);
        }

        private async void searchConcessionaireButton_Click(object? sender, EventArgs e)
        {
            await LoadConcessionairesAsync();
        }

        private async void refreshConcessionaireButton_Click(object? sender, EventArgs e)
        {
            searchConcessionaireTextBox.Clear();
            await LoadConcessionairesAsync();
        }

        private async void searchConcessionaireTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            await LoadConcessionairesAsync();
        }

        private void concessionaireGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= concessionaireGrid.Rows.Count)
            {
                return;
            }

            AddConcessionaireToSelected(concessionaireGrid.Rows[e.RowIndex]);
        }

        private async void concessionaireGrid_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.L)
            {
                return;
            }

            if (concessionaireGrid.CurrentRow is not { } row || row.IsNewRow)
            {
                return;
            }

            string concessionaireCode = GetCellString(row, "concessionaire_code").Trim();
            if (string.IsNullOrWhiteSpace(concessionaireCode))
            {
                MessageBox.Show(this, "Unable to determine concessionaire account code for ledger preview.", "Ledger Preview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string concessionaireName = GetCellString(row, "concessionaire_name").Trim();

            e.SuppressKeyPress = true;
            e.Handled = true;

            await ShowLedgerPreviewAsync(concessionaireCode, concessionaireName);
        }

        private void concessionaireGrid_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.RowIndex >= concessionaireGrid.Rows.Count)
            {
                return;
            }

            concessionaireGrid.ClearSelection();
            DataGridViewRow row = concessionaireGrid.Rows[e.RowIndex];
            row.Selected = true;

            DataGridViewCell? firstVisibleCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(cell => cell.Visible);
            if (firstVisibleCell is not null)
            {
                concessionaireGrid.CurrentCell = firstVisibleCell;
            }

            string concessionaireCode = GetCellString(row, "concessionaire_code").Trim();
            _printLedgerMenuItem.Enabled = !string.IsNullOrWhiteSpace(concessionaireCode);

            Point menuPosition = new(Cursor.Position.X + 10, Cursor.Position.Y);
            _concessionaireGridMenu.Show(menuPosition);
        }

        private async void printLedgerMenuItem_Click(object? sender, EventArgs e)
        {
            if (concessionaireGrid.CurrentRow is not { } row || row.IsNewRow)
            {
                return;
            }

            string concessionaireCode = GetCellString(row, "concessionaire_code").Trim();
            if (string.IsNullOrWhiteSpace(concessionaireCode))
            {
                MessageBox.Show(this, "Unable to determine concessionaire account code for ledger preview.", "Ledger Preview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string concessionaireName = GetCellString(row, "concessionaire_name").Trim();

            await ShowLedgerPreviewAsync(concessionaireCode, concessionaireName);
        }

        private async Task ShowLedgerPreviewAsync(string concessionaireCode, string concessionaireName)
        {

            try
            {
                await LedgerPrintHelper.ShowPreviewAsync(
                    this,
                    _user.Role,
                    new CollectionLedgerRequest(concessionaireCode, concessionaireName, _user.FullName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ledger Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void searchCollectionButton_Click(object? sender, EventArgs e)
        {
            await LoadCollectionsAsync();
        }

        private async void refreshCollectionButton_Click(object? sender, EventArgs e)
        {
            collectionSearchTextBox.Clear();
            billingDatePicker.Value = DateTime.Today;
            await LoadCollectionsAsync();
        }

        private async void collectionSearchTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            await LoadCollectionsAsync();
        }

        private async void billingDatePicker_ValueChanged(object? sender, EventArgs e)
        {
            if (_isInitializing)
            {
                return;
            }

            await LoadCollectionsAsync();
        }

        private void selectedGrid_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.RowIndex >= selectedGrid.Rows.Count)
            {
                return;
            }

            selectedGrid.ClearSelection();
            DataGridViewRow row = selectedGrid.Rows[e.RowIndex];
            row.Selected = true;

            DataGridViewCell? firstVisibleCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(cell => cell.Visible);
            if (firstVisibleCell is not null)
            {
                selectedGrid.CurrentCell = firstVisibleCell;
            }

            Point menuPosition = new(Cursor.Position.X + 10, Cursor.Position.Y);
            _selectedGridMenu.Show(menuPosition);
        }

        private void selectedGrid_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }

            e.SuppressKeyPress = true;
            RemoveSelectedConcessionaireRows();
        }

        private void selectedGridDeleteMenuItem_Click(object? sender, EventArgs e)
        {
            RemoveSelectedConcessionaireRows();
        }

        private void collectionGrid_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.RowIndex >= collectionGrid.Rows.Count)
            {
                return;
            }

            collectionGrid.ClearSelection();
            DataGridViewRow row = collectionGrid.Rows[e.RowIndex];
            row.Selected = true;

            DataGridViewCell? firstVisibleCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(cell => cell.Visible);
            if (firstVisibleCell is not null)
            {
                collectionGrid.CurrentCell = firstVisibleCell;
            }

            _voidCollectionMenuItem.Enabled = IsVoidableStatus(row);

            Point menuPosition = new(Cursor.Position.X + 10, Cursor.Position.Y);
            _collectionGridMenu.Show(menuPosition);
        }

        private async void voidCollectionMenuItem_Click(object? sender, EventArgs e)
        {
            if (collectionGrid.CurrentRow is not { } row || row.IsNewRow)
            {
                return;
            }

            if (!TryGetIntCellValue(row, "collection_id", out int collectionId) || collectionId <= 0)
            {
                MessageBox.Show(this, "Unable to determine selected collection id.", "Void Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsVoidableStatus(row))
            {
                MessageBox.Show(this, "Selected collection is already voided.", "Void Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                this,
                $"Void collection #{collectionId}? This action cannot be undone.",
                "Confirm Void",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            if (!VoidReasonPromptDialog.TryGetReason(
                    this,
                    "Void Collection",
                    $"Enter reason for voiding collection #{collectionId}:",
                    out string voidRemarks))
            {
                return;
            }

            try
            {
                SetBusyState(true, "Voiding collection...");
                await CollectionService.VoidCollectionAsync(_user.Role, collectionId, _user.UserId, voidRemarks);

                await LoadCollectionsAsync();
                await LoadConcessionairesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Void Collection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
            }
        }

        private async void refreshOrNumberButton_Click(object? sender, EventArgs e)
        {
            await LoadNextOrNumberAsync();
        }

        private async void previewBillButton_Click(object? sender, EventArgs e)
        {
            if (!TryBuildCollectionPaymentRequest(out CollectionPaymentRequest paymentRequest, out string validationMessage))
            {
                if (!string.IsNullOrWhiteSpace(validationMessage))
                {
                    MessageBox.Show(this, validationMessage, "Collection Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            try
            {
                SetBusyState(true, "Preparing collection preview...");

                var previewRequest = new CollectionPreviewRequest(
                    paymentRequest.OrNumber,
                    paymentRequest.PaymentDate,
                    paymentRequest.PaymentType,
                    paymentRequest.ReferenceNumber,
                    paymentRequest.Remarks,
                    paymentRequest.PayorName,
                    paymentRequest.Others,
                    paymentRequest.ConcessionaireIds);

                DataTable previewRows = await CollectionService.PreviewCollectionAsync(_user.Role, previewRequest);

                using var previewForm = new BillPreviewForm(previewRows);
                DialogResult previewResult = previewForm.ShowDialog(this);
                if (previewResult != DialogResult.OK || !previewForm.IsConfirmed)
                {
                    return;
                }

                await CollectionService.PostCollectionAsync(_user.Role, paymentRequest);

                _selectedConcessionaires.Clear();
                UpdatePayorNameFromSelection();
                amountReceivedNUD.Value = 0;
                paymentForOthersNUD.Value = 0;
                paymentReferenceTextBox.Clear();

                await LoadNextOrNumberAsync();
                await LoadCollectionsAsync();
                await LoadConcessionairesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Post Collection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
            }
        }

        private async void paymentForSCFButton_Click(object? sender, EventArgs e)
        {
            if (!TryBuildScfPaymentRequest(out CollectionScfPaymentRequest request, out string validationMessage))
            {
                if (!string.IsNullOrWhiteSpace(validationMessage))
                {
                    MessageBox.Show(this, validationMessage, "Collection Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                this,
                "Post SCF/Others collection for selected concessionaire?",
                "Confirm SCF Posting",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                SetBusyState(true, "Posting SCF/Others collection...");

                await CollectionService.PostScfCollectionAsync(_user.Role, request);

                _selectedConcessionaires.Clear();
                UpdatePayorNameFromSelection();
                amountReceivedNUD.Value = 0;
                paymentForOthersNUD.Value = 0;
                paymentReferenceTextBox.Clear();

                await LoadNextOrNumberAsync();
                await LoadCollectionsAsync();
                await LoadConcessionairesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Post SCF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
            }
        }

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void leftLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}