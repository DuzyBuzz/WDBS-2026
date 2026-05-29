namespace WDBS_2026.Components.Biller;

partial class MeterReadingUserControl
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
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        contentSplitContainer = new SplitContainer();
        leftCardPanel = new Panel();
        leftLayout = new TableLayoutPanel();
        meterReadingGrid = new DataGridView();
        leftStatusLabel = new Label();
        meterEntryFiltersLayout = new TableLayoutPanel();
        printLatestReadingSheetButton = new Button();
        generateBillsButton = new Button();
        refreshMeterReadingsButton = new Button();
        dueDatePicker = new DateTimePicker();
        readingDatePicker = new DateTimePicker();
        zoneComboBox = new ComboBox();
        dueDateLabel = new Label();
        readingDateLabel = new Label();
        zoneLabel = new Label();
        meterSearchTextBox = new TextBox();
        createBillsTitleLabel = new Label();
        rightCardPanel = new Panel();
        rightLayout = new TableLayoutPanel();
        generatedBillsGrid = new DataGridView();
        rightStatusLabel = new Label();
        generatedBillsFilterLayout = new TableLayoutPanel();
        refreshGeneratedBillsButton = new Button();
        generatedBillsSearchTextBox = new TextBox();
        generatedBillsDatePicker = new DateTimePicker();
        generatedBillsDateLabel = new Label();
        generatedBillsTitleLabel = new Label();
        subtitleLabel = new Label();
        leftActionLayout = new TableLayoutPanel();
        createBillsSubtitleLabel = new Label();
        generatedBillsSubtitleLabel = new Label();
        rootLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)contentSplitContainer).BeginInit();
        contentSplitContainer.Panel1.SuspendLayout();
        contentSplitContainer.Panel2.SuspendLayout();
        contentSplitContainer.SuspendLayout();
        leftCardPanel.SuspendLayout();
        leftLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)meterReadingGrid).BeginInit();
        meterEntryFiltersLayout.SuspendLayout();
        rightCardPanel.SuspendLayout();
        rightLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)generatedBillsGrid).BeginInit();
        generatedBillsFilterLayout.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(contentSplitContainer, 0, 2);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(0, 4, 0, 0);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(1100, 700);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(0, 4);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(143, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Meter Reading and Billing";
        // 
        // contentSplitContainer
        // 
        contentSplitContainer.Dock = DockStyle.Fill;
        contentSplitContainer.Location = new Point(0, 31);
        contentSplitContainer.Margin = new Padding(0, 12, 0, 0);
        contentSplitContainer.Name = "contentSplitContainer";
        contentSplitContainer.Orientation = Orientation.Horizontal;
        // 
        // contentSplitContainer.Panel1
        // 
        contentSplitContainer.Panel1.Controls.Add(leftCardPanel);
        contentSplitContainer.Panel1MinSize = 460;
        // 
        // contentSplitContainer.Panel2
        // 
        contentSplitContainer.Panel2.Controls.Add(rightCardPanel);
        contentSplitContainer.Size = new Size(1100, 669);
        contentSplitContainer.SplitterDistance = 476;
        contentSplitContainer.TabIndex = 2;
        // 
        // leftCardPanel
        // 
        leftCardPanel.Controls.Add(leftLayout);
        leftCardPanel.Dock = DockStyle.Fill;
        leftCardPanel.Location = new Point(0, 0);
        leftCardPanel.Margin = new Padding(0);
        leftCardPanel.Name = "leftCardPanel";
        leftCardPanel.Size = new Size(1100, 476);
        leftCardPanel.TabIndex = 0;
        // 
        // leftLayout
        // 
        leftLayout.ColumnCount = 1;
        leftLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        leftLayout.Controls.Add(meterReadingGrid, 0, 5);
        leftLayout.Controls.Add(leftStatusLabel, 0, 4);
        leftLayout.Controls.Add(meterEntryFiltersLayout, 0, 2);
        leftLayout.Controls.Add(createBillsTitleLabel, 0, 0);
        leftLayout.Dock = DockStyle.Fill;
        leftLayout.Location = new Point(0, 0);
        leftLayout.Margin = new Padding(0);
        leftLayout.Name = "leftLayout";
        leftLayout.RowCount = 6;
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        leftLayout.Size = new Size(1100, 476);
        leftLayout.TabIndex = 0;
        // 
        // meterReadingGrid
        // 
        meterReadingGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        meterReadingGrid.Dock = DockStyle.Fill;
        meterReadingGrid.Location = new Point(0, 101);
        meterReadingGrid.Margin = new Padding(0, 12, 0, 0);
        meterReadingGrid.Name = "meterReadingGrid";
        meterReadingGrid.Size = new Size(1100, 375);
        meterReadingGrid.TabIndex = 5;
        // 
        // leftStatusLabel
        // 
        leftStatusLabel.AutoSize = true;
        leftStatusLabel.Location = new Point(0, 74);
        leftStatusLabel.Margin = new Padding(0, 12, 0, 0);
        leftStatusLabel.Name = "leftStatusLabel";
        leftStatusLabel.Size = new Size(253, 15);
        leftStatusLabel.TabIndex = 4;
        leftStatusLabel.Text = "Select a zone and enter current readings to bill.";
        // 
        // meterEntryFiltersLayout
        // 
        meterEntryFiltersLayout.ColumnCount = 10;
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.ColumnStyles.Add(new ColumnStyle());
        meterEntryFiltersLayout.Controls.Add(printLatestReadingSheetButton, 9, 0);
        meterEntryFiltersLayout.Controls.Add(generateBillsButton, 8, 0);
        meterEntryFiltersLayout.Controls.Add(refreshMeterReadingsButton, 7, 0);
        meterEntryFiltersLayout.Controls.Add(dueDatePicker, 5, 0);
        meterEntryFiltersLayout.Controls.Add(readingDatePicker, 3, 0);
        meterEntryFiltersLayout.Controls.Add(zoneComboBox, 1, 0);
        meterEntryFiltersLayout.Controls.Add(dueDateLabel, 4, 0);
        meterEntryFiltersLayout.Controls.Add(readingDateLabel, 2, 0);
        meterEntryFiltersLayout.Controls.Add(zoneLabel, 0, 0);
        meterEntryFiltersLayout.Controls.Add(meterSearchTextBox, 6, 0);
        meterEntryFiltersLayout.Dock = DockStyle.Fill;
        meterEntryFiltersLayout.Location = new Point(0, 31);
        meterEntryFiltersLayout.Margin = new Padding(0, 16, 0, 0);
        meterEntryFiltersLayout.Name = "meterEntryFiltersLayout";
        meterEntryFiltersLayout.RowCount = 1;
        meterEntryFiltersLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        meterEntryFiltersLayout.Size = new Size(1100, 31);
        meterEntryFiltersLayout.TabIndex = 2;
        // 
        // printLatestReadingSheetButton
        // 
        printLatestReadingSheetButton.Location = new Point(968, 0);
        printLatestReadingSheetButton.Margin = new Padding(8, 0, 0, 0);
        printLatestReadingSheetButton.Name = "printLatestReadingSheetButton";
        printLatestReadingSheetButton.Size = new Size(132, 30);
        printLatestReadingSheetButton.TabIndex = 9;
        printLatestReadingSheetButton.Text = "Print Latest Sheet";
        printLatestReadingSheetButton.UseVisualStyleBackColor = true;
        printLatestReadingSheetButton.Click += printLatestReadingSheetButton_Click;
        // 
        // generateBillsButton
        // 
        generateBillsButton.Location = new Point(820, 0);
        generateBillsButton.Margin = new Padding(0, 0, 8, 0);
        generateBillsButton.Name = "generateBillsButton";
        generateBillsButton.Size = new Size(132, 30);
        generateBillsButton.TabIndex = 8;
        generateBillsButton.Text = "Generate Bills";
        generateBillsButton.UseVisualStyleBackColor = true;
        generateBillsButton.Click += generateBillsButton_Click;
        // 
        // refreshMeterReadingsButton
        // 
        refreshMeterReadingsButton.Location = new Point(692, 0);
        refreshMeterReadingsButton.Margin = new Padding(0, 0, 8, 0);
        refreshMeterReadingsButton.Name = "refreshMeterReadingsButton";
        refreshMeterReadingsButton.Size = new Size(120, 30);
        refreshMeterReadingsButton.TabIndex = 7;
        refreshMeterReadingsButton.Text = "Reload Readings";
        refreshMeterReadingsButton.UseVisualStyleBackColor = true;
        refreshMeterReadingsButton.Click += refreshMeterReadingsButton_Click;
        // 
        // dueDatePicker
        // 
        dueDatePicker.Dock = DockStyle.Top;
        dueDatePicker.Format = DateTimePickerFormat.Short;
        dueDatePicker.Location = new Point(476, 3);
        dueDatePicker.Margin = new Padding(6, 3, 16, 3);
        dueDatePicker.Name = "dueDatePicker";
        dueDatePicker.Size = new Size(120, 23);
        dueDatePicker.TabIndex = 5;
        dueDatePicker.ValueChanged += dueDatePicker_ValueChanged;
        // 
        // readingDatePicker
        // 
        readingDatePicker.Dock = DockStyle.Top;
        readingDatePicker.Format = DateTimePickerFormat.Short;
        readingDatePicker.Location = new Point(279, 3);
        readingDatePicker.Margin = new Padding(6, 3, 16, 3);
        readingDatePicker.Name = "readingDatePicker";
        readingDatePicker.Size = new Size(120, 23);
        readingDatePicker.TabIndex = 4;
        readingDatePicker.ValueChanged += readingDatePicker_ValueChanged;
        // 
        // zoneComboBox
        // 
        zoneComboBox.Dock = DockStyle.Top;
        zoneComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        zoneComboBox.FormattingEnabled = true;
        zoneComboBox.Location = new Point(40, 3);
        zoneComboBox.Margin = new Padding(6, 3, 16, 3);
        zoneComboBox.Name = "zoneComboBox";
        zoneComboBox.Size = new Size(140, 23);
        zoneComboBox.TabIndex = 3;
        zoneComboBox.SelectedIndexChanged += zoneComboBox_SelectedIndexChanged;
        // 
        // dueDateLabel
        // 
        dueDateLabel.AutoSize = true;
        dueDateLabel.Location = new Point(415, 0);
        dueDateLabel.Margin = new Padding(0);
        dueDateLabel.Name = "dueDateLabel";
        dueDateLabel.Size = new Size(55, 15);
        dueDateLabel.TabIndex = 2;
        dueDateLabel.Text = "Due Date";
        // 
        // readingDateLabel
        // 
        readingDateLabel.AutoSize = true;
        readingDateLabel.Location = new Point(196, 0);
        readingDateLabel.Margin = new Padding(0);
        readingDateLabel.Name = "readingDateLabel";
        readingDateLabel.Size = new Size(77, 15);
        readingDateLabel.TabIndex = 1;
        readingDateLabel.Text = "Reading Date";
        // 
        // zoneLabel
        // 
        zoneLabel.AutoSize = true;
        zoneLabel.Location = new Point(0, 0);
        zoneLabel.Margin = new Padding(0);
        zoneLabel.Name = "zoneLabel";
        zoneLabel.Size = new Size(34, 15);
        zoneLabel.TabIndex = 0;
        zoneLabel.Text = "Zone";
        // 
        // meterSearchTextBox
        // 
        meterSearchTextBox.Dock = DockStyle.Fill;
        meterSearchTextBox.Location = new Point(612, 3);
        meterSearchTextBox.Margin = new Padding(0, 3, 12, 3);
        meterSearchTextBox.Name = "meterSearchTextBox";
        meterSearchTextBox.PlaceholderText = "Search Account No or Name (press Enter)";
        meterSearchTextBox.Size = new Size(68, 23);
        meterSearchTextBox.TabIndex = 6;
        meterSearchTextBox.KeyDown += meterSearchTextBox_KeyDown;
        // 
        // createBillsTitleLabel
        // 
        createBillsTitleLabel.AutoSize = true;
        createBillsTitleLabel.Location = new Point(0, 0);
        createBillsTitleLabel.Margin = new Padding(0);
        createBillsTitleLabel.Name = "createBillsTitleLabel";
        createBillsTitleLabel.Size = new Size(65, 15);
        createBillsTitleLabel.TabIndex = 0;
        createBillsTitleLabel.Text = "Create Bills";
        // 
        // rightCardPanel
        // 
        rightCardPanel.Controls.Add(rightLayout);
        rightCardPanel.Dock = DockStyle.Fill;
        rightCardPanel.Location = new Point(0, 0);
        rightCardPanel.Margin = new Padding(0);
        rightCardPanel.Name = "rightCardPanel";
        rightCardPanel.Size = new Size(1100, 189);
        rightCardPanel.TabIndex = 0;
        // 
        // rightLayout
        // 
        rightLayout.ColumnCount = 1;
        rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rightLayout.Controls.Add(generatedBillsGrid, 0, 4);
        rightLayout.Controls.Add(rightStatusLabel, 0, 3);
        rightLayout.Controls.Add(generatedBillsFilterLayout, 0, 2);
        rightLayout.Controls.Add(generatedBillsTitleLabel, 0, 0);
        rightLayout.Dock = DockStyle.Fill;
        rightLayout.Location = new Point(0, 0);
        rightLayout.Margin = new Padding(0);
        rightLayout.Name = "rightLayout";
        rightLayout.RowCount = 5;
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rightLayout.Size = new Size(1100, 189);
        rightLayout.TabIndex = 0;
        // 
        // generatedBillsGrid
        // 
        generatedBillsGrid.AllowUserToOrderColumns = true;
        generatedBillsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        generatedBillsGrid.Dock = DockStyle.Fill;
        generatedBillsGrid.Location = new Point(0, 101);
        generatedBillsGrid.Margin = new Padding(0, 12, 0, 0);
        generatedBillsGrid.Name = "generatedBillsGrid";
        generatedBillsGrid.Size = new Size(1100, 88);
        generatedBillsGrid.TabIndex = 4;
        // 
        // rightStatusLabel
        // 
        rightStatusLabel.AutoSize = true;
        rightStatusLabel.Location = new Point(0, 74);
        rightStatusLabel.Margin = new Padding(0, 12, 0, 0);
        rightStatusLabel.Name = "rightStatusLabel";
        rightStatusLabel.Size = new Size(284, 15);
        rightStatusLabel.TabIndex = 3;
        rightStatusLabel.Text = "Generated bills for the selected date will appear here.";
        // 
        // generatedBillsFilterLayout
        // 
        generatedBillsFilterLayout.ColumnCount = 4;
        generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle());
        generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle());
        generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        generatedBillsFilterLayout.ColumnStyles.Add(new ColumnStyle());
        generatedBillsFilterLayout.Controls.Add(refreshGeneratedBillsButton, 3, 0);
        generatedBillsFilterLayout.Controls.Add(generatedBillsSearchTextBox, 2, 0);
        generatedBillsFilterLayout.Controls.Add(generatedBillsDatePicker, 1, 0);
        generatedBillsFilterLayout.Controls.Add(generatedBillsDateLabel, 0, 0);
        generatedBillsFilterLayout.Dock = DockStyle.Fill;
        generatedBillsFilterLayout.Location = new Point(0, 31);
        generatedBillsFilterLayout.Margin = new Padding(0, 16, 0, 0);
        generatedBillsFilterLayout.Name = "generatedBillsFilterLayout";
        generatedBillsFilterLayout.RowCount = 1;
        generatedBillsFilterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        generatedBillsFilterLayout.Size = new Size(1100, 31);
        generatedBillsFilterLayout.TabIndex = 2;
        // 
        // refreshGeneratedBillsButton
        // 
        refreshGeneratedBillsButton.Location = new Point(972, 0);
        refreshGeneratedBillsButton.Margin = new Padding(0);
        refreshGeneratedBillsButton.Name = "refreshGeneratedBillsButton";
        refreshGeneratedBillsButton.Size = new Size(128, 30);
        refreshGeneratedBillsButton.TabIndex = 2;
        refreshGeneratedBillsButton.Text = "Refresh Bills";
        refreshGeneratedBillsButton.UseVisualStyleBackColor = true;
        refreshGeneratedBillsButton.Click += refreshGeneratedBillsButton_Click;
        // 
        // generatedBillsSearchTextBox
        // 
        generatedBillsSearchTextBox.Dock = DockStyle.Fill;
        generatedBillsSearchTextBox.Location = new Point(205, 3);
        generatedBillsSearchTextBox.Margin = new Padding(12, 3, 12, 3);
        generatedBillsSearchTextBox.Name = "generatedBillsSearchTextBox";
        generatedBillsSearchTextBox.PlaceholderText = "Search Bill No, Account No, or Name (press Enter)";
        generatedBillsSearchTextBox.Size = new Size(755, 23);
        generatedBillsSearchTextBox.TabIndex = 3;
        generatedBillsSearchTextBox.KeyDown += generatedBillsSearchTextBox_KeyDown;
        // 
        // generatedBillsDatePicker
        // 
        generatedBillsDatePicker.Format = DateTimePickerFormat.Short;
        generatedBillsDatePicker.Location = new Point(70, 3);
        generatedBillsDatePicker.Name = "generatedBillsDatePicker";
        generatedBillsDatePicker.Size = new Size(120, 23);
        generatedBillsDatePicker.TabIndex = 1;
        generatedBillsDatePicker.ValueChanged += generatedBillsDatePicker_ValueChanged;
        // 
        // generatedBillsDateLabel
        // 
        generatedBillsDateLabel.Anchor = AnchorStyles.Left;
        generatedBillsDateLabel.AutoSize = true;
        generatedBillsDateLabel.Location = new Point(0, 8);
        generatedBillsDateLabel.Margin = new Padding(0);
        generatedBillsDateLabel.Name = "generatedBillsDateLabel";
        generatedBillsDateLabel.Size = new Size(67, 15);
        generatedBillsDateLabel.TabIndex = 0;
        generatedBillsDateLabel.Text = "Billing Date";
        // 
        // generatedBillsTitleLabel
        // 
        generatedBillsTitleLabel.AutoSize = true;
        generatedBillsTitleLabel.Location = new Point(0, 0);
        generatedBillsTitleLabel.Margin = new Padding(0);
        generatedBillsTitleLabel.Name = "generatedBillsTitleLabel";
        generatedBillsTitleLabel.Size = new Size(85, 15);
        generatedBillsTitleLabel.TabIndex = 0;
        generatedBillsTitleLabel.Text = "Generated Bills";
        // 
        // subtitleLabel
        // 
        subtitleLabel.AutoSize = true;
        subtitleLabel.Location = new Point(0, 27);
        subtitleLabel.Margin = new Padding(0, 8, 0, 0);
        subtitleLabel.MaximumSize = new Size(980, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(620, 15);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "Create bills in the top section, then review generated bills for any billing date in the bottom section.";
        // 
        // leftActionLayout
        // 
        leftActionLayout.ColumnCount = 3;
        leftActionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        leftActionLayout.ColumnStyles.Add(new ColumnStyle());
        leftActionLayout.ColumnStyles.Add(new ColumnStyle());
        leftActionLayout.Dock = DockStyle.Fill;
        leftActionLayout.Location = new Point(0, 122);
        leftActionLayout.Margin = new Padding(0, 16, 0, 0);
        leftActionLayout.Name = "leftActionLayout";
        leftActionLayout.RowCount = 1;
        leftActionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        leftActionLayout.Size = new Size(560, 30);
        leftActionLayout.TabIndex = 3;
        // 
        // createBillsSubtitleLabel
        // 
        createBillsSubtitleLabel.AutoSize = true;
        createBillsSubtitleLabel.Location = new Point(0, 21);
        createBillsSubtitleLabel.Margin = new Padding(0, 6, 0, 0);
        createBillsSubtitleLabel.MaximumSize = new Size(520, 0);
        createBillsSubtitleLabel.Name = "createBillsSubtitleLabel";
        createBillsSubtitleLabel.Size = new Size(507, 30);
        createBillsSubtitleLabel.TabIndex = 1;
        createBillsSubtitleLabel.Text = "Select a zone, review the concessionaire rows, enter present readings, and generate bills for the chosen reading date.";
        // 
        // generatedBillsSubtitleLabel
        // 
        generatedBillsSubtitleLabel.AutoSize = true;
        generatedBillsSubtitleLabel.Location = new Point(0, 21);
        generatedBillsSubtitleLabel.Margin = new Padding(0, 6, 0, 0);
        generatedBillsSubtitleLabel.MaximumSize = new Size(500, 0);
        generatedBillsSubtitleLabel.Name = "generatedBillsSubtitleLabel";
        generatedBillsSubtitleLabel.Size = new Size(490, 30);
        generatedBillsSubtitleLabel.TabIndex = 1;
        generatedBillsSubtitleLabel.Text = "Review bills already created from the selected billing date. The list is filtered by date so daily billing runs stay focused.";
        // 
        // MeterReadingUserControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(rootLayout);
        Name = "MeterReadingUserControl";
        Size = new Size(1100, 700);
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        contentSplitContainer.Panel1.ResumeLayout(false);
        contentSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)contentSplitContainer).EndInit();
        contentSplitContainer.ResumeLayout(false);
        leftCardPanel.ResumeLayout(false);
        leftLayout.ResumeLayout(false);
        leftLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)meterReadingGrid).EndInit();
        meterEntryFiltersLayout.ResumeLayout(false);
        meterEntryFiltersLayout.PerformLayout();
        rightCardPanel.ResumeLayout(false);
        rightLayout.ResumeLayout(false);
        rightLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)generatedBillsGrid).EndInit();
        generatedBillsFilterLayout.ResumeLayout(false);
        generatedBillsFilterLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
    private SplitContainer contentSplitContainer;
    private Panel leftCardPanel;
    private TableLayoutPanel leftLayout;
    private Label createBillsTitleLabel;
    private Label createBillsSubtitleLabel;
    private TableLayoutPanel meterEntryFiltersLayout;
    private Button printLatestReadingSheetButton;
    private Label zoneLabel;
    private ComboBox zoneComboBox;
    private TextBox meterSearchTextBox;
    private Label readingDateLabel;
    private DateTimePicker readingDatePicker;
    private Label dueDateLabel;
    private DateTimePicker dueDatePicker;
    private TableLayoutPanel leftActionLayout;
    private Button refreshMeterReadingsButton;
    private Button generateBillsButton;
    private Label leftStatusLabel;
    private DataGridView meterReadingGrid;
    private Panel rightCardPanel;
    private TableLayoutPanel rightLayout;
    private Label generatedBillsTitleLabel;
    private Label generatedBillsSubtitleLabel;
    private TableLayoutPanel generatedBillsFilterLayout;
    private Label generatedBillsDateLabel;
    private DateTimePicker generatedBillsDatePicker;
    private TextBox generatedBillsSearchTextBox;
    private Button refreshGeneratedBillsButton;
    private Label rightStatusLabel;
    private DataGridView generatedBillsGrid;
}
