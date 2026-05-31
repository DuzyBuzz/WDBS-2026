using System.ComponentModel;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.User;
using WDBS_2026.Models;
using WDBS_2026.Services.Admin;

namespace WDBS_2026.Components.Admin
{
    public partial class AdminUsersUserControl : UserControl
    {
        private readonly AuthenticatedUserDto _user;
        private bool _isLoading;

        public AdminUsersUserControl()
            : this(new AuthenticatedUserDto
            {
                UserId = 0,
                Username = "designer",
                FullName = "Dashboard Designer",
                Role = UserRole.Admin
            })
        {
        }

        public AdminUsersUserControl(AuthenticatedUserDto user)
        {
            _user = user;
            InitializeComponent();
            ApplyTheme();
            ConfigureGrid();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsDesignerHosted())
            {
                return;
            }

            await LoadUsersAsync();
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

            foreach (Label label in new[] { usersLabel })
            {
                label.Font = AppTheme.SectionFont;
                label.ForeColor = AppTheme.BodyTextColor;
            }

            foreach (TextBox textBox in new[] { searchTextBox })
            {
                AppTheme.ApplyInput(textBox);
            }

            AppTheme.ApplySeverityButton(addUserButton, ButtonSeverity.Success);
            AppTheme.ApplySeverityButton(deleteUserButton, ButtonSeverity.Danger);
            AppTheme.ApplySeverityButton(searchButton, ButtonSeverity.Info);
            AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Neutral);
        }

        private void ConfigureGrid()
        {
            usersGrid.AllowUserToAddRows = false;
            usersGrid.AllowUserToDeleteRows = false;
            usersGrid.AllowUserToResizeRows = false;
            usersGrid.AutoGenerateColumns = true;
            usersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            usersGrid.MultiSelect = false;
            usersGrid.ReadOnly = true;
            usersGrid.RowHeadersVisible = false;
            usersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            usersGrid.EnableHeadersVisualStyles = false;
            usersGrid.BorderStyle = BorderStyle.FixedSingle;
            usersGrid.BackgroundColor = AppTheme.SurfaceColor;
            usersGrid.GridColor = AppTheme.BorderColor;
            usersGrid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
            usersGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            usersGrid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;
            usersGrid.DefaultCellStyle.BackColor = Color.White;
            usersGrid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
            usersGrid.DefaultCellStyle.Font = AppTheme.BodyFont;
            usersGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
            usersGrid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
            usersGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
            usersGrid.CellDoubleClick += usersGrid_CellDoubleClick;
        }

        private async Task LoadUsersAsync()
        {
            if (_isLoading)
            {
                return;
            }

            try
            {
                _isLoading = true;
                SetBusyState(true, "Loading users...");

                DataTable users = await AdminUsersService.GetUsersAsync(_user.Role, searchTextBox.Text.Trim());
                usersGrid.DataSource = users;
                ApplyGridLayout();

                statusLabel.ForeColor = AppTheme.MutedTextColor;
                statusLabel.Text = users.Rows.Count == 0 ? "No users found." : "Users loaded.";
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = "Failed to load users.";
                MessageBox.Show(this, ex.Message, "Admin Users", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
                _isLoading = false;
            }
        }

        private void ApplyGridLayout()
        {
            if (usersGrid.Columns.Count == 0)
            {
                return;
            }

            SetHeader("user_id", "User ID");
            SetHeader("username", "Username");
            SetHeader("full_name", "Full Name");
            SetHeader("role", "Role");
            SetHeader("is_active", "Active");
            SetHeader("created_at", "Created");

            if (usersGrid.Columns["user_id"] is { } idColumn)
            {
                idColumn.FillWeight = 35F;
                idColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (usersGrid.Columns["created_at"] is { } createdAtColumn)
            {
                createdAtColumn.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                createdAtColumn.FillWeight = 75F;
            }

            if (usersGrid.Columns["is_active"] is { } activeColumn)
            {
                activeColumn.FillWeight = 40F;
            }
        }

        private void SetHeader(string columnName, string header)
        {
            if (usersGrid.Columns[columnName] is { } column)
            {
                column.HeaderText = header;
            }
        }

        private void SetBusyState(bool isBusy, string? message = null)
        {
            foreach (Control control in new Control[]
                     {
                         addUserButton,
                         deleteUserButton,
                         searchTextBox,
                         searchButton,
                         refreshButton,
                         usersGrid
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

        private static bool TryGetSelectedUser(DataGridView grid, out int userId, out string username, out string fullName, out UserRole role, out bool isActive)
        {
            userId = 0;
            username = string.Empty;
            fullName = string.Empty;
            role = UserRole.Cashier;
            isActive = false;

            if (grid.CurrentRow is not { } row || row.IsNewRow)
            {
                return false;
            }

            if (!int.TryParse(Convert.ToString(row.Cells["user_id"].Value, CultureInfo.InvariantCulture), out userId) || userId <= 0)
            {
                return false;
            }

            username = Convert.ToString(row.Cells["username"].Value, CultureInfo.CurrentCulture) ?? string.Empty;
            fullName = Convert.ToString(row.Cells["full_name"].Value, CultureInfo.CurrentCulture) ?? string.Empty;

            string roleText = Convert.ToString(row.Cells["role"].Value, CultureInfo.CurrentCulture) ?? string.Empty;
            if (!Enum.TryParse(roleText, true, out role))
            {
                role = UserRole.Cashier;
            }

            object? activeRaw = row.Cells["is_active"].Value;
            if (activeRaw is bool boolValue)
            {
                isActive = boolValue;
            }
            else if (bool.TryParse(Convert.ToString(activeRaw, CultureInfo.InvariantCulture), out bool parsed))
            {
                isActive = parsed;
            }

            return true;
        }

        private async void addUserButton_Click(object sender, EventArgs e)
        {
            using var form = new UserUpserForm(_user.Role);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await LoadUsersAsync();
            }
        }

        private async void deleteUserButton_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedUser(usersGrid, out int userId, out _, out _, out _, out _))
            {
                MessageBox.Show(this, "Select a user to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userId == _user.UserId)
            {
                MessageBox.Show(this, "You cannot delete your own account.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this,
                "Delete this user account?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                SetBusyState(true, "Deleting user...");
                await AdminUsersService.DeleteUserAsync(_user.Role, userId);
                statusLabel.ForeColor = AppTheme.SuccessColor;
                statusLabel.Text = "User deleted.";
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = "Failed to delete user.";
                MessageBox.Show(this, ex.Message, "Admin Users", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false);
            }
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            await LoadUsersAsync();
        }

        private async void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            await LoadUsersAsync();
        }

        private async void usersGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= usersGrid.Rows.Count)
            {
                return;
            }

            if (!TryGetSelectedUser(usersGrid, out int userId, out string username, out string fullName, out UserRole role, out bool isActive))
            {
                return;
            }

            using var form = new UserUpserForm(_user.Role, userId, username, fullName, role, isActive);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await LoadUsersAsync();
            }
        }
    }
}
