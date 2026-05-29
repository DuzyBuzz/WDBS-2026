using WDBS_2026.DTOs.Auth;
using WDBS_2026.DTOs.User;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Forms.User;

public partial class UserCredentialsForm : Form
{
    private readonly IUserProfileService _userProfileService;
    private readonly AuthenticatedUserDto _currentUser;

    public UserCredentialsForm(AuthenticatedUserDto currentUser, IUserProfileService userProfileService)
    {
        _currentUser = currentUser;
        _userProfileService = userProfileService;

        InitializeComponent();
        ApplyTheme();
        LoadCurrentUser();
    }

    public AuthenticatedUserDto? UpdatedUser { get; private set; }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);
        credentialsCardPanel.BackColor = AppTheme.SurfaceColor;
        credentialsCardPanel.BorderStyle = BorderStyle.FixedSingle;

        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(helpLabel);
        AppTheme.ApplySubtitle(statusLabel);

        foreach (Label label in new[]
                 {
                     usernameLabel, fullNameLabel, roleLabel, currentPasswordLabel, newPasswordLabel, confirmPasswordLabel
                 })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        foreach (TextBox textBox in new[]
                 {
                     usernameTextBox, fullNameTextBox, roleTextBox,
                     currentPasswordTextBox, newPasswordTextBox, confirmPasswordTextBox
                 })
        {
            AppTheme.ApplyInput(textBox);
        }

        roleTextBox.ReadOnly = true;
        roleTextBox.BackColor = Color.FromArgb(247, 250, 252);

        showPasswordsCheckBox.Font = AppTheme.BodyFont;
        showPasswordsCheckBox.ForeColor = AppTheme.MutedTextColor;

        AppTheme.ApplyPrimaryButton(saveButton);
        AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);
    }

    private void LoadCurrentUser()
    {
        usernameTextBox.Text = _currentUser.Username;
        fullNameTextBox.Text = _currentUser.FullName;
        roleTextBox.Text = _currentUser.Role.ToString().ToUpperInvariant();
    }

    private void showPasswordsCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        bool showPassword = showPasswordsCheckBox.Checked;
        currentPasswordTextBox.UseSystemPasswordChar = !showPassword;
        newPasswordTextBox.UseSystemPasswordChar = !showPassword;
        confirmPasswordTextBox.UseSystemPasswordChar = !showPassword;
    }

    private async void saveButton_Click(object sender, EventArgs e)
    {
        SetBusyState(true);
        statusLabel.ForeColor = AppTheme.MutedTextColor;
        statusLabel.Text = "Saving changes...";

        try
        {
            var request = new UserCredentialsUpdateRequestDto
            {
                UserId = _currentUser.UserId,
                Username = usernameTextBox.Text,
                FullName = fullNameTextBox.Text,
                CurrentPassword = currentPasswordTextBox.Text,
                NewPassword = newPasswordTextBox.Text,
                ConfirmNewPassword = confirmPasswordTextBox.Text
            };

            UserCredentialsUpdateResultDto result = await _userProfileService.UpdateCredentialsAsync(request);
            if (!result.IsSuccess || result.User is null)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = result.Message;
                return;
            }

            UpdatedUser = result.User;
            statusLabel.ForeColor = AppTheme.SuccessColor;
            statusLabel.Text = result.Message;

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Unable to save credentials.";
            MessageBox.Show(this, ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void SetBusyState(bool isBusy)
    {
        usernameTextBox.Enabled = !isBusy;
        fullNameTextBox.Enabled = !isBusy;
        roleTextBox.Enabled = false;
        currentPasswordTextBox.Enabled = !isBusy;
        newPasswordTextBox.Enabled = !isBusy;
        confirmPasswordTextBox.Enabled = !isBusy;
        showPasswordsCheckBox.Enabled = !isBusy;
        saveButton.Enabled = !isBusy;
        cancelButton.Enabled = !isBusy;
        UseWaitCursor = isBusy;
    }
}
