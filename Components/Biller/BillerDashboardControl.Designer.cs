namespace WDBS_2026.Components.Biller;

partial class BillerDashboardControl
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
        layoutPanel = new TableLayoutPanel();
        headingLabel = new Label();
        toolbarPanel = new Panel();
        prevMonthButton = new Button();
        selectedMonthLabel = new Label();
        nextMonthButton = new Button();
        dashboardStatusLabel = new Label();
        refreshButton = new Button();
        dashboardPanel = new TableLayoutPanel();
        metricsLayoutPanel = new TableLayoutPanel();
        totalBilledCardPanel = new Panel();
        totalBilledValueLabel = new Label();
        totalBilledCaptionLabel = new Label();
        billCountCardPanel = new Panel();
        billCountValueLabel = new Label();
        billCountCaptionLabel = new Label();
        unpaidBillsCardPanel = new Panel();
        unpaidBillsValueLabel = new Label();
        unpaidBillsCaptionLabel = new Label();
        activeConcessionairesCardPanel = new Panel();
        activeConcessionairesValueLabel = new Label();
        activeConcessionairesCaptionLabel = new Label();
        reportsTabControl = new TabControl();
        topConcessionairesTabPage = new TabPage();
        topConcessionairesSplitContainer = new SplitContainer();
        topConcessionairesCardPanel = new Panel();
        topConcessionairesTitleLabel = new Label();
        topConcessionairesTablePanel = new TableLayoutPanel();
        topConcessionairesTableTitleLabel = new Label();
        topConcessionairesGrid = new DataGridView();
        billedTrendTabPage = new TabPage();
        billedTrendSplitContainer = new SplitContainer();
        billedTrendCardPanel = new Panel();
        billedTrendTitleLabel = new Label();
        billedTrendTablePanel = new TableLayoutPanel();
        billedTrendTableTitleLabel = new Label();
        billedTrendGrid = new DataGridView();
        billingStatusTabPage = new TabPage();
        billingStatusSplitContainer = new SplitContainer();
        billingStatusCardPanel = new Panel();
        billingStatusTitleLabel = new Label();
        billingStatusTablePanel = new TableLayoutPanel();
        billingStatusTableTitleLabel = new Label();
        billingStatusGrid = new DataGridView();
        concessionairesByZoneTabPage = new TabPage();
        concessionairesByZoneSplitContainer = new SplitContainer();
        concessionairesByZoneCardPanel = new Panel();
        concessionairesByZoneTitleLabel = new Label();
        concessionairesByZoneTablePanel = new TableLayoutPanel();
        concessionairesByZoneTableTitleLabel = new Label();
        concessionairesByZoneGrid = new DataGridView();
        concessionaireStatusTabPage = new TabPage();
        concessionaireStatusSplitContainer = new SplitContainer();
        concessionaireStatusCardPanel = new Panel();
        concessionaireStatusTitleLabel = new Label();
        concessionaireStatusTablePanel = new TableLayoutPanel();
        concessionaireStatusTableTitleLabel = new Label();
        concessionaireStatusGrid = new DataGridView();
        layoutPanel.SuspendLayout();
        toolbarPanel.SuspendLayout();
        dashboardPanel.SuspendLayout();
        metricsLayoutPanel.SuspendLayout();
        totalBilledCardPanel.SuspendLayout();
        billCountCardPanel.SuspendLayout();
        unpaidBillsCardPanel.SuspendLayout();
        activeConcessionairesCardPanel.SuspendLayout();
        reportsTabControl.SuspendLayout();
        topConcessionairesTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)topConcessionairesSplitContainer).BeginInit();
        topConcessionairesSplitContainer.Panel1.SuspendLayout();
        topConcessionairesSplitContainer.Panel2.SuspendLayout();
        topConcessionairesSplitContainer.SuspendLayout();
        topConcessionairesCardPanel.SuspendLayout();
        topConcessionairesTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)topConcessionairesGrid).BeginInit();
        billedTrendTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)billedTrendSplitContainer).BeginInit();
        billedTrendSplitContainer.Panel1.SuspendLayout();
        billedTrendSplitContainer.Panel2.SuspendLayout();
        billedTrendSplitContainer.SuspendLayout();
        billedTrendCardPanel.SuspendLayout();
        billedTrendTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)billedTrendGrid).BeginInit();
        billingStatusTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)billingStatusSplitContainer).BeginInit();
        billingStatusSplitContainer.Panel1.SuspendLayout();
        billingStatusSplitContainer.Panel2.SuspendLayout();
        billingStatusSplitContainer.SuspendLayout();
        billingStatusCardPanel.SuspendLayout();
        billingStatusTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)billingStatusGrid).BeginInit();
        concessionairesByZoneTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)concessionairesByZoneSplitContainer).BeginInit();
        concessionairesByZoneSplitContainer.Panel1.SuspendLayout();
        concessionairesByZoneSplitContainer.Panel2.SuspendLayout();
        concessionairesByZoneSplitContainer.SuspendLayout();
        concessionairesByZoneCardPanel.SuspendLayout();
        concessionairesByZoneTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)concessionairesByZoneGrid).BeginInit();
        concessionaireStatusTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireStatusSplitContainer).BeginInit();
        concessionaireStatusSplitContainer.Panel1.SuspendLayout();
        concessionaireStatusSplitContainer.Panel2.SuspendLayout();
        concessionaireStatusSplitContainer.SuspendLayout();
        concessionaireStatusCardPanel.SuspendLayout();
        concessionaireStatusTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireStatusGrid).BeginInit();
        SuspendLayout();
        // 
        // layoutPanel
        // 
        layoutPanel.ColumnCount = 1;
        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutPanel.Controls.Add(headingLabel, 0, 0);
        layoutPanel.Controls.Add(toolbarPanel, 0, 1);
        layoutPanel.Controls.Add(dashboardPanel, 0, 2);
        layoutPanel.Dock = DockStyle.Fill;
        layoutPanel.Location = new Point(0, 0);
        layoutPanel.Margin = new Padding(3, 2, 3, 2);
        layoutPanel.Name = "layoutPanel";
        layoutPanel.Padding = new Padding(0, 3, 0, 0);
        layoutPanel.RowCount = 3;
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        layoutPanel.Size = new Size(1120, 568);
        layoutPanel.TabIndex = 0;
        // 
        // headingLabel
        // 
        headingLabel.AutoSize = true;
        headingLabel.Location = new Point(3, 3);
        headingLabel.Name = "headingLabel";
        headingLabel.Size = new Size(135, 15);
        headingLabel.TabIndex = 0;
        headingLabel.Text = "Water Bill Data Analytics";
        // 
        // toolbarPanel
        // 
        toolbarPanel.Controls.Add(prevMonthButton);
        toolbarPanel.Controls.Add(selectedMonthLabel);
        toolbarPanel.Controls.Add(nextMonthButton);
        toolbarPanel.Controls.Add(dashboardStatusLabel);
        toolbarPanel.Controls.Add(refreshButton);
        toolbarPanel.Dock = DockStyle.Fill;
        toolbarPanel.Location = new Point(0, 24);
        toolbarPanel.Margin = new Padding(0, 6, 0, 0);
        toolbarPanel.Name = "toolbarPanel";
        toolbarPanel.Size = new Size(1120, 35);
        toolbarPanel.TabIndex = 2;
        // 
        // prevMonthButton
        // 
        prevMonthButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        prevMonthButton.Location = new Point(686, 5);
        prevMonthButton.Margin = new Padding(3, 2, 3, 2);
        prevMonthButton.Name = "prevMonthButton";
        prevMonthButton.Size = new Size(75, 25);
        prevMonthButton.TabIndex = 1;
        prevMonthButton.Text = "Prev";
        prevMonthButton.UseVisualStyleBackColor = true;
        prevMonthButton.Click += prevMonthButton_Click;
        // 
        // selectedMonthLabel
        // 
        selectedMonthLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        selectedMonthLabel.Location = new Point(767, 10);
        selectedMonthLabel.Name = "selectedMonthLabel";
        selectedMonthLabel.Size = new Size(125, 15);
        selectedMonthLabel.TabIndex = 2;
        selectedMonthLabel.Text = "May 2026";
        selectedMonthLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // nextMonthButton
        // 
        nextMonthButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        nextMonthButton.Location = new Point(898, 5);
        nextMonthButton.Margin = new Padding(3, 2, 3, 2);
        nextMonthButton.Name = "nextMonthButton";
        nextMonthButton.Size = new Size(75, 25);
        nextMonthButton.TabIndex = 3;
        nextMonthButton.Text = "Next";
        nextMonthButton.UseVisualStyleBackColor = true;
        nextMonthButton.Click += nextMonthButton_Click;
        // 
        // dashboardStatusLabel
        // 
        dashboardStatusLabel.AutoSize = true;
        dashboardStatusLabel.Location = new Point(0, 10);
        dashboardStatusLabel.Name = "dashboardStatusLabel";
        dashboardStatusLabel.Size = new Size(218, 15);
        dashboardStatusLabel.TabIndex = 0;
        dashboardStatusLabel.Text = "Loading dashboard metrics and charts...";
        // 
        // refreshButton
        // 
        refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        refreshButton.Location = new Point(980, 5);
        refreshButton.Margin = new Padding(3, 2, 3, 2);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(140, 25);
        refreshButton.TabIndex = 4;
        refreshButton.Text = "Refresh Dashboard";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += refreshButton_Click;
        // 
        // dashboardPanel
        // 
        dashboardPanel.ColumnCount = 1;
        dashboardPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        dashboardPanel.Controls.Add(metricsLayoutPanel, 0, 0);
        dashboardPanel.Controls.Add(reportsTabControl, 0, 1);
        dashboardPanel.Dock = DockStyle.Fill;
        dashboardPanel.Location = new Point(0, 59);
        dashboardPanel.Margin = new Padding(0);
        dashboardPanel.Name = "dashboardPanel";
        dashboardPanel.RowCount = 2;
        dashboardPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        dashboardPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        dashboardPanel.Size = new Size(1120, 509);
        dashboardPanel.TabIndex = 3;
        // 
        // metricsLayoutPanel
        // 
        metricsLayoutPanel.ColumnCount = 4;
        metricsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        metricsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        metricsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        metricsLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        metricsLayoutPanel.Controls.Add(totalBilledCardPanel, 0, 0);
        metricsLayoutPanel.Controls.Add(billCountCardPanel, 1, 0);
        metricsLayoutPanel.Controls.Add(unpaidBillsCardPanel, 2, 0);
        metricsLayoutPanel.Controls.Add(activeConcessionairesCardPanel, 3, 0);
        metricsLayoutPanel.Dock = DockStyle.Fill;
        metricsLayoutPanel.Location = new Point(0, 0);
        metricsLayoutPanel.Margin = new Padding(0);
        metricsLayoutPanel.Name = "metricsLayoutPanel";
        metricsLayoutPanel.RowCount = 1;
        metricsLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        metricsLayoutPanel.Size = new Size(1120, 56);
        metricsLayoutPanel.TabIndex = 0;
        // 
        // totalBilledCardPanel
        // 
        totalBilledCardPanel.Controls.Add(totalBilledValueLabel);
        totalBilledCardPanel.Controls.Add(totalBilledCaptionLabel);
        totalBilledCardPanel.Dock = DockStyle.Top;
        totalBilledCardPanel.Location = new Point(0, 0);
        totalBilledCardPanel.Margin = new Padding(0);
        totalBilledCardPanel.Name = "totalBilledCardPanel";
        totalBilledCardPanel.Padding = new Padding(3);
        totalBilledCardPanel.Size = new Size(280, 56);
        totalBilledCardPanel.TabIndex = 0;
        // 
        // totalBilledValueLabel
        // 
        totalBilledValueLabel.AutoSize = true;
        totalBilledValueLabel.Dock = DockStyle.Top;
        totalBilledValueLabel.Location = new Point(3, 18);
        totalBilledValueLabel.Name = "totalBilledValueLabel";
        totalBilledValueLabel.Size = new Size(54, 15);
        totalBilledValueLabel.TabIndex = 1;
        totalBilledValueLabel.Text = "PHP 0.00";
        // 
        // totalBilledCaptionLabel
        // 
        totalBilledCaptionLabel.AutoSize = true;
        totalBilledCaptionLabel.Dock = DockStyle.Top;
        totalBilledCaptionLabel.Location = new Point(3, 3);
        totalBilledCaptionLabel.Name = "totalBilledCaptionLabel";
        totalBilledCaptionLabel.Size = new Size(100, 15);
        totalBilledCaptionLabel.TabIndex = 0;
        totalBilledCaptionLabel.Text = "Total Billed (MTD)";
        // 
        // billCountCardPanel
        // 
        billCountCardPanel.Controls.Add(billCountValueLabel);
        billCountCardPanel.Controls.Add(billCountCaptionLabel);
        billCountCardPanel.Dock = DockStyle.Top;
        billCountCardPanel.Location = new Point(280, 0);
        billCountCardPanel.Margin = new Padding(0);
        billCountCardPanel.Name = "billCountCardPanel";
        billCountCardPanel.Padding = new Padding(3);
        billCountCardPanel.Size = new Size(280, 56);
        billCountCardPanel.TabIndex = 1;
        // 
        // billCountValueLabel
        // 
        billCountValueLabel.AutoSize = true;
        billCountValueLabel.Dock = DockStyle.Top;
        billCountValueLabel.Location = new Point(3, 18);
        billCountValueLabel.Name = "billCountValueLabel";
        billCountValueLabel.Size = new Size(13, 15);
        billCountValueLabel.TabIndex = 1;
        billCountValueLabel.Text = "0";
        // 
        // billCountCaptionLabel
        // 
        billCountCaptionLabel.AutoSize = true;
        billCountCaptionLabel.Dock = DockStyle.Top;
        billCountCaptionLabel.Location = new Point(3, 3);
        billCountCaptionLabel.Name = "billCountCaptionLabel";
        billCountCaptionLabel.Size = new Size(91, 15);
        billCountCaptionLabel.TabIndex = 0;
        billCountCaptionLabel.Text = "Bills This Month";
        // 
        // unpaidBillsCardPanel
        // 
        unpaidBillsCardPanel.Controls.Add(unpaidBillsValueLabel);
        unpaidBillsCardPanel.Controls.Add(unpaidBillsCaptionLabel);
        unpaidBillsCardPanel.Dock = DockStyle.Top;
        unpaidBillsCardPanel.Location = new Point(560, 0);
        unpaidBillsCardPanel.Margin = new Padding(0);
        unpaidBillsCardPanel.Name = "unpaidBillsCardPanel";
        unpaidBillsCardPanel.Padding = new Padding(3);
        unpaidBillsCardPanel.Size = new Size(280, 56);
        unpaidBillsCardPanel.TabIndex = 2;
        // 
        // unpaidBillsValueLabel
        // 
        unpaidBillsValueLabel.AutoSize = true;
        unpaidBillsValueLabel.Dock = DockStyle.Top;
        unpaidBillsValueLabel.Location = new Point(3, 18);
        unpaidBillsValueLabel.Name = "unpaidBillsValueLabel";
        unpaidBillsValueLabel.Size = new Size(13, 15);
        unpaidBillsValueLabel.TabIndex = 1;
        unpaidBillsValueLabel.Text = "0";
        // 
        // unpaidBillsCaptionLabel
        // 
        unpaidBillsCaptionLabel.AutoSize = true;
        unpaidBillsCaptionLabel.Dock = DockStyle.Top;
        unpaidBillsCaptionLabel.Location = new Point(3, 3);
        unpaidBillsCaptionLabel.Name = "unpaidBillsCaptionLabel";
        unpaidBillsCaptionLabel.Size = new Size(105, 15);
        unpaidBillsCaptionLabel.TabIndex = 0;
        unpaidBillsCaptionLabel.Text = "Unpaid Bills (MTD)";
        // 
        // activeConcessionairesCardPanel
        // 
        activeConcessionairesCardPanel.Controls.Add(activeConcessionairesValueLabel);
        activeConcessionairesCardPanel.Controls.Add(activeConcessionairesCaptionLabel);
        activeConcessionairesCardPanel.Dock = DockStyle.Top;
        activeConcessionairesCardPanel.Location = new Point(840, 0);
        activeConcessionairesCardPanel.Margin = new Padding(0);
        activeConcessionairesCardPanel.Name = "activeConcessionairesCardPanel";
        activeConcessionairesCardPanel.Padding = new Padding(3);
        activeConcessionairesCardPanel.Size = new Size(280, 56);
        activeConcessionairesCardPanel.TabIndex = 3;
        // 
        // activeConcessionairesValueLabel
        // 
        activeConcessionairesValueLabel.AutoSize = true;
        activeConcessionairesValueLabel.Dock = DockStyle.Top;
        activeConcessionairesValueLabel.Location = new Point(3, 18);
        activeConcessionairesValueLabel.Name = "activeConcessionairesValueLabel";
        activeConcessionairesValueLabel.Size = new Size(13, 15);
        activeConcessionairesValueLabel.TabIndex = 1;
        activeConcessionairesValueLabel.Text = "0";
        // 
        // activeConcessionairesCaptionLabel
        // 
        activeConcessionairesCaptionLabel.AutoSize = true;
        activeConcessionairesCaptionLabel.Dock = DockStyle.Top;
        activeConcessionairesCaptionLabel.Location = new Point(3, 3);
        activeConcessionairesCaptionLabel.Name = "activeConcessionairesCaptionLabel";
        activeConcessionairesCaptionLabel.Size = new Size(128, 15);
        activeConcessionairesCaptionLabel.TabIndex = 0;
        activeConcessionairesCaptionLabel.Text = "Active Concessionaires";
        // 
        // reportsTabControl
        // 
        reportsTabControl.Controls.Add(topConcessionairesTabPage);
        reportsTabControl.Controls.Add(billedTrendTabPage);
        reportsTabControl.Controls.Add(billingStatusTabPage);
        reportsTabControl.Controls.Add(concessionairesByZoneTabPage);
        reportsTabControl.Controls.Add(concessionaireStatusTabPage);
        reportsTabControl.Dock = DockStyle.Fill;
        reportsTabControl.Location = new Point(0, 65);
        reportsTabControl.Margin = new Padding(0, 9, 0, 0);
        reportsTabControl.Name = "reportsTabControl";
        reportsTabControl.SelectedIndex = 0;
        reportsTabControl.Size = new Size(1120, 444);
        reportsTabControl.TabIndex = 1;
        // 
        // topConcessionairesTabPage
        // 
        topConcessionairesTabPage.Controls.Add(topConcessionairesSplitContainer);
        topConcessionairesTabPage.Location = new Point(4, 24);
        topConcessionairesTabPage.Margin = new Padding(3, 2, 3, 2);
        topConcessionairesTabPage.Name = "topConcessionairesTabPage";
        topConcessionairesTabPage.Padding = new Padding(8, 6, 8, 6);
        topConcessionairesTabPage.Size = new Size(1112, 416);
        topConcessionairesTabPage.TabIndex = 4;
        topConcessionairesTabPage.Text = "Top Concessionaires";
        topConcessionairesTabPage.UseVisualStyleBackColor = true;
        // 
        // topConcessionairesSplitContainer
        // 
        topConcessionairesSplitContainer.Dock = DockStyle.Fill;
        topConcessionairesSplitContainer.Location = new Point(8, 6);
        topConcessionairesSplitContainer.Margin = new Padding(3, 2, 3, 2);
        topConcessionairesSplitContainer.Name = "topConcessionairesSplitContainer";
        topConcessionairesSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // topConcessionairesSplitContainer.Panel1
        // 
        topConcessionairesSplitContainer.Panel1.Controls.Add(topConcessionairesCardPanel);
        topConcessionairesSplitContainer.Panel1MinSize = 160;
        // 
        // topConcessionairesSplitContainer.Panel2
        // 
        topConcessionairesSplitContainer.Panel2.Controls.Add(topConcessionairesTablePanel);
        topConcessionairesSplitContainer.Size = new Size(1096, 404);
        topConcessionairesSplitContainer.SplitterDistance = 300;
        topConcessionairesSplitContainer.TabIndex = 0;
        // 
        // topConcessionairesCardPanel
        // 
        topConcessionairesCardPanel.Controls.Add(topConcessionairesTitleLabel);
        topConcessionairesCardPanel.Dock = DockStyle.Fill;
        topConcessionairesCardPanel.Location = new Point(0, 0);
        topConcessionairesCardPanel.Margin = new Padding(3, 2, 3, 2);
        topConcessionairesCardPanel.Name = "topConcessionairesCardPanel";
        topConcessionairesCardPanel.Padding = new Padding(16, 9, 16, 11);
        topConcessionairesCardPanel.Size = new Size(1096, 300);
        topConcessionairesCardPanel.TabIndex = 0;
        // 
        // topConcessionairesTitleLabel
        // 
        topConcessionairesTitleLabel.Dock = DockStyle.Top;
        topConcessionairesTitleLabel.Location = new Point(16, 9);
        topConcessionairesTitleLabel.Name = "topConcessionairesTitleLabel";
        topConcessionairesTitleLabel.Size = new Size(1064, 21);
        topConcessionairesTitleLabel.TabIndex = 0;
        topConcessionairesTitleLabel.Text = "Top 10 Concessionaires by Billed Amount (Current Month)";
        topConcessionairesTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // topConcessionairesTablePanel
        // 
        topConcessionairesTablePanel.ColumnCount = 1;
        topConcessionairesTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        topConcessionairesTablePanel.Controls.Add(topConcessionairesTableTitleLabel, 0, 0);
        topConcessionairesTablePanel.Controls.Add(topConcessionairesGrid, 0, 1);
        topConcessionairesTablePanel.Dock = DockStyle.Fill;
        topConcessionairesTablePanel.Location = new Point(0, 0);
        topConcessionairesTablePanel.Margin = new Padding(3, 2, 3, 2);
        topConcessionairesTablePanel.Name = "topConcessionairesTablePanel";
        topConcessionairesTablePanel.RowCount = 2;
        topConcessionairesTablePanel.RowStyles.Add(new RowStyle());
        topConcessionairesTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        topConcessionairesTablePanel.Size = new Size(1096, 100);
        topConcessionairesTablePanel.TabIndex = 0;
        // 
        // topConcessionairesTableTitleLabel
        // 
        topConcessionairesTableTitleLabel.AutoSize = true;
        topConcessionairesTableTitleLabel.Location = new Point(0, 0);
        topConcessionairesTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        topConcessionairesTableTitleLabel.Name = "topConcessionairesTableTitleLabel";
        topConcessionairesTableTitleLabel.Size = new Size(160, 15);
        topConcessionairesTableTitleLabel.TabIndex = 0;
        topConcessionairesTableTitleLabel.Text = "Top Concessionaires Analysis";
        // 
        // topConcessionairesGrid
        // 
        topConcessionairesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        topConcessionairesGrid.Dock = DockStyle.Fill;
        topConcessionairesGrid.Location = new Point(0, 21);
        topConcessionairesGrid.Margin = new Padding(0);
        topConcessionairesGrid.Name = "topConcessionairesGrid";
        topConcessionairesGrid.Size = new Size(1096, 79);
        topConcessionairesGrid.TabIndex = 1;
        // 
        // billedTrendTabPage
        // 
        billedTrendTabPage.Controls.Add(billedTrendSplitContainer);
        billedTrendTabPage.Location = new Point(4, 24);
        billedTrendTabPage.Margin = new Padding(3, 2, 3, 2);
        billedTrendTabPage.Name = "billedTrendTabPage";
        billedTrendTabPage.Padding = new Padding(8, 6, 8, 6);
        billedTrendTabPage.Size = new Size(1112, 416);
        billedTrendTabPage.TabIndex = 0;
        billedTrendTabPage.Text = "Billed Trend";
        billedTrendTabPage.UseVisualStyleBackColor = true;
        // 
        // billedTrendSplitContainer
        // 
        billedTrendSplitContainer.Dock = DockStyle.Fill;
        billedTrendSplitContainer.Location = new Point(8, 6);
        billedTrendSplitContainer.Margin = new Padding(3, 2, 3, 2);
        billedTrendSplitContainer.Name = "billedTrendSplitContainer";
        billedTrendSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // billedTrendSplitContainer.Panel1
        // 
        billedTrendSplitContainer.Panel1.Controls.Add(billedTrendCardPanel);
        billedTrendSplitContainer.Panel1MinSize = 160;
        // 
        // billedTrendSplitContainer.Panel2
        // 
        billedTrendSplitContainer.Panel2.Controls.Add(billedTrendTablePanel);
        billedTrendSplitContainer.Size = new Size(1096, 404);
        billedTrendSplitContainer.SplitterDistance = 300;
        billedTrendSplitContainer.TabIndex = 0;
        // 
        // billedTrendCardPanel
        // 
        billedTrendCardPanel.Controls.Add(billedTrendTitleLabel);
        billedTrendCardPanel.Dock = DockStyle.Fill;
        billedTrendCardPanel.Location = new Point(0, 0);
        billedTrendCardPanel.Margin = new Padding(3, 2, 3, 2);
        billedTrendCardPanel.Name = "billedTrendCardPanel";
        billedTrendCardPanel.Padding = new Padding(16, 9, 16, 11);
        billedTrendCardPanel.Size = new Size(1096, 300);
        billedTrendCardPanel.TabIndex = 0;
        // 
        // billedTrendTitleLabel
        // 
        billedTrendTitleLabel.Dock = DockStyle.Top;
        billedTrendTitleLabel.Location = new Point(16, 9);
        billedTrendTitleLabel.Name = "billedTrendTitleLabel";
        billedTrendTitleLabel.Size = new Size(1064, 21);
        billedTrendTitleLabel.TabIndex = 0;
        billedTrendTitleLabel.Text = "Monthly Billed Amount Trend";
        billedTrendTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // billedTrendTablePanel
        // 
        billedTrendTablePanel.ColumnCount = 1;
        billedTrendTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        billedTrendTablePanel.Controls.Add(billedTrendTableTitleLabel, 0, 0);
        billedTrendTablePanel.Controls.Add(billedTrendGrid, 0, 1);
        billedTrendTablePanel.Dock = DockStyle.Fill;
        billedTrendTablePanel.Location = new Point(0, 0);
        billedTrendTablePanel.Margin = new Padding(3, 2, 3, 2);
        billedTrendTablePanel.Name = "billedTrendTablePanel";
        billedTrendTablePanel.RowCount = 2;
        billedTrendTablePanel.RowStyles.Add(new RowStyle());
        billedTrendTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        billedTrendTablePanel.Size = new Size(1096, 100);
        billedTrendTablePanel.TabIndex = 0;
        // 
        // billedTrendTableTitleLabel
        // 
        billedTrendTableTitleLabel.AutoSize = true;
        billedTrendTableTitleLabel.Location = new Point(0, 0);
        billedTrendTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        billedTrendTableTitleLabel.Name = "billedTrendTableTitleLabel";
        billedTrendTableTitleLabel.Size = new Size(112, 15);
        billedTrendTableTitleLabel.TabIndex = 0;
        billedTrendTableTitleLabel.Text = "Trend Report Details";
        // 
        // billedTrendGrid
        // 
        billedTrendGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        billedTrendGrid.Dock = DockStyle.Fill;
        billedTrendGrid.Location = new Point(0, 21);
        billedTrendGrid.Margin = new Padding(0);
        billedTrendGrid.Name = "billedTrendGrid";
        billedTrendGrid.Size = new Size(1096, 79);
        billedTrendGrid.TabIndex = 1;
        // 
        // billingStatusTabPage
        // 
        billingStatusTabPage.Controls.Add(billingStatusSplitContainer);
        billingStatusTabPage.Location = new Point(4, 24);
        billingStatusTabPage.Margin = new Padding(3, 2, 3, 2);
        billingStatusTabPage.Name = "billingStatusTabPage";
        billingStatusTabPage.Padding = new Padding(8, 6, 8, 6);
        billingStatusTabPage.Size = new Size(1112, 416);
        billingStatusTabPage.TabIndex = 1;
        billingStatusTabPage.Text = "Billing Status";
        billingStatusTabPage.UseVisualStyleBackColor = true;
        // 
        // billingStatusSplitContainer
        // 
        billingStatusSplitContainer.Dock = DockStyle.Fill;
        billingStatusSplitContainer.Location = new Point(8, 6);
        billingStatusSplitContainer.Margin = new Padding(3, 2, 3, 2);
        billingStatusSplitContainer.Name = "billingStatusSplitContainer";
        billingStatusSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // billingStatusSplitContainer.Panel1
        // 
        billingStatusSplitContainer.Panel1.Controls.Add(billingStatusCardPanel);
        billingStatusSplitContainer.Panel1MinSize = 160;
        // 
        // billingStatusSplitContainer.Panel2
        // 
        billingStatusSplitContainer.Panel2.Controls.Add(billingStatusTablePanel);
        billingStatusSplitContainer.Size = new Size(1096, 404);
        billingStatusSplitContainer.SplitterDistance = 300;
        billingStatusSplitContainer.TabIndex = 0;
        // 
        // billingStatusCardPanel
        // 
        billingStatusCardPanel.Controls.Add(billingStatusTitleLabel);
        billingStatusCardPanel.Dock = DockStyle.Fill;
        billingStatusCardPanel.Location = new Point(0, 0);
        billingStatusCardPanel.Margin = new Padding(3, 2, 3, 2);
        billingStatusCardPanel.Name = "billingStatusCardPanel";
        billingStatusCardPanel.Padding = new Padding(16, 9, 16, 11);
        billingStatusCardPanel.Size = new Size(1096, 300);
        billingStatusCardPanel.TabIndex = 0;
        // 
        // billingStatusTitleLabel
        // 
        billingStatusTitleLabel.Dock = DockStyle.Top;
        billingStatusTitleLabel.Location = new Point(16, 9);
        billingStatusTitleLabel.Name = "billingStatusTitleLabel";
        billingStatusTitleLabel.Size = new Size(1064, 21);
        billingStatusTitleLabel.TabIndex = 0;
        billingStatusTitleLabel.Text = "Billing Status Breakdown (Current Month)";
        billingStatusTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // billingStatusTablePanel
        // 
        billingStatusTablePanel.ColumnCount = 1;
        billingStatusTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        billingStatusTablePanel.Controls.Add(billingStatusTableTitleLabel, 0, 0);
        billingStatusTablePanel.Controls.Add(billingStatusGrid, 0, 1);
        billingStatusTablePanel.Dock = DockStyle.Fill;
        billingStatusTablePanel.Location = new Point(0, 0);
        billingStatusTablePanel.Margin = new Padding(3, 2, 3, 2);
        billingStatusTablePanel.Name = "billingStatusTablePanel";
        billingStatusTablePanel.RowCount = 2;
        billingStatusTablePanel.RowStyles.Add(new RowStyle());
        billingStatusTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        billingStatusTablePanel.Size = new Size(1096, 100);
        billingStatusTablePanel.TabIndex = 0;
        // 
        // billingStatusTableTitleLabel
        // 
        billingStatusTableTitleLabel.AutoSize = true;
        billingStatusTableTitleLabel.Location = new Point(0, 0);
        billingStatusTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        billingStatusTableTitleLabel.Name = "billingStatusTableTitleLabel";
        billingStatusTableTitleLabel.Size = new Size(115, 15);
        billingStatusTableTitleLabel.TabIndex = 0;
        billingStatusTableTitleLabel.Text = "Status Report Details";
        // 
        // billingStatusGrid
        // 
        billingStatusGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        billingStatusGrid.Dock = DockStyle.Fill;
        billingStatusGrid.Location = new Point(0, 21);
        billingStatusGrid.Margin = new Padding(0);
        billingStatusGrid.Name = "billingStatusGrid";
        billingStatusGrid.Size = new Size(1096, 79);
        billingStatusGrid.TabIndex = 1;
        // 
        // concessionairesByZoneTabPage
        // 
        concessionairesByZoneTabPage.Controls.Add(concessionairesByZoneSplitContainer);
        concessionairesByZoneTabPage.Location = new Point(4, 24);
        concessionairesByZoneTabPage.Margin = new Padding(3, 2, 3, 2);
        concessionairesByZoneTabPage.Name = "concessionairesByZoneTabPage";
        concessionairesByZoneTabPage.Padding = new Padding(8, 6, 8, 6);
        concessionairesByZoneTabPage.Size = new Size(1112, 416);
        concessionairesByZoneTabPage.TabIndex = 2;
        concessionairesByZoneTabPage.Text = "By Zone";
        concessionairesByZoneTabPage.UseVisualStyleBackColor = true;
        // 
        // concessionairesByZoneSplitContainer
        // 
        concessionairesByZoneSplitContainer.Dock = DockStyle.Fill;
        concessionairesByZoneSplitContainer.Location = new Point(8, 6);
        concessionairesByZoneSplitContainer.Margin = new Padding(3, 2, 3, 2);
        concessionairesByZoneSplitContainer.Name = "concessionairesByZoneSplitContainer";
        concessionairesByZoneSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // concessionairesByZoneSplitContainer.Panel1
        // 
        concessionairesByZoneSplitContainer.Panel1.Controls.Add(concessionairesByZoneCardPanel);
        concessionairesByZoneSplitContainer.Panel1MinSize = 160;
        // 
        // concessionairesByZoneSplitContainer.Panel2
        // 
        concessionairesByZoneSplitContainer.Panel2.Controls.Add(concessionairesByZoneTablePanel);
        concessionairesByZoneSplitContainer.Size = new Size(1096, 404);
        concessionairesByZoneSplitContainer.SplitterDistance = 300;
        concessionairesByZoneSplitContainer.TabIndex = 0;
        // 
        // concessionairesByZoneCardPanel
        // 
        concessionairesByZoneCardPanel.Controls.Add(concessionairesByZoneTitleLabel);
        concessionairesByZoneCardPanel.Dock = DockStyle.Fill;
        concessionairesByZoneCardPanel.Location = new Point(0, 0);
        concessionairesByZoneCardPanel.Margin = new Padding(3, 2, 3, 2);
        concessionairesByZoneCardPanel.Name = "concessionairesByZoneCardPanel";
        concessionairesByZoneCardPanel.Padding = new Padding(16, 9, 16, 11);
        concessionairesByZoneCardPanel.Size = new Size(1096, 300);
        concessionairesByZoneCardPanel.TabIndex = 0;
        // 
        // concessionairesByZoneTitleLabel
        // 
        concessionairesByZoneTitleLabel.Dock = DockStyle.Top;
        concessionairesByZoneTitleLabel.Location = new Point(16, 9);
        concessionairesByZoneTitleLabel.Name = "concessionairesByZoneTitleLabel";
        concessionairesByZoneTitleLabel.Size = new Size(1064, 21);
        concessionairesByZoneTitleLabel.TabIndex = 0;
        concessionairesByZoneTitleLabel.Text = "Concessionaires by Zone";
        concessionairesByZoneTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // concessionairesByZoneTablePanel
        // 
        concessionairesByZoneTablePanel.ColumnCount = 1;
        concessionairesByZoneTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        concessionairesByZoneTablePanel.Controls.Add(concessionairesByZoneTableTitleLabel, 0, 0);
        concessionairesByZoneTablePanel.Controls.Add(concessionairesByZoneGrid, 0, 1);
        concessionairesByZoneTablePanel.Dock = DockStyle.Fill;
        concessionairesByZoneTablePanel.Location = new Point(0, 0);
        concessionairesByZoneTablePanel.Margin = new Padding(3, 2, 3, 2);
        concessionairesByZoneTablePanel.Name = "concessionairesByZoneTablePanel";
        concessionairesByZoneTablePanel.RowCount = 2;
        concessionairesByZoneTablePanel.RowStyles.Add(new RowStyle());
        concessionairesByZoneTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        concessionairesByZoneTablePanel.Size = new Size(1096, 100);
        concessionairesByZoneTablePanel.TabIndex = 0;
        // 
        // concessionairesByZoneTableTitleLabel
        // 
        concessionairesByZoneTableTitleLabel.AutoSize = true;
        concessionairesByZoneTableTitleLabel.Location = new Point(0, 0);
        concessionairesByZoneTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        concessionairesByZoneTableTitleLabel.Name = "concessionairesByZoneTableTitleLabel";
        concessionairesByZoneTableTitleLabel.Size = new Size(110, 15);
        concessionairesByZoneTableTitleLabel.TabIndex = 0;
        concessionairesByZoneTableTitleLabel.Text = "Zone Report Details";
        // 
        // concessionairesByZoneGrid
        // 
        concessionairesByZoneGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        concessionairesByZoneGrid.Dock = DockStyle.Fill;
        concessionairesByZoneGrid.Location = new Point(0, 21);
        concessionairesByZoneGrid.Margin = new Padding(0);
        concessionairesByZoneGrid.Name = "concessionairesByZoneGrid";
        concessionairesByZoneGrid.Size = new Size(1096, 79);
        concessionairesByZoneGrid.TabIndex = 1;
        // 
        // concessionaireStatusTabPage
        // 
        concessionaireStatusTabPage.Controls.Add(concessionaireStatusSplitContainer);
        concessionaireStatusTabPage.Location = new Point(4, 24);
        concessionaireStatusTabPage.Margin = new Padding(3, 2, 3, 2);
        concessionaireStatusTabPage.Name = "concessionaireStatusTabPage";
        concessionaireStatusTabPage.Padding = new Padding(8, 6, 8, 6);
        concessionaireStatusTabPage.Size = new Size(1112, 416);
        concessionaireStatusTabPage.TabIndex = 3;
        concessionaireStatusTabPage.Text = "Concessionaire Status";
        concessionaireStatusTabPage.UseVisualStyleBackColor = true;
        // 
        // concessionaireStatusSplitContainer
        // 
        concessionaireStatusSplitContainer.Dock = DockStyle.Fill;
        concessionaireStatusSplitContainer.Location = new Point(8, 6);
        concessionaireStatusSplitContainer.Margin = new Padding(3, 2, 3, 2);
        concessionaireStatusSplitContainer.Name = "concessionaireStatusSplitContainer";
        concessionaireStatusSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // concessionaireStatusSplitContainer.Panel1
        // 
        concessionaireStatusSplitContainer.Panel1.Controls.Add(concessionaireStatusCardPanel);
        concessionaireStatusSplitContainer.Panel1MinSize = 160;
        // 
        // concessionaireStatusSplitContainer.Panel2
        // 
        concessionaireStatusSplitContainer.Panel2.Controls.Add(concessionaireStatusTablePanel);
        concessionaireStatusSplitContainer.Size = new Size(1096, 404);
        concessionaireStatusSplitContainer.SplitterDistance = 300;
        concessionaireStatusSplitContainer.TabIndex = 0;
        // 
        // concessionaireStatusCardPanel
        // 
        concessionaireStatusCardPanel.Controls.Add(concessionaireStatusTitleLabel);
        concessionaireStatusCardPanel.Dock = DockStyle.Fill;
        concessionaireStatusCardPanel.Location = new Point(0, 0);
        concessionaireStatusCardPanel.Margin = new Padding(3, 2, 3, 2);
        concessionaireStatusCardPanel.Name = "concessionaireStatusCardPanel";
        concessionaireStatusCardPanel.Padding = new Padding(16, 9, 16, 11);
        concessionaireStatusCardPanel.Size = new Size(1096, 300);
        concessionaireStatusCardPanel.TabIndex = 0;
        // 
        // concessionaireStatusTitleLabel
        // 
        concessionaireStatusTitleLabel.Dock = DockStyle.Top;
        concessionaireStatusTitleLabel.Location = new Point(16, 9);
        concessionaireStatusTitleLabel.Name = "concessionaireStatusTitleLabel";
        concessionaireStatusTitleLabel.Size = new Size(1064, 21);
        concessionaireStatusTitleLabel.TabIndex = 0;
        concessionaireStatusTitleLabel.Text = "Concessionaire Status Mix";
        concessionaireStatusTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // concessionaireStatusTablePanel
        // 
        concessionaireStatusTablePanel.ColumnCount = 1;
        concessionaireStatusTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        concessionaireStatusTablePanel.Controls.Add(concessionaireStatusTableTitleLabel, 0, 0);
        concessionaireStatusTablePanel.Controls.Add(concessionaireStatusGrid, 0, 1);
        concessionaireStatusTablePanel.Dock = DockStyle.Fill;
        concessionaireStatusTablePanel.Location = new Point(0, 0);
        concessionaireStatusTablePanel.Margin = new Padding(3, 2, 3, 2);
        concessionaireStatusTablePanel.Name = "concessionaireStatusTablePanel";
        concessionaireStatusTablePanel.RowCount = 2;
        concessionaireStatusTablePanel.RowStyles.Add(new RowStyle());
        concessionaireStatusTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        concessionaireStatusTablePanel.Size = new Size(1096, 100);
        concessionaireStatusTablePanel.TabIndex = 0;
        // 
        // concessionaireStatusTableTitleLabel
        // 
        concessionaireStatusTableTitleLabel.AutoSize = true;
        concessionaireStatusTableTitleLabel.Location = new Point(0, 0);
        concessionaireStatusTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        concessionaireStatusTableTitleLabel.Name = "concessionaireStatusTableTitleLabel";
        concessionaireStatusTableTitleLabel.Size = new Size(160, 15);
        concessionaireStatusTableTitleLabel.TabIndex = 0;
        concessionaireStatusTableTitleLabel.Text = "Concessionaire Status Details";
        // 
        // concessionaireStatusGrid
        // 
        concessionaireStatusGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        concessionaireStatusGrid.Dock = DockStyle.Fill;
        concessionaireStatusGrid.Location = new Point(0, 21);
        concessionaireStatusGrid.Margin = new Padding(0);
        concessionaireStatusGrid.Name = "concessionaireStatusGrid";
        concessionaireStatusGrid.Size = new Size(1096, 79);
        concessionaireStatusGrid.TabIndex = 1;
        // 
        // BillerDashboardControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(layoutPanel);
        Margin = new Padding(3, 2, 3, 2);
        Name = "BillerDashboardControl";
        Size = new Size(1120, 568);
        layoutPanel.ResumeLayout(false);
        layoutPanel.PerformLayout();
        toolbarPanel.ResumeLayout(false);
        toolbarPanel.PerformLayout();
        dashboardPanel.ResumeLayout(false);
        metricsLayoutPanel.ResumeLayout(false);
        totalBilledCardPanel.ResumeLayout(false);
        totalBilledCardPanel.PerformLayout();
        billCountCardPanel.ResumeLayout(false);
        billCountCardPanel.PerformLayout();
        unpaidBillsCardPanel.ResumeLayout(false);
        unpaidBillsCardPanel.PerformLayout();
        activeConcessionairesCardPanel.ResumeLayout(false);
        activeConcessionairesCardPanel.PerformLayout();
        reportsTabControl.ResumeLayout(false);
        topConcessionairesTabPage.ResumeLayout(false);
        topConcessionairesSplitContainer.Panel1.ResumeLayout(false);
        topConcessionairesSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)topConcessionairesSplitContainer).EndInit();
        topConcessionairesSplitContainer.ResumeLayout(false);
        topConcessionairesCardPanel.ResumeLayout(false);
        topConcessionairesTablePanel.ResumeLayout(false);
        topConcessionairesTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)topConcessionairesGrid).EndInit();
        billedTrendTabPage.ResumeLayout(false);
        billedTrendSplitContainer.Panel1.ResumeLayout(false);
        billedTrendSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)billedTrendSplitContainer).EndInit();
        billedTrendSplitContainer.ResumeLayout(false);
        billedTrendCardPanel.ResumeLayout(false);
        billedTrendTablePanel.ResumeLayout(false);
        billedTrendTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)billedTrendGrid).EndInit();
        billingStatusTabPage.ResumeLayout(false);
        billingStatusSplitContainer.Panel1.ResumeLayout(false);
        billingStatusSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)billingStatusSplitContainer).EndInit();
        billingStatusSplitContainer.ResumeLayout(false);
        billingStatusCardPanel.ResumeLayout(false);
        billingStatusTablePanel.ResumeLayout(false);
        billingStatusTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)billingStatusGrid).EndInit();
        concessionairesByZoneTabPage.ResumeLayout(false);
        concessionairesByZoneSplitContainer.Panel1.ResumeLayout(false);
        concessionairesByZoneSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)concessionairesByZoneSplitContainer).EndInit();
        concessionairesByZoneSplitContainer.ResumeLayout(false);
        concessionairesByZoneCardPanel.ResumeLayout(false);
        concessionairesByZoneTablePanel.ResumeLayout(false);
        concessionairesByZoneTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)concessionairesByZoneGrid).EndInit();
        concessionaireStatusTabPage.ResumeLayout(false);
        concessionaireStatusSplitContainer.Panel1.ResumeLayout(false);
        concessionaireStatusSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)concessionaireStatusSplitContainer).EndInit();
        concessionaireStatusSplitContainer.ResumeLayout(false);
        concessionaireStatusCardPanel.ResumeLayout(false);
        concessionaireStatusTablePanel.ResumeLayout(false);
        concessionaireStatusTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireStatusGrid).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel layoutPanel;
    private Label headingLabel;
    private Panel toolbarPanel;
    private Button prevMonthButton;
    private Label selectedMonthLabel;
    private Button nextMonthButton;
    private Label dashboardStatusLabel;
    private Button refreshButton;
    private TableLayoutPanel dashboardPanel;
    private TableLayoutPanel metricsLayoutPanel;
    private Panel totalBilledCardPanel;
    private Label totalBilledValueLabel;
    private Label totalBilledCaptionLabel;
    private Panel billCountCardPanel;
    private Label billCountValueLabel;
    private Label billCountCaptionLabel;
    private Panel unpaidBillsCardPanel;
    private Label unpaidBillsValueLabel;
    private Label unpaidBillsCaptionLabel;
    private Panel activeConcessionairesCardPanel;
    private Label activeConcessionairesValueLabel;
    private Label activeConcessionairesCaptionLabel;
    private TabControl reportsTabControl;
    private TabPage billedTrendTabPage;
    private SplitContainer billedTrendSplitContainer;
    private Panel billedTrendCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart billedTrendChart;
    private Label billedTrendTitleLabel;
    private TableLayoutPanel billedTrendTablePanel;
    private Label billedTrendTableTitleLabel;
    private DataGridView billedTrendGrid;
    private TabPage billingStatusTabPage;
    private SplitContainer billingStatusSplitContainer;
    private Panel billingStatusCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart billingStatusChart;
    private Label billingStatusTitleLabel;
    private TableLayoutPanel billingStatusTablePanel;
    private Label billingStatusTableTitleLabel;
    private DataGridView billingStatusGrid;
    private TabPage concessionairesByZoneTabPage;
    private SplitContainer concessionairesByZoneSplitContainer;
    private Panel concessionairesByZoneCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart concessionairesByZoneChart;
    private Label concessionairesByZoneTitleLabel;
    private TableLayoutPanel concessionairesByZoneTablePanel;
    private Label concessionairesByZoneTableTitleLabel;
    private DataGridView concessionairesByZoneGrid;
    private TabPage concessionaireStatusTabPage;
    private SplitContainer concessionaireStatusSplitContainer;
    private Panel concessionaireStatusCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart concessionaireStatusChart;
    private Label concessionaireStatusTitleLabel;
    private TableLayoutPanel concessionaireStatusTablePanel;
    private Label concessionaireStatusTableTitleLabel;
    private DataGridView concessionaireStatusGrid;
    private TabPage topConcessionairesTabPage;
    private SplitContainer topConcessionairesSplitContainer;
    private Panel topConcessionairesCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart topConcessionairesChart;
    private Label topConcessionairesTitleLabel;
    private TableLayoutPanel topConcessionairesTablePanel;
    private Label topConcessionairesTableTitleLabel;
    private DataGridView topConcessionairesGrid;
}
