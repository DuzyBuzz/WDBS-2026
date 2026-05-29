using WDBS_2026.DTOs.Auth;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Forms;

public partial class LoginForm : Form
{
    private const int LoginCardMaxWidth = 460;

    private readonly IAuthenticationService _authenticationService;
    private readonly IUserProfileService _userProfileService;

    public LoginForm(IAuthenticationService authenticationService, IUserProfileService userProfileService)
    {
        _authenticationService = authenticationService;
        _userProfileService = userProfileService;
        InitializeComponent();
        ApplyTheme();
        AcceptButton = loginButton;
        Resize += loginForm_Resize;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        LoadBrandImages();
        UpdateResponsiveLayout();
        usernameTextBox.Focus();
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);

        loginCardPanel.BackColor = AppTheme.SurfaceColor;
        loginCardPanel.BorderStyle = BorderStyle.FixedSingle;
        rootLayout.BackColor = AppTheme.ShellBackgroundColor;

        AppTheme.ApplyPageTitle(signInLabel);
        AppTheme.ApplySubtitle(signInCaptionLabel);
        AppTheme.ApplySubtitle(statusLabel);

        foreach (Label label in new[] { usernameLabel, passwordLabel })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        showPasswordCheckBox.Font = AppTheme.BodyFont;
        showPasswordCheckBox.ForeColor = AppTheme.MutedTextColor;
        showPasswordCheckBox.BackColor = Color.Transparent;

        AppTheme.ApplyInput(usernameTextBox);
        AppTheme.ApplyInput(passwordTextBox);
        AppTheme.ApplyPrimaryButton(loginButton);

        loginButton.Height = 42;
        loginButton.Text = "Login";
        statusLabel.Text = string.Empty;
    }

    private void loginForm_Resize(object? sender, EventArgs e)
    {
        UpdateResponsiveLayout();
    }

    private void showPasswordCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
    }

    private async void loginButton_Click(object sender, EventArgs e)
    {
        await AttemptLoginAsync();
    }

    private async void credentialsTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        await AttemptLoginAsync();
    }

    private async Task AttemptLoginAsync()
    {
        SetBusyState(true);
        statusLabel.ForeColor = AppTheme.MutedTextColor;
        statusLabel.Text = "Signing you in...";

        try
        {
            var request = new LoginRequestDto
            {
                Username = usernameTextBox.Text,
                Password = passwordTextBox.Text
            };

            var result = await _authenticationService.AuthenticateAsync(request);
            if (!result.IsSuccess || result.User is null)
            {
                statusLabel.ForeColor = AppTheme.DangerColor;
                statusLabel.Text = result.Message;
                passwordTextBox.SelectAll();
                passwordTextBox.Focus();
                return;
            }

            statusLabel.ForeColor = AppTheme.SuccessColor;
            statusLabel.Text = result.Message;

            Hide();

            using var shell = new MainShellForm(result.User, _userProfileService);
            shell.ShowDialog(this);

            Show();
            Activate();
            passwordTextBox.Clear();
            usernameTextBox.SelectAll();
            usernameTextBox.Focus();
            statusLabel.ForeColor = AppTheme.MutedTextColor;
            statusLabel.Text = "Enter your credentials to continue.";
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Sign-in failed.";
            MessageBox.Show(this, ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private void SetBusyState(bool isBusy)
    {
        usernameTextBox.Enabled = !isBusy;
        passwordTextBox.Enabled = !isBusy;
        showPasswordCheckBox.Enabled = !isBusy;
        loginButton.Enabled = !isBusy;
        UseWaitCursor = isBusy;
    }

    private void UpdateResponsiveLayout()
    {
        int horizontalPadding = ClientSize.Width < 720 ? 20 : 32;
        int verticalPadding = ClientSize.Height < 560 ? 20 : 32;
        int targetWidth = Math.Min(LoginCardMaxWidth, Math.Max(320, ClientSize.Width - (horizontalPadding * 2)));

        rootLayout.Padding = new Padding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
        loginCardPanel.MaximumSize = new Size(targetWidth, 0);
        loginCardPanel.MinimumSize = new Size(Math.Min(320, targetWidth), 0);
        logoLayout.ColumnStyles[0].Width = ClientSize.Width < 720 ? 72F : 86F;
        logoLayout.ColumnStyles[2].Width = ClientSize.Width < 720 ? 72F : 86F;
    }

    private void LoadBrandImages()
    {
        SetPictureBoxImage(philippinesLogoPictureBox, Path.Combine(AppContext.BaseDirectory, "Resources", "republika_ng_pilipinas.jpg"));
        SetPictureBoxImage(tubunganLogoPictureBox, Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg"));

        string iconPath = Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.ico");
        if (File.Exists(iconPath))
        {
            Icon = new Icon(iconPath);
        }
    }

    private static void SetPictureBoxImage(PictureBox pictureBox, string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            pictureBox.Visible = false;
            return;
        }

        using var sourceImage = Image.FromFile(imagePath);
        pictureBox.Image = new Bitmap(sourceImage);
    }
}