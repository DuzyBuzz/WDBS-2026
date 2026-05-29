using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.Services.Dashboard;

namespace WDBS_2026.Components.Biller;

public partial class BillerDashboardControl : UserControl
{
    private readonly AuthenticatedUserDto _user;
    private bool _dashboardLoaded;
    private bool _isBusy;
    private bool _chartsEnabled = true;
    private BillerDashboardSnapshot? _latestSnapshot;

    public BillerDashboardControl(AuthenticatedUserDto user)
    {
        _user = user;
        InitializeComponent();
        subtitleLabel.Text = $"Signed in as {_user.FullName}. Monitor billing and concessionaire performance for this month.";

        ConfigureReportGrids();

        try
        {
            ConfigureCharts();
        }
        catch (Exception ex)
        {
            _chartsEnabled = false;
            dashboardStatusLabel.ForeColor = AppTheme.WarningColor;
            dashboardStatusLabel.Text = $"Charts are unavailable on this device: {ex.Message}";
            reportsTabControl.Visible = false;
        }

        ApplyTheme();
        ApplyResponsiveLayout();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
        {
            return;
        }

        if (_dashboardLoaded)
        {
            return;
        }

        _dashboardLoaded = true;

        try
        {
            await LoadDashboardAsync();
        }
        catch (Exception ex)
        {
            dashboardStatusLabel.ForeColor = AppTheme.DangerColor;
            dashboardStatusLabel.Text = "Dashboard initialization failed. You can continue using other modules.";
            MessageBox.Show(this, ex.Message, "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);

        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
        {
            return;
        }

        ApplyResponsiveLayout();

        if (_latestSnapshot is not null)
        {
            TryBindVisuals(_latestSnapshot);
        }
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.ShellBackgroundColor;
        AppTheme.ApplyPageTitle(headingLabel);
        AppTheme.ApplySubtitle(subtitleLabel);
        AppTheme.ApplySeverityButton(refreshButton, ButtonSeverity.Info);
        refreshButton.TextAlign = ContentAlignment.MiddleCenter;

        foreach (Panel card in new[]
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
            AppTheme.ApplyCard(card);
        }

        foreach (Label label in new[]
                 {
                     totalBilledCaptionLabel,
                     billCountCaptionLabel,
                     unpaidBillsCaptionLabel,
                     activeConcessionairesCaptionLabel,
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
            AppTheme.ApplyCardTitle(label);
        }

        AppTheme.ApplySubtitle(dashboardStatusLabel);

        foreach (Label label in new[] { totalBilledValueLabel, billCountValueLabel, unpaidBillsValueLabel, activeConcessionairesValueLabel })
        {
            label.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label.ForeColor = AppTheme.PrimaryDarkColor;
        }

        reportsTabControl.DrawMode = TabDrawMode.Normal;

        foreach (Chart chart in new[] { billedTrendChart, billingStatusChart, concessionairesByZoneChart, concessionaireStatusChart, topConcessionairesChart })
        {
            chart.BackColor = AppTheme.SurfaceColor;
            chart.SuppressExceptions = true;
            chart.Palette = ChartColorPalette.None;
            chart.PaletteCustomColors = new[]
            {
                AppTheme.InfoColor,
                AppTheme.AccentColor,
                AppTheme.WarningColor,
                AppTheme.PrimaryColor,
                AppTheme.SuccessColor,
                AppTheme.DangerColor,
                Color.FromArgb(127, 88, 153),
                Color.FromArgb(199, 83, 52)
            };
        }

        foreach (DataGridView grid in GetReportGrids())
        {
            ApplyGridTheme(grid);
        }
    }

    private void ApplyGridTheme(DataGridView grid)
    {
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AllowUserToOrderColumns = false;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        grid.BackgroundColor = AppTheme.SurfaceColor;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.EnableHeadersVisualStyles = false;
        grid.GridColor = AppTheme.BorderColor;

        grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryDarkColor;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = AppTheme.SectionFont;

        grid.DefaultCellStyle.Font = AppTheme.BodyFont;
        grid.DefaultCellStyle.ForeColor = AppTheme.BodyTextColor;
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 233, 241);
        grid.DefaultCellStyle.SelectionForeColor = AppTheme.BodyTextColor;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 251);
    }

    private void ConfigureReportGrids()
    {
        ConfigureGridColumns(billedTrendGrid, "Period", "Billed Amount (PHP)");
        ConfigureGridColumns(billingStatusGrid, "Status", "Count");
        ConfigureGridColumns(concessionairesByZoneGrid, "Zone", "Concessionaires");
        ConfigureGridColumns(concessionaireStatusGrid, "Status", "Count");
        ConfigureGridColumns(topConcessionairesGrid, "Concessionaire", "Billed Amount (PHP)");
    }

    private static void ConfigureGridColumns(DataGridView grid, string firstColumnName, string secondColumnName)
    {
        grid.Columns.Clear();
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "NameColumn",
            HeaderText = firstColumnName,
            FillWeight = 60F,
            SortMode = DataGridViewColumnSortMode.NotSortable
        });
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ValueColumn",
            HeaderText = secondColumnName,
            FillWeight = 40F,
            SortMode = DataGridViewColumnSortMode.NotSortable,
            DefaultCellStyle =
            {
                Alignment = DataGridViewContentAlignment.MiddleRight
            }
        });
    }

    private IEnumerable<DataGridView> GetReportGrids()
    {
        return new[]
        {
            billedTrendGrid,
            billingStatusGrid,
            concessionairesByZoneGrid,
            concessionaireStatusGrid,
            topConcessionairesGrid
        };
    }

    private void ConfigureCharts()
    {
        ConfigureBilledTrendChart();
        ConfigureBillingStatusChart();
        ConfigureConcessionairesByZoneChart();
        ConfigureConcessionaireStatusChart();
        ConfigureTopConcessionairesChart();
    }

    private void ConfigureBilledTrendChart()
    {
        ChartArea area = BuildDefaultArea("TrendArea", showXGrid: false, showYGrid: true);
        area.AxisX.Interval = 1;
        area.AxisY.LabelStyle.Format = "N0";

        billedTrendChart.Series.Clear();
        billedTrendChart.ChartAreas.Clear();
        billedTrendChart.Legends.Clear();
        billedTrendChart.ChartAreas.Add(area);
        billedTrendChart.Series.Add(new Series("Amount")
        {
            ChartType = SeriesChartType.Column,
            XValueType = ChartValueType.String,
            IsValueShownAsLabel = false,
            BorderWidth = 1,
            Color = AppTheme.InfoColor
        });
    }

    private void ConfigureBillingStatusChart()
    {
        billingStatusChart.Series.Clear();
        billingStatusChart.ChartAreas.Clear();
        billingStatusChart.Legends.Clear();

        billingStatusChart.ChartAreas.Add(BuildDefaultArea("BillingStatusArea", showXGrid: false, showYGrid: false));
        billingStatusChart.Legends.Add(BuildDefaultLegend("BillingStatusLegend"));
        billingStatusChart.Series.Add(new Series("Status")
        {
            ChartType = SeriesChartType.Doughnut,
            IsValueShownAsLabel = true,
            LabelFormat = "N0",
            Legend = "BillingStatusLegend",
            Font = AppTheme.CaptionFont,
            BorderColor = Color.White,
            BorderWidth = 1
        });
    }

    private void ConfigureConcessionairesByZoneChart()
    {
        ChartArea area = BuildDefaultArea("ZoneArea", showXGrid: false, showYGrid: false);
        area.AxisX.Interval = 1;
        area.AxisX.LabelStyle.Angle = -35;

        concessionairesByZoneChart.Series.Clear();
        concessionairesByZoneChart.ChartAreas.Clear();
        concessionairesByZoneChart.Legends.Clear();
        concessionairesByZoneChart.ChartAreas.Add(area);
        concessionairesByZoneChart.Series.Add(new Series("Count")
        {
            ChartType = SeriesChartType.Column,
            XValueType = ChartValueType.String,
            IsValueShownAsLabel = true,
            LabelFormat = "N0",
            Font = AppTheme.CaptionFont,
            Color = AppTheme.AccentColor
        });
    }

    private void ConfigureConcessionaireStatusChart()
    {
        concessionaireStatusChart.Series.Clear();
        concessionaireStatusChart.ChartAreas.Clear();
        concessionaireStatusChart.Legends.Clear();

        concessionaireStatusChart.ChartAreas.Add(BuildDefaultArea("ConcessionaireStatusArea", showXGrid: false, showYGrid: false));
        concessionaireStatusChart.Legends.Add(BuildDefaultLegend("ConcessionaireStatusLegend"));
        concessionaireStatusChart.Series.Add(new Series("Status")
        {
            ChartType = SeriesChartType.Doughnut,
            IsValueShownAsLabel = true,
            LabelFormat = "N0",
            Legend = "ConcessionaireStatusLegend",
            Font = AppTheme.CaptionFont,
            BorderColor = Color.White,
            BorderWidth = 1
        });
    }

    private void ConfigureTopConcessionairesChart()
    {
        ChartArea area = BuildDefaultArea("TopConcessionairesArea", showXGrid: false, showYGrid: true);
        area.AxisX.LabelStyle.Format = "N0";
        area.AxisY.Interval = 1;
        area.AxisY.IsLabelAutoFit = false;
        area.AxisY.LabelStyle.Font = AppTheme.CaptionFont;

        topConcessionairesChart.Series.Clear();
        topConcessionairesChart.ChartAreas.Clear();
        topConcessionairesChart.Legends.Clear();
        topConcessionairesChart.ChartAreas.Add(area);
        topConcessionairesChart.Series.Add(new Series("Amount")
        {
            ChartType = SeriesChartType.Bar,
            XValueType = ChartValueType.Double,
            IsValueShownAsLabel = true,
            LabelFormat = "N0",
            Font = AppTheme.CaptionFont,
            Color = AppTheme.PrimaryColor
        });
    }

    private static ChartArea BuildDefaultArea(string name, bool showXGrid, bool showYGrid)
    {
        var area = new ChartArea(name)
        {
            BackColor = Color.White,
            AxisX =
            {
                MajorGrid = { Enabled = showXGrid, LineColor = Color.FromArgb(230, 236, 240) },
                LineColor = Color.FromArgb(180, 196, 206),
                LabelStyle = { ForeColor = AppTheme.MutedTextColor, Font = AppTheme.CaptionFont }
            },
            AxisY =
            {
                MajorGrid = { Enabled = showYGrid, LineColor = Color.FromArgb(230, 236, 240) },
                LineColor = Color.FromArgb(180, 196, 206),
                LabelStyle = { ForeColor = AppTheme.MutedTextColor, Font = AppTheme.CaptionFont }
            }
        };

        return area;
    }

    private static Legend BuildDefaultLegend(string name)
    {
        return new Legend(name)
        {
            Docking = Docking.Right,
            Alignment = StringAlignment.Center,
            BackColor = Color.Transparent,
            Font = AppTheme.CaptionFont,
            ForeColor = AppTheme.BodyTextColor
        };
    }

    private async Task LoadDashboardAsync()
    {
        if (_isBusy)
        {
            return;
        }

        try
        {
            SetBusyState(true, "Loading dashboard metrics, charts, and tables...");

            DateTime currentMonth = DateTime.Today;
            BillerDashboardSnapshot snapshot = await BillerDashboardDataService.GetSnapshotAsync(_user.Role, currentMonth);
            _latestSnapshot = snapshot;

            BindMetrics(snapshot);

            bool visualsBound = TryBindVisuals(snapshot);
            dashboardStatusLabel.ForeColor = AppTheme.MutedTextColor;
            dashboardStatusLabel.Text = visualsBound
                ? $"Updated {DateTime.Now:MMM dd, yyyy hh:mm tt}. Showing current month dashboard data."
                : $"Updated {DateTime.Now:MMM dd, yyyy hh:mm tt}. Summary cards are ready; report visuals will render when enough space is available.";
        }
        catch (Exception ex)
        {
            dashboardStatusLabel.ForeColor = AppTheme.DangerColor;
            dashboardStatusLabel.Text = "Dashboard load failed. You can retry using Refresh Dashboard.";
            MessageBox.Show(this, ex.Message, "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private void BindMetrics(BillerDashboardSnapshot snapshot)
    {
        totalBilledValueLabel.Text = $"PHP {snapshot.TotalBilledAmount:N2}";
        billCountValueLabel.Text = snapshot.TotalBillCount.ToString("N0", CultureInfo.CurrentCulture);
        unpaidBillsValueLabel.Text = snapshot.UnpaidBillCount.ToString("N0", CultureInfo.CurrentCulture);
        activeConcessionairesValueLabel.Text = snapshot.ActiveConcessionaireCount.ToString("N0", CultureInfo.CurrentCulture);
    }

    private bool TryBindVisuals(BillerDashboardSnapshot snapshot)
    {
        if (!_chartsEnabled)
        {
            return false;
        }

        if (!CanBindCharts())
        {
            return false;
        }

        try
        {
            BindAllCharts(snapshot);
            BindAllTables(snapshot);
            return true;
        }
        catch
        {
            BindChartFallbacks();
            BindTableFallbacks();
            return false;
        }
    }

    private bool CanBindCharts()
    {
        if (!IsHandleCreated || !Visible)
        {
            return false;
        }

        foreach (Chart chart in new[] { billedTrendChart, billingStatusChart, concessionairesByZoneChart, concessionaireStatusChart, topConcessionairesChart })
        {
            if (!chart.Visible || chart.Width <= 16 || chart.Height <= 16)
            {
                return false;
            }
        }

        return true;
    }

    private void BindTrend(IReadOnlyList<BillerDashboardPoint> points)
    {
        Series series = billedTrendChart.Series["Amount"];
        series.Points.Clear();

        if (points.Count == 0)
        {
            int emptyIndex = series.Points.AddXY("No Data", 0D);
            DataPoint emptyPoint = series.Points[emptyIndex];
            emptyPoint.Color = AppTheme.BorderColor;
            return;
        }

        foreach (BillerDashboardPoint point in points)
        {
            int index = series.Points.AddXY(point.Label, (double)point.Value);
            DataPoint dataPoint = series.Points[index];
            dataPoint.ToolTip = $"{point.Label}: PHP {point.Value:N2}";
        }
    }

    private void BindColumn(Chart chart, string seriesName, IReadOnlyList<BillerDashboardPoint> points)
    {
        Series series = chart.Series[seriesName];
        series.Points.Clear();

        if (points.Count == 0)
        {
            int emptyIndex = series.Points.AddXY("No Data", 0D);
            DataPoint emptyPoint = series.Points[emptyIndex];
            emptyPoint.Color = AppTheme.BorderColor;
            return;
        }

        foreach (BillerDashboardPoint point in points)
        {
            int index = series.Points.AddXY(point.Label, (double)point.Value);
            DataPoint dataPoint = series.Points[index];
            dataPoint.ToolTip = $"{point.Label}: {point.Value:N0}";
        }
    }

    private void BindDoughnut(Chart chart, string seriesName, IReadOnlyList<BillerDashboardPoint> points)
    {
        Series series = chart.Series[seriesName];
        series.Points.Clear();

        if (points.Count == 0)
        {
            int emptyIndex = series.Points.AddXY("No Data", 1D);
            DataPoint emptyPoint = series.Points[emptyIndex];
            emptyPoint.Label = "No Data";
            emptyPoint.LegendText = "No Data";
            emptyPoint.Color = AppTheme.BorderColor;
            return;
        }

        foreach (BillerDashboardPoint point in points)
        {
            int index = series.Points.AddXY(point.Label, (double)point.Value);
            DataPoint dataPoint = series.Points[index];
            dataPoint.LegendText = point.Label;
            dataPoint.ToolTip = $"{point.Label}: {point.Value:N0}";
        }
    }

    private void BindTopConcessionaires(IReadOnlyList<BillerDashboardPoint> points)
    {
        Series series = topConcessionairesChart.Series["Amount"];
        series.Points.Clear();

        if (points.Count == 0)
        {
            int emptyIndex = series.Points.AddXY(0D, "No Data");
            DataPoint emptyPoint = series.Points[emptyIndex];
            emptyPoint.Color = AppTheme.BorderColor;
            emptyPoint.Label = "No Data";
            return;
        }

        foreach (BillerDashboardPoint point in points)
        {
            int index = series.Points.AddXY((double)point.Value, point.Label);
            DataPoint dataPoint = series.Points[index];
            dataPoint.ToolTip = $"{point.Label}: PHP {point.Value:N2}";
        }
    }

    private void BindAllCharts(BillerDashboardSnapshot snapshot)
    {
        BindTrend(snapshot.BilledTrend);
        BindDoughnut(billingStatusChart, "Status", snapshot.BillingStatusBreakdown);
        BindColumn(concessionairesByZoneChart, "Count", snapshot.ConcessionairesByZone);
        BindDoughnut(concessionaireStatusChart, "Status", snapshot.ConcessionaireStatusMix);
        BindTopConcessionaires(snapshot.TopConcessionairesByAmount);
    }

    private void BindAllTables(BillerDashboardSnapshot snapshot)
    {
        BindPointsToGrid(billedTrendGrid, snapshot.BilledTrend, currency: true);
        BindPointsToGrid(billingStatusGrid, snapshot.BillingStatusBreakdown, currency: false);
        BindPointsToGrid(concessionairesByZoneGrid, snapshot.ConcessionairesByZone, currency: false);
        BindPointsToGrid(concessionaireStatusGrid, snapshot.ConcessionaireStatusMix, currency: false);
        BindPointsToGrid(topConcessionairesGrid, snapshot.TopConcessionairesByAmount, currency: true);
    }

    private static void BindPointsToGrid(DataGridView grid, IReadOnlyList<BillerDashboardPoint> points, bool currency)
    {
        grid.Rows.Clear();

        if (points.Count == 0)
        {
            grid.Rows.Add("No Data", currency ? "PHP 0.00" : "0");
            return;
        }

        foreach (BillerDashboardPoint point in points)
        {
            string valueText = currency
                ? $"PHP {point.Value:N2}"
                : point.Value.ToString("N0", CultureInfo.CurrentCulture);
            grid.Rows.Add(point.Label, valueText);
        }

        grid.ClearSelection();
    }

    private void BindChartFallbacks()
    {
        BindTrend(Array.Empty<BillerDashboardPoint>());
        BindDoughnut(billingStatusChart, "Status", Array.Empty<BillerDashboardPoint>());
        BindColumn(concessionairesByZoneChart, "Count", Array.Empty<BillerDashboardPoint>());
        BindDoughnut(concessionaireStatusChart, "Status", Array.Empty<BillerDashboardPoint>());
        BindTopConcessionaires(Array.Empty<BillerDashboardPoint>());
    }

    private void BindTableFallbacks()
    {
        BindPointsToGrid(billedTrendGrid, Array.Empty<BillerDashboardPoint>(), currency: true);
        BindPointsToGrid(billingStatusGrid, Array.Empty<BillerDashboardPoint>(), currency: false);
        BindPointsToGrid(concessionairesByZoneGrid, Array.Empty<BillerDashboardPoint>(), currency: false);
        BindPointsToGrid(concessionaireStatusGrid, Array.Empty<BillerDashboardPoint>(), currency: false);
        BindPointsToGrid(topConcessionairesGrid, Array.Empty<BillerDashboardPoint>(), currency: true);
    }

    private void SetBusyState(bool busy, string? message = null)
    {
        _isBusy = busy;
        refreshButton.Enabled = !busy;
        UseWaitCursor = busy;

        if (!string.IsNullOrWhiteSpace(message))
        {
            dashboardStatusLabel.ForeColor = AppTheme.MutedTextColor;
            dashboardStatusLabel.Text = message;
        }
    }

    private async void refreshButton_Click(object sender, EventArgs e)
    {
        await LoadDashboardAsync();
    }

    private void ApplyResponsiveLayout()
    {
        ConfigureToolbarLayout();
        ConfigureSplitterDistances();
    }

    private void ConfigureToolbarLayout()
    {
        refreshButton.Text = Width < 950 ? "Refresh" : "Refresh Dashboard";
        refreshButton.Width = Width < 950 ? 102 : 140;

        int statusMaxWidth = Math.Max(180, toolbarPanel.ClientSize.Width - refreshButton.Width - 20);
        dashboardStatusLabel.MaximumSize = new Size(statusMaxWidth, 0);
    }

    private void ConfigureSplitterDistances()
    {
        foreach (SplitContainer split in new[]
                 {
                     billedTrendSplitContainer,
                     billingStatusSplitContainer,
                     concessionairesByZoneSplitContainer,
                     concessionaireStatusSplitContainer,
                     topConcessionairesSplitContainer
                 })
        {
            if (split.Height <= 0)
            {
                continue;
            }

            int preferredTop = Math.Max(split.Panel1MinSize, (int)Math.Round(split.Height * 0.55));
            int maxTop = Math.Max(split.Panel1MinSize, split.Height - split.Panel2MinSize - split.SplitterWidth);
            split.SplitterDistance = Math.Min(preferredTop, maxTop);
        }
    }
}
