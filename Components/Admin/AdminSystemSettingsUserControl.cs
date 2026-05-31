using System.ComponentModel;
using System.Data;
using System.Globalization;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Models;
using WDBS_2026.Services.Admin;

namespace WDBS_2026.Components.Admin
{
    public partial class AdminSystemSettingsUserControl : UserControl
    {
        private readonly AuthenticatedUserDto _user;
        private bool _isLoading;
        private Dictionary<string, string> _loadedSettings = new(StringComparer.OrdinalIgnoreCase);
        private DataTable _servicesTable = new();
        private DataTable _zonesTable = new();
        private int _nextServiceId = 1;
        private int _nextZoneId = 1;

        public AdminSystemSettingsUserControl()
            : this(new AuthenticatedUserDto
            {
                UserId = 0,
                Username = "designer",
                FullName = "Dashboard Designer",
                Role = UserRole.Admin
            })
        {
        }

        public AdminSystemSettingsUserControl(AuthenticatedUserDto user)
        {
            _user = user;
            InitializeComponent();
            ApplyTheme();
            ConfigureGrids();
            servicesGrid.DefaultValuesNeeded += servicesGrid_DefaultValuesNeeded;
            zonesGrid.DefaultValuesNeeded += zonesGrid_DefaultValuesNeeded;
            servicesGrid.UserDeletingRow += servicesGrid_UserDeletingRow;
            zonesGrid.UserDeletingRow += zonesGrid_UserDeletingRow;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsDesignerHosted())
            {
                return;
            }

            await LoadAllAsync();
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
            AppTheme.ApplySubtitle(statusLabel);

            foreach (Label label in new[]
                     {
                         systemSettingsSectionLabel,
                         discountPercentLabel,
                         discountThresholdLabel,
                         penalizeAfterDaysLabel,
                         penaltyPercentLabel,
                         taxPercentLabel,
                         servicesSectionLabel,
                         zonesSectionLabel
                     })
            {
                label.ForeColor = AppTheme.BodyTextColor;
                label.Font = AppTheme.BodyFont;
            }

            foreach (TextBox textBox in new[]
                     {
                         discountPercentTextBox,
                         discountThresholdTextBox,
                         penalizeAfterDaysTextBox,
                         penaltyPercentTextBox,
                         taxPercentTextBox
                     })
            {
                AppTheme.ApplyInput(textBox);
            }

            AppTheme.ApplySeverityButton(saveSettingsButton, ButtonSeverity.Primary);
        }

        private void ConfigureGrids()
        {
            foreach (DataGridView grid in new[] { servicesGrid, zonesGrid })
            {
                grid.AllowUserToAddRows = true;
                grid.AllowUserToDeleteRows = true;
                grid.AllowUserToResizeRows = false;
                grid.MultiSelect = false;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.RowHeadersVisible = false;
                grid.AutoGenerateColumns = true;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grid.BackgroundColor = AppTheme.SurfaceColor;
                grid.BorderStyle = BorderStyle.FixedSingle;
                grid.EnableHeadersVisualStyles = false;
                grid.GridColor = AppTheme.BorderColor;
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
        }

        private async Task LoadAllAsync()
        {
            if (_isLoading)
            {
                return;
            }

            try
            {
                _isLoading = true;
                SetBusyState(true, "Loading system settings, services, and zones...");

                Dictionary<string, string> settings = await AdminSystemSettingsService.GetSystemSettingsAsync(_user.Role);
                _loadedSettings = new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
                SetSettingText(discountPercentTextBox, settings, "discount_percent");
                SetSettingText(discountThresholdTextBox, settings, "discount_thresh_hold");
                SetSettingText(penalizeAfterDaysTextBox, settings, "penalize_after_days");
                SetSettingText(penaltyPercentTextBox, settings, "penalty_percent");
                SetSettingText(taxPercentTextBox, settings, "tax_percent");

                _servicesTable = await AdminSystemSettingsService.GetServicesAsync(_user.Role);
                _zonesTable = await AdminSystemSettingsService.GetZonesAsync(_user.Role);

                _nextServiceId = GetNextId(_servicesTable, "service_id");
                _nextZoneId = GetNextId(_zonesTable, "zone_id");

                servicesGrid.DataSource = _servicesTable;
                zonesGrid.DataSource = _zonesTable;

                ApplyServicesGridLayout();
                ApplyZonesGridLayout();

                statusLabel.ForeColor = AppTheme.MutedTextColor;
                statusLabel.Text = "Admin settings loaded.";
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = "Failed to load admin settings.";
                MessageBox.Show(this, ex.Message, "Admin System Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
                _isLoading = false;
            }
        }

        private static void SetSettingText(TextBox textBox, IReadOnlyDictionary<string, string> values, string key)
        {
            textBox.Text = values.TryGetValue(key, out string? value) ? value : string.Empty;
        }

        private void ApplyServicesGridLayout()
        {
            if (servicesGrid.Columns.Count == 0)
            {
                return;
            }

            SetHeader(servicesGrid, "service_id", "Service ID");
            SetHeader(servicesGrid, "service_type", "Service Type");
            SetHeader(servicesGrid, "pipe_size", "Pipe Size");
            SetHeader(servicesGrid, "min_rate", "Min Rate");
            SetHeader(servicesGrid, "rate_11_20", "Rate 11-20");
            SetHeader(servicesGrid, "rate_21_30", "Rate 21-30");
            SetHeader(servicesGrid, "rate_31_40", "Rate 31-40");
            SetHeader(servicesGrid, "rate_41_above", "Rate 41 Above");

            SetReadOnly(servicesGrid, "service_id", true);

            foreach (string moneyColumn in new[] { "min_rate", "rate_11_20", "rate_21_30", "rate_31_40", "rate_41_above" })
            {
                if (servicesGrid.Columns[moneyColumn] is { } column)
                {
                    column.DefaultCellStyle.Format = "N2";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (servicesGrid.Columns["service_id"] is { } idColumn)
            {
                idColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ApplyZonesGridLayout()
        {
            if (zonesGrid.Columns.Count == 0)
            {
                return;
            }

            SetHeader(zonesGrid, "zone_id", "Zone ID");
            SetHeader(zonesGrid, "zone_name", "Zone Name");

            SetReadOnly(zonesGrid, "zone_id", true);

            if (zonesGrid.Columns["zone_id"] is { } idColumn)
            {
                idColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                idColumn.FillWeight = 30F;
            }

            if (zonesGrid.Columns["zone_name"] is { } nameColumn)
            {
                nameColumn.FillWeight = 170F;
            }
        }

        private static void SetHeader(DataGridView grid, string columnName, string header)
        {
            if (grid.Columns[columnName] is { } column)
            {
                column.HeaderText = header;
            }
        }

        private static void SetReadOnly(DataGridView grid, string columnName, bool readOnly)
        {
            if (grid.Columns[columnName] is { } column)
            {
                column.ReadOnly = readOnly;
            }
        }

        private static int GetNextId(DataTable table, string columnName)
        {
            int maxId = 0;
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (!int.TryParse(Convert.ToString(row[columnName], CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out int id)
                    && !int.TryParse(Convert.ToString(row[columnName], CultureInfo.CurrentCulture), NumberStyles.Integer, CultureInfo.CurrentCulture, out id))
                {
                    continue;
                }

                if (id > maxId)
                {
                    maxId = id;
                }
            }

            return maxId + 1;
        }

        private void SetBusyState(bool isBusy, string? message = null)
        {
            foreach (Control control in new Control[]
                     {
                         settingsInputLayout,
                         saveSettingsButton,
                         servicesGrid,
                         zonesGrid
                     })
            {
                control.Enabled = !isBusy;
            }

            if (isBusy && !string.IsNullOrWhiteSpace(message))
            {
                statusLabel.ForeColor = AppTheme.MutedTextColor;
                statusLabel.Text = message;
            }
        }

        private Dictionary<string, string>? TryBuildSettingsPayload()
        {
            if (!TryParsePercent(discountPercentTextBox.Text.Trim(), "Discount percent", out decimal discountPercent))
            {
                return null;
            }

            if (!TryParseInt(penalizeAfterDaysTextBox.Text.Trim(), "Penalize after days", out int penalizeAfterDays))
            {
                return null;
            }

            if (!TryParseInt(discountThresholdTextBox.Text.Trim(), "Discount threshold", out int discountThreshold))
            {
                return null;
            }

            if (!TryParsePercent(penaltyPercentTextBox.Text.Trim(), "Penalty percent", out decimal penaltyPercent))
            {
                return null;
            }

            if (!TryParsePercent(taxPercentTextBox.Text.Trim(), "Tax percent", out decimal taxPercent))
            {
                return null;
            }

            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["discount_percent"] = discountPercent.ToString("0.##", CultureInfo.InvariantCulture),
                ["discount_thresh_hold"] = discountThreshold.ToString(CultureInfo.InvariantCulture),
                ["penalize_after_days"] = penalizeAfterDays.ToString(CultureInfo.InvariantCulture),
                ["penalty_percent"] = penaltyPercent.ToString("0.##", CultureInfo.InvariantCulture),
                ["tax_percent"] = taxPercent.ToString("0.##", CultureInfo.InvariantCulture)
            };
        }

        private bool TryParsePercent(string raw, string fieldName, out decimal value)
        {
            value = 0;

            if (!decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                && !decimal.TryParse(raw, NumberStyles.Number, CultureInfo.CurrentCulture, out parsed))
            {
                MessageBox.Show(this, $"{fieldName} must be a valid number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (parsed < 0 || parsed > 100)
            {
                MessageBox.Show(this, $"{fieldName} must be between 0 and 100.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            value = decimal.Round(parsed, 2, MidpointRounding.AwayFromZero);
            return true;
        }

        private bool TryParseInt(string raw, string fieldName, out int value)
        {
            value = 0;
            if (!int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed)
                && !int.TryParse(raw, NumberStyles.Integer, CultureInfo.CurrentCulture, out parsed))
            {
                MessageBox.Show(this, $"{fieldName} must be a valid whole number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (parsed < 0)
            {
                MessageBox.Show(this, $"{fieldName} cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            value = parsed;
            return true;
        }

        private async void saveSettingsButton_Click(object sender, EventArgs e)
        {
            await SaveAllChangesAsync();
        }

        private async Task SaveAllChangesAsync()
        {
            Dictionary<string, string>? settingsPayload = TryBuildSettingsPayload();
            if (settingsPayload is null)
            {
                return;
            }

            servicesGrid.EndEdit();
            zonesGrid.EndEdit();

            BindingContext? context = BindingContext;
            if (context is not null && context[_servicesTable] is CurrencyManager servicesCurrencyManager)
            {
                servicesCurrencyManager.EndCurrentEdit();
            }

            if (context is not null && context[_zonesTable] is CurrencyManager zonesCurrencyManager)
            {
                zonesCurrencyManager.EndCurrentEdit();
            }

            ValidateServicesGridValues();
            ValidateZonesGridValues();

            bool settingsChanged = AreSettingsModified(settingsPayload);
            bool servicesChanged = _servicesTable.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted) is not null;
            bool zonesChanged = _zonesTable.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted) is not null;

            if (!settingsChanged && !servicesChanged && !zonesChanged)
            {
                statusLabel.ForeColor = AppTheme.MutedTextColor;
                statusLabel.Text = "No changes to save.";
                return;
            }

            DialogResult saveConfirmation = MessageBox.Show(
                this,
                "Save all pending changes to system settings, services, and zones?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (saveConfirmation != DialogResult.Yes)
            {
                statusLabel.ForeColor = AppTheme.MutedTextColor;
                statusLabel.Text = "Save cancelled.";
                return;
            }

            try
            {
                SetBusyState(true, "Saving admin settings changes...");

                if (settingsChanged)
                {
                    await AdminSystemSettingsService.SaveSystemSettingsAsync(_user.Role, settingsPayload);
                    _loadedSettings = new Dictionary<string, string>(settingsPayload, StringComparer.OrdinalIgnoreCase);
                }

                if (servicesChanged)
                {
                    await AdminSystemSettingsService.SaveServiceChangesAsync(_user.Role, _servicesTable);
                    _servicesTable = await AdminSystemSettingsService.GetServicesAsync(_user.Role);
                    _nextServiceId = GetNextId(_servicesTable, "service_id");
                    servicesGrid.DataSource = _servicesTable;
                    ApplyServicesGridLayout();
                }

                if (zonesChanged)
                {
                    await AdminSystemSettingsService.SaveZoneChangesAsync(_user.Role, _zonesTable);
                    _zonesTable = await AdminSystemSettingsService.GetZonesAsync(_user.Role);
                    _nextZoneId = GetNextId(_zonesTable, "zone_id");
                    zonesGrid.DataSource = _zonesTable;
                    ApplyZonesGridLayout();
                }

                statusLabel.ForeColor = AppTheme.SuccessColor;
                statusLabel.Text = "All modified admin settings were saved.";
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = "Failed to save admin settings changes.";
                MessageBox.Show(this, ex.Message, "Admin System Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
            }
        }

        private bool AreSettingsModified(IReadOnlyDictionary<string, string> currentSettings)
        {
            foreach ((string key, string value) in currentSettings)
            {
                _loadedSettings.TryGetValue(key, out string? loadedValue);
                string normalizedCurrent = NormalizeSettingValue(key, value);
                string normalizedLoaded = NormalizeSettingValue(key, loadedValue ?? string.Empty);

                if (!string.Equals(normalizedCurrent, normalizedLoaded, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string NormalizeSettingValue(string key, string value)
        {
            if (key is "discount_thresh_hold" or "penalize_after_days")
            {
                if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intValue)
                    || int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out intValue))
                {
                    return intValue.ToString(CultureInfo.InvariantCulture);
                }
            }

            if (key is "discount_percent" or "penalty_percent" or "tax_percent")
            {
                if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal decimalValue)
                    || decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out decimalValue))
                {
                    return decimal.Round(decimalValue, 2, MidpointRounding.AwayFromZero).ToString("0.##", CultureInfo.InvariantCulture);
                }
            }

            return value.Trim();
        }

        private void ValidateServicesGridValues()
        {
            foreach (DataRow row in _servicesTable.Rows)
            {
                if (row.RowState is not (DataRowState.Modified or DataRowState.Added))
                {
                    continue;
                }

                if (!TryParsePositiveId(row, "service_id", out int serviceId))
                {
                    throw new InvalidOperationException("Service ID must be a valid positive whole number.");
                }

                string serviceType = Convert.ToString(row["service_type"], CultureInfo.CurrentCulture) ?? string.Empty;
                if (string.IsNullOrWhiteSpace(serviceType))
                {
                    throw new InvalidOperationException("Service type cannot be empty.");
                }

                row["service_type"] = serviceType.Trim().ToUpperInvariant();

                string pipeSize = Convert.ToString(row["pipe_size"], CultureInfo.CurrentCulture) ?? string.Empty;
                if (string.IsNullOrWhiteSpace(pipeSize))
                {
                    throw new InvalidOperationException("Pipe size cannot be empty.");
                }

                row["pipe_size"] = pipeSize.Trim().ToUpperInvariant();

                foreach (string columnName in new[] { "min_rate", "rate_11_20", "rate_21_30", "rate_31_40", "rate_41_above" })
                {
                    if (!decimal.TryParse(Convert.ToString(row[columnName], CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                        && !decimal.TryParse(Convert.ToString(row[columnName], CultureInfo.CurrentCulture), NumberStyles.Number, CultureInfo.CurrentCulture, out parsed))
                    {
                        throw new InvalidOperationException($"Services value for {columnName} must be a valid number.");
                    }

                    if (parsed < 0)
                    {
                        throw new InvalidOperationException($"Services value for {columnName} cannot be negative.");
                    }
                }
            }

            EnsureUniqueIds(_servicesTable, "service_id", "Service ID");
        }

        private void ValidateZonesGridValues()
        {
            foreach (DataRow row in _zonesTable.Rows)
            {
                if (row.RowState is not (DataRowState.Modified or DataRowState.Added))
                {
                    continue;
                }

                if (!TryParsePositiveId(row, "zone_id", out int zoneId))
                {
                    throw new InvalidOperationException("Zone ID must be a valid positive whole number.");
                }

                string zoneName = Convert.ToString(row["zone_name"], CultureInfo.CurrentCulture) ?? string.Empty;
                if (string.IsNullOrWhiteSpace(zoneName))
                {
                    throw new InvalidOperationException("Zone name cannot be empty.");
                }

                row["zone_name"] = zoneName.Trim().ToUpperInvariant();
            }

            EnsureUniqueIds(_zonesTable, "zone_id", "Zone ID");
        }

        private static bool TryParsePositiveId(DataRow row, string columnName, out int value)
        {
            value = 0;
            if (!int.TryParse(Convert.ToString(row[columnName], CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed)
                && !int.TryParse(Convert.ToString(row[columnName], CultureInfo.CurrentCulture), NumberStyles.Integer, CultureInfo.CurrentCulture, out parsed))
            {
                return false;
            }

            if (parsed <= 0)
            {
                return false;
            }

            value = parsed;
            return true;
        }

        private static void EnsureUniqueIds(DataTable table, string columnName, string displayName)
        {
            var seen = new HashSet<int>();
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (!TryParsePositiveId(row, columnName, out int id))
                {
                    continue;
                }

                if (!seen.Add(id))
                {
                    throw new InvalidOperationException($"{displayName} values must be unique.");
                }
            }
        }

        private void servicesGrid_DefaultValuesNeeded(object? sender, DataGridViewRowEventArgs e)
        {
            if (servicesGrid.Columns["service_id"] is null)
            {
                return;
            }

            e.Row.Cells["service_id"].Value = _nextServiceId;
            _nextServiceId++;
        }

        private void zonesGrid_DefaultValuesNeeded(object? sender, DataGridViewRowEventArgs e)
        {
            if (zonesGrid.Columns["zone_id"] is null)
            {
                return;
            }

            e.Row.Cells["zone_id"].Value = _nextZoneId;
            _nextZoneId++;
        }

        private void servicesGrid_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow? row = e.Row;
            if (row is null || row.IsNewRow)
            {
                return;
            }

            DataGridViewCell? serviceIdCell = servicesGrid.Columns["service_id"] is null
                ? null
                : row.Cells["service_id"];
            object? serviceIdValue = serviceIdCell?.Value;

            string serviceIdText = Convert.ToString(serviceIdValue, CultureInfo.CurrentCulture) ?? "(new)";

            DialogResult confirmation = MessageBox.Show(
                this,
                $"Delete service row {serviceIdText}?",
                "Confirm Delete Service",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            e.Cancel = confirmation != DialogResult.Yes;
        }

        private void zonesGrid_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow? row = e.Row;
            if (row is null || row.IsNewRow)
            {
                return;
            }

            DataGridViewCell? zoneIdCell = zonesGrid.Columns["zone_id"] is null
                ? null
                : row.Cells["zone_id"];
            object? zoneIdValue = zoneIdCell?.Value;

            string zoneIdText = Convert.ToString(zoneIdValue, CultureInfo.CurrentCulture) ?? "(new)";

            DialogResult confirmation = MessageBox.Show(
                this,
                $"Delete zone row {zoneIdText}?",
                "Confirm Delete Zone",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            e.Cancel = confirmation != DialogResult.Yes;
        }
    }
}
