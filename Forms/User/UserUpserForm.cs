using WDBS_2026.Models;
using WDBS_2026.Services.Admin;

namespace WDBS_2026.Forms.User;

public partial class UserUpserForm : Form
{
    private readonly UserRole currentRole;
    private readonly int? userId;
    private readonly bool isEditMode;

    public UserUpserForm(UserRole role)
    {
        currentRole = role;
        InitializeComponent();
        InitializeForm();
    }

    public UserUpserForm(UserRole role, int userId, string username, string fullName, UserRole accountRole, bool isActive)
    {
        currentRole = role;
        this.userId = userId;
        isEditMode = true;
        InitializeComponent();
        InitializeForm();
        LoadUserData(username, fullName, accountRole, isActive);
    }

    private void InitializeForm()
    {
        Text = isEditMode ? "Update User" : "Add User";
        titleLabel.Text = isEditMode ? "Update User" : "Add User";
        saveButton.Text = isEditMode ? "Update" : "Save";
        passwordTextBox.PlaceholderText = isEditMode ? "Leave blank to keep current password" : string.Empty;

        if (!isEditMode)
        {
            isActiveCheckBox.Checked = true;
        }

        roleComboBox.DataSource = Enum.GetValues(typeof(UserRole));
        roleComboBox.SelectedItem = UserRole.Cashier;
    }

    private void LoadUserData(string username, string fullName, UserRole accountRole, bool isActive)
    {
        usernameTextBox.Text = username;
        fullNameTextBox.Text = fullName;
        roleComboBox.SelectedItem = accountRole;
        isActiveCheckBox.Checked = isActive;
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        if (!TryBuildRequest(out AdminUserWriteRequest request))
        {
            return;
        }

        try
        {
            saveButton.Enabled = false;
            if (isEditMode && userId.HasValue)
            {
                await AdminUsersService.UpdateUserAsync(currentRole, userId.Value, request);
            }
            else
            {
                await AdminUsersService.CreateUserAsync(currentRole, request);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Save User", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            saveButton.Enabled = true;
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private bool TryBuildRequest(out AdminUserWriteRequest request)
    {
        request = new AdminUserWriteRequest();

        string username = usernameTextBox.Text.Trim();
        string fullName = fullNameTextBox.Text.Trim();
        string password = passwordTextBox.Text;

        if (string.IsNullOrWhiteSpace(username))
        {
            MessageBox.Show(this, "Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            usernameTextBox.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            MessageBox.Show(this, "Full name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            fullNameTextBox.Focus();
            return false;
        }

        if (!isEditMode && string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show(this, "Password is required for new users.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            passwordTextBox.Focus();
            return false;
        }

        if (roleComboBox.SelectedItem is not UserRole selectedRole)
        {
            MessageBox.Show(this, "Select a user role.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            roleComboBox.Focus();
            return false;
        }

        request = new AdminUserWriteRequest
        {
            Username = username,
            FullName = fullName,
            Password = password,
            Role = selectedRole,
            IsActive = isActiveCheckBox.Checked
        };

        return true;
    }
}
