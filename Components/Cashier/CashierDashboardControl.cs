using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Models;
using WDBS_2026.Services.Dashboard;

namespace WDBS_2026.Components.Cashier;

public partial class CashierDashboardControl : UserControl
{
    private readonly AuthenticatedUserDto _user;
    private CashierDashboardSnapshot? _snapshot;
    private TabPage? _monthlyComparisonTabPage;
    private SplitContainer? _monthlyTrendSplitContainer;
    private Panel? _monthlyTrendCardPanel;
    private Label? _monthlyTrendTitleLabel;
    private TableLayoutPanel? _monthlyTrendTablePanel;
    private Label? _monthlyTrendTableTitleLabel;
    private DataGridView? _monthlyTrendGrid;
    private Chart? _monthlyTrendChart;
    private DateTime _selectedMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    private bool _isLoading;
    private bool _isChartRebindQueued;

    public CashierDashboardControl() 
        : this(new AuthenticatedUserDto
        {
            UserId = 0,
            Username = "designer",
            FullName = "Dashboard Designer",
            Role = UserRole.Cashier
        })
    {
    }

    public CashierDashboardControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        InitializeMonthlyComparisonTab();
        ApplyTheme();
        ConfigureGrids();
        ConfigureCharts();
        UpdateSelectedMonthDisplay();

        if (IsDesignerHosted())
        {
            BindDesignTimeSample();
            return;
        }

        SizeChanged += CashierDashboardControl_SizeChanged;
        reportsTabControl.SelectedIndexChanged += reportsTabControl_SelectedIndexChanged;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (IsDesignerHosted())
        {
            return;
        }

        await RefreshDashboardAsync();
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
            SetBusyState(true, $"Loading cashier dashboard for {_selectedMonth:MMM yyyy}...");
            CashierDashboardSnapshot snapshot = await CashierDashboardDataService.GetSnapshotAsync(_user.Role, _selectedMonth);
            _snapshot = snapshot;

            BindKpis(snapshot);
            BindTables(snapshot);
            UpdateReportTitles(snapshot);
            BindAllCharts(snapshot);
            UpdateSelectedMonthDisplay(snapshot.PeriodStart);

            dashboardStatusLabel.ForeColor = AppTheme.MutedTextColor;
            dashboardStatusLabel.Text = $"Updated {DateTime.Now:MMM dd, yyyy hh:mm tt} ({snapshot.PeriodStart:MMM yyyy})";
        }
        catch (Exception ex)
        {
            dashboardStatusLabel.ForeColor = AppTheme.DangerColor;
            dashboardStatusLabel.Text = "Failed to load cashier dashboard data.";
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
        dashboardPanel.BackColor = AppTheme.ShellBackgroundColor;
        toolbarPanel.BackColor = AppTheme.ShellBackgroundColor;
        metricsLayoutPanel.BackColor = AppTheme.ShellBackgroundColor;

        AppTheme.ApplyPageTitle(headingLabel);
        AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(prevDayButton, ButtonSeverity.Neutral);
        AppTheme.ApplySeverityButton(nextDayButton, ButtonSeverity.Neutral);
        AppTheme.ApplySubtitle(dashboardStatusLabel);
        AppTheme.ApplySubtitle(selectedDateLabel);

        foreach (Panel card in new[]
                 {
                     totalCollectedCardPanel,
                     receiptCountCardPanel,
                     scfCollectedCardPanel,
                     outstandingAccountsCardPanel,
                     collectionTrendCardPanel,
                     paymentTypesCardPanel,
                     topAccountsCardPanel,
                     recentCollectionsCardPanel
                 })
        {
            AppTheme.ApplyCard(card);
        }

        if (_monthlyTrendCardPanel is not null)
        {
            AppTheme.ApplyCard(_monthlyTrendCardPanel);
        }

        foreach (Label label in new[]
                 {
                     collectionTrendTitleLabel,
                     paymentTypesTitleLabel,
                     topAccountsTitleLabel,
                     recentCollectionsTitleLabel,
                     collectionTrendTableTitleLabel,
                     paymentTypesTableTitleLabel,
                     topAccountsTableTitleLabel,
                     recentCollectionsTableTitleLabel
                 })
        {
            label.Font = AppTheme.SectionFont;
            label.ForeColor = AppTheme.BodyTextColor;
        }

        if (_monthlyTrendTitleLabel is not null)
        {
            _monthlyTrendTitleLabel.Font = AppTheme.SectionFont;
            _monthlyTrendTitleLabel.ForeColor = AppTheme.BodyTextColor;
        }

        if (_monthlyTrendTableTitleLabel is not null)
        {
            _monthlyTrendTableTitleLabel.Font = AppTheme.SectionFont;
            _monthlyTrendTableTitleLabel.ForeColor = AppTheme.BodyTextColor;
        }

        foreach (Label label in new[]
                 {
                     totalCollectedCaptionLabel,
                     receiptCountCaptionLabel,
                     scfCollectedCaptionLabel,
                     outstandingAccountsCaptionLabel,
                     recentCollectionsSummaryLabel
                 })
        {
            label.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            label.ForeColor = label == recentCollectionsSummaryLabel ? AppTheme.MutedTextColor : AppTheme.MutedTextColor;
        }

        totalCollectedCaptionLabel.Text = "Collected (Monthly)";
        receiptCountCaptionLabel.Text = "ORs (Monthly)";
        scfCollectedCaptionLabel.Text = "SCF (Monthly)";
        outstandingAccountsCaptionLabel.Text = "Collection Rate";

        foreach (Label label in new[]
                 {
                     totalCollectedValueLabel,
                     receiptCountValueLabel,
                     scfCollectedValueLabel,
                     outstandingAccountsValueLabel
                 })
        {
            label.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label.ForeColor = AppTheme.PrimaryDarkColor;
        }

        reportsTabControl.Font = AppTheme.BodyFont;
        ApplyCompactKpiLayout();
    }

    private void ApplyCompactKpiLayout()
    {
        ConfigureKpiCard(totalCollectedCardPanel, totalCollectedCaptionLabel, totalCollectedValueLabel);
        ConfigureKpiCard(receiptCountCardPanel, receiptCountCaptionLabel, receiptCountValueLabel);
        ConfigureKpiCard(scfCollectedCardPanel, scfCollectedCaptionLabel, scfCollectedValueLabel);
        ConfigureKpiCard(outstandingAccountsCardPanel, outstandingAccountsCaptionLabel, outstandingAccountsValueLabel);
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
        var grids = new List<DataGridView>
        {
            collectionTrendGrid,
            paymentTypesGrid,
            topAccountsGrid,
            recentCollectionsGrid
        };

        if (_monthlyTrendGrid is not null)
        {
            grids.Add(_monthlyTrendGrid);
        }

        foreach (DataGridView grid in grids)
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
        collectionTrendChart = EnsureChart(
            collectionTrendChart,
            collectionTrendCardPanel,
            collectionTrendTitleLabel,
            nameof(collectionTrendChart));

        paymentTypesChart = EnsureChart(
            paymentTypesChart,
            paymentTypesCardPanel,
            paymentTypesTitleLabel,
            nameof(paymentTypesChart));

        topAccountsChart = EnsureChart(
            topAccountsChart,
            topAccountsCardPanel,
            topAccountsTitleLabel,
            nameof(topAccountsChart));

        if (_monthlyTrendCardPanel is not null && _monthlyTrendTitleLabel is not null)
        {
            _monthlyTrendChart = EnsureChart(
                _monthlyTrendChart,
                _monthlyTrendCardPanel,
                _monthlyTrendTitleLabel,
                nameof(_monthlyTrendChart));
        }

        collectionTrendChart.SuppressExceptions = true;
        paymentTypesChart.SuppressExceptions = true;
        topAccountsChart.SuppressExceptions = true;

        if (_monthlyTrendChart is not null)
        {
            _monthlyTrendChart.SuppressExceptions = true;
        }
    }

    private void InitializeMonthlyComparisonTab()
    {
        if (_monthlyComparisonTabPage is not null)
        {
            return;
        }

        _monthlyComparisonTabPage = new TabPage
        {
            Name = "monthlyComparisonTabPage",
            Text = "Monthly Collection Comparison",
            Padding = new Padding(8, 6, 8, 6),
            UseVisualStyleBackColor = true
        };

        _monthlyTrendSplitContainer = new SplitContainer
        {
            Name = "monthlyTrendSplitContainer",
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 240,
            Panel1MinSize = 160
        };

        _monthlyTrendCardPanel = new Panel
        {
            Name = "monthlyTrendCardPanel",
            Dock = DockStyle.Fill,
            Padding = new Padding(16, 9, 16, 11)
        };

        _monthlyTrendTitleLabel = new Label
        {
            Name = "monthlyTrendTitleLabel",
            Dock = DockStyle.Top,
            Height = 21,
            TextAlign = ContentAlignment.MiddleLeft,
            Text = "Monthly Collection Comparison"
        };

        _monthlyTrendChart = new Chart
        {
            Name = "monthlyTrendChart",
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            BackColor = Color.White
        };

        _monthlyTrendCardPanel.Controls.Add(_monthlyTrendChart);
        _monthlyTrendCardPanel.Controls.Add(_monthlyTrendTitleLabel);
        _monthlyTrendTitleLabel.BringToFront();

        _monthlyTrendTablePanel = new TableLayoutPanel
        {
            Name = "monthlyTrendTablePanel",
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            RowCount = 2
        };
        _monthlyTrendTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _monthlyTrendTablePanel.RowStyles.Add(new RowStyle());
        _monthlyTrendTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        _monthlyTrendTableTitleLabel = new Label
        {
            Name = "monthlyTrendTableTitleLabel",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 6),
            Text = "Monthly Collected vs Uncollected"
        };

        _monthlyTrendGrid = new DataGridView
        {
            Name = "monthlyTrendGrid",
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        };

        _monthlyTrendTablePanel.Controls.Add(_monthlyTrendTableTitleLabel, 0, 0);
        _monthlyTrendTablePanel.Controls.Add(_monthlyTrendGrid, 0, 1);

        _monthlyTrendSplitContainer.Panel1.Controls.Add(_monthlyTrendCardPanel);
        _monthlyTrendSplitContainer.Panel2.Controls.Add(_monthlyTrendTablePanel);
        _monthlyComparisonTabPage.Controls.Add(_monthlyTrendSplitContainer);

        reportsTabControl.Controls.Add(_monthlyComparisonTabPage);
        EnsureReportTabSequence();
    }

    private void EnsureReportTabSequence()
    {
        if (_monthlyComparisonTabPage is null)
        {
            return;
        }

        reportsTabControl.TabPages.Clear();
        reportsTabControl.TabPages.Add(collectionTrendTabPage);
        reportsTabControl.TabPages.Add(_monthlyComparisonTabPage);
        reportsTabControl.TabPages.Add(topAccountsTabPage);
        reportsTabControl.TabPages.Add(paymentTypesTabPage);
        reportsTabControl.TabPages.Add(recentCollectionsTabPage);
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
        DateTime sampleDate = DateTime.Today;
        DateTime samplePeriodStart = new(sampleDate.Year, sampleDate.Month, 1);
        var sample = new CashierDashboardSnapshot(
            samplePeriodStart,
            samplePeriodStart.AddMonths(1),
            128450.75M,
            42,
            18320.25M,
            137,
            87.25M,
            new[]
            {
                new CashierDashboardPoint(samplePeriodStart.AddDays(0).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 4250M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 6380M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 5125M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(3).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 7430M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(4).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 3890M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(5).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 8040M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(6).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 6890M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(7).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 5785M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(8).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 7440M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(9).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 6630M)
            },
            new[]
            {
                new CashierDashboardPoint(samplePeriodStart.AddDays(0).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 780M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 540M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 910M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(3).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 620M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(4).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 1050M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(5).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 430M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(6).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 860M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(7).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 710M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(8).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 575M),
                new CashierDashboardPoint(samplePeriodStart.AddDays(9).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 690M)
            },
            BuildMonthlySamplePoints(samplePeriodStart, 96500M, 3900M),
            BuildMonthlySamplePoints(samplePeriodStart, 7200M, 410M),
            new[]
            {
                new CashierDashboardPoint("CASH", 28),
                new CashierDashboardPoint("GCASH", 9),
                new CashierDashboardPoint("CHECK", 3),
                new CashierDashboardPoint("BANK TRANSFER", 2)
            },
            new[]
            {
                new CashierDashboardPoint("101-0001 - Juan Dela Cruz", 16420.50M),
                new CashierDashboardPoint("101-0012 - Maria Santos", 14980.25M),
                new CashierDashboardPoint("102-0004 - Rizal Trading", 11840.00M),
                new CashierDashboardPoint("103-0018 - Lopez Store", 10210.50M),
                new CashierDashboardPoint("104-0007 - Ana Reyes", 9340.75M),
                new CashierDashboardPoint("104-0012 - Noel Abao", 8985.00M),
                new CashierDashboardPoint("105-0002 - Vista Mart", 8742.30M),
                new CashierDashboardPoint("105-0017 - Lito Ramos", 8320.40M),
                new CashierDashboardPoint("106-0009 - Lily Padilla", 8055.75M),
                new CashierDashboardPoint("106-0014 - Delta Bakery", 7940.10M)
            },
            new[]
            {
                new CashierRecentCollection(sampleDate.AddDays(-1).AddMinutes(-15), "0004211", "101-0001 - Juan Dela Cruz", "Cash", 2450.50M),
                new CashierRecentCollection(sampleDate.AddDays(-2).AddMinutes(-32), "0004210", "102-0004 - Rizal Trading", "GCash", 5180.25M),
                new CashierRecentCollection(sampleDate.AddDays(-3).AddMinutes(-54), "0004209", "104-0007 - Ana Reyes", "Cash", 1795.00M),
                new CashierRecentCollection(sampleDate.AddDays(-4).AddMinutes(-76), "0004208", "103-0018 - Lopez Store", "Check", 6240.75M)
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

        selectedDateLabel.Text = _selectedMonth.ToString("MMM yyyy", CultureInfo.InvariantCulture);
    }

    private void BindKpis(CashierDashboardSnapshot snapshot)
    {
        totalCollectedValueLabel.Text = snapshot.TotalCollectedAmount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
        receiptCountValueLabel.Text = snapshot.OfficialReceiptCount.ToString("N0", CultureInfo.InvariantCulture);
        scfCollectedValueLabel.Text = snapshot.ScfCollectedAmount.ToString("C2", CultureInfo.GetCultureInfo("en-PH"));
        outstandingAccountsValueLabel.Text = $"{snapshot.MonthlyCollectionRate:N2}%";
        recentCollectionsSummaryLabel.Text = "Showing latest cashier activity entries from the collection log.";
    }

    private void UpdateReportTitles(CashierDashboardSnapshot snapshot)
    {
        DateTime trendStart = snapshot.PeriodStart;
        DateTime trendEnd = snapshot.PeriodEnd.AddDays(-1);
        DateTime monthlyTrendStart = snapshot.PeriodStart.AddMonths(-11);

        collectionTrendTitleLabel.Text = $"Rolling Daily Collection Trend ({trendStart:MMM dd} to {trendEnd:MMM dd, yyyy})";
        collectionTrendTableTitleLabel.Text = "Rolling Daily Collection";
        if (_monthlyTrendTitleLabel is not null)
        {
            _monthlyTrendTitleLabel.Text = $"Rolling Monthly Collection Trend ({monthlyTrendStart:MMM yyyy} to {snapshot.PeriodStart:MMM yyyy})";
        }

        if (_monthlyTrendTableTitleLabel is not null)
        {
            _monthlyTrendTableTitleLabel.Text = "Rolling Monthly Collection";
        }

        paymentTypesTitleLabel.Text = $"Payment Type Mix ({snapshot.PeriodStart:MMM yyyy})";
        paymentTypesTableTitleLabel.Text = "Payment Method Distribution";
        topAccountsTitleLabel.Text = $"Top 10 Collected Accounts ({snapshot.PeriodStart:MMM yyyy})";
        topAccountsTableTitleLabel.Text = "Top 10 Highest Collected Concessionaires";
        recentCollectionsTitleLabel.Text = "Recent Cashier Activity";
        recentCollectionsTableTitleLabel.Text = "Latest Cashier Activity";
    }

    private void BindTables(CashierDashboardSnapshot snapshot)
    {
        collectionTrendGrid.DataSource = BuildTrendRows(snapshot.CollectionTrend, snapshot.UncollectedTrend);
        if (_monthlyTrendGrid is not null)
        {
            _monthlyTrendGrid.DataSource = BuildTrendRows(snapshot.MonthlyCollectionTrend, snapshot.MonthlyUncollectedTrend);
        }
        paymentTypesGrid.DataSource = BuildValueRows(snapshot.PaymentTypeBreakdown, "Payment Type");
        topAccountsGrid.DataSource = BuildTopAccountsRows(snapshot.TopCollectedAccounts);
        recentCollectionsGrid.DataSource = BuildRecentCollectionRows(snapshot.RecentCollections);

        FormatTrendGrid();
        FormatMonthlyTrendGrid();
        FormatDistributionGrid(paymentTypesGrid, "Payment Type");
        FormatTopAccountsGrid();
        FormatRecentCollectionsGrid();
    }

    private void BindAllCharts(CashierDashboardSnapshot snapshot)
    {
        bool hasChartErrors = false;

        hasChartErrors |= !BindCollectionComparisonChart(
            collectionTrendChart,
            snapshot.CollectionTrend,
            snapshot.UncollectedTrend);

        if (_monthlyTrendChart is not null)
        {
            hasChartErrors |= !BindCollectionComparisonChart(
                _monthlyTrendChart,
                snapshot.MonthlyCollectionTrend,
                snapshot.MonthlyUncollectedTrend);
        }

        hasChartErrors |= !BindChart(
            paymentTypesChart,
            snapshot.PaymentTypeBreakdown,
            "Payments",
            SeriesChartType.Doughnut,
            AppTheme.PrimaryColor,
            false);

        hasChartErrors |= !BindChart(
            topAccountsChart,
            snapshot.TopCollectedAccounts,
            "Collected Amount",
            SeriesChartType.Bar,
            AppTheme.SuccessColor,
            true);

        if (hasChartErrors)
        {
            dashboardStatusLabel.ForeColor = AppTheme.WarningColor;
            dashboardStatusLabel.Text = "Some charts could not be rendered, but dashboard data is still available.";
        }
    }

    private bool BindCollectionComparisonChart(
        Chart chart,
        IReadOnlyList<CashierDashboardPoint> collectedPoints,
        IReadOnlyList<CashierDashboardPoint> uncollectedPoints)
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
            area.AxisY.LabelStyle.Format = "\u20B1#,##0.##";
            area.AxisY.IsStartedFromZero = true;

            chart.ChartAreas.Add(area);

            var legend = new Legend("DefaultLegend")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = AppTheme.CaptionFont,
                ForeColor = AppTheme.BodyTextColor
            };
            chart.Legends.Add(legend);

            var collectedSeries = new Series("Collected")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = AppTheme.SuccessColor,
                IsValueShownAsLabel = false,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6
            };

            var uncollectedSeries = new Series("Uncollected")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = AppTheme.WarningColor,
                IsValueShownAsLabel = false,
                MarkerStyle = MarkerStyle.Diamond,
                MarkerSize = 6
            };

            int pointCount = Math.Max(collectedPoints.Count, uncollectedPoints.Count);
            for (int index = 0; index < pointCount; index++)
            {
                CashierDashboardPoint? collected = index < collectedPoints.Count ? collectedPoints[index] : null;
                CashierDashboardPoint? uncollected = index < uncollectedPoints.Count ? uncollectedPoints[index] : null;

                string rawLabel = collected?.Label ?? uncollected?.Label ?? string.Empty;
                string displayLabel = FormatPointLabel(rawLabel);
                decimal collectedValue = collected?.Value ?? 0M;
                decimal uncollectedValue = uncollected?.Value ?? 0M;

                int collectedPointIndex = collectedSeries.Points.AddXY(displayLabel, Convert.ToDouble(collectedValue));
                collectedSeries.Points[collectedPointIndex].ToolTip = $"{rawLabel} | Collected: {FormatPointValue(collectedValue, currency: true)}";

                int uncollectedPointIndex = uncollectedSeries.Points.AddXY(displayLabel, Convert.ToDouble(uncollectedValue));
                uncollectedSeries.Points[uncollectedPointIndex].ToolTip = $"{rawLabel} | Uncollected: {FormatPointValue(uncollectedValue, currency: true)}";
            }

            chart.Series.Add(collectedSeries);
            chart.Series.Add(uncollectedSeries);

            if (pointCount == 0)
            {
                var emptySeries = new Series("No Data")
                {
                    ChartArea = area.Name,
                    ChartType = SeriesChartType.Line,
                    Color = Color.FromArgb(224, 232, 238),
                    IsValueShownAsLabel = true
                };
                emptySeries.Points.AddXY("No Data", 0D);
                chart.Series.Add(emptySeries);
            }

            return true;
        }
        catch (Exception ex)
        {
            AppDiagnostics.ReportException("CashierDashboard chart bind [Collection Comparison]", ex, showDialog: false);
            return false;
        }
        finally
        {
            chart.ResumeLayout();
        }
    }

    private bool BindChart(
        Chart chart,
        IReadOnlyList<CashierDashboardPoint> points,
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

            foreach (CashierDashboardPoint point in points)
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
            AppDiagnostics.ReportException($"CashierDashboard chart bind [{seriesName}]", ex, showDialog: false);
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
        if (DateTime.TryParseExact(rawLabel, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime day))
        {
            return day.ToString("MMM dd", CultureInfo.InvariantCulture);
        }

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

    private static List<TrendRow> BuildTrendRows(
        IReadOnlyList<CashierDashboardPoint> collectedPoints,
        IReadOnlyList<CashierDashboardPoint> uncollectedPoints)
    {
        var rows = new List<TrendRow>();
        decimal previousCollected = 0M;

        int rowCount = Math.Max(collectedPoints.Count, uncollectedPoints.Count);

        for (int index = 0; index < rowCount; index++)
        {
            CashierDashboardPoint? collected = index < collectedPoints.Count ? collectedPoints[index] : null;
            CashierDashboardPoint? uncollected = index < uncollectedPoints.Count ? uncollectedPoints[index] : null;

            string rawLabel = collected?.Label ?? uncollected?.Label ?? string.Empty;
            decimal collectedAmount = collected?.Value ?? 0M;
            decimal uncollectedAmount = uncollected?.Value ?? 0M;
            decimal netAmount = collectedAmount - uncollectedAmount;
            decimal delta = index == 0 ? 0M : collectedAmount - previousCollected;
            decimal deltaPercent = index == 0 || previousCollected == 0M ? 0M : (delta / previousCollected) * 100M;

            rows.Add(new TrendRow
            {
                Period = FormatPointLabel(rawLabel),
                CollectedAmount = collectedAmount,
                UncollectedAmount = uncollectedAmount,
                NetAmount = netAmount,
                CollectedDelta = delta,
                DeltaPercent = deltaPercent
            });

            previousCollected = collectedAmount;
        }

        return rows;
    }

    private static List<ValueRow> BuildValueRows(IReadOnlyList<CashierDashboardPoint> points, string labelColumnName)
    {
        decimal total = points.Sum(x => x.Value);
        var rows = new List<ValueRow>();

        foreach (CashierDashboardPoint point in points)
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

    private static IReadOnlyList<CashierDashboardPoint> BuildMonthlySamplePoints(
        DateTime periodStart,
        decimal baseAmount,
        decimal variation)
    {
        var points = new List<CashierDashboardPoint>(12);
        DateTime cursor = periodStart.AddMonths(-11);

        for (int index = 0; index < 12; index++)
        {
            decimal value = baseAmount + (index * variation);
            points.Add(new CashierDashboardPoint(cursor.ToString("yyyy-MM", CultureInfo.InvariantCulture), value));
            cursor = cursor.AddMonths(1);
        }

        return points;
    }

    private static List<TopAccountRow> BuildTopAccountsRows(IReadOnlyList<CashierDashboardPoint> points)
    {
        decimal total = points.Sum(x => x.Value);
        decimal runningTotal = 0M;
        var rows = new List<TopAccountRow>();

        for (int index = 0; index < points.Count; index++)
        {
            CashierDashboardPoint point = points[index];
            runningTotal += point.Value;
            decimal share = total <= 0M ? 0M : (point.Value / total) * 100M;

            rows.Add(new TopAccountRow
            {
                Rank = index + 1,
                Account = point.Label,
                Amount = point.Value,
                SharePercent = share,
                CumulativeSharePercent = total <= 0M ? 0M : (runningTotal / total) * 100M
            });
        }

        return rows;
    }

    private static List<RecentCollectionRow> BuildRecentCollectionRows(IReadOnlyList<CashierRecentCollection> items)
    {
        return items
            .Select(item => new RecentCollectionRow
            {
                CollectionDate = item.CollectionDate,
                OrNumber = item.OrNumber,
                Concessionaire = item.Concessionaire,
                PaymentType = item.PaymentType,
                Amount = item.Amount
            })
            .ToList();
    }

    private void FormatTrendGrid()
    {
        DataGridViewColumn? periodColumn = collectionTrendGrid.Columns[nameof(TrendRow.Period)];
        DataGridViewColumn? collectedColumn = collectionTrendGrid.Columns[nameof(TrendRow.CollectedAmount)];
        DataGridViewColumn? uncollectedColumn = collectionTrendGrid.Columns[nameof(TrendRow.UncollectedAmount)];
        DataGridViewColumn? netColumn = collectionTrendGrid.Columns[nameof(TrendRow.NetAmount)];
        DataGridViewColumn? deltaColumn = collectionTrendGrid.Columns[nameof(TrendRow.CollectedDelta)];
        DataGridViewColumn? deltaPercentColumn = collectionTrendGrid.Columns[nameof(TrendRow.DeltaPercent)];

        if (periodColumn is null
            || collectedColumn is null
            || uncollectedColumn is null
            || netColumn is null
            || deltaColumn is null
            || deltaPercentColumn is null)
        {
            return;
        }

        periodColumn.HeaderText = "Date";
        collectedColumn.HeaderText = "Collected";
        uncollectedColumn.HeaderText = "Uncollected";
        netColumn.HeaderText = "Net";
        deltaColumn.HeaderText = "Collected Change";
        deltaPercentColumn.HeaderText = "Change (%)";

        CultureInfo phpCulture = CultureInfo.GetCultureInfo("en-PH");
        collectedColumn.DefaultCellStyle.Format = "C2";
        uncollectedColumn.DefaultCellStyle.Format = "C2";
        netColumn.DefaultCellStyle.Format = "C2";
        deltaColumn.DefaultCellStyle.Format = "C2";
        collectedColumn.DefaultCellStyle.FormatProvider = phpCulture;
        uncollectedColumn.DefaultCellStyle.FormatProvider = phpCulture;
        netColumn.DefaultCellStyle.FormatProvider = phpCulture;
        deltaColumn.DefaultCellStyle.FormatProvider = phpCulture;
        deltaPercentColumn.DefaultCellStyle.Format = "N2";
        collectedColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        uncollectedColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        netColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        deltaColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        deltaPercentColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        periodColumn.FillWeight = 85F;
        collectedColumn.FillWeight = 110F;
        uncollectedColumn.FillWeight = 115F;
        netColumn.FillWeight = 95F;
        deltaColumn.FillWeight = 130F;
        deltaPercentColumn.FillWeight = 75F;
    }

    private void FormatMonthlyTrendGrid()
    {
        if (_monthlyTrendGrid is null)
        {
            return;
        }

        DataGridViewColumn? periodColumn = _monthlyTrendGrid.Columns[nameof(TrendRow.Period)];
        DataGridViewColumn? collectedColumn = _monthlyTrendGrid.Columns[nameof(TrendRow.CollectedAmount)];
        DataGridViewColumn? uncollectedColumn = _monthlyTrendGrid.Columns[nameof(TrendRow.UncollectedAmount)];
        DataGridViewColumn? netColumn = _monthlyTrendGrid.Columns[nameof(TrendRow.NetAmount)];
        DataGridViewColumn? deltaColumn = _monthlyTrendGrid.Columns[nameof(TrendRow.CollectedDelta)];
        DataGridViewColumn? deltaPercentColumn = _monthlyTrendGrid.Columns[nameof(TrendRow.DeltaPercent)];

        if (periodColumn is null
            || collectedColumn is null
            || uncollectedColumn is null
            || netColumn is null
            || deltaColumn is null
            || deltaPercentColumn is null)
        {
            return;
        }

        periodColumn.HeaderText = "Month";
        collectedColumn.HeaderText = "Collected";
        uncollectedColumn.HeaderText = "Uncollected";
        netColumn.HeaderText = "Net";
        deltaColumn.HeaderText = "Collected Change";
        deltaPercentColumn.HeaderText = "Change (%)";

        CultureInfo phpCulture = CultureInfo.GetCultureInfo("en-PH");
        collectedColumn.DefaultCellStyle.Format = "C2";
        uncollectedColumn.DefaultCellStyle.Format = "C2";
        netColumn.DefaultCellStyle.Format = "C2";
        deltaColumn.DefaultCellStyle.Format = "C2";
        collectedColumn.DefaultCellStyle.FormatProvider = phpCulture;
        uncollectedColumn.DefaultCellStyle.FormatProvider = phpCulture;
        netColumn.DefaultCellStyle.FormatProvider = phpCulture;
        deltaColumn.DefaultCellStyle.FormatProvider = phpCulture;
        deltaPercentColumn.DefaultCellStyle.Format = "N2";
        collectedColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        uncollectedColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        netColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        deltaColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        deltaPercentColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        periodColumn.FillWeight = 85F;
        collectedColumn.FillWeight = 110F;
        uncollectedColumn.FillWeight = 115F;
        netColumn.FillWeight = 95F;
        deltaColumn.FillWeight = 130F;
        deltaPercentColumn.FillWeight = 75F;
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

    private void FormatTopAccountsGrid()
    {
        DataGridViewColumn? rankColumn = topAccountsGrid.Columns[nameof(TopAccountRow.Rank)];
        DataGridViewColumn? accountColumn = topAccountsGrid.Columns[nameof(TopAccountRow.Account)];
        DataGridViewColumn? amountColumn = topAccountsGrid.Columns[nameof(TopAccountRow.Amount)];
        DataGridViewColumn? shareColumn = topAccountsGrid.Columns[nameof(TopAccountRow.SharePercent)];
        DataGridViewColumn? cumulativeColumn = topAccountsGrid.Columns[nameof(TopAccountRow.CumulativeSharePercent)];

        if (rankColumn is null || accountColumn is null || amountColumn is null || shareColumn is null || cumulativeColumn is null)
        {
            return;
        }

        rankColumn.HeaderText = "Top";
        accountColumn.HeaderText = "Concessionaire";
        amountColumn.HeaderText = "Collected Amount";
        shareColumn.HeaderText = "Share (%)";
        cumulativeColumn.HeaderText = "Cumulative Share (%)";

        rankColumn.FillWeight = 35F;
        accountColumn.FillWeight = 240F;
        amountColumn.FillWeight = 110F;
        shareColumn.FillWeight = 80F;
        cumulativeColumn.FillWeight = 110F;

        amountColumn.DefaultCellStyle.Format = "N2";
        shareColumn.DefaultCellStyle.Format = "N2";
        cumulativeColumn.DefaultCellStyle.Format = "N2";

        rankColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        amountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        shareColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        cumulativeColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    }

    private void FormatRecentCollectionsGrid()
    {
        DataGridViewColumn? dateColumn = recentCollectionsGrid.Columns[nameof(RecentCollectionRow.CollectionDate)];
        DataGridViewColumn? orNumberColumn = recentCollectionsGrid.Columns[nameof(RecentCollectionRow.OrNumber)];
        DataGridViewColumn? concessionaireColumn = recentCollectionsGrid.Columns[nameof(RecentCollectionRow.Concessionaire)];
        DataGridViewColumn? paymentTypeColumn = recentCollectionsGrid.Columns[nameof(RecentCollectionRow.PaymentType)];
        DataGridViewColumn? amountColumn = recentCollectionsGrid.Columns[nameof(RecentCollectionRow.Amount)];

        if (dateColumn is null || orNumberColumn is null || concessionaireColumn is null || paymentTypeColumn is null || amountColumn is null)
        {
            return;
        }

        dateColumn.HeaderText = "Date / Time";
        orNumberColumn.HeaderText = "OR Number";
        concessionaireColumn.HeaderText = "Concessionaire";
        paymentTypeColumn.HeaderText = "Payment Type";
        amountColumn.HeaderText = "Collected";

        dateColumn.FillWeight = 90F;
        orNumberColumn.FillWeight = 70F;
        concessionaireColumn.FillWeight = 210F;
        paymentTypeColumn.FillWeight = 80F;
        amountColumn.FillWeight = 85F;

        dateColumn.DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt";
        amountColumn.DefaultCellStyle.Format = "N2";
        amountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                    AppDiagnostics.ReportException("CashierDashboard queued chart rebind", ex, showDialog: false);
                }
            }));
        }
        catch (Exception ex)
        {
            _isChartRebindQueued = false;
            AppDiagnostics.ReportException("CashierDashboard BeginInvoke rebind", ex, showDialog: false);
        }
    }

    private void SetBusyState(bool isBusy, string? busyMessage = null)
    {
        prevDayButton.Enabled = !isBusy;
        nextDayButton.Enabled = !isBusy;
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

    private async void prevDayButton_Click(object sender, EventArgs e)
    {
        _selectedMonth = _selectedMonth.AddMonths(-1);
        UpdateSelectedMonthDisplay();
        await RefreshDashboardAsync();
    }

    private async void nextDayButton_Click(object sender, EventArgs e)
    {
        _selectedMonth = _selectedMonth.AddMonths(1);
        UpdateSelectedMonthDisplay();
        await RefreshDashboardAsync();
    }

    private void CashierDashboardControl_SizeChanged(object? sender, EventArgs e)
    {
        QueueChartRebind();
    }

    private void reportsTabControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        QueueChartRebind();
    }

    private sealed class TrendRow
    {
        public string Period { get; init; } = string.Empty;

        public decimal CollectedAmount { get; init; }

        public decimal UncollectedAmount { get; init; }

        public decimal NetAmount { get; init; }

        public decimal CollectedDelta { get; init; }

        public decimal DeltaPercent { get; init; }
    }

    private sealed class ValueRow
    {
        public string LabelColumnName { get; init; } = string.Empty;

        public string Label { get; init; } = string.Empty;

        public decimal Value { get; init; }

        public decimal SharePercent { get; init; }
    }

    private sealed class TopAccountRow
    {
        public int Rank { get; init; }

        public string Account { get; init; } = string.Empty;

        public decimal Amount { get; init; }

        public decimal SharePercent { get; init; }

        public decimal CumulativeSharePercent { get; init; }
    }

    private sealed class RecentCollectionRow
    {
        public DateTime CollectionDate { get; init; }

        public string OrNumber { get; init; } = string.Empty;

        public string Concessionaire { get; init; } = string.Empty;

        public string PaymentType { get; init; } = string.Empty;

        public decimal Amount { get; init; }
    }
}