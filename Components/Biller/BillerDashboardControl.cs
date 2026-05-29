using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Models;
using WDBS_2026.Properties;
using WDBS_2026.Services.Dashboard;

namespace WDBS_2026.Components.Biller;

public partial class BillerDashboardControl : UserControl
{
    private readonly AuthenticatedUserDto _user;

    private BillerDashboardSnapshot? _snapshot;
    private DateTime _selectedMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    private bool _isLoading;
    private bool _isChartRebindQueued;
    private bool _isRestoringSplitterDistances;
    private bool _splitterDistancesInitialized;
    private readonly Dictionary<SplitContainer, SplitterSettingAccessor> _splitterSettings = new();

    public BillerDashboardControl()
        : this(new AuthenticatedUserDto
        {
            UserId = 0,
            Username = "designer",
            FullName = "Dashboard Designer",
            Role = UserRole.Biller
        })
    {
    }

    public BillerDashboardControl(AuthenticatedUserDto user)
    {
        _user = user;

        InitializeComponent();
        ApplyTheme();
        ConfigureGrids();
        ConfigureCharts();
        UpdateSelectedMonthDisplay();

        if (IsDesignerHosted())
        {
            BindDesignTimeSample();
            return;
        }

        InitializeSplitterPersistence();
        SizeChanged += BillerDashboardControl_SizeChanged;
        reportsTabControl.SelectedIndexChanged += reportsTabControl_SelectedIndexChanged;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (IsDesignerHosted())
        {
            return;
        }

        RestoreSplitterDistancesDeferred();
        await RefreshDashboardAsync();
    }

    private void InitializeSplitterPersistence()
    {
        if (_splitterSettings.Count > 0)
        {
            return;
        }

        _splitterSettings[billedTrendSplitContainer] = new SplitterSettingAccessor(
            settings => settings.BillerDashboardBilledTrendSplitterDistance,
            (settings, value) => settings.BillerDashboardBilledTrendSplitterDistance = value);

        _splitterSettings[billingStatusSplitContainer] = new SplitterSettingAccessor(
            settings => settings.BillerDashboardBillingStatusSplitterDistance,
            (settings, value) => settings.BillerDashboardBillingStatusSplitterDistance = value);

        _splitterSettings[concessionairesByZoneSplitContainer] = new SplitterSettingAccessor(
            settings => settings.BillerDashboardConcessionairesByZoneSplitterDistance,
            (settings, value) => settings.BillerDashboardConcessionairesByZoneSplitterDistance = value);

        _splitterSettings[concessionaireStatusSplitContainer] = new SplitterSettingAccessor(
            settings => settings.BillerDashboardConcessionaireStatusSplitterDistance,
            (settings, value) => settings.BillerDashboardConcessionaireStatusSplitterDistance = value);

        _splitterSettings[topConcessionairesSplitContainer] = new SplitterSettingAccessor(
            settings => settings.BillerDashboardTopConcessionairesSplitterDistance,
            (settings, value) => settings.BillerDashboardTopConcessionairesSplitterDistance = value);

        foreach (SplitContainer splitter in _splitterSettings.Keys)
        {
            splitter.SplitterMoved += Splitter_SplitterMoved;
        }
    }

    private void RestoreSplitterDistancesDeferred()
    {
        if (_splitterDistancesInitialized || IsDisposed || !IsHandleCreated)
        {
            return;
        }

        BeginInvoke(new MethodInvoker(() =>
        {
            if (_splitterDistancesInitialized || IsDisposed)
            {
                return;
            }

            RestoreSplitterDistances();
            _splitterDistancesInitialized = true;
        }));
    }

    private void RestoreSplitterDistances()
    {
        if (_splitterSettings.Count == 0)
        {
            return;
        }

        _isRestoringSplitterDistances = true;
        try
        {
            UserInterfaceSettings settings = UserInterfaceSettings.Default;

            foreach ((SplitContainer splitter, SplitterSettingAccessor accessor) in _splitterSettings)
            {
                int preferredDistance = accessor.Read(settings);
                ApplySplitterDistance(splitter, preferredDistance);
            }
        }
        finally
        {
            _isRestoringSplitterDistances = false;
        }
    }

    private static void ApplySplitterDistance(SplitContainer splitter, int preferredDistance)
    {
        if (splitter.IsDisposed)
        {
            return;
        }

        int currentSize = splitter.Orientation == Orientation.Horizontal
            ? splitter.Height
            : splitter.Width;

        if (currentSize <= 0)
        {
            return;
        }

        int minDistance = Math.Max(splitter.Panel1MinSize, 0);
        int maxDistance = currentSize - splitter.SplitterWidth - Math.Max(splitter.Panel2MinSize, 0);

        if (maxDistance < minDistance)
        {
            return;
        }

        int clampedDistance = Math.Clamp(preferredDistance, minDistance, maxDistance);

        if (splitter.SplitterDistance != clampedDistance)
        {
            splitter.SplitterDistance = clampedDistance;
        }
    }

    private void Splitter_SplitterMoved(object? sender, SplitterEventArgs e)
    {
        if (_isRestoringSplitterDistances || IsDesignerHosted() || IsDisposed)
        {
            return;
        }

        if (sender is not SplitContainer splitter || !_splitterSettings.TryGetValue(splitter, out SplitterSettingAccessor? accessor))
        {
            return;
        }

        try
        {
            UserInterfaceSettings settings = UserInterfaceSettings.Default;
            accessor.Write(settings, splitter.SplitterDistance);
            settings.Save();
        }
        catch (Exception ex)
        {
            AppDiagnostics.ReportException("BillerDashboard splitter settings save", ex, showDialog: false);
        }
    }

    private static bool IsDesignerHosted()
    {
        return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }

    private async Task RefreshDashboardAsync()
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;

        try
        {
            SetBusyState(true, $"Loading dashboard metrics and charts for {_selectedMonth:MMM yyyy}...");
            BillerDashboardSnapshot snapshot = await BillerDashboardDataService.GetSnapshotAsync(_user.Role, _selectedMonth);
            _snapshot = snapshot;

            BindKpis(snapshot);
            BindTables(snapshot);
            UpdateReportTitles(snapshot);
            BindAllCharts(snapshot);
            UpdateSelectedMonthDisplay(snapshot.PeriodStart);

            dashboardStatusLabel.ForeColor = AppTheme.MutedTextColor;
            dashboardStatusLabel.Text = $"Updated {DateTime.Now:MMM dd, yyyy hh:mm tt} ({_selectedMonth:MMM yyyy})";
        }
        catch (Exception ex)
        {
            dashboardStatusLabel.ForeColor = AppTheme.DangerColor;
            dashboardStatusLabel.Text = "Failed to load dashboard data.";
            MessageBox.Show(this, ex.Message, "Dashboard Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
            _isLoading = false;
        }
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        layoutPanel.BackColor = AppTheme.ShellBackgroundColor;

        AppTheme.ApplyPageTitle(headingLabel);
        //AppTheme.ApplySubtitle(subtitleLabel);
        //subtitleLabel.Text = $"Signed in as {_user.FullName}. Analyst view of biller performance for the current month.";

        AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(prevMonthButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(nextMonthButton, ButtonSeverity.Neutral);
        AppTheme.ApplySubtitle(dashboardStatusLabel);
        AppTheme.ApplySubtitle(selectedMonthLabel);

        foreach (Panel panel in new[]
                 {
                     totalBilledCardPanel,
                     billCountCardPanel,
                     unpaidBillsCardPanel,
                     activeConcessionairesCardPanel,
                     billedTrendCardPanel,
                     billingStatusCardPanel,
                     concessionairesByZoneCardPanel,
                     concessionaireStatusCardPanel,
                     topConcessionairesCardPanel
                 })
        {
            AppTheme.ApplyCard(panel);
        }

        foreach (Label caption in new[]
                 {
                     totalBilledCaptionLabel,
                     billCountCaptionLabel,
                     unpaidBillsCaptionLabel,
                     activeConcessionairesCaptionLabel
                 })
        {
            caption.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            caption.ForeColor = AppTheme.MutedTextColor;
        }

        foreach (Label caption in new[]
                 {
                     billedTrendTitleLabel,
                     billingStatusTitleLabel,
                     concessionairesByZoneTitleLabel,
                     concessionaireStatusTitleLabel,
                     topConcessionairesTitleLabel,
                     billedTrendTableTitleLabel,
                     billingStatusTableTitleLabel,
                     concessionairesByZoneTableTitleLabel,
                     concessionaireStatusTableTitleLabel,
                     topConcessionairesTableTitleLabel
                 })
        {
            caption.Font = AppTheme.SectionFont;
            caption.ForeColor = AppTheme.BodyTextColor;
        }

        foreach (Label value in new[]
                 {
                     totalBilledValueLabel,
                     billCountValueLabel,
                     unpaidBillsValueLabel,
                     activeConcessionairesValueLabel
                 })
        {
            value.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            value.ForeColor = AppTheme.PrimaryDarkColor;
        }

        ApplyCompactKpiLayout();
    }

    private void ApplyCompactKpiLayout()
    {
        ConfigureKpiCard(totalBilledCardPanel, totalBilledCaptionLabel, totalBilledValueLabel);
        ConfigureKpiCard(billCountCardPanel, billCountCaptionLabel, billCountValueLabel);
        ConfigureKpiCard(unpaidBillsCardPanel, unpaidBillsCaptionLabel, unpaidBillsValueLabel);
        ConfigureKpiCard(activeConcessionairesCardPanel, activeConcessionairesCaptionLabel, activeConcessionairesValueLabel);
    }

    private static void ConfigureKpiCard(Panel card, Label caption, Label value)
    {
        card.Padding = new Padding(6, 3, 6, 3);

        caption.Dock = DockStyle.Top;
        caption.AutoSize = false;
        caption.Height = 14;
        caption.TextAlign = ContentAlignment.MiddleLeft;
        caption.Margin = Padding.Empty;

        value.Dock = DockStyle.Fill;
        value.AutoSize = false;
        value.TextAlign = ContentAlignment.MiddleLeft;
        value.Margin = Padding.Empty;
    }

    private void ConfigureGrids()
    {
        foreach (DataGridView grid in new[]
                 {
                     billedTrendGrid,
                     billingStatusGrid,
                     concessionairesByZoneGrid,
                     concessionaireStatusGrid,
                     topConcessionairesGrid
                 })
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
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

    private void ConfigureCharts()
    {
        billedTrendChart = EnsureChart(
            billedTrendChart,
            billedTrendCardPanel,
            billedTrendTitleLabel,
            nameof(billedTrendChart));

        billingStatusChart = EnsureChart(
            billingStatusChart,
            billingStatusCardPanel,
            billingStatusTitleLabel,
            nameof(billingStatusChart));

        concessionairesByZoneChart = EnsureChart(
            concessionairesByZoneChart,
            concessionairesByZoneCardPanel,
            concessionairesByZoneTitleLabel,
            nameof(concessionairesByZoneChart));

        concessionaireStatusChart = EnsureChart(
            concessionaireStatusChart,
            concessionaireStatusCardPanel,
            concessionaireStatusTitleLabel,
            nameof(concessionaireStatusChart));

        topConcessionairesChart = EnsureChart(
            topConcessionairesChart,
            topConcessionairesCardPanel,
            topConcessionairesTitleLabel,
            nameof(topConcessionairesChart));

        billedTrendChart.SuppressExceptions = true;
        billingStatusChart.SuppressExceptions = true;
        concessionairesByZoneChart.SuppressExceptions = true;
        concessionaireStatusChart.SuppressExceptions = true;
        topConcessionairesChart.SuppressExceptions = true;
    }

    private static Chart EnsureChart(Chart? chart, Panel hostPanel, Label titleLabel, string chartName)
    {
        if (chart is null)
        {
            chart = new Chart
            {
                Name = chartName,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                BackColor = Color.White
            };

            hostPanel.Controls.Add(chart);
            titleLabel.BringToFront();
        }

        return chart;
    }

    private void BindDesignTimeSample()
    {
        var sample = new BillerDashboardSnapshot(
            new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
            new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1),
            482150.40M,
            389,
            42,
            124,
            new[] { new BillerDashboardPoint("2026-05", 482150.40M) },
            new[] { new BillerDashboardPoint("PAID", 300), new BillerDashboardPoint("UNPAID", 89) },
            new[] { new BillerDashboardPoint("Zone 1", 42), new BillerDashboardPoint("Zone 2", 31), new BillerDashboardPoint("Zone 3", 51) },
            new[] { new BillerDashboardPoint("ACTIVE", 124), new BillerDashboardPoint("INACTIVE", 11) },
            new[]
            {
                new BillerDashboardPoint("101-0001 - Juan Dela Cruz", 45620.22M),
                new BillerDashboardPoint("101-0002 - Maria Santos", 32114.67M),
                new BillerDashboardPoint("101-0003 - Rizal Trading", 28091.14M)
            });

        _snapshot = sample;
        _selectedMonth = sample.PeriodStart;
        BindKpis(sample);
        BindTables(sample);
        UpdateReportTitles(sample);
        BindAllCharts(sample);
        UpdateSelectedMonthDisplay(sample.PeriodStart);
        dashboardStatusLabel.Text = "Designer preview data";
    }

    private void UpdateSelectedMonthDisplay(DateTime? month = null)
    {
        if (month.HasValue)
        {
            _selectedMonth = new DateTime(month.Value.Year, month.Value.Month, 1);
        }

        selectedMonthLabel.Text = _selectedMonth.ToString("MMM yyyy", CultureInfo.InvariantCulture);
    }

    private void BindKpis(BillerDashboardSnapshot snapshot)
    {
        totalBilledValueLabel.Text = snapshot.TotalBilledAmount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
        billCountValueLabel.Text = snapshot.TotalBillCount.ToString("N0", CultureInfo.InvariantCulture);
        unpaidBillsValueLabel.Text = snapshot.UnpaidBillCount.ToString("N0", CultureInfo.InvariantCulture);
        activeConcessionairesValueLabel.Text = snapshot.ActiveConcessionaireCount.ToString("N0", CultureInfo.InvariantCulture);
    }

    private void UpdateReportTitles(BillerDashboardSnapshot snapshot)
    {
        DateTime trendStart = snapshot.PeriodStart.AddMonths(-11);
        DateTime trendEnd = snapshot.PeriodStart;

        billedTrendTitleLabel.Text = $"Billed Amount Trend ({trendStart:MMM yyyy} to {trendEnd:MMM yyyy})";
        billedTrendTableTitleLabel.Text = "Rolling 12-Month Billed Trend";
        topConcessionairesTitleLabel.Text = $"Top 10 Concessionaires by Billed Amount ({snapshot.PeriodStart:MMM yyyy})";
    }

    private void BindTables(BillerDashboardSnapshot snapshot)
    {
        billedTrendGrid.DataSource = BuildTrendRows(snapshot.BilledTrend);
        billingStatusGrid.DataSource = BuildValueRows(snapshot.BillingStatusBreakdown, "Status");
        concessionairesByZoneGrid.DataSource = BuildValueRows(snapshot.ConcessionairesByZone, "Zone");
        concessionaireStatusGrid.DataSource = BuildValueRows(snapshot.ConcessionaireStatusMix, "Status");
        topConcessionairesGrid.DataSource = BuildTopConcessionaireRows(snapshot.TopConcessionairesByAmount);

        FormatTrendGrid();
        FormatDistributionGrid(billingStatusGrid, "Status");
        FormatDistributionGrid(concessionairesByZoneGrid, "Zone");
        FormatDistributionGrid(concessionaireStatusGrid, "Status");
        FormatTopConcessionairesGrid();
    }

    private void BindAllCharts(BillerDashboardSnapshot snapshot)
    {
        bool hasChartErrors = false;

        hasChartErrors |= !BindChart(
            billedTrendChart,
            snapshot.BilledTrend,
            "Monthly Billed Amount",
            SeriesChartType.Column,
            AppTheme.InfoColor,
            true);

        hasChartErrors |= !BindChart(
            billingStatusChart,
            snapshot.BillingStatusBreakdown,
            "Bills",
            SeriesChartType.Doughnut,
            AppTheme.PrimaryColor,
            false);

        hasChartErrors |= !BindChart(
            concessionairesByZoneChart,
            snapshot.ConcessionairesByZone,
            "Concessionaires",
            SeriesChartType.Bar,
            AppTheme.AccentColor,
            false);

        hasChartErrors |= !BindChart(
            concessionaireStatusChart,
            snapshot.ConcessionaireStatusMix,
            "Concessionaires",
            SeriesChartType.Pie,
            AppTheme.WarningColor,
            false);

        hasChartErrors |= !BindChart(
            topConcessionairesChart,
            snapshot.TopConcessionairesByAmount,
            "Billed Amount",
            SeriesChartType.Bar,
            AppTheme.SuccessColor,
            true);

        if (hasChartErrors)
        {
            dashboardStatusLabel.ForeColor = AppTheme.WarningColor;
            dashboardStatusLabel.Text = "Some charts could not be rendered, but dashboard data is still available.";
        }
    }

    private bool BindChart(
        Chart chart,
        IReadOnlyList<BillerDashboardPoint> points,
        string seriesName,
        SeriesChartType chartType,
        Color baseColor,
        bool formatAsCurrency)
    {
        if (!CanRenderChart(chart))
        {
            return true;
        }

        chart.SuspendLayout();

        try
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            var area = new ChartArea("MainArea")
            {
                BackColor = Color.White
            };

            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LabelStyle.ForeColor = AppTheme.BodyTextColor;
            area.AxisX.LabelStyle.Font = AppTheme.CaptionFont;

            area.AxisY.MajorGrid.LineColor = Color.FromArgb(233, 239, 244);
            area.AxisY.LabelStyle.ForeColor = AppTheme.MutedTextColor;
            area.AxisY.LabelStyle.Font = AppTheme.CaptionFont;
            area.AxisY.IsStartedFromZero = true;
            if (formatAsCurrency)
            {
                area.AxisY.LabelStyle.Format = "#,##0.##";
            }

            if (chartType is SeriesChartType.Pie or SeriesChartType.Doughnut)
            {
                area.AxisX.Enabled = AxisEnabled.False;
                area.AxisY.Enabled = AxisEnabled.False;
            }

            chart.ChartAreas.Add(area);

            var legend = new Legend("DefaultLegend")
            {
                Docking = Docking.Right,
                Font = AppTheme.CaptionFont,
                ForeColor = AppTheme.BodyTextColor
            };
            chart.Legends.Add(legend);

            var series = new Series(seriesName)
            {
                ChartArea = area.Name,
                ChartType = chartType,
                IsValueShownAsLabel = true,
                LabelForeColor = AppTheme.BodyTextColor,
                Font = AppTheme.CaptionFont
            };

            if (chartType is SeriesChartType.Column or SeriesChartType.Bar)
            {
                series.Color = baseColor;
            }

            if (chartType == SeriesChartType.Doughnut)
            {
                series["DoughnutRadius"] = "50";
            }

            foreach (BillerDashboardPoint point in points)
            {
                string label = FormatPointLabel(point.Label);
                int pointIndex = series.Points.AddXY(label, Convert.ToDouble(point.Value));
                DataPoint dataPoint = series.Points[pointIndex];
                dataPoint.ToolTip = $"{point.Label}: {FormatPointValue(point.Value, formatAsCurrency)}";
                dataPoint.Label = FormatPointValue(point.Value, formatAsCurrency);
            }

            if (series.Points.Count == 0)
            {
                int pointIndex = series.Points.AddXY("No Data", 0D);
                DataPoint emptyPoint = series.Points[pointIndex];
                emptyPoint.Color = Color.FromArgb(224, 232, 238);
                emptyPoint.Label = "No Data";
                emptyPoint.ToolTip = "No data for selected period.";
            }

            chart.Series.Add(series);
            return true;
        }
        catch (Exception ex)
        {
            AppDiagnostics.ReportException($"BillerDashboard chart bind [{seriesName}]", ex, showDialog: false);
            return false;
        }
        finally
        {
            chart.ResumeLayout();
        }
    }

    private static bool CanRenderChart(Control control)
    {
        if (!control.Visible)
        {
            return false;
        }

        return control.ClientSize.Width >= 80
               && control.ClientSize.Height >= 80
               && control.Parent is not null
               && control.Parent.ClientSize.Width > 0
               && control.Parent.ClientSize.Height > 0;
    }

    private static string FormatPointLabel(string rawLabel)
    {
        if (DateTime.TryParseExact(rawLabel, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime month))
        {
            return month.ToString("MMM yyyy", CultureInfo.InvariantCulture);
        }

        return rawLabel;
    }

    private static string FormatPointValue(decimal value, bool currency)
    {
        if (currency)
        {
            return value.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
        }

        return value % 1 == 0
            ? value.ToString("N0", CultureInfo.InvariantCulture)
            : value.ToString("N2", CultureInfo.InvariantCulture);
    }

    private static List<TrendRow> BuildTrendRows(IReadOnlyList<BillerDashboardPoint> points)
    {
        var rows = new List<TrendRow>();
        decimal previous = 0M;

        for (int index = 0; index < points.Count; index++)
        {
            BillerDashboardPoint point = points[index];
            decimal delta = index == 0 ? 0M : point.Value - previous;
            decimal deltaPercent = index == 0 || previous == 0M ? 0M : (delta / previous) * 100M;

            rows.Add(new TrendRow
            {
                Period = FormatPointLabel(point.Label),
                Amount = point.Value,
                Delta = delta,
                DeltaPercent = deltaPercent
            });

            previous = point.Value;
        }

        return rows;
    }

    private static List<ValueRow> BuildValueRows(IReadOnlyList<BillerDashboardPoint> points, string labelColumnName)
    {
        decimal total = points.Sum(x => x.Value);
        var rows = new List<ValueRow>();

        foreach (BillerDashboardPoint point in points)
        {
            decimal share = total <= 0M ? 0M : (point.Value / total) * 100M;

            rows.Add(new ValueRow
            {
                LabelColumnName = labelColumnName,
                Label = point.Label,
                Value = point.Value,
                SharePercent = share
            });
        }

        return rows;
    }

    private static List<TopConcessionaireRow> BuildTopConcessionaireRows(IReadOnlyList<BillerDashboardPoint> points)
    {
        decimal total = points.Sum(x => x.Value);
        decimal runningTotal = 0M;
        var rows = new List<TopConcessionaireRow>();

        for (int index = 0; index < points.Count; index++)
        {
            BillerDashboardPoint point = points[index];
            runningTotal += point.Value;

            decimal share = total <= 0M ? 0M : (point.Value / total) * 100M;
            decimal cumulativeShare = total <= 0M ? 0M : (runningTotal / total) * 100M;

            rows.Add(new TopConcessionaireRow
            {
                Rank = index + 1,
                Concessionaire = point.Label,
                Amount = point.Value,
                SharePercent = share,
                CumulativeSharePercent = cumulativeShare
            });
        }

        return rows;
    }

    private void FormatTrendGrid()
    {
        DataGridViewColumn? periodColumn = billedTrendGrid.Columns[nameof(TrendRow.Period)];
        DataGridViewColumn? amountColumn = billedTrendGrid.Columns[nameof(TrendRow.Amount)];
        DataGridViewColumn? deltaColumn = billedTrendGrid.Columns[nameof(TrendRow.Delta)];
        DataGridViewColumn? deltaPercentColumn = billedTrendGrid.Columns[nameof(TrendRow.DeltaPercent)];

        if (periodColumn is null || amountColumn is null || deltaColumn is null || deltaPercentColumn is null)
        {
            return;
        }

        periodColumn.HeaderText = "Period";
        amountColumn.HeaderText = "Amount";
        deltaColumn.HeaderText = "Change";
        deltaPercentColumn.HeaderText = "Change (%)";

        amountColumn.DefaultCellStyle.Format = "C2";
        deltaColumn.DefaultCellStyle.Format = "C2";
        deltaPercentColumn.DefaultCellStyle.Format = "N2";
        amountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        deltaColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        deltaPercentColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private static void FormatDistributionGrid(DataGridView grid, string labelColumnName)
    {
        DataGridViewColumn? labelColumn = grid.Columns[nameof(ValueRow.Label)];
        DataGridViewColumn? valueColumn = grid.Columns[nameof(ValueRow.Value)];
        DataGridViewColumn? shareColumn = grid.Columns[nameof(ValueRow.SharePercent)];

        if (labelColumn is null || valueColumn is null || shareColumn is null)
        {
            return;
        }

        labelColumn.HeaderText = labelColumnName;
        valueColumn.HeaderText = "Count";
        shareColumn.HeaderText = "Share (%)";

        if (grid.Columns[nameof(ValueRow.LabelColumnName)] is { } technicalColumn)
        {
            technicalColumn.Visible = false;
        }

        valueColumn.DefaultCellStyle.Format = "N0";
        shareColumn.DefaultCellStyle.Format = "N2";
        valueColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        shareColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private void FormatTopConcessionairesGrid()
    {
        DataGridViewColumn? rankColumn = topConcessionairesGrid.Columns[nameof(TopConcessionaireRow.Rank)];
        DataGridViewColumn? concessionaireColumn = topConcessionairesGrid.Columns[nameof(TopConcessionaireRow.Concessionaire)];
        DataGridViewColumn? amountColumn = topConcessionairesGrid.Columns[nameof(TopConcessionaireRow.Amount)];
        DataGridViewColumn? shareColumn = topConcessionairesGrid.Columns[nameof(TopConcessionaireRow.SharePercent)];
        DataGridViewColumn? cumulativeShareColumn = topConcessionairesGrid.Columns[nameof(TopConcessionaireRow.CumulativeSharePercent)];

        if (rankColumn is null || concessionaireColumn is null || amountColumn is null || shareColumn is null || cumulativeShareColumn is null)
        {
            return;
        }

        rankColumn.HeaderText = "Rank";
        concessionaireColumn.HeaderText = "Concessionaire";
        amountColumn.HeaderText = "Billed Amount";
        shareColumn.HeaderText = "Share (%)";
        cumulativeShareColumn.HeaderText = "Cumulative Share (%)";

        rankColumn.FillWeight = 35F;
        concessionaireColumn.FillWeight = 230F;
        amountColumn.FillWeight = 110F;
        shareColumn.FillWeight = 80F;
        cumulativeShareColumn.FillWeight = 110F;

        amountColumn.DefaultCellStyle.Format = "N2";
        shareColumn.DefaultCellStyle.Format = "N2";
        cumulativeShareColumn.DefaultCellStyle.Format = "N2";

        rankColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        amountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        shareColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        cumulativeShareColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private void QueueChartRebind()
    {
        if (_isLoading || _isChartRebindQueued || _snapshot is null || IsDisposed || !IsHandleCreated)
        {
            return;
        }

        _isChartRebindQueued = true;
        try
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                _isChartRebindQueued = false;

                if (_isLoading || _snapshot is null || IsDisposed || !IsHandleCreated)
                {
                    return;
                }

                try
                {
                    BindAllCharts(_snapshot);
                }
                catch (Exception ex)
                {
                    AppDiagnostics.ReportException("BillerDashboard queued chart rebind", ex, showDialog: false);
                }
            }));
        }
        catch (Exception ex)
        {
            _isChartRebindQueued = false;
            AppDiagnostics.ReportException("BillerDashboard BeginInvoke rebind", ex, showDialog: false);
        }
    }

    private void SetBusyState(bool isBusy, string? busyMessage = null)
    {
        prevMonthButton.Enabled = !isBusy;
        nextMonthButton.Enabled = !isBusy;
        refreshButton.Enabled = !isBusy;
        reportsTabControl.Enabled = !isBusy;

        if (isBusy)
        {
            dashboardStatusLabel.ForeColor = AppTheme.MutedTextColor;
            dashboardStatusLabel.Text = busyMessage ?? "Working...";
        }
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        await RefreshDashboardAsync();
    }

    private async void prevMonthButton_Click(object sender, EventArgs e)
    {
        _selectedMonth = _selectedMonth.AddMonths(-1);
        UpdateSelectedMonthDisplay();
        await RefreshDashboardAsync();
    }

    private async void nextMonthButton_Click(object sender, EventArgs e)
    {
        _selectedMonth = _selectedMonth.AddMonths(1);
        UpdateSelectedMonthDisplay();
        await RefreshDashboardAsync();
    }

    private void BillerDashboardControl_SizeChanged(object? sender, EventArgs e)
    {
        RestoreSplitterDistancesDeferred();
        QueueChartRebind();
    }

    private void reportsTabControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        QueueChartRebind();
    }

    private sealed class SplitterSettingAccessor(
        Func<UserInterfaceSettings, int> read,
        Action<UserInterfaceSettings, int> write)
    {
        public int Read(UserInterfaceSettings settings)
        {
            return read(settings);
        }

        public void Write(UserInterfaceSettings settings, int value)
        {
            write(settings, value);
        }
    }

    private sealed class TrendRow
    {
        public string Period { get; init; } = string.Empty;

        public decimal Amount { get; init; }

        public decimal Delta { get; init; }

        public decimal DeltaPercent { get; init; }
    }

    private sealed class ValueRow
    {
        public string LabelColumnName { get; init; } = string.Empty;

        public string Label { get; init; } = string.Empty;

        public decimal Value { get; init; }

        public decimal SharePercent { get; init; }
    }

    private sealed class TopConcessionaireRow
    {
        public int Rank { get; init; }

        public string Concessionaire { get; init; } = string.Empty;

        public decimal Amount { get; init; }

        public decimal SharePercent { get; init; }

        public decimal CumulativeSharePercent { get; init; }
    }
}
