using System.Reflection;
using System.Diagnostics;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Forms;

public partial class LoginForm : Form
{
    private const int LoginCardMaxWidth = 460;
    private const string VersionUrl = "https://github.com/DuzyBuzz/WDBS-2026/releases/latest/download/version.txt";
    private const string UpdaterFileName = "Updater.exe";

    private readonly IAuthenticationService _authenticationService;
    private readonly IUserProfileService _userProfileService;
    private Version _currentAppVersion = new(1, 0, 0, 0);
    private bool _isCheckingForUpdates;
    private bool _hasUpdateAvailable;

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
        _ = CheckForUpdatesAvailabilityAsync();
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
        AppTheme.ApplySeverityButton(checkForUpdatesButton, ButtonSeverity.Info);

        loginButton.Height = 42;
        loginButton.Text = "Login";
        checkForUpdatesButton.Text = "Update Available";
        checkForUpdatesButton.Height = 30;
        versionLabel.Font = AppTheme.CaptionFont;
        versionLabel.ForeColor = AppTheme.MutedTextColor;
        ApplyUpdateButtonVisibility(false);
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
        bool loginHidden = false;

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
            loginHidden = true;

            using var shell = new MainShellForm(result.User, _userProfileService);
            shell.ShowDialog(this);

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
            if (loginHidden && !IsDisposed)
            {
                Show();
                Activate();
            }

            SetBusyState(false);
        }
    }

    private void SetBusyState(bool isBusy)
    {
        usernameTextBox.Enabled = !isBusy;
        passwordTextBox.Enabled = !isBusy;
        showPasswordCheckBox.Enabled = !isBusy;
        loginButton.Enabled = !isBusy;
        checkForUpdatesButton.Enabled = !isBusy && !_isCheckingForUpdates && _hasUpdateAvailable;
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
        SetPictureBoxImage(tubunganLogoPictureBox, Path.Combine(AppContext.BaseDirectory, "Resources", "logo.png"));
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

    private void LoginForm_Load(object sender, EventArgs e)
    {
        Version appVersion = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0, 0);
        _currentAppVersion = NormalizeVersion(appVersion);
        versionLabel.Text = $"Version: {FormatVersion(_currentAppVersion)}";
    }

    private async void checkForUpdatesButton_Click(object sender, EventArgs e)
    {
        LaunchUpdater();
        await Task.CompletedTask;
    }

    private async Task CheckForUpdatesAvailabilityAsync()
    {
        if (_isCheckingForUpdates)
        {
            return;
        }

        _isCheckingForUpdates = true;
        SetBusyState(false);

        try
        {
            Version remoteVersion = await GetRemoteVersionAsync();

            _hasUpdateAvailable = remoteVersion > _currentAppVersion;
            ApplyUpdateButtonVisibility(_hasUpdateAvailable);

            if (_hasUpdateAvailable)
            {
                statusLabel.ForeColor = AppTheme.InfoColor;
                statusLabel.Text = $"Update {FormatVersion(remoteVersion)} is available.";
            }
        }
        catch
        {
            _hasUpdateAvailable = false;
            ApplyUpdateButtonVisibility(false);
        }
        finally
        {
            _isCheckingForUpdates = false;
            SetBusyState(false);
        }
    }

    private void LaunchUpdater()
    {
        if (!_hasUpdateAvailable)
        {
            return;
        }

        SetBusyState(true);

        try
        {
            string updaterPath = Path.Combine(AppContext.BaseDirectory, UpdaterFileName);
            if (!File.Exists(updaterPath))
            {
                statusLabel.ForeColor = AppTheme.WarningColor;
                statusLabel.Text = "Updater file is missing.";
                MessageBox.Show(this,
                    $"Could not find {UpdaterFileName} in:\n{AppContext.BaseDirectory}",
                    "Updater Missing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = updaterPath,
                WorkingDirectory = AppContext.BaseDirectory,
                UseShellExecute = true
            };

            Process? updaterProcess = Process.Start(startInfo);
            if (updaterProcess is null)
            {
                throw new InvalidOperationException("Updater process could not be started.");
            }

            statusLabel.ForeColor = AppTheme.SuccessColor;
            statusLabel.Text = "Launching updater...";

            Close();
        }
        catch (Exception ex)
        {
            statusLabel.ForeColor = AppTheme.DangerColor;
            statusLabel.Text = "Unable to start updater.";
            MessageBox.Show(this,
                ex.Message,
                "Update Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            if (!IsDisposed)
            {
                SetBusyState(false);
            }
        }
    }

    private static async Task<Version> GetRemoteVersionAsync()
    {
        using var client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        using HttpResponseMessage response = await client.GetAsync(VersionUrl);
        response.EnsureSuccessStatusCode();

        string remoteVersionText = (await response.Content.ReadAsStringAsync()).Trim();
        if (string.IsNullOrWhiteSpace(remoteVersionText))
        {
            throw new FormatException("The version.txt file is empty.");
        }

        if (!Version.TryParse(remoteVersionText, out Version? remoteVersion))
        {
            throw new FormatException("The version.txt file has an invalid version format.");
        }

        return NormalizeVersion(remoteVersion);
    }

    private void ApplyUpdateButtonVisibility(bool isVisible)
    {
        checkForUpdatesButton.Visible = isVisible;

        if (updateFooterLayout.ColumnStyles.Count > 1)
        {
            updateFooterLayout.ColumnStyles[1].Width = isVisible ? 146F : 0F;
        }
    }

    private static Version NormalizeVersion(Version version)
    {
        int build = version.Build >= 0 ? version.Build : 0;
        int revision = version.Revision >= 0 ? version.Revision : 0;
        return new Version(version.Major, version.Minor, build, revision);
    }

    private static string FormatVersion(Version version)
    {
        return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}