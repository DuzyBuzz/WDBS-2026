using System.ComponentModel;

namespace WDBS_2026;

internal enum ButtonSeverity
{
    Primary,
    Info,
    Success,
    Warning,
    Danger,
    Neutral
}

internal readonly record struct ButtonPalette(
    Color BackColor,
    Color HoverColor,
    Color DownColor,
    Color ForeColor,
    Color BorderColor);

internal static class AppTheme
{
    private const string GlobalIconRelativePath = "Resources\\tubungan logo.ico";

    private static Icon? _cachedGlobalIcon;
    private static bool _isGlobalIconLoaded;

    public static readonly Color ShellBackgroundColor = Color.FromArgb(239, 245, 248);
    public static readonly Color SurfaceColor = Color.White;
    public static readonly Color PrimaryColor = Color.FromArgb(18, 96, 128);
    public static readonly Color PrimaryDarkColor = Color.FromArgb(11, 58, 79);
    public static readonly Color AccentColor = Color.FromArgb(0, 129, 112);
    public static readonly Color InfoColor = Color.FromArgb(5, 114, 184);
    public static readonly Color BorderColor = Color.FromArgb(212, 223, 230);
    public static readonly Color BodyTextColor = Color.FromArgb(28, 43, 57);
    public static readonly Color MutedTextColor = Color.FromArgb(95, 112, 126);
    public static readonly Color SuccessColor = Color.FromArgb(46, 125, 50);
    public static readonly Color WarningColor = Color.FromArgb(222, 117, 33);
    public static readonly Color DangerColor = Color.FromArgb(173, 43, 43);
    public static readonly Color NeutralColor = Color.FromArgb(87, 99, 109);

    public static readonly Font DisplayFont = new("Segoe UI Semibold", 24F, FontStyle.Bold);
    public static readonly Font HeadingFont = new("Segoe UI Semibold", 16F, FontStyle.Bold);
    public static readonly Font SectionFont = new("Segoe UI Semibold", 12F, FontStyle.Bold);
    public static readonly Font BodyFont = new("Segoe UI", 10F, FontStyle.Regular);
    public static readonly Font CaptionFont = new("Segoe UI", 9F, FontStyle.Regular);

    public static void ApplyFormSurface(Form form)
    {
        form.BackColor = ShellBackgroundColor;
        form.Font = BodyFont;

        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        {
            ApplyGlobalFormIcon(form);
        }
    }

    public static void ApplySurfacePanel(Panel panel)
    {
        panel.BackColor = SurfaceColor;
        panel.BorderStyle = BorderStyle.FixedSingle;
    }

    public static void ApplyCard(Panel panel)
    {
        panel.BackColor = SurfaceColor;
        panel.BorderStyle = BorderStyle.FixedSingle;
        panel.Padding = new Padding(22);
        panel.Margin = new Padding(0, 0, 18, 18);
    }

    public static void ApplyPageTitle(Label label)
    {
        label.AutoSize = true;
        label.Font = HeadingFont;
        label.ForeColor = BodyTextColor;
    }

    public static void ApplyHeroTitle(Label label)
    {
        label.AutoSize = true;
        label.Font = DisplayFont;
        label.ForeColor = Color.White;
    }

    public static void ApplySubtitle(Label label)
    {
        label.AutoSize = true;
        label.Font = BodyFont;
        label.ForeColor = MutedTextColor;
    }

    public static void ApplyLightSubtitle(Label label)
    {
        label.AutoSize = true;
        label.Font = BodyFont;
        label.ForeColor = Color.FromArgb(219, 233, 241);
    }

    public static void ApplyInput(TextBox textBox)
    {
        textBox.BackColor = Color.White;
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.Font = BodyFont;
        textBox.ForeColor = BodyTextColor;
    }

    public static void ApplyPrimaryButton(Button button)
    {
        ApplySeverityButton(button, ButtonSeverity.Primary);
    }

    public static void ApplyNavigationButton(Button button, bool selected = false)
    {
        ApplyNavigationButton(button, selected ? ButtonSeverity.Primary : ButtonSeverity.Neutral);
    }

    public static void ApplyNavigationButton(Button button, ButtonSeverity severity)
    {
        ApplySeverityButton(button, severity);
        button.TextAlign = ContentAlignment.MiddleLeft;
        button.Padding = new Padding(18, 0, 0, 0);
    }

    public static void ApplySeverityButton(Button button, ButtonSeverity severity)
    {
        ButtonPalette palette = GetButtonPalette(severity);

        button.BackColor = palette.BackColor;
        button.FlatAppearance.BorderColor = palette.BorderColor;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseDownBackColor = palette.DownColor;
        button.FlatAppearance.MouseOverBackColor = palette.HoverColor;
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        button.ForeColor = palette.ForeColor;
        button.Cursor = Cursors.Hand;
    }

    public static void ApplyGhostButton(Button button)
    {
        button.FlatAppearance.BorderColor = Color.FromArgb(101, 146, 168);
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 89, 120);
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 77, 105);
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        button.ForeColor = Color.White;
        button.BackColor = Color.Transparent;
        button.Cursor = Cursors.Hand;
        button.TextAlign = ContentAlignment.MiddleLeft;
        button.Padding = new Padding(18, 0, 0, 0);
    }

    public static void ApplyCardTitle(Label label)
    {
        label.AutoSize = true;
        label.Font = SectionFont;
        label.ForeColor = BodyTextColor;
    }

    public static void ApplyCardBody(Label label)
    {
        label.AutoSize = true;
        label.Font = BodyFont;
        label.ForeColor = MutedTextColor;
    }

    private static ButtonPalette GetButtonPalette(ButtonSeverity severity)
    {
        return severity switch
        {
            ButtonSeverity.Primary => new ButtonPalette(
                PrimaryColor,
                Color.FromArgb(28, 115, 150),
                PrimaryDarkColor,
                Color.White,
                PrimaryColor),
            ButtonSeverity.Info => new ButtonPalette(
                InfoColor,
                Color.FromArgb(23, 132, 202),
                Color.FromArgb(3, 88, 142),
                Color.White,
                InfoColor),
            ButtonSeverity.Success => new ButtonPalette(
                SuccessColor,
                Color.FromArgb(56, 142, 60),
                Color.FromArgb(27, 94, 32),
                Color.White,
                SuccessColor),
            ButtonSeverity.Warning => new ButtonPalette(
                WarningColor,
                Color.FromArgb(245, 137, 41),
                Color.FromArgb(194, 92, 18),
                Color.White,
                WarningColor),
            ButtonSeverity.Danger => new ButtonPalette(
                DangerColor,
                Color.FromArgb(194, 68, 68),
                Color.FromArgb(136, 33, 33),
                Color.White,
                DangerColor),
            _ => new ButtonPalette(
                NeutralColor,
                Color.FromArgb(107, 121, 133),
                Color.FromArgb(69, 79, 87),
                Color.White,
                NeutralColor)
        };
    }

    private static void ApplyGlobalFormIcon(Form form)
    {
        Icon? icon = GetGlobalIcon();
        if (icon is null)
        {
            return;
        }

        form.Icon = (Icon)icon.Clone();
    }

    private static Icon? GetGlobalIcon()
    {
        if (_isGlobalIconLoaded)
        {
            return _cachedGlobalIcon;
        }

        string iconPath = Path.Combine(AppContext.BaseDirectory, GlobalIconRelativePath);
        if (File.Exists(iconPath))
        {
            _cachedGlobalIcon = new Icon(iconPath);
        }

        _isGlobalIconLoaded = true;
        return _cachedGlobalIcon;
    }
}