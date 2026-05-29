using WDBS_2026.Components.Admin;
using WDBS_2026.Components.Biller;
using WDBS_2026.Components.Cashier;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Forms.User;
using WDBS_2026.Models;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Forms;

public partial class MainShellForm : Form
{
    private const int CollapsedNavigationWidth = 88;

    private readonly IUserProfileService _userProfileService;
    private readonly List<Button> _roleButtons = new();
    private readonly Dictionary<Button, NavigationButtonMetadata> _navigationButtonMetadata = new();

    private AuthenticatedUserDto _currentUser;

    public MainShellForm(AuthenticatedUserDto user, IUserProfileService userProfileService)
    {
        _currentUser = user;
        _userProfileService = userProfileService;
        InitializeComponent();
        ApplyTheme();
        InitializeShell();
        Resize += mainShellForm_Resize;
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);

        navigationPanel.BackColor = AppTheme.PrimaryDarkColor;
        contentHostPanel.BackColor = AppTheme.ShellBackgroundColor;

        LoadNavigationLogo();



        AppTheme.ApplyNavigationButton(dashboardButton, ButtonSeverity.Primary);
        AppTheme.ApplyNavigationButton(profileButton, ButtonSeverity.Info);
        AppTheme.ApplyNavigationButton(logoutButton, ButtonSeverity.Danger);

        RegisterNavigationButton(dashboardButton, "Dashboard", "🏠");
        RegisterNavigationButton(profileButton, "Profile", "👤");
        RegisterNavigationButton(logoutButton, "Log Out", "🚪");

        foreach (Button button in _roleButtons)
        {
            AppTheme.ApplyNavigationButton(button, ButtonSeverity.Neutral);
        }
    }

    private void InitializeShell()
    {
        Text = "Water District Billing System";
        BuildRoleNavigationButtons();
        LoadDashboard();
        UpdateResponsiveNavigation();
    }

    private void BuildRoleNavigationButtons()
    {
        _roleButtons.Clear();
        _navigationButtonMetadata.RemoveWhere(static pair => pair.Key.Parent == null);
        roleButtonsFlowPanel.Controls.Clear();

        IEnumerable<string> roleModules = _currentUser.Role switch
        {
            UserRole.Biller => new[] { "Concessionaire", "Reading", "Billing" },
            UserRole.Cashier => new[] { "Cashier", "Collections", "Report" },
            UserRole.Admin => new[] { "Users", "System Settings", "Billing", "Collection", "Reports" },
            _ => Array.Empty<string>()
        };

        foreach (string module in roleModules)
        {
            var button = new Button
            {
                Width = roleButtonsFlowPanel.ClientSize.Width,
                Height = 40,
                Margin = new Padding(0, 0, 0, 10),
                Tag = module
            };

            button.Click += roleModuleButton_Click;
            AppTheme.ApplyNavigationButton(button, ButtonSeverity.Neutral);
            RegisterNavigationButton(button, module, GetModuleEmoji(module));
            roleButtonsFlowPanel.Controls.Add(button);
            _roleButtons.Add(button);
        }

        ApplyNavigationButtonContent();
    }

    private void LoadDashboard()
    {
        try
        {
            Control dashboard = _currentUser.Role switch
            {
                UserRole.Admin => new AdminDashboardControl(_currentUser),
                UserRole.Biller => new BillerDashboardControl(_currentUser),
                UserRole.Cashier => new CashierDashboardControl(_currentUser),
                _ => throw new InvalidOperationException("Unsupported user role.")
            };

            dashboard.Dock = DockStyle.Fill;
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(dashboard);
        }
        catch (Exception ex)
        {
            contentHostPanel.Controls.Clear();
            contentHostPanel.Controls.Add(CreateDashboardErrorPanel(ex.Message));

            MessageBox.Show(
                this,
                "The dashboard could not be opened, but the application will stay running.\n\n" + ex.Message,
                "Dashboard Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private Control CreateDashboardErrorPanel(string details)
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = AppTheme.ShellBackgroundColor,
            Padding = new Padding(24)
        };

        var title = new Label
        {
            AutoSize = true,
            Text = "Dashboard failed to load",
            Font = AppTheme.HeadingFont,
            ForeColor = AppTheme.DangerColor,
            Location = new Point(24, 24)
        };

        var body = new Label
        {
            AutoSize = true,
            MaximumSize = new Size(900, 0),
            Text = "You can continue using other modules while this issue is being fixed.\n\n" + details,
            Font = AppTheme.BodyFont,
            ForeColor = AppTheme.BodyTextColor,
            Location = new Point(24, 68)
        };

        panel.Controls.Add(title);
        panel.Controls.Add(body);
        return panel;
    }

    private void roleModuleButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.Tag is not string moduleName)
        {
            return;
        }

        if (_currentUser.Role == UserRole.Biller && string.Equals(moduleName, "Concessionaire", StringComparison.OrdinalIgnoreCase))
        {
            LoadModuleControl(new ConcessionaireUserControl(_currentUser.Role));
            return;
        }

        if (_currentUser.Role == UserRole.Biller && string.Equals(moduleName, "Reading", StringComparison.OrdinalIgnoreCase))
        {
            LoadModuleControl(new MeterReadingUserControl(_currentUser));
            return;
        }

        if (_currentUser.Role == UserRole.Biller &&
            (string.Equals(moduleName, "Billing", StringComparison.OrdinalIgnoreCase)
             || string.Equals(moduleName, "Reports", StringComparison.OrdinalIgnoreCase)))
        {
            LoadModuleControl(new BillingUserControl(_currentUser));
            return;
        }

        MessageBox.Show(
            this,
            $"{moduleName} page will open here.",
            "Module Navigation",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void LoadModuleControl(Control control)
    {
        control.Dock = DockStyle.Fill;
        contentHostPanel.Controls.Clear();
        contentHostPanel.Controls.Add(control);
    }

    private void profileButton_Click(object sender, EventArgs e)
    {
        using var profileForm = new UserCredentialsForm(_currentUser, _userProfileService);
        DialogResult result = profileForm.ShowDialog(this);
        if (result != DialogResult.OK || profileForm.UpdatedUser is null)
        {
            return;
        }

        _currentUser = profileForm.UpdatedUser;

        // Refresh dashboard so any user-dependent captions immediately reflect updated profile info.
        LoadDashboard();
    }

    private static string GetRoleSummary(UserRole role)
    {
        return role switch
        {
            UserRole.Admin => "Users, services, settings, and operational oversight.",
            UserRole.Biller => "Concessionaires, SCF setup, and meter-reading operations.",
            UserRole.Cashier => "Collection posting, SCF payments, and cashier records.",
            _ => string.Empty
        };
    }

    private void dashboardButton_Click(object sender, EventArgs e)
    {
        LoadDashboard();
    }

    private void logoutButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void mainShellForm_Resize(object? sender, EventArgs e)
    {
        UpdateResponsiveNavigation();
    }

    private void UpdateResponsiveNavigation()
    {
        navigationPanel.Width = CollapsedNavigationWidth;
        navigationPanel.Padding = new Padding(10, 14, 10, 14);
        contentHostPanel.Padding = ClientSize.Width < 1060 ? new Padding(12) : new Padding(16);

        const int navigationButtonPadding = 0;
        const int navigationButtonHeight = 46;

        ApplyButtonLayout(dashboardButton, navigationButtonPadding, navigationButtonHeight, true);
        ApplyButtonLayout(profileButton, navigationButtonPadding, navigationButtonHeight, true);
        ApplyButtonLayout(logoutButton, navigationButtonPadding, navigationButtonHeight, true);

        foreach (Button roleButton in _roleButtons)
        {
            ApplyButtonLayout(roleButton, navigationButtonPadding, navigationButtonHeight, true);
            roleButton.Width = roleButtonsFlowPanel.ClientSize.Width;
        }

        ApplyNavigationButtonContent();
    }

    private void ApplyButtonLayout(Button button, int navigationButtonPadding, int navigationButtonHeight, bool collapsedMode)
    {
        button.Padding = collapsedMode ? Padding.Empty : new Padding(navigationButtonPadding, 0, 0, 0);
        button.Height = navigationButtonHeight;
        button.TextAlign = collapsedMode ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
        button.Font = collapsedMode
            ? new Font("Segoe UI Emoji", 15F, FontStyle.Regular)
            : new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
    }

    private void ApplyNavigationButtonContent()
    {
        foreach ((Button button, NavigationButtonMetadata metadata) in _navigationButtonMetadata)
        {
            button.Text = metadata.Emoji;
            navigationToolTip.SetToolTip(button, metadata.Label);
        }
    }

    private void LoadNavigationLogo()
    {
        string[] candidatePaths =
        {
            Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan logo.jpg"),
            Path.Combine(AppContext.BaseDirectory, "Resources", "tubungan_logo.jpg"),
            Path.Combine(AppContext.BaseDirectory, "Resources", "logo.png")
        };

        foreach (string path in candidatePaths)
        {
            if (!File.Exists(path))
            {
                continue;
            }

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var image = Image.FromStream(stream);
            navigationLogoPictureBox.Image = new Bitmap(image);
            return;
        }

        navigationLogoPictureBox.Visible = false;
    }

    private void RegisterNavigationButton(Button button, string label, string emoji)
    {
        _navigationButtonMetadata[button] = new NavigationButtonMetadata(label, emoji);
        navigationToolTip.SetToolTip(button, label);
    }

    private static string GetModuleEmoji(string module)
    {
        return module switch
        {
            "Concessionaire" => "🧾",
            "Reading" => "📟",
            "Billing" => "💵",
            "Reports" => "📊",
            "Cashier" => "💰",
            "Collections" => "📥",
            "Report" => "📈",
            "Users" => "👥",
            "System Settings" => "⚙️",
            "Collection" => "📦",
            _ => "📁"
        };
    }

    private sealed record NavigationButtonMetadata(string Label, string Emoji);
}

internal static class DictionaryExtensions
{
    public static void RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        where TKey : notnull
    {
        List<TKey> keys = dictionary.Where(predicate).Select(pair => pair.Key).ToList();
        foreach (TKey key in keys)
        {
            dictionary.Remove(key);
        }
    }
}