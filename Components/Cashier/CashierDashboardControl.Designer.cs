namespace WDBS_2026.Components.Cashier;

partial class CashierDashboardControl
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
        prevDayButton = new Button();
        selectedDateLabel = new Label();
        nextDayButton = new Button();
        dashboardStatusLabel = new Label();
        refreshButton = new Button();
        dashboardPanel = new TableLayoutPanel();
        metricsLayoutPanel = new TableLayoutPanel();
        totalCollectedCardPanel = new Panel();
        totalCollectedValueLabel = new Label();
        totalCollectedCaptionLabel = new Label();
        receiptCountCardPanel = new Panel();
        receiptCountValueLabel = new Label();
        receiptCountCaptionLabel = new Label();
        scfCollectedCardPanel = new Panel();
        scfCollectedValueLabel = new Label();
        scfCollectedCaptionLabel = new Label();
        outstandingAccountsCardPanel = new Panel();
        outstandingAccountsValueLabel = new Label();
        outstandingAccountsCaptionLabel = new Label();
        reportsTabControl = new TabControl();
        collectionTrendTabPage = new TabPage();
        collectionTrendSplitContainer = new SplitContainer();
        collectionTrendCardPanel = new Panel();
        collectionTrendTitleLabel = new Label();
        collectionTrendTablePanel = new TableLayoutPanel();
        collectionTrendTableTitleLabel = new Label();
        collectionTrendGrid = new DataGridView();
        monthlyComparisonTabPage = new TabPage();
        monthlyTrendSplitContainer = new SplitContainer();
        monthlyTrendCardPanel = new Panel();
        monthlyTrendTitleLabel = new Label();
        monthlyTrendTablePanel = new TableLayoutPanel();
        monthlyTrendTableTitleLabel = new Label();
        monthlyTrendGrid = new DataGridView();
        topAccountsTabPage = new TabPage();
        topAccountsSplitContainer = new SplitContainer();
        topAccountsCardPanel = new Panel();
        topAccountsTitleLabel = new Label();
        topAccountsTablePanel = new TableLayoutPanel();
        topAccountsTableTitleLabel = new Label();
        topAccountsGrid = new DataGridView();
        paymentTypesTabPage = new TabPage();
        paymentTypesSplitContainer = new SplitContainer();
        paymentTypesCardPanel = new Panel();
        paymentTypesTitleLabel = new Label();
        paymentTypesTablePanel = new TableLayoutPanel();
        paymentTypesTableTitleLabel = new Label();
        paymentTypesGrid = new DataGridView();
        recentCollectionsTabPage = new TabPage();
        recentCollectionsTablePanel = new TableLayoutPanel();
        recentCollectionsTableTitleLabel = new Label();
        recentCollectionsGrid = new DataGridView();
        layoutPanel.SuspendLayout();
        toolbarPanel.SuspendLayout();
        dashboardPanel.SuspendLayout();
        metricsLayoutPanel.SuspendLayout();
        totalCollectedCardPanel.SuspendLayout();
        receiptCountCardPanel.SuspendLayout();
        scfCollectedCardPanel.SuspendLayout();
        outstandingAccountsCardPanel.SuspendLayout();
        reportsTabControl.SuspendLayout();
        collectionTrendTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)collectionTrendSplitContainer).BeginInit();
        collectionTrendSplitContainer.Panel1.SuspendLayout();
        collectionTrendSplitContainer.Panel2.SuspendLayout();
        collectionTrendSplitContainer.SuspendLayout();
        collectionTrendCardPanel.SuspendLayout();
        collectionTrendTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)collectionTrendGrid).BeginInit();
        monthlyComparisonTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)monthlyTrendSplitContainer).BeginInit();
        monthlyTrendSplitContainer.Panel1.SuspendLayout();
        monthlyTrendSplitContainer.Panel2.SuspendLayout();
        monthlyTrendSplitContainer.SuspendLayout();
        monthlyTrendCardPanel.SuspendLayout();
        monthlyTrendTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)monthlyTrendGrid).BeginInit();
        topAccountsTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)topAccountsSplitContainer).BeginInit();
        topAccountsSplitContainer.Panel1.SuspendLayout();
        topAccountsSplitContainer.Panel2.SuspendLayout();
        topAccountsSplitContainer.SuspendLayout();
        topAccountsCardPanel.SuspendLayout();
        topAccountsTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)topAccountsGrid).BeginInit();
        paymentTypesTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)paymentTypesSplitContainer).BeginInit();
        paymentTypesSplitContainer.Panel1.SuspendLayout();
        paymentTypesSplitContainer.Panel2.SuspendLayout();
        paymentTypesSplitContainer.SuspendLayout();
        paymentTypesCardPanel.SuspendLayout();
        paymentTypesTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)paymentTypesGrid).BeginInit();
        recentCollectionsTabPage.SuspendLayout();
        recentCollectionsTablePanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)recentCollectionsGrid).BeginInit();
        SuspendLayout();
        // 
        // layoutPanel
        // 
        layoutPanel.ColumnCount = 1;
        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutPanel.Controls.Add(headingLabel, 0, 0);
        layoutPanel.Controls.Add(toolbarPanel, 0, 2);
        layoutPanel.Controls.Add(dashboardPanel, 0, 3);
        layoutPanel.Dock = DockStyle.Fill;
        layoutPanel.Location = new Point(0, 0);
        layoutPanel.Margin = new Padding(3, 2, 3, 2);
        layoutPanel.Name = "layoutPanel";
        layoutPanel.Padding = new Padding(0, 3, 0, 0);
        layoutPanel.RowCount = 4;
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutPanel.Size = new Size(1120, 568);
        layoutPanel.TabIndex = 0;
        // 
        // headingLabel
        // 
        headingLabel.AutoSize = true;
        headingLabel.Location = new Point(3, 3);
        headingLabel.Name = "headingLabel";
        headingLabel.Size = new Size(124, 15);
        headingLabel.TabIndex = 0;
        headingLabel.Text = "Cashier Data Analytics";
        // 
        // toolbarPanel
        // 
        toolbarPanel.Controls.Add(prevDayButton);
        toolbarPanel.Controls.Add(selectedDateLabel);
        toolbarPanel.Controls.Add(nextDayButton);
        toolbarPanel.Controls.Add(dashboardStatusLabel);
        toolbarPanel.Controls.Add(refreshButton);
        toolbarPanel.Dock = DockStyle.Fill;
        toolbarPanel.Location = new Point(0, 24);
        toolbarPanel.Margin = new Padding(0, 6, 0, 0);
        toolbarPanel.Name = "toolbarPanel";
        toolbarPanel.Size = new Size(1120, 35);
        toolbarPanel.TabIndex = 2;
        // 
        // prevDayButton
        // 
        prevDayButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        prevDayButton.Location = new Point(686, 5);
        prevDayButton.Margin = new Padding(3, 2, 3, 2);
        prevDayButton.Name = "prevDayButton";
        prevDayButton.Size = new Size(75, 25);
        prevDayButton.TabIndex = 1;
        prevDayButton.Text = "Prev";
        prevDayButton.UseVisualStyleBackColor = true;
        prevDayButton.Click += prevDayButton_Click;
        // 
        // selectedDateLabel
        // 
        selectedDateLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        selectedDateLabel.Location = new Point(767, 10);
        selectedDateLabel.Name = "selectedDateLabel";
        selectedDateLabel.Size = new Size(125, 15);
        selectedDateLabel.TabIndex = 2;
        selectedDateLabel.Text = "May 30, 2026";
        selectedDateLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // nextDayButton
        // 
        nextDayButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        nextDayButton.Location = new Point(898, 5);
        nextDayButton.Margin = new Padding(3, 2, 3, 2);
        nextDayButton.Name = "nextDayButton";
        nextDayButton.Size = new Size(75, 25);
        nextDayButton.TabIndex = 3;
        nextDayButton.Text = "Next";
        nextDayButton.UseVisualStyleBackColor = true;
        nextDayButton.Click += nextDayButton_Click;
        // 
        // dashboardStatusLabel
        // 
        dashboardStatusLabel.AutoSize = true;
        dashboardStatusLabel.Location = new Point(0, 10);
        dashboardStatusLabel.Name = "dashboardStatusLabel";
        dashboardStatusLabel.Size = new Size(249, 15);
        dashboardStatusLabel.TabIndex = 0;
        dashboardStatusLabel.Text = "Loading cashier collections and activity data...";
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
        dashboardPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
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
        metricsLayoutPanel.Controls.Add(totalCollectedCardPanel, 0, 0);
        metricsLayoutPanel.Controls.Add(receiptCountCardPanel, 1, 0);
        metricsLayoutPanel.Controls.Add(scfCollectedCardPanel, 2, 0);
        metricsLayoutPanel.Controls.Add(outstandingAccountsCardPanel, 3, 0);
        metricsLayoutPanel.Dock = DockStyle.Fill;
        metricsLayoutPanel.Location = new Point(0, 0);
        metricsLayoutPanel.Margin = new Padding(0);
        metricsLayoutPanel.Name = "metricsLayoutPanel";
        metricsLayoutPanel.RowCount = 1;
        metricsLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        metricsLayoutPanel.Size = new Size(1120, 69);
        metricsLayoutPanel.TabIndex = 0;
        // 
        // totalCollectedCardPanel
        // 
        totalCollectedCardPanel.Controls.Add(totalCollectedValueLabel);
        totalCollectedCardPanel.Controls.Add(totalCollectedCaptionLabel);
        totalCollectedCardPanel.Dock = DockStyle.Fill;
        totalCollectedCardPanel.Location = new Point(0, 0);
        totalCollectedCardPanel.Margin = new Padding(0);
        totalCollectedCardPanel.Name = "totalCollectedCardPanel";
        totalCollectedCardPanel.Padding = new Padding(3);
        totalCollectedCardPanel.Size = new Size(280, 69);
        totalCollectedCardPanel.TabIndex = 0;
        // 
        // totalCollectedValueLabel
        // 
        totalCollectedValueLabel.AutoSize = true;
        totalCollectedValueLabel.Dock = DockStyle.Top;
        totalCollectedValueLabel.Location = new Point(3, 18);
        totalCollectedValueLabel.Name = "totalCollectedValueLabel";
        totalCollectedValueLabel.Size = new Size(54, 15);
        totalCollectedValueLabel.TabIndex = 1;
        totalCollectedValueLabel.Text = "PHP 0.00";
        // 
        // totalCollectedCaptionLabel
        // 
        totalCollectedCaptionLabel.AutoSize = true;
        totalCollectedCaptionLabel.Dock = DockStyle.Top;
        totalCollectedCaptionLabel.Location = new Point(3, 3);
        totalCollectedCaptionLabel.Name = "totalCollectedCaptionLabel";
        totalCollectedCaptionLabel.Size = new Size(91, 15);
        totalCollectedCaptionLabel.TabIndex = 0;
        totalCollectedCaptionLabel.Text = "Collected Today";
        // 
        // receiptCountCardPanel
        // 
        receiptCountCardPanel.Controls.Add(receiptCountValueLabel);
        receiptCountCardPanel.Controls.Add(receiptCountCaptionLabel);
        receiptCountCardPanel.Dock = DockStyle.Fill;
        receiptCountCardPanel.Location = new Point(280, 0);
        receiptCountCardPanel.Margin = new Padding(0);
        receiptCountCardPanel.Name = "receiptCountCardPanel";
        receiptCountCardPanel.Padding = new Padding(3);
        receiptCountCardPanel.Size = new Size(280, 69);
        receiptCountCardPanel.TabIndex = 1;
        // 
        // receiptCountValueLabel
        // 
        receiptCountValueLabel.AutoSize = true;
        receiptCountValueLabel.Dock = DockStyle.Top;
        receiptCountValueLabel.Location = new Point(3, 18);
        receiptCountValueLabel.Name = "receiptCountValueLabel";
        receiptCountValueLabel.Size = new Size(13, 15);
        receiptCountValueLabel.TabIndex = 1;
        receiptCountValueLabel.Text = "0";
        // 
        // receiptCountCaptionLabel
        // 
        receiptCountCaptionLabel.AutoSize = true;
        receiptCountCaptionLabel.Dock = DockStyle.Top;
        receiptCountCaptionLabel.Location = new Point(3, 3);
        receiptCountCaptionLabel.Name = "receiptCountCaptionLabel";
        receiptCountCaptionLabel.Size = new Size(67, 15);
        receiptCountCaptionLabel.TabIndex = 0;
        receiptCountCaptionLabel.Text = "ORs Posted";
        // 
        // scfCollectedCardPanel
        // 
        scfCollectedCardPanel.Controls.Add(scfCollectedValueLabel);
        scfCollectedCardPanel.Controls.Add(scfCollectedCaptionLabel);
        scfCollectedCardPanel.Dock = DockStyle.Fill;
        scfCollectedCardPanel.Location = new Point(560, 0);
        scfCollectedCardPanel.Margin = new Padding(0);
        scfCollectedCardPanel.Name = "scfCollectedCardPanel";
        scfCollectedCardPanel.Padding = new Padding(3);
        scfCollectedCardPanel.Size = new Size(280, 69);
        scfCollectedCardPanel.TabIndex = 2;
        // 
        // scfCollectedValueLabel
        // 
        scfCollectedValueLabel.AutoSize = true;
        scfCollectedValueLabel.Dock = DockStyle.Top;
        scfCollectedValueLabel.Location = new Point(3, 18);
        scfCollectedValueLabel.Name = "scfCollectedValueLabel";
        scfCollectedValueLabel.Size = new Size(54, 15);
        scfCollectedValueLabel.TabIndex = 1;
        scfCollectedValueLabel.Text = "PHP 0.00";
        // 
        // scfCollectedCaptionLabel
        // 
        scfCollectedCaptionLabel.AutoSize = true;
        scfCollectedCaptionLabel.Dock = DockStyle.Top;
        scfCollectedCaptionLabel.Location = new Point(3, 3);
        scfCollectedCaptionLabel.Name = "scfCollectedCaptionLabel";
        scfCollectedCaptionLabel.Size = new Size(80, 15);
        scfCollectedCaptionLabel.TabIndex = 0;
        scfCollectedCaptionLabel.Text = "SCF Collected";
        // 
        // outstandingAccountsCardPanel
        // 
        outstandingAccountsCardPanel.Controls.Add(outstandingAccountsValueLabel);
        outstandingAccountsCardPanel.Controls.Add(outstandingAccountsCaptionLabel);
        outstandingAccountsCardPanel.Dock = DockStyle.Fill;
        outstandingAccountsCardPanel.Location = new Point(840, 0);
        outstandingAccountsCardPanel.Margin = new Padding(0);
        outstandingAccountsCardPanel.Name = "outstandingAccountsCardPanel";
        outstandingAccountsCardPanel.Padding = new Padding(3);
        outstandingAccountsCardPanel.Size = new Size(280, 69);
        outstandingAccountsCardPanel.TabIndex = 3;
        // 
        // outstandingAccountsValueLabel
        // 
        outstandingAccountsValueLabel.AutoSize = true;
        outstandingAccountsValueLabel.Dock = DockStyle.Top;
        outstandingAccountsValueLabel.Location = new Point(3, 18);
        outstandingAccountsValueLabel.Name = "outstandingAccountsValueLabel";
        outstandingAccountsValueLabel.Size = new Size(13, 15);
        outstandingAccountsValueLabel.TabIndex = 1;
        outstandingAccountsValueLabel.Text = "0";
        // 
        // outstandingAccountsCaptionLabel
        // 
        outstandingAccountsCaptionLabel.AutoSize = true;
        outstandingAccountsCaptionLabel.Dock = DockStyle.Top;
        outstandingAccountsCaptionLabel.Location = new Point(3, 3);
        outstandingAccountsCaptionLabel.Name = "outstandingAccountsCaptionLabel";
        outstandingAccountsCaptionLabel.Size = new Size(126, 15);
        outstandingAccountsCaptionLabel.TabIndex = 0;
        outstandingAccountsCaptionLabel.Text = "Outstanding Accounts";
        // 
        // reportsTabControl
        // 
        reportsTabControl.Controls.Add(collectionTrendTabPage);
        reportsTabControl.Controls.Add(monthlyComparisonTabPage);
        reportsTabControl.Controls.Add(topAccountsTabPage);
        reportsTabControl.Controls.Add(paymentTypesTabPage);
        reportsTabControl.Controls.Add(recentCollectionsTabPage);
        reportsTabControl.Dock = DockStyle.Fill;
        reportsTabControl.Location = new Point(0, 78);
        reportsTabControl.Margin = new Padding(0, 9, 0, 0);
        reportsTabControl.Name = "reportsTabControl";
        reportsTabControl.SelectedIndex = 0;
        reportsTabControl.Size = new Size(1120, 431);
        reportsTabControl.TabIndex = 1;
        // 
        // collectionTrendTabPage
        // 
        collectionTrendTabPage.Controls.Add(collectionTrendSplitContainer);
        collectionTrendTabPage.Location = new Point(4, 24);
        collectionTrendTabPage.Name = "collectionTrendTabPage";
        collectionTrendTabPage.Padding = new Padding(8, 6, 8, 6);
        collectionTrendTabPage.Size = new Size(1112, 403);
        collectionTrendTabPage.TabIndex = 0;
        collectionTrendTabPage.Text = "Daily Collection Comparison";
        collectionTrendTabPage.UseVisualStyleBackColor = true;
        // 
        // collectionTrendSplitContainer
        // 
        collectionTrendSplitContainer.Dock = DockStyle.Fill;
        collectionTrendSplitContainer.Location = new Point(8, 6);
        collectionTrendSplitContainer.Name = "collectionTrendSplitContainer";
        collectionTrendSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // collectionTrendSplitContainer.Panel1
        // 
        collectionTrendSplitContainer.Panel1.Controls.Add(collectionTrendCardPanel);
        collectionTrendSplitContainer.Panel1MinSize = 160;
        // 
        // collectionTrendSplitContainer.Panel2
        // 
        collectionTrendSplitContainer.Panel2.Controls.Add(collectionTrendTablePanel);
        collectionTrendSplitContainer.Size = new Size(1096, 391);
        collectionTrendSplitContainer.SplitterDistance = 300;
        collectionTrendSplitContainer.TabIndex = 0;
        // 
        // collectionTrendCardPanel
        // 
        collectionTrendCardPanel.Controls.Add(collectionTrendTitleLabel);
        collectionTrendCardPanel.Dock = DockStyle.Fill;
        collectionTrendCardPanel.Location = new Point(0, 0);
        collectionTrendCardPanel.Name = "collectionTrendCardPanel";
        collectionTrendCardPanel.Padding = new Padding(16, 9, 16, 11);
        collectionTrendCardPanel.Size = new Size(1096, 300);
        collectionTrendCardPanel.TabIndex = 0;
        // 
        // collectionTrendTitleLabel
        // 
        collectionTrendTitleLabel.Dock = DockStyle.Top;
        collectionTrendTitleLabel.Location = new Point(16, 9);
        collectionTrendTitleLabel.Name = "collectionTrendTitleLabel";
        collectionTrendTitleLabel.Size = new Size(1064, 21);
        collectionTrendTitleLabel.TabIndex = 0;
        collectionTrendTitleLabel.Text = "Collection Trend";
        collectionTrendTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // collectionTrendTablePanel
        // 
        collectionTrendTablePanel.ColumnCount = 1;
        collectionTrendTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        collectionTrendTablePanel.Controls.Add(collectionTrendTableTitleLabel, 0, 0);
        collectionTrendTablePanel.Controls.Add(collectionTrendGrid, 0, 1);
        collectionTrendTablePanel.Dock = DockStyle.Fill;
        collectionTrendTablePanel.Location = new Point(0, 0);
        collectionTrendTablePanel.Name = "collectionTrendTablePanel";
        collectionTrendTablePanel.RowCount = 2;
        collectionTrendTablePanel.RowStyles.Add(new RowStyle());
        collectionTrendTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        collectionTrendTablePanel.Size = new Size(1096, 87);
        collectionTrendTablePanel.TabIndex = 0;
        // 
        // collectionTrendTableTitleLabel
        // 
        collectionTrendTableTitleLabel.AutoSize = true;
        collectionTrendTableTitleLabel.Location = new Point(0, 0);
        collectionTrendTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        collectionTrendTableTitleLabel.Name = "collectionTrendTableTitleLabel";
        collectionTrendTableTitleLabel.Size = new Size(166, 15);
        collectionTrendTableTitleLabel.TabIndex = 0;
        collectionTrendTableTitleLabel.Text = "Daily Collected vs Uncollected";
        // 
        // collectionTrendGrid
        // 
        collectionTrendGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        collectionTrendGrid.Dock = DockStyle.Fill;
        collectionTrendGrid.Location = new Point(0, 21);
        collectionTrendGrid.Margin = new Padding(0);
        collectionTrendGrid.Name = "collectionTrendGrid";
        collectionTrendGrid.Size = new Size(1096, 66);
        collectionTrendGrid.TabIndex = 1;
        // 
        // monthlyComparisonTabPage
        // 
        monthlyComparisonTabPage.Controls.Add(monthlyTrendSplitContainer);
        monthlyComparisonTabPage.Location = new Point(4, 24);
        monthlyComparisonTabPage.Name = "monthlyComparisonTabPage";
        monthlyComparisonTabPage.Padding = new Padding(8, 6, 8, 6);
        monthlyComparisonTabPage.Size = new Size(1112, 403);
        monthlyComparisonTabPage.TabIndex = 1;
        monthlyComparisonTabPage.Text = "Monthly Collection Comparison";
        monthlyComparisonTabPage.UseVisualStyleBackColor = true;
        // 
        // monthlyTrendSplitContainer
        // 
        monthlyTrendSplitContainer.Dock = DockStyle.Fill;
        monthlyTrendSplitContainer.Location = new Point(8, 6);
        monthlyTrendSplitContainer.Name = "monthlyTrendSplitContainer";
        monthlyTrendSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // monthlyTrendSplitContainer.Panel1
        // 
        monthlyTrendSplitContainer.Panel1.Controls.Add(monthlyTrendCardPanel);
        monthlyTrendSplitContainer.Panel1MinSize = 160;
        // 
        // monthlyTrendSplitContainer.Panel2
        // 
        monthlyTrendSplitContainer.Panel2.Controls.Add(monthlyTrendTablePanel);
        monthlyTrendSplitContainer.Size = new Size(1096, 391);
        monthlyTrendSplitContainer.SplitterDistance = 300;
        monthlyTrendSplitContainer.TabIndex = 0;
        // 
        // monthlyTrendCardPanel
        // 
        monthlyTrendCardPanel.Controls.Add(monthlyTrendTitleLabel);
        monthlyTrendCardPanel.Dock = DockStyle.Fill;
        monthlyTrendCardPanel.Location = new Point(0, 0);
        monthlyTrendCardPanel.Name = "monthlyTrendCardPanel";
        monthlyTrendCardPanel.Padding = new Padding(16, 9, 16, 11);
        monthlyTrendCardPanel.Size = new Size(1096, 300);
        monthlyTrendCardPanel.TabIndex = 0;
        // 
        // monthlyTrendTitleLabel
        // 
        monthlyTrendTitleLabel.Dock = DockStyle.Top;
        monthlyTrendTitleLabel.Location = new Point(16, 9);
        monthlyTrendTitleLabel.Name = "monthlyTrendTitleLabel";
        monthlyTrendTitleLabel.Size = new Size(1064, 21);
        monthlyTrendTitleLabel.TabIndex = 0;
        monthlyTrendTitleLabel.Text = "Monthly Collection Comparison";
        monthlyTrendTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // monthlyTrendTablePanel
        // 
        monthlyTrendTablePanel.ColumnCount = 1;
        monthlyTrendTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        monthlyTrendTablePanel.Controls.Add(monthlyTrendTableTitleLabel, 0, 0);
        monthlyTrendTablePanel.Controls.Add(monthlyTrendGrid, 0, 1);
        monthlyTrendTablePanel.Dock = DockStyle.Fill;
        monthlyTrendTablePanel.Location = new Point(0, 0);
        monthlyTrendTablePanel.Name = "monthlyTrendTablePanel";
        monthlyTrendTablePanel.RowCount = 2;
        monthlyTrendTablePanel.RowStyles.Add(new RowStyle());
        monthlyTrendTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        monthlyTrendTablePanel.Size = new Size(1096, 87);
        monthlyTrendTablePanel.TabIndex = 0;
        // 
        // monthlyTrendTableTitleLabel
        // 
        monthlyTrendTableTitleLabel.AutoSize = true;
        monthlyTrendTableTitleLabel.Location = new Point(0, 0);
        monthlyTrendTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        monthlyTrendTableTitleLabel.Name = "monthlyTrendTableTitleLabel";
        monthlyTrendTableTitleLabel.Size = new Size(185, 15);
        monthlyTrendTableTitleLabel.TabIndex = 0;
        monthlyTrendTableTitleLabel.Text = "Monthly Collected vs Uncollected";
        // 
        // monthlyTrendGrid
        // 
        monthlyTrendGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        monthlyTrendGrid.Dock = DockStyle.Fill;
        monthlyTrendGrid.Location = new Point(0, 21);
        monthlyTrendGrid.Margin = new Padding(0);
        monthlyTrendGrid.Name = "monthlyTrendGrid";
        monthlyTrendGrid.Size = new Size(1096, 66);
        monthlyTrendGrid.TabIndex = 1;
        // 
        // topAccountsTabPage
        // 
        topAccountsTabPage.Controls.Add(topAccountsSplitContainer);
        topAccountsTabPage.Location = new Point(4, 24);
        topAccountsTabPage.Name = "topAccountsTabPage";
        topAccountsTabPage.Padding = new Padding(8, 6, 8, 6);
        topAccountsTabPage.Size = new Size(1112, 403);
        topAccountsTabPage.TabIndex = 3;
        topAccountsTabPage.Text = "Top Accounts";
        topAccountsTabPage.UseVisualStyleBackColor = true;
        // 
        // topAccountsSplitContainer
        // 
        topAccountsSplitContainer.Dock = DockStyle.Fill;
        topAccountsSplitContainer.Location = new Point(8, 6);
        topAccountsSplitContainer.Name = "topAccountsSplitContainer";
        topAccountsSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // topAccountsSplitContainer.Panel1
        // 
        topAccountsSplitContainer.Panel1.Controls.Add(topAccountsCardPanel);
        topAccountsSplitContainer.Panel1MinSize = 160;
        // 
        // topAccountsSplitContainer.Panel2
        // 
        topAccountsSplitContainer.Panel2.Controls.Add(topAccountsTablePanel);
        topAccountsSplitContainer.Size = new Size(1096, 391);
        topAccountsSplitContainer.SplitterDistance = 300;
        topAccountsSplitContainer.TabIndex = 0;
        // 
        // topAccountsCardPanel
        // 
        topAccountsCardPanel.Controls.Add(topAccountsTitleLabel);
        topAccountsCardPanel.Dock = DockStyle.Fill;
        topAccountsCardPanel.Location = new Point(0, 0);
        topAccountsCardPanel.Name = "topAccountsCardPanel";
        topAccountsCardPanel.Padding = new Padding(16, 9, 16, 11);
        topAccountsCardPanel.Size = new Size(1096, 300);
        topAccountsCardPanel.TabIndex = 0;
        // 
        // topAccountsTitleLabel
        // 
        topAccountsTitleLabel.Dock = DockStyle.Top;
        topAccountsTitleLabel.Location = new Point(16, 9);
        topAccountsTitleLabel.Name = "topAccountsTitleLabel";
        topAccountsTitleLabel.Size = new Size(1064, 21);
        topAccountsTitleLabel.TabIndex = 0;
        topAccountsTitleLabel.Text = "Top Collected Accounts";
        topAccountsTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // topAccountsTablePanel
        // 
        topAccountsTablePanel.ColumnCount = 1;
        topAccountsTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        topAccountsTablePanel.Controls.Add(topAccountsTableTitleLabel, 0, 0);
        topAccountsTablePanel.Controls.Add(topAccountsGrid, 0, 1);
        topAccountsTablePanel.Dock = DockStyle.Fill;
        topAccountsTablePanel.Location = new Point(0, 0);
        topAccountsTablePanel.Name = "topAccountsTablePanel";
        topAccountsTablePanel.RowCount = 2;
        topAccountsTablePanel.RowStyles.Add(new RowStyle());
        topAccountsTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        topAccountsTablePanel.Size = new Size(1096, 87);
        topAccountsTablePanel.TabIndex = 0;
        // 
        // topAccountsTableTitleLabel
        // 
        topAccountsTableTitleLabel.AutoSize = true;
        topAccountsTableTitleLabel.Location = new Point(0, 0);
        topAccountsTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        topAccountsTableTitleLabel.Name = "topAccountsTableTitleLabel";
        topAccountsTableTitleLabel.Size = new Size(189, 15);
        topAccountsTableTitleLabel.TabIndex = 0;
        topAccountsTableTitleLabel.Text = "Highest Collected Concessionaires";
        // 
        // topAccountsGrid
        // 
        topAccountsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        topAccountsGrid.Dock = DockStyle.Fill;
        topAccountsGrid.Location = new Point(0, 21);
        topAccountsGrid.Margin = new Padding(0);
        topAccountsGrid.Name = "topAccountsGrid";
        topAccountsGrid.Size = new Size(1096, 66);
        topAccountsGrid.TabIndex = 1;
        // 
        // paymentTypesTabPage
        // 
        paymentTypesTabPage.Controls.Add(paymentTypesSplitContainer);
        paymentTypesTabPage.Location = new Point(4, 24);
        paymentTypesTabPage.Name = "paymentTypesTabPage";
        paymentTypesTabPage.Padding = new Padding(8, 6, 8, 6);
        paymentTypesTabPage.Size = new Size(1112, 403);
        paymentTypesTabPage.TabIndex = 2;
        paymentTypesTabPage.Text = "Payment Types";
        paymentTypesTabPage.UseVisualStyleBackColor = true;
        // 
        // paymentTypesSplitContainer
        // 
        paymentTypesSplitContainer.Dock = DockStyle.Fill;
        paymentTypesSplitContainer.Location = new Point(8, 6);
        paymentTypesSplitContainer.Name = "paymentTypesSplitContainer";
        paymentTypesSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // paymentTypesSplitContainer.Panel1
        // 
        paymentTypesSplitContainer.Panel1.Controls.Add(paymentTypesCardPanel);
        paymentTypesSplitContainer.Panel1MinSize = 160;
        // 
        // paymentTypesSplitContainer.Panel2
        // 
        paymentTypesSplitContainer.Panel2.Controls.Add(paymentTypesTablePanel);
        paymentTypesSplitContainer.Size = new Size(1096, 391);
        paymentTypesSplitContainer.SplitterDistance = 300;
        paymentTypesSplitContainer.TabIndex = 0;
        // 
        // paymentTypesCardPanel
        // 
        paymentTypesCardPanel.Controls.Add(paymentTypesTitleLabel);
        paymentTypesCardPanel.Dock = DockStyle.Fill;
        paymentTypesCardPanel.Location = new Point(0, 0);
        paymentTypesCardPanel.Name = "paymentTypesCardPanel";
        paymentTypesCardPanel.Padding = new Padding(16, 9, 16, 11);
        paymentTypesCardPanel.Size = new Size(1096, 300);
        paymentTypesCardPanel.TabIndex = 0;
        // 
        // paymentTypesTitleLabel
        // 
        paymentTypesTitleLabel.Dock = DockStyle.Top;
        paymentTypesTitleLabel.Location = new Point(16, 9);
        paymentTypesTitleLabel.Name = "paymentTypesTitleLabel";
        paymentTypesTitleLabel.Size = new Size(1064, 21);
        paymentTypesTitleLabel.TabIndex = 0;
        paymentTypesTitleLabel.Text = "Payment Type Mix";
        paymentTypesTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // paymentTypesTablePanel
        // 
        paymentTypesTablePanel.ColumnCount = 1;
        paymentTypesTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        paymentTypesTablePanel.Controls.Add(paymentTypesTableTitleLabel, 0, 0);
        paymentTypesTablePanel.Controls.Add(paymentTypesGrid, 0, 1);
        paymentTypesTablePanel.Dock = DockStyle.Fill;
        paymentTypesTablePanel.Location = new Point(0, 0);
        paymentTypesTablePanel.Name = "paymentTypesTablePanel";
        paymentTypesTablePanel.RowCount = 2;
        paymentTypesTablePanel.RowStyles.Add(new RowStyle());
        paymentTypesTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        paymentTypesTablePanel.Size = new Size(1096, 87);
        paymentTypesTablePanel.TabIndex = 0;
        // 
        // paymentTypesTableTitleLabel
        // 
        paymentTypesTableTitleLabel.AutoSize = true;
        paymentTypesTableTitleLabel.Location = new Point(0, 0);
        paymentTypesTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        paymentTypesTableTitleLabel.Name = "paymentTypesTableTitleLabel";
        paymentTypesTableTitleLabel.Size = new Size(164, 15);
        paymentTypesTableTitleLabel.TabIndex = 0;
        paymentTypesTableTitleLabel.Text = "Payment Method Distribution";
        // 
        // paymentTypesGrid
        // 
        paymentTypesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        paymentTypesGrid.Dock = DockStyle.Fill;
        paymentTypesGrid.Location = new Point(0, 21);
        paymentTypesGrid.Margin = new Padding(0);
        paymentTypesGrid.Name = "paymentTypesGrid";
        paymentTypesGrid.Size = new Size(1096, 66);
        paymentTypesGrid.TabIndex = 1;
        // 
        // recentCollectionsTabPage
        // 
        recentCollectionsTabPage.Controls.Add(recentCollectionsTablePanel);
        recentCollectionsTabPage.Location = new Point(4, 24);
        recentCollectionsTabPage.Name = "recentCollectionsTabPage";
        recentCollectionsTabPage.Padding = new Padding(8, 6, 8, 6);
        recentCollectionsTabPage.Size = new Size(1112, 403);
        recentCollectionsTabPage.TabIndex = 4;
        recentCollectionsTabPage.Text = "Recent Collections";
        recentCollectionsTabPage.UseVisualStyleBackColor = true;
        // 
        // recentCollectionsTablePanel
        // 
        recentCollectionsTablePanel.ColumnCount = 1;
        recentCollectionsTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        recentCollectionsTablePanel.Controls.Add(recentCollectionsTableTitleLabel, 0, 0);
        recentCollectionsTablePanel.Controls.Add(recentCollectionsGrid, 0, 1);
        recentCollectionsTablePanel.Dock = DockStyle.Fill;
        recentCollectionsTablePanel.Location = new Point(8, 6);
        recentCollectionsTablePanel.Name = "recentCollectionsTablePanel";
        recentCollectionsTablePanel.RowCount = 2;
        recentCollectionsTablePanel.RowStyles.Add(new RowStyle());
        recentCollectionsTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        recentCollectionsTablePanel.Size = new Size(1096, 391);
        recentCollectionsTablePanel.TabIndex = 0;
        // 
        // recentCollectionsTableTitleLabel
        // 
        recentCollectionsTableTitleLabel.AutoSize = true;
        recentCollectionsTableTitleLabel.Location = new Point(0, 0);
        recentCollectionsTableTitleLabel.Margin = new Padding(0, 0, 0, 6);
        recentCollectionsTableTitleLabel.Name = "recentCollectionsTableTitleLabel";
        recentCollectionsTableTitleLabel.Size = new Size(123, 15);
        recentCollectionsTableTitleLabel.TabIndex = 0;
        recentCollectionsTableTitleLabel.Text = "Latest Cashier Activity";
        // 
        // recentCollectionsGrid
        // 
        recentCollectionsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        recentCollectionsGrid.Dock = DockStyle.Fill;
        recentCollectionsGrid.Location = new Point(0, 21);
        recentCollectionsGrid.Margin = new Padding(0);
        recentCollectionsGrid.Name = "recentCollectionsGrid";
        recentCollectionsGrid.Size = new Size(1096, 370);
        recentCollectionsGrid.TabIndex = 1;
        // 
        // CashierDashboardControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(layoutPanel);
        Name = "CashierDashboardControl";
        Size = new Size(1120, 568);
        layoutPanel.ResumeLayout(false);
        layoutPanel.PerformLayout();
        toolbarPanel.ResumeLayout(false);
        toolbarPanel.PerformLayout();
        dashboardPanel.ResumeLayout(false);
        metricsLayoutPanel.ResumeLayout(false);
        totalCollectedCardPanel.ResumeLayout(false);
        totalCollectedCardPanel.PerformLayout();
        receiptCountCardPanel.ResumeLayout(false);
        receiptCountCardPanel.PerformLayout();
        scfCollectedCardPanel.ResumeLayout(false);
        scfCollectedCardPanel.PerformLayout();
        outstandingAccountsCardPanel.ResumeLayout(false);
        outstandingAccountsCardPanel.PerformLayout();
        reportsTabControl.ResumeLayout(false);
        collectionTrendTabPage.ResumeLayout(false);
        collectionTrendSplitContainer.Panel1.ResumeLayout(false);
        collectionTrendSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)collectionTrendSplitContainer).EndInit();
        collectionTrendSplitContainer.ResumeLayout(false);
        collectionTrendCardPanel.ResumeLayout(false);
        collectionTrendTablePanel.ResumeLayout(false);
        collectionTrendTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)collectionTrendGrid).EndInit();
        monthlyComparisonTabPage.ResumeLayout(false);
        monthlyTrendSplitContainer.Panel1.ResumeLayout(false);
        monthlyTrendSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)monthlyTrendSplitContainer).EndInit();
        monthlyTrendSplitContainer.ResumeLayout(false);
        monthlyTrendCardPanel.ResumeLayout(false);
        monthlyTrendTablePanel.ResumeLayout(false);
        monthlyTrendTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)monthlyTrendGrid).EndInit();
        topAccountsTabPage.ResumeLayout(false);
        topAccountsSplitContainer.Panel1.ResumeLayout(false);
        topAccountsSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)topAccountsSplitContainer).EndInit();
        topAccountsSplitContainer.ResumeLayout(false);
        topAccountsCardPanel.ResumeLayout(false);
        topAccountsTablePanel.ResumeLayout(false);
        topAccountsTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)topAccountsGrid).EndInit();
        paymentTypesTabPage.ResumeLayout(false);
        paymentTypesSplitContainer.Panel1.ResumeLayout(false);
        paymentTypesSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)paymentTypesSplitContainer).EndInit();
        paymentTypesSplitContainer.ResumeLayout(false);
        paymentTypesCardPanel.ResumeLayout(false);
        paymentTypesTablePanel.ResumeLayout(false);
        paymentTypesTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)paymentTypesGrid).EndInit();
        recentCollectionsTabPage.ResumeLayout(false);
        recentCollectionsTablePanel.ResumeLayout(false);
        recentCollectionsTablePanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)recentCollectionsGrid).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel layoutPanel;
    private Label headingLabel;
    private Panel toolbarPanel;
    private Button prevDayButton;
    private Label selectedDateLabel;
    private Button nextDayButton;
    private Label dashboardStatusLabel;
    private Button refreshButton;
    private TableLayoutPanel dashboardPanel;
    private TableLayoutPanel metricsLayoutPanel;
    private Panel totalCollectedCardPanel;
    private Label totalCollectedValueLabel;
    private Label totalCollectedCaptionLabel;
    private Panel receiptCountCardPanel;
    private Label receiptCountValueLabel;
    private Label receiptCountCaptionLabel;
    private Panel scfCollectedCardPanel;
    private Label scfCollectedValueLabel;
    private Label scfCollectedCaptionLabel;
    private Panel outstandingAccountsCardPanel;
    private Label outstandingAccountsValueLabel;
    private Label outstandingAccountsCaptionLabel;
    private TabControl reportsTabControl;
    private TabPage collectionTrendTabPage;
    private SplitContainer collectionTrendSplitContainer;
    private Panel collectionTrendCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart collectionTrendChart;
    private Label collectionTrendTitleLabel;
    private TableLayoutPanel collectionTrendTablePanel;
    private Label collectionTrendTableTitleLabel;
    private DataGridView collectionTrendGrid;
    private TabPage paymentTypesTabPage;
    private SplitContainer paymentTypesSplitContainer;
    private Panel paymentTypesCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart paymentTypesChart;
    private Label paymentTypesTitleLabel;
    private TableLayoutPanel paymentTypesTablePanel;
    private Label paymentTypesTableTitleLabel;
    private DataGridView paymentTypesGrid;
    private TabPage topAccountsTabPage;
    private SplitContainer topAccountsSplitContainer;
    private Panel topAccountsCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart topAccountsChart;
    private Label topAccountsTitleLabel;
    private TableLayoutPanel topAccountsTablePanel;
    private Label topAccountsTableTitleLabel;
    private DataGridView topAccountsGrid;
    private TabPage recentCollectionsTabPage;
    private TableLayoutPanel recentCollectionsTablePanel;
    private Label recentCollectionsTableTitleLabel;
    private DataGridView recentCollectionsGrid;
    private TabPage monthlyComparisonTabPage;
    private SplitContainer monthlyTrendSplitContainer;
    private Panel monthlyTrendCardPanel;
    private System.Windows.Forms.DataVisualization.Charting.Chart monthlyTrendChart;
    private Label monthlyTrendTitleLabel;
    private TableLayoutPanel monthlyTrendTablePanel;
    private Label monthlyTrendTableTitleLabel;
    private DataGridView monthlyTrendGrid;
}