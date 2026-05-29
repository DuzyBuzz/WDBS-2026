using System.Globalization;

namespace WDBS_2026.Forms.Billing;

public enum BillingReportParameterMode
{
    Daily,
    Monthly
}

public partial class BillingReportParametersForm : Form
{
    private BillingReportParameterMode _mode = BillingReportParameterMode.Daily;

    public BillingReportParametersForm()
    {
        InitializeComponent();
        PopulateMonths();
        ApplyTheme();
        ConfigureDefaults();
        ApplyMode();
    }

    public BillingReportParametersForm(BillingReportParameterMode mode)
        : this()
    {
        _mode = mode;
        ApplyMode();
    }

    public DateTime DateFrom { get; private set; }

    public DateTime DateTo { get; private set; }

    public string ReportTitle { get; private set; } = string.Empty;

    public string PeriodCaption { get; private set; } = string.Empty;

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);
        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(helpLabel);

        foreach (Label label in new[] { dayLabel, monthLabel, yearLabel })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        monthComboBox.Font = AppTheme.BodyFont;
        dayPicker.Font = AppTheme.BodyFont;
        yearUpDown.Font = AppTheme.BodyFont;

        AppTheme.ApplyPrimaryButton(okButton);
        AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);
        AppTheme.ApplySurfacePanel(contentPanel);
    }

    private void PopulateMonths()
    {
        monthComboBox.Items.Clear();
        for (int month = 1; month <= 12; month++)
        {
            monthComboBox.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month));
        }
    }

    private void ConfigureDefaults()
    {
        DateTime today = DateTime.Today;
        dayPicker.Value = today;
        monthComboBox.SelectedIndex = today.Month - 1;
        yearUpDown.Value = today.Year;
    }

    private void ApplyMode()
    {
        bool isDaily = _mode == BillingReportParameterMode.Daily;

        titleLabel.Text = isDaily ? "Daily Billing Report" : "Monthly Billing Report";
        helpLabel.Text = isDaily
            ? "Select the billing day to preview or export the report."
            : "Select the billing month and year to preview or export the report.";
        Text = titleLabel.Text;

        dayLabel.Visible = isDaily;
        dayPicker.Visible = isDaily;

        monthLabel.Visible = !isDaily;
        monthComboBox.Visible = !isDaily;
        yearLabel.Visible = !isDaily;
        yearUpDown.Visible = !isDaily;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
        if (_mode == BillingReportParameterMode.Daily)
        {
            DateFrom = dayPicker.Value.Date;
            DateTo = dayPicker.Value.Date;
            ReportTitle = "Daily Billing Report";
            PeriodCaption = $"Billing Date: {DateFrom:MMMM dd, yyyy}";
        }
        else
        {
            int month = monthComboBox.SelectedIndex + 1;
            int year = Convert.ToInt32(yearUpDown.Value, CultureInfo.InvariantCulture);

            DateFrom = new DateTime(year, month, 1);
            DateTo = DateFrom.AddMonths(1).AddDays(-1);
            ReportTitle = "Monthly Billing Report";
            PeriodCaption = $"Billing Month: {DateFrom:MMMM yyyy}";
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}