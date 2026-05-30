namespace WDBS_2026.Components.Cashier;

partial class CashierUserControl
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
        subtitleLabel = new Label();
        mainSplitContainer = new SplitContainer();
        leftLayout = new TableLayoutPanel();
        concessionaireHeaderLabel = new Label();
        concessionaireFilterLayout = new TableLayoutPanel();
        searchConcessionaireTextBox = new TextBox();
        searchConcessionaireButton = new Button();
        refreshConcessionaireButton = new Button();
        concessionaireGrid = new DataGridView();
        collectionHeaderLabel = new Label();
        collectionFilterLayout = new TableLayoutPanel();
        collectionDateLabel = new Label();
        billingDatePicker = new DateTimePicker();
        collectionSearchTextBox = new TextBox();
        searchCollectionButton = new Button();
        refreshCollectionButton = new Button();
        collectionGrid = new DataGridView();
        rightLayout = new TableLayoutPanel();
        selectedHeaderLabel = new Label();
        selectedGrid = new DataGridView();
        paymentHeaderLabel = new Label();
        actionLayout = new TableLayoutPanel();
        previewBillButton = new Button();
        paymentForSCFButton = new Button();
        previewHeaderLabel = new Label();
        previewPanel = new Panel();
        tableLayoutPanel1 = new TableLayoutPanel();
        othersLabel = new Label();
        amountLabel = new Label();
        referenceLabel = new Label();
        payorLabel = new Label();
        paymentTypeLabel = new Label();
        ORTextBox = new TextBox();
        collectionDateTimePicker = new DateTimePicker();
        dateLabel = new Label();
        orLabel = new Label();
        payorNameTextbox = new TextBox();
        paymentReferenceTextBox = new TextBox();
        amountReceivedNUD = new NumericUpDown();
        remarksLabel = new Label();
        remarksTextBox = new TextBox();
        paymentForOthersNUD = new NumericUpDown();
        paymentTypeComboBox = new ComboBox();
        rootLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
        mainSplitContainer.Panel1.SuspendLayout();
        mainSplitContainer.Panel2.SuspendLayout();
        mainSplitContainer.SuspendLayout();
        leftLayout.SuspendLayout();
        concessionaireFilterLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireGrid).BeginInit();
        collectionFilterLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)collectionGrid).BeginInit();
        rightLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)selectedGrid).BeginInit();
        actionLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)amountReceivedNUD).BeginInit();
        ((System.ComponentModel.ISupportInitialize)paymentForOthersNUD).BeginInit();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(mainSplitContainer, 0, 3);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(16);
        rootLayout.RowCount = 4;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(1460, 900);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(16, 16);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(146, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Cashier Collection Posting";
        // 
        // subtitleLabel
        // 
        subtitleLabel.AutoSize = true;
        subtitleLabel.Location = new Point(16, 39);
        subtitleLabel.Margin = new Padding(0, 8, 0, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(479, 15);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "Double-click concessionaires to select, preview totals, and post normal or SCF collections.";
        // 
        // mainSplitContainer
        // 
        mainSplitContainer.Dock = DockStyle.Fill;
        mainSplitContainer.FixedPanel = FixedPanel.Panel2;
        mainSplitContainer.Location = new Point(16, 72);
        mainSplitContainer.Margin = new Padding(0, 18, 0, 0);
        mainSplitContainer.Name = "mainSplitContainer";
        // 
        // mainSplitContainer.Panel1
        // 
        mainSplitContainer.Panel1.Controls.Add(leftLayout);
        // 
        // mainSplitContainer.Panel2
        // 
        mainSplitContainer.Panel2.Controls.Add(rightLayout);
        mainSplitContainer.Size = new Size(1428, 812);
        mainSplitContainer.SplitterDistance = 968;
        mainSplitContainer.TabIndex = 3;
        mainSplitContainer.SplitterMoved += mainSplitContainer_SplitterMoved;
        // 
        // leftLayout
        // 
        leftLayout.ColumnCount = 1;
        leftLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        leftLayout.Controls.Add(concessionaireHeaderLabel, 0, 0);
        leftLayout.Controls.Add(concessionaireFilterLayout, 0, 1);
        leftLayout.Controls.Add(concessionaireGrid, 0, 2);
        leftLayout.Controls.Add(collectionHeaderLabel, 0, 3);
        leftLayout.Controls.Add(collectionFilterLayout, 0, 4);
        leftLayout.Controls.Add(collectionGrid, 0, 5);
        leftLayout.Dock = DockStyle.Fill;
        leftLayout.Location = new Point(0, 0);
        leftLayout.Name = "leftLayout";
        leftLayout.RowCount = 6;
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle());
        leftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
        leftLayout.Size = new Size(968, 812);
        leftLayout.TabIndex = 0;
        leftLayout.Paint += leftLayout_Paint;
        // 
        // concessionaireHeaderLabel
        // 
        concessionaireHeaderLabel.AutoSize = true;
        concessionaireHeaderLabel.Location = new Point(0, 0);
        concessionaireHeaderLabel.Margin = new Padding(0);
        concessionaireHeaderLabel.Name = "concessionaireHeaderLabel";
        concessionaireHeaderLabel.Size = new Size(130, 15);
        concessionaireHeaderLabel.TabIndex = 0;
        concessionaireHeaderLabel.Text = "Concessionaire Lookup";
        // 
        // concessionaireFilterLayout
        // 
        concessionaireFilterLayout.ColumnCount = 3;
        concessionaireFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        concessionaireFilterLayout.ColumnStyles.Add(new ColumnStyle());
        concessionaireFilterLayout.ColumnStyles.Add(new ColumnStyle());
        concessionaireFilterLayout.Controls.Add(searchConcessionaireTextBox, 0, 0);
        concessionaireFilterLayout.Controls.Add(searchConcessionaireButton, 1, 0);
        concessionaireFilterLayout.Controls.Add(refreshConcessionaireButton, 2, 0);
        concessionaireFilterLayout.Dock = DockStyle.Fill;
        concessionaireFilterLayout.Location = new Point(0, 23);
        concessionaireFilterLayout.Margin = new Padding(0, 8, 0, 0);
        concessionaireFilterLayout.Name = "concessionaireFilterLayout";
        concessionaireFilterLayout.RowCount = 1;
        concessionaireFilterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        concessionaireFilterLayout.Size = new Size(968, 34);
        concessionaireFilterLayout.TabIndex = 1;
        // 
        // searchConcessionaireTextBox
        // 
        searchConcessionaireTextBox.Dock = DockStyle.Fill;
        searchConcessionaireTextBox.Location = new Point(0, 5);
        searchConcessionaireTextBox.Margin = new Padding(0, 5, 8, 0);
        searchConcessionaireTextBox.Name = "searchConcessionaireTextBox";
        searchConcessionaireTextBox.PlaceholderText = "Search account no, concessionaire, meter, address...";
        searchConcessionaireTextBox.Size = new Size(686, 23);
        searchConcessionaireTextBox.TabIndex = 0;
        // 
        // searchConcessionaireButton
        // 
        searchConcessionaireButton.Location = new Point(694, 0);
        searchConcessionaireButton.Margin = new Padding(0, 0, 8, 0);
        searchConcessionaireButton.Name = "searchConcessionaireButton";
        searchConcessionaireButton.Size = new Size(130, 34);
        searchConcessionaireButton.TabIndex = 1;
        searchConcessionaireButton.Text = "Search";
        searchConcessionaireButton.UseVisualStyleBackColor = true;
        // 
        // refreshConcessionaireButton
        // 
        refreshConcessionaireButton.Location = new Point(832, 0);
        refreshConcessionaireButton.Margin = new Padding(0);
        refreshConcessionaireButton.Name = "refreshConcessionaireButton";
        refreshConcessionaireButton.Size = new Size(136, 34);
        refreshConcessionaireButton.TabIndex = 2;
        refreshConcessionaireButton.Text = "Refresh";
        refreshConcessionaireButton.UseVisualStyleBackColor = true;
        // 
        // concessionaireGrid
        // 
        concessionaireGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        concessionaireGrid.Dock = DockStyle.Fill;
        concessionaireGrid.Location = new Point(0, 67);
        concessionaireGrid.Margin = new Padding(0, 10, 0, 0);
        concessionaireGrid.Name = "concessionaireGrid";
        concessionaireGrid.Size = new Size(968, 366);
        concessionaireGrid.TabIndex = 2;
        // 
        // collectionHeaderLabel
        // 
        collectionHeaderLabel.AutoSize = true;
        collectionHeaderLabel.Location = new Point(0, 443);
        collectionHeaderLabel.Margin = new Padding(0, 10, 0, 0);
        collectionHeaderLabel.Name = "collectionHeaderLabel";
        collectionHeaderLabel.Size = new Size(99, 15);
        collectionHeaderLabel.TabIndex = 3;
        collectionHeaderLabel.Text = "Collection Queue";
        // 
        // collectionFilterLayout
        // 
        collectionFilterLayout.ColumnCount = 5;
        collectionFilterLayout.ColumnStyles.Add(new ColumnStyle());
        collectionFilterLayout.ColumnStyles.Add(new ColumnStyle());
        collectionFilterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        collectionFilterLayout.ColumnStyles.Add(new ColumnStyle());
        collectionFilterLayout.ColumnStyles.Add(new ColumnStyle());
        collectionFilterLayout.Controls.Add(collectionDateLabel, 0, 0);
        collectionFilterLayout.Controls.Add(billingDatePicker, 1, 0);
        collectionFilterLayout.Controls.Add(collectionSearchTextBox, 2, 0);
        collectionFilterLayout.Controls.Add(searchCollectionButton, 3, 0);
        collectionFilterLayout.Controls.Add(refreshCollectionButton, 4, 0);
        collectionFilterLayout.Dock = DockStyle.Fill;
        collectionFilterLayout.Location = new Point(0, 470);
        collectionFilterLayout.Margin = new Padding(0, 12, 0, 0);
        collectionFilterLayout.Name = "collectionFilterLayout";
        collectionFilterLayout.RowCount = 1;
        collectionFilterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        collectionFilterLayout.Size = new Size(968, 34);
        collectionFilterLayout.TabIndex = 4;
        // 
        // collectionDateLabel
        // 
        collectionDateLabel.Anchor = AnchorStyles.Left;
        collectionDateLabel.AutoSize = true;
        collectionDateLabel.Location = new Point(0, 9);
        collectionDateLabel.Margin = new Padding(0);
        collectionDateLabel.Name = "collectionDateLabel";
        collectionDateLabel.Size = new Size(88, 15);
        collectionDateLabel.TabIndex = 0;
        collectionDateLabel.Text = "Collection Date";
        // 
        // billingDatePicker
        // 
        billingDatePicker.Format = DateTimePickerFormat.Short;
        billingDatePicker.Location = new Point(94, 3);
        billingDatePicker.Margin = new Padding(6, 3, 12, 3);
        billingDatePicker.Name = "billingDatePicker";
        billingDatePicker.Size = new Size(120, 23);
        billingDatePicker.TabIndex = 1;
        // 
        // collectionSearchTextBox
        // 
        collectionSearchTextBox.Dock = DockStyle.Fill;
        collectionSearchTextBox.Location = new Point(226, 3);
        collectionSearchTextBox.Margin = new Padding(0, 3, 12, 3);
        collectionSearchTextBox.Name = "collectionSearchTextBox";
        collectionSearchTextBox.PlaceholderText = "Search invoice, account, or concessionaire (press Enter)...";
        collectionSearchTextBox.Size = new Size(594, 23);
        collectionSearchTextBox.TabIndex = 2;
        // 
        // searchCollectionButton
        // 
        searchCollectionButton.Location = new Point(832, 0);
        searchCollectionButton.Margin = new Padding(0, 0, 8, 0);
        searchCollectionButton.Name = "searchCollectionButton";
        searchCollectionButton.Size = new Size(64, 34);
        searchCollectionButton.TabIndex = 3;
        searchCollectionButton.Text = "Search";
        searchCollectionButton.UseVisualStyleBackColor = true;
        // 
        // refreshCollectionButton
        // 
        refreshCollectionButton.Location = new Point(904, 0);
        refreshCollectionButton.Margin = new Padding(0);
        refreshCollectionButton.Name = "refreshCollectionButton";
        refreshCollectionButton.Size = new Size(64, 34);
        refreshCollectionButton.TabIndex = 4;
        refreshCollectionButton.Text = "Refresh";
        refreshCollectionButton.UseVisualStyleBackColor = true;
        // 
        // collectionGrid
        // 
        collectionGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        collectionGrid.Dock = DockStyle.Fill;
        collectionGrid.Location = new Point(0, 514);
        collectionGrid.Margin = new Padding(0, 10, 0, 0);
        collectionGrid.Name = "collectionGrid";
        collectionGrid.Size = new Size(968, 298);
        collectionGrid.TabIndex = 5;
        // 
        // rightLayout
        // 
        rightLayout.ColumnCount = 1;
        rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rightLayout.Controls.Add(selectedHeaderLabel, 0, 0);
        rightLayout.Controls.Add(selectedGrid, 0, 1);
        rightLayout.Controls.Add(paymentHeaderLabel, 0, 2);
        rightLayout.Controls.Add(actionLayout, 0, 4);
        rightLayout.Controls.Add(previewHeaderLabel, 0, 5);
        rightLayout.Controls.Add(previewPanel, 0, 6);
        rightLayout.Controls.Add(tableLayoutPanel1, 0, 3);
        rightLayout.Dock = DockStyle.Fill;
        rightLayout.Location = new Point(0, 0);
        rightLayout.Name = "rightLayout";
        rightLayout.RowCount = 7;
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 57.0441971F));
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42.9558029F));
        rightLayout.RowStyles.Add(new RowStyle());
        rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
        rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
        rightLayout.Size = new Size(456, 812);
        rightLayout.TabIndex = 0;
        // 
        // selectedHeaderLabel
        // 
        selectedHeaderLabel.AutoSize = true;
        selectedHeaderLabel.Location = new Point(0, 0);
        selectedHeaderLabel.Margin = new Padding(0);
        selectedHeaderLabel.Name = "selectedHeaderLabel";
        selectedHeaderLabel.Size = new Size(139, 15);
        selectedHeaderLabel.TabIndex = 0;
        selectedHeaderLabel.Text = "Selected Concessionaires";
        // 
        // selectedGrid
        // 
        selectedGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        selectedGrid.Dock = DockStyle.Fill;
        selectedGrid.Location = new Point(0, 25);
        selectedGrid.Margin = new Padding(0, 10, 0, 0);
        selectedGrid.Name = "selectedGrid";
        selectedGrid.Size = new Size(456, 403);
        selectedGrid.TabIndex = 1;
        // 
        // paymentHeaderLabel
        // 
        paymentHeaderLabel.AutoSize = true;
        paymentHeaderLabel.Location = new Point(0, 438);
        paymentHeaderLabel.Margin = new Padding(0, 10, 0, 0);
        paymentHeaderLabel.Name = "paymentHeaderLabel";
        paymentHeaderLabel.Size = new Size(84, 15);
        paymentHeaderLabel.TabIndex = 2;
        paymentHeaderLabel.Text = "Payment Entry";
        // 
        // actionLayout
        // 
        actionLayout.ColumnCount = 2;
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        actionLayout.Controls.Add(previewBillButton, 1, 0);
        actionLayout.Controls.Add(paymentForSCFButton, 0, 0);
        actionLayout.Dock = DockStyle.Fill;
        actionLayout.Location = new Point(0, 774);
        actionLayout.Margin = new Padding(0, 10, 0, 0);
        actionLayout.Name = "actionLayout";
        actionLayout.RowCount = 1;
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        actionLayout.Size = new Size(456, 37);
        actionLayout.TabIndex = 4;
        // 
        // previewBillButton
        // 
        previewBillButton.Dock = DockStyle.Fill;
        previewBillButton.Location = new Point(228, 0);
        previewBillButton.Margin = new Padding(0, 0, 4, 0);
        previewBillButton.Name = "previewBillButton";
        previewBillButton.Size = new Size(224, 37);
        previewBillButton.TabIndex = 0;
        previewBillButton.Text = "Post Bill";
        previewBillButton.UseVisualStyleBackColor = true;
        // 
        // paymentForSCFButton
        // 
        paymentForSCFButton.Dock = DockStyle.Fill;
        paymentForSCFButton.Location = new Point(4, 0);
        paymentForSCFButton.Margin = new Padding(4, 0, 0, 0);
        paymentForSCFButton.Name = "paymentForSCFButton";
        paymentForSCFButton.Size = new Size(224, 37);
        paymentForSCFButton.TabIndex = 2;
        paymentForSCFButton.Text = "Post SCF/Others";
        paymentForSCFButton.UseVisualStyleBackColor = true;
        // 
        // previewHeaderLabel
        // 
        previewHeaderLabel.AutoSize = true;
        previewHeaderLabel.Location = new Point(0, 821);
        previewHeaderLabel.Margin = new Padding(0, 10, 0, 0);
        previewHeaderLabel.Name = "previewHeaderLabel";
        previewHeaderLabel.Size = new Size(67, 1);
        previewHeaderLabel.TabIndex = 5;
        previewHeaderLabel.Text = "OR Preview";
        previewHeaderLabel.Visible = false;
        // 
        // previewPanel
        // 
        previewPanel.BorderStyle = BorderStyle.FixedSingle;
        previewPanel.Dock = DockStyle.Fill;
        previewPanel.Location = new Point(0, 821);
        previewPanel.Margin = new Padding(0, 10, 0, 0);
        previewPanel.Name = "previewPanel";
        previewPanel.Size = new Size(456, 1);
        previewPanel.TabIndex = 6;
        previewPanel.Visible = false;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(othersLabel, 0, 6);
        tableLayoutPanel1.Controls.Add(amountLabel, 0, 5);
        tableLayoutPanel1.Controls.Add(referenceLabel, 0, 4);
        tableLayoutPanel1.Controls.Add(payorLabel, 0, 2);
        tableLayoutPanel1.Controls.Add(paymentTypeLabel, 0, 3);
        tableLayoutPanel1.Controls.Add(ORTextBox, 1, 0);
        tableLayoutPanel1.Controls.Add(collectionDateTimePicker, 1, 1);
        tableLayoutPanel1.Controls.Add(dateLabel, 0, 1);
        tableLayoutPanel1.Controls.Add(orLabel, 0, 0);
        tableLayoutPanel1.Controls.Add(payorNameTextbox, 1, 2);
        tableLayoutPanel1.Controls.Add(paymentReferenceTextBox, 1, 4);
        tableLayoutPanel1.Controls.Add(amountReceivedNUD, 1, 5);
        tableLayoutPanel1.Controls.Add(remarksLabel, 0, 7);
        tableLayoutPanel1.Controls.Add(remarksTextBox, 1, 7);
        tableLayoutPanel1.Controls.Add(paymentForOthersNUD, 1, 6);
        tableLayoutPanel1.Controls.Add(paymentTypeComboBox, 1, 3);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(3, 456);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 8;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(450, 305);
        tableLayoutPanel1.TabIndex = 7;
        // 
        // othersLabel
        // 
        othersLabel.AutoSize = true;
        othersLabel.Dock = DockStyle.Fill;
        othersLabel.Location = new Point(0, 188);
        othersLabel.Margin = new Padding(0, 8, 8, 0);
        othersLabel.Name = "othersLabel";
        othersLabel.Size = new Size(142, 22);
        othersLabel.TabIndex = 13;
        othersLabel.Text = "Others Collected";
        // 
        // amountLabel
        // 
        amountLabel.AutoSize = true;
        amountLabel.Dock = DockStyle.Fill;
        amountLabel.Location = new Point(0, 158);
        amountLabel.Margin = new Padding(0, 8, 8, 0);
        amountLabel.Name = "amountLabel";
        amountLabel.Size = new Size(142, 22);
        amountLabel.TabIndex = 11;
        amountLabel.Text = "Amount Received";
        // 
        // referenceLabel
        // 
        referenceLabel.AutoSize = true;
        referenceLabel.Dock = DockStyle.Fill;
        referenceLabel.Location = new Point(0, 128);
        referenceLabel.Margin = new Padding(0, 8, 8, 0);
        referenceLabel.Name = "referenceLabel";
        referenceLabel.Size = new Size(142, 22);
        referenceLabel.TabIndex = 9;
        referenceLabel.Text = "Reference Number";
        // 
        // payorLabel
        // 
        payorLabel.AutoSize = true;
        payorLabel.Dock = DockStyle.Fill;
        payorLabel.Location = new Point(0, 68);
        payorLabel.Margin = new Padding(0, 8, 8, 0);
        payorLabel.Name = "payorLabel";
        payorLabel.Size = new Size(142, 22);
        payorLabel.TabIndex = 7;
        payorLabel.Text = "Payor Name";
        // 
        // paymentTypeLabel
        // 
        paymentTypeLabel.AutoSize = true;
        paymentTypeLabel.Dock = DockStyle.Fill;
        paymentTypeLabel.Location = new Point(0, 98);
        paymentTypeLabel.Margin = new Padding(0, 8, 8, 0);
        paymentTypeLabel.Name = "paymentTypeLabel";
        paymentTypeLabel.Size = new Size(142, 22);
        paymentTypeLabel.TabIndex = 5;
        paymentTypeLabel.Text = "Payment Type";
        // 
        // ORTextBox
        // 
        ORTextBox.Dock = DockStyle.Fill;
        ORTextBox.Location = new Point(158, 0);
        ORTextBox.Margin = new Padding(8, 0, 8, 0);
        ORTextBox.Name = "ORTextBox";
        ORTextBox.Size = new Size(284, 23);
        ORTextBox.TabIndex = 1;
        // 
        // collectionDateTimePicker
        // 
        collectionDateTimePicker.Dock = DockStyle.Fill;
        collectionDateTimePicker.Format = DateTimePickerFormat.Short;
        collectionDateTimePicker.Location = new Point(158, 38);
        collectionDateTimePicker.Margin = new Padding(8, 8, 0, 0);
        collectionDateTimePicker.Name = "collectionDateTimePicker";
        collectionDateTimePicker.Size = new Size(292, 23);
        collectionDateTimePicker.TabIndex = 4;
        // 
        // dateLabel
        // 
        dateLabel.AutoSize = true;
        dateLabel.Dock = DockStyle.Fill;
        dateLabel.Location = new Point(0, 38);
        dateLabel.Margin = new Padding(0, 8, 8, 0);
        dateLabel.Name = "dateLabel";
        dateLabel.Size = new Size(142, 22);
        dateLabel.TabIndex = 3;
        dateLabel.Text = "Payment Date";
        // 
        // orLabel
        // 
        orLabel.AutoSize = true;
        orLabel.Dock = DockStyle.Fill;
        orLabel.Location = new Point(0, 0);
        orLabel.Margin = new Padding(0, 0, 8, 0);
        orLabel.Name = "orLabel";
        orLabel.Size = new Size(142, 30);
        orLabel.TabIndex = 0;
        orLabel.Text = "OR # ";
        // 
        // payorNameTextbox
        // 
        payorNameTextbox.Dock = DockStyle.Fill;
        payorNameTextbox.Location = new Point(158, 68);
        payorNameTextbox.Margin = new Padding(8, 8, 0, 0);
        payorNameTextbox.Name = "payorNameTextbox";
        payorNameTextbox.Size = new Size(292, 23);
        payorNameTextbox.TabIndex = 8;
        // 
        // paymentReferenceTextBox
        // 
        paymentReferenceTextBox.Dock = DockStyle.Fill;
        paymentReferenceTextBox.Location = new Point(158, 128);
        paymentReferenceTextBox.Margin = new Padding(8, 8, 0, 0);
        paymentReferenceTextBox.Name = "paymentReferenceTextBox";
        paymentReferenceTextBox.Size = new Size(292, 23);
        paymentReferenceTextBox.TabIndex = 10;
        // 
        // amountReceivedNUD
        // 
        amountReceivedNUD.DecimalPlaces = 2;
        amountReceivedNUD.Dock = DockStyle.Fill;
        amountReceivedNUD.Location = new Point(158, 158);
        amountReceivedNUD.Margin = new Padding(8, 8, 0, 0);
        amountReceivedNUD.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        amountReceivedNUD.Name = "amountReceivedNUD";
        amountReceivedNUD.Size = new Size(292, 23);
        amountReceivedNUD.TabIndex = 12;
        amountReceivedNUD.ThousandsSeparator = true;
        // 
        // remarksLabel
        // 
        remarksLabel.AutoSize = true;
        remarksLabel.Dock = DockStyle.Fill;
        remarksLabel.Location = new Point(0, 218);
        remarksLabel.Margin = new Padding(0, 8, 8, 0);
        remarksLabel.Name = "remarksLabel";
        remarksLabel.Size = new Size(142, 87);
        remarksLabel.TabIndex = 15;
        remarksLabel.Text = "Remarks";
        // 
        // remarksTextBox
        // 
        remarksTextBox.Dock = DockStyle.Fill;
        remarksTextBox.Location = new Point(158, 218);
        remarksTextBox.Margin = new Padding(8, 8, 0, 0);
        remarksTextBox.Multiline = true;
        remarksTextBox.Name = "remarksTextBox";
        remarksTextBox.Size = new Size(292, 87);
        remarksTextBox.TabIndex = 16;
        // 
        // paymentForOthersNUD
        // 
        paymentForOthersNUD.DecimalPlaces = 2;
        paymentForOthersNUD.Dock = DockStyle.Fill;
        paymentForOthersNUD.Location = new Point(158, 188);
        paymentForOthersNUD.Margin = new Padding(8, 8, 0, 0);
        paymentForOthersNUD.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        paymentForOthersNUD.Name = "paymentForOthersNUD";
        paymentForOthersNUD.Size = new Size(292, 23);
        paymentForOthersNUD.TabIndex = 14;
        paymentForOthersNUD.ThousandsSeparator = true;
        // 
        // paymentTypeComboBox
        // 
        paymentTypeComboBox.Dock = DockStyle.Fill;
        paymentTypeComboBox.FormattingEnabled = true;
        paymentTypeComboBox.Location = new Point(158, 98);
        paymentTypeComboBox.Margin = new Padding(8, 8, 0, 0);
        paymentTypeComboBox.Name = "paymentTypeComboBox";
        paymentTypeComboBox.Size = new Size(292, 23);
        paymentTypeComboBox.TabIndex = 6;
        // 
        // CashierUserControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(rootLayout);
        Name = "CashierUserControl";
        Size = new Size(1460, 900);
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        mainSplitContainer.Panel1.ResumeLayout(false);
        mainSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
        mainSplitContainer.ResumeLayout(false);
        leftLayout.ResumeLayout(false);
        leftLayout.PerformLayout();
        concessionaireFilterLayout.ResumeLayout(false);
        concessionaireFilterLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireGrid).EndInit();
        collectionFilterLayout.ResumeLayout(false);
        collectionFilterLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)collectionGrid).EndInit();
        rightLayout.ResumeLayout(false);
        rightLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)selectedGrid).EndInit();
        actionLayout.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)amountReceivedNUD).EndInit();
        ((System.ComponentModel.ISupportInitialize)paymentForOthersNUD).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
    private SplitContainer mainSplitContainer;
    private TableLayoutPanel leftLayout;
    private Label concessionaireHeaderLabel;
    private TableLayoutPanel concessionaireFilterLayout;
    private TextBox searchConcessionaireTextBox;
    private Button searchConcessionaireButton;
    private Button refreshConcessionaireButton;
    private DataGridView concessionaireGrid;
    private Label collectionHeaderLabel;
    private TableLayoutPanel collectionFilterLayout;
    private Label collectionDateLabel;
    private DateTimePicker billingDatePicker;
    private TextBox collectionSearchTextBox;
    private Button searchCollectionButton;
    private Button refreshCollectionButton;
    private DataGridView collectionGrid;
    private TableLayoutPanel rightLayout;
    private Label selectedHeaderLabel;
    private DataGridView selectedGrid;
    private Label previewHeaderLabel;
    private Panel previewPanel;
    private Label paymentHeaderLabel;
    private Label orLabel;
    private TextBox ORTextBox;
    private Label dateLabel;
    private DateTimePicker collectionDateTimePicker;
    private Label paymentTypeLabel;
    private ComboBox paymentTypeComboBox;
    private Label payorLabel;
    private TextBox payorNameTextbox;
    private Label referenceLabel;
    private TextBox paymentReferenceTextBox;
    private Label amountLabel;
    private NumericUpDown amountReceivedNUD;
    private Label othersLabel;
    private NumericUpDown paymentForOthersNUD;
    private Label remarksLabel;
    private TextBox remarksTextBox;
    private TableLayoutPanel actionLayout;
    private Button previewBillButton;
    private Button paymentForSCFButton;
    private TableLayoutPanel tableLayoutPanel1;
}
