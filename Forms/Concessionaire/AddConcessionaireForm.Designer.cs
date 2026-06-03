namespace WDBS_2026.Forms.Concessionaire;

partial class UpsertConcessionaireForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        rootLayout = new TableLayoutPanel();
        flowLayoutPanel1 = new FlowLayoutPanel();
        tableLayoutPanel1 = new TableLayoutPanel();
        tinLabel = new Label();
        accountNoLabel = new Label();
        accountNoTextBox = new TextBox();
        nameLabel = new Label();
        nameTextBox = new TextBox();
        addressLabel = new Label();
        tinTextBox = new TextBox();
        addressTextBox = new TextBox();
        tableLayoutPanel2 = new TableLayoutPanel();
        serviceLabel = new Label();
        serviceTypeCheckBox = new CheckBox();
        zoneComboBox = new ComboBox();
        serviceComboBox = new ComboBox();
        zoneLabel = new Label();
        zoneCheckBox = new CheckBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        meterNumberTextBox = new TextBox();
        firstReadingDatePicker = new DateTimePicker();
        meterLabel = new Label();
        firstReadingDateLabel = new Label();
        tableLayoutPanel4 = new TableLayoutPanel();
        scfTotalTextBox = new TextBox();
        scfMonthlyTextBox = new TextBox();
        scfTotalLabel = new Label();
        scfMonthlyLabel = new Label();
        tableLayoutPanel6 = new TableLayoutPanel();
        notBillableCheckBox = new CheckBox();
        taxExemptedCheckBox = new CheckBox();
        discountedCheckBox = new CheckBox();
        dueExemptedCheckBox = new CheckBox();
        tableLayoutPanel5 = new TableLayoutPanel();
        collectionStatusFieldLabel = new Label();
        statusComboBox = new ComboBox();
        statusLabel = new Label();
        actionsLayout = new TableLayoutPanel();
        cancelButton = new Button();
        saveButton = new Button();
        titleLabel = new Label();
        rootLayout.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel6.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        actionsLayout.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(flowLayoutPanel1, 0, 1);
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(24);
        rootLayout.RowCount = 2;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        rootLayout.Size = new Size(779, 683);
        rootLayout.TabIndex = 0;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(tableLayoutPanel1);
        flowLayoutPanel1.Controls.Add(tableLayoutPanel2);
        flowLayoutPanel1.Controls.Add(tableLayoutPanel3);
        flowLayoutPanel1.Controls.Add(tableLayoutPanel4);
        flowLayoutPanel1.Controls.Add(tableLayoutPanel6);
        flowLayoutPanel1.Controls.Add(tableLayoutPanel5);
        flowLayoutPanel1.Controls.Add(statusLabel);
        flowLayoutPanel1.Controls.Add(actionsLayout);
        flowLayoutPanel1.Dock = DockStyle.Fill;
        flowLayoutPanel1.Location = new Point(27, 42);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(725, 614);
        flowLayoutPanel1.TabIndex = 2;
        flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(tinLabel, 0, 2);
        tableLayoutPanel1.Controls.Add(accountNoLabel, 0, 0);
        tableLayoutPanel1.Controls.Add(accountNoTextBox, 1, 0);
        tableLayoutPanel1.Controls.Add(nameLabel, 0, 1);
        tableLayoutPanel1.Controls.Add(nameTextBox, 1, 1);
        tableLayoutPanel1.Controls.Add(addressLabel, 0, 3);
        tableLayoutPanel1.Controls.Add(tinTextBox, 1, 2);
        tableLayoutPanel1.Controls.Add(addressTextBox, 1, 3);
        tableLayoutPanel1.Dock = DockStyle.Top;
        tableLayoutPanel1.Location = new Point(3, 3);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 4;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.Size = new Size(690, 163);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // tinLabel
        // 
        tinLabel.AutoSize = true;
        tinLabel.Location = new Point(12, 94);
        tinLabel.Margin = new Padding(12, 14, 0, 0);
        tinLabel.Name = "tinLabel";
        tinLabel.Size = new Size(25, 15);
        tinLabel.TabIndex = 9;
        tinLabel.Text = "TIN";
        // 
        // accountNoLabel
        // 
        accountNoLabel.AutoSize = true;
        accountNoLabel.Location = new Point(12, 14);
        accountNoLabel.Margin = new Padding(12, 14, 0, 0);
        accountNoLabel.Name = "accountNoLabel";
        accountNoLabel.Size = new Size(74, 15);
        accountNoLabel.TabIndex = 5;
        accountNoLabel.Text = "Account No.";
        // 
        // accountNoTextBox
        // 
        accountNoTextBox.CharacterCasing = CharacterCasing.Upper;
        accountNoTextBox.Location = new Point(160, 10);
        accountNoTextBox.Margin = new Padding(10, 10, 12, 0);
        accountNoTextBox.Name = "accountNoTextBox";
        accountNoTextBox.Size = new Size(518, 23);
        accountNoTextBox.TabIndex = 3;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(12, 54);
        nameLabel.Margin = new Padding(12, 14, 0, 0);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(39, 15);
        nameLabel.TabIndex = 4;
        nameLabel.Text = "Name";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(160, 50);
        nameTextBox.Margin = new Padding(10, 10, 12, 0);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(518, 23);
        nameTextBox.TabIndex = 5;
        // 
        // addressLabel
        // 
        addressLabel.AutoSize = true;
        addressLabel.Location = new Point(12, 134);
        addressLabel.Margin = new Padding(12, 14, 0, 0);
        addressLabel.Name = "addressLabel";
        addressLabel.Size = new Size(49, 15);
        addressLabel.TabIndex = 8;
        addressLabel.Text = "Address";
        // 
        // tinTextBox
        // 
        tinTextBox.Location = new Point(160, 90);
        tinTextBox.Margin = new Padding(10, 10, 12, 0);
        tinTextBox.Name = "tinTextBox";
        tinTextBox.Size = new Size(518, 23);
        tinTextBox.TabIndex = 7;
        // 
        // addressTextBox
        // 
        addressTextBox.Location = new Point(160, 130);
        addressTextBox.Margin = new Padding(10, 10, 12, 0);
        addressTextBox.Name = "addressTextBox";
        addressTextBox.Size = new Size(518, 23);
        addressTextBox.TabIndex = 9;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 6;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 61F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Controls.Add(serviceLabel, 0, 0);
        tableLayoutPanel2.Controls.Add(serviceTypeCheckBox, 1, 0);
        tableLayoutPanel2.Controls.Add(zoneComboBox, 5, 0);
        tableLayoutPanel2.Controls.Add(serviceComboBox, 2, 0);
        tableLayoutPanel2.Controls.Add(zoneLabel, 3, 0);
        tableLayoutPanel2.Controls.Add(zoneCheckBox, 4, 0);
        tableLayoutPanel2.Dock = DockStyle.Top;
        tableLayoutPanel2.Location = new Point(3, 172);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel2.Size = new Size(690, 45);
        tableLayoutPanel2.TabIndex = 1;
        // 
        // serviceLabel
        // 
        serviceLabel.AutoSize = true;
        serviceLabel.Location = new Point(12, 14);
        serviceLabel.Margin = new Padding(12, 14, 0, 0);
        serviceLabel.Name = "serviceLabel";
        serviceLabel.Size = new Size(71, 15);
        serviceLabel.TabIndex = 12;
        serviceLabel.Text = "Service Type";
        // 
        // serviceTypeCheckBox
        // 
        serviceTypeCheckBox.AutoSize = true;
        serviceTypeCheckBox.Checked = true;
        serviceTypeCheckBox.CheckState = CheckState.Checked;
        serviceTypeCheckBox.Location = new Point(160, 15);
        serviceTypeCheckBox.Margin = new Padding(10, 15, 12, 0);
        serviceTypeCheckBox.Name = "serviceTypeCheckBox";
        serviceTypeCheckBox.Size = new Size(15, 14);
        serviceTypeCheckBox.TabIndex = 14;
        serviceTypeCheckBox.UseVisualStyleBackColor = true;
        // 
        // zoneComboBox
        // 
        zoneComboBox.Dock = DockStyle.Fill;
        zoneComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        zoneComboBox.FormattingEnabled = true;
        zoneComboBox.Location = new Point(502, 10);
        zoneComboBox.Margin = new Padding(10, 10, 12, 0);
        zoneComboBox.Name = "zoneComboBox";
        zoneComboBox.Size = new Size(176, 23);
        zoneComboBox.TabIndex = 11;
        // 
        // serviceComboBox
        // 
        serviceComboBox.Dock = DockStyle.Fill;
        serviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        serviceComboBox.FormattingEnabled = true;
        serviceComboBox.Location = new Point(200, 10);
        serviceComboBox.Margin = new Padding(10, 10, 12, 0);
        serviceComboBox.Name = "serviceComboBox";
        serviceComboBox.Size = new Size(175, 23);
        serviceComboBox.TabIndex = 13;
        // 
        // zoneLabel
        // 
        zoneLabel.AutoSize = true;
        zoneLabel.Location = new Point(399, 14);
        zoneLabel.Margin = new Padding(12, 14, 0, 0);
        zoneLabel.Name = "zoneLabel";
        zoneLabel.Size = new Size(34, 15);
        zoneLabel.TabIndex = 10;
        zoneLabel.Text = "Zone";
        // 
        // zoneCheckBox
        // 
        zoneCheckBox.AutoSize = true;
        zoneCheckBox.Checked = true;
        zoneCheckBox.CheckState = CheckState.Checked;
        zoneCheckBox.Location = new Point(458, 15);
        zoneCheckBox.Margin = new Padding(10, 15, 12, 0);
        zoneCheckBox.Name = "zoneCheckBox";
        zoneCheckBox.Size = new Size(15, 14);
        zoneCheckBox.TabIndex = 15;
        zoneCheckBox.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.ColumnCount = 4;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.77778F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.74074F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.4814816F));
        tableLayoutPanel3.Controls.Add(meterNumberTextBox, 1, 0);
        tableLayoutPanel3.Controls.Add(firstReadingDatePicker, 3, 0);
        tableLayoutPanel3.Controls.Add(meterLabel, 0, 0);
        tableLayoutPanel3.Controls.Add(firstReadingDateLabel, 2, 0);
        tableLayoutPanel3.Dock = DockStyle.Top;
        tableLayoutPanel3.Location = new Point(3, 223);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel3.Size = new Size(690, 45);
        tableLayoutPanel3.TabIndex = 2;
        // 
        // meterNumberTextBox
        // 
        meterNumberTextBox.Dock = DockStyle.Fill;
        meterNumberTextBox.Location = new Point(160, 10);
        meterNumberTextBox.Margin = new Padding(10, 10, 12, 0);
        meterNumberTextBox.Name = "meterNumberTextBox";
        meterNumberTextBox.Size = new Size(182, 23);
        meterNumberTextBox.TabIndex = 15;
        // 
        // firstReadingDatePicker
        // 
        firstReadingDatePicker.Checked = false;
        firstReadingDatePicker.Dock = DockStyle.Fill;
        firstReadingDatePicker.Format = DateTimePickerFormat.Short;
        firstReadingDatePicker.Location = new Point(530, 10);
        firstReadingDatePicker.Margin = new Padding(10, 10, 12, 0);
        firstReadingDatePicker.Name = "firstReadingDatePicker";
        firstReadingDatePicker.ShowCheckBox = true;
        firstReadingDatePicker.Size = new Size(148, 23);
        firstReadingDatePicker.TabIndex = 17;
        // 
        // meterLabel
        // 
        meterLabel.AutoSize = true;
        meterLabel.Location = new Point(12, 14);
        meterLabel.Margin = new Padding(12, 14, 0, 0);
        meterLabel.Name = "meterLabel";
        meterLabel.Size = new Size(85, 15);
        meterLabel.TabIndex = 14;
        meterLabel.Text = "Meter Number";
        // 
        // firstReadingDateLabel
        // 
        firstReadingDateLabel.AutoSize = true;
        firstReadingDateLabel.Location = new Point(366, 14);
        firstReadingDateLabel.Margin = new Padding(12, 14, 0, 0);
        firstReadingDateLabel.Name = "firstReadingDateLabel";
        firstReadingDateLabel.Size = new Size(102, 15);
        firstReadingDateLabel.TabIndex = 16;
        firstReadingDateLabel.Text = "First Reading Date";
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.ColumnCount = 4;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.11881F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.9405937F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.9405937F));
        tableLayoutPanel4.Controls.Add(scfTotalTextBox, 1, 0);
        tableLayoutPanel4.Controls.Add(scfMonthlyTextBox, 3, 0);
        tableLayoutPanel4.Controls.Add(scfTotalLabel, 0, 0);
        tableLayoutPanel4.Controls.Add(scfMonthlyLabel, 2, 0);
        tableLayoutPanel4.Dock = DockStyle.Top;
        tableLayoutPanel4.Location = new Point(3, 274);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel4.Size = new Size(690, 45);
        tableLayoutPanel4.TabIndex = 3;
        // 
        // scfTotalTextBox
        // 
        scfTotalTextBox.Dock = DockStyle.Fill;
        scfTotalTextBox.Location = new Point(160, 10);
        scfTotalTextBox.Margin = new Padding(10, 10, 12, 0);
        scfTotalTextBox.Name = "scfTotalTextBox";
        scfTotalTextBox.Size = new Size(183, 23);
        scfTotalTextBox.TabIndex = 19;
        // 
        // scfMonthlyTextBox
        // 
        scfMonthlyTextBox.Dock = DockStyle.Fill;
        scfMonthlyTextBox.Location = new Point(532, 10);
        scfMonthlyTextBox.Margin = new Padding(10, 10, 12, 0);
        scfMonthlyTextBox.Name = "scfMonthlyTextBox";
        scfMonthlyTextBox.Size = new Size(146, 23);
        scfMonthlyTextBox.TabIndex = 21;
        scfMonthlyTextBox.Text = "500";
        // 
        // scfTotalLabel
        // 
        scfTotalLabel.AutoSize = true;
        scfTotalLabel.Location = new Point(12, 14);
        scfTotalLabel.Margin = new Padding(12, 14, 0, 0);
        scfTotalLabel.Name = "scfTotalLabel";
        scfTotalLabel.Size = new Size(84, 15);
        scfTotalLabel.TabIndex = 18;
        scfTotalLabel.Text = "SCF Total Amt.";
        // 
        // scfMonthlyLabel
        // 
        scfMonthlyLabel.AutoSize = true;
        scfMonthlyLabel.Location = new Point(367, 14);
        scfMonthlyLabel.Margin = new Padding(12, 14, 0, 0);
        scfMonthlyLabel.Name = "scfMonthlyLabel";
        scfMonthlyLabel.Size = new Size(75, 15);
        scfMonthlyLabel.TabIndex = 20;
        scfMonthlyLabel.Text = "SCF Monthly";
        // 
        // tableLayoutPanel6
        // 
        tableLayoutPanel6.ColumnCount = 4;
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.73913F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.710144F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.347826F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.202898F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel6.Controls.Add(notBillableCheckBox, 3, 0);
        tableLayoutPanel6.Controls.Add(taxExemptedCheckBox, 0, 0);
        tableLayoutPanel6.Controls.Add(discountedCheckBox, 2, 0);
        tableLayoutPanel6.Controls.Add(dueExemptedCheckBox, 1, 0);
        tableLayoutPanel6.Dock = DockStyle.Top;
        tableLayoutPanel6.Location = new Point(3, 325);
        tableLayoutPanel6.Name = "tableLayoutPanel6";
        tableLayoutPanel6.RowCount = 1;
        tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
        tableLayoutPanel6.Size = new Size(690, 48);
        tableLayoutPanel6.TabIndex = 26;
        // 
        // notBillableCheckBox
        // 
        notBillableCheckBox.AutoSize = true;
        notBillableCheckBox.Location = new Point(533, 10);
        notBillableCheckBox.Margin = new Padding(10, 10, 12, 0);
        notBillableCheckBox.Name = "notBillableCheckBox";
        notBillableCheckBox.Size = new Size(87, 19);
        notBillableCheckBox.TabIndex = 3;
        notBillableCheckBox.Text = "Not Billable";
        notBillableCheckBox.UseVisualStyleBackColor = true;
        notBillableCheckBox.CheckedChanged += notBillableCheckBox_CheckedChanged;
        // 
        // taxExemptedCheckBox
        // 
        taxExemptedCheckBox.AutoSize = true;
        taxExemptedCheckBox.Location = new Point(10, 10);
        taxExemptedCheckBox.Margin = new Padding(10, 10, 12, 0);
        taxExemptedCheckBox.Name = "taxExemptedCheckBox";
        taxExemptedCheckBox.Size = new Size(99, 19);
        taxExemptedCheckBox.TabIndex = 0;
        taxExemptedCheckBox.Text = "Tax Exempted";
        taxExemptedCheckBox.UseVisualStyleBackColor = true;
        // 
        // discountedCheckBox
        // 
        discountedCheckBox.AutoSize = true;
        discountedCheckBox.Location = new Point(365, 10);
        discountedCheckBox.Margin = new Padding(10, 10, 12, 0);
        discountedCheckBox.Name = "discountedCheckBox";
        discountedCheckBox.Size = new Size(86, 19);
        discountedCheckBox.TabIndex = 2;
        discountedCheckBox.Text = "Discounted";
        discountedCheckBox.UseVisualStyleBackColor = true;
        discountedCheckBox.CheckedChanged += discountedCheckBox_CheckedChanged;
        // 
        // dueExemptedCheckBox
        // 
        dueExemptedCheckBox.AutoSize = true;
        dueExemptedCheckBox.Location = new Point(160, 10);
        dueExemptedCheckBox.Margin = new Padding(10, 10, 12, 0);
        dueExemptedCheckBox.Name = "dueExemptedCheckBox";
        dueExemptedCheckBox.Size = new Size(103, 19);
        dueExemptedCheckBox.TabIndex = 1;
        dueExemptedCheckBox.Text = "Due Exempted";
        dueExemptedCheckBox.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.ColumnCount = 2;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tableLayoutPanel5.Controls.Add(collectionStatusFieldLabel, 0, 0);
        tableLayoutPanel5.Controls.Add(statusComboBox, 1, 0);
        tableLayoutPanel5.Dock = DockStyle.Top;
        tableLayoutPanel5.Location = new Point(3, 379);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 2;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
        tableLayoutPanel5.Size = new Size(690, 63);
        tableLayoutPanel5.TabIndex = 29;
        // 
        // collectionStatusFieldLabel
        // 
        collectionStatusFieldLabel.AutoSize = true;
        collectionStatusFieldLabel.Location = new Point(12, 14);
        collectionStatusFieldLabel.Margin = new Padding(12, 14, 0, 0);
        collectionStatusFieldLabel.Name = "collectionStatusFieldLabel";
        collectionStatusFieldLabel.Size = new Size(39, 15);
        collectionStatusFieldLabel.TabIndex = 26;
        collectionStatusFieldLabel.Text = "Status";
        // 
        // statusComboBox
        // 
        statusComboBox.Dock = DockStyle.Left;
        statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        statusComboBox.FormattingEnabled = true;
        statusComboBox.Location = new Point(160, 10);
        statusComboBox.Margin = new Padding(10, 10, 12, 0);
        statusComboBox.Name = "statusComboBox";
        statusComboBox.Size = new Size(189, 23);
        statusComboBox.TabIndex = 25;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(12, 459);
        statusLabel.Margin = new Padding(12, 14, 0, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(39, 15);
        statusLabel.TabIndex = 30;
        statusLabel.Text = "Ready";
        // 
        // actionsLayout
        // 
        actionsLayout.ColumnCount = 3;
        actionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        actionsLayout.ColumnStyles.Add(new ColumnStyle());
        actionsLayout.ColumnStyles.Add(new ColumnStyle());
        actionsLayout.Controls.Add(cancelButton, 1, 0);
        actionsLayout.Controls.Add(saveButton, 2, 0);
        actionsLayout.Dock = DockStyle.Top;
        actionsLayout.Location = new Point(0, 490);
        actionsLayout.Margin = new Padding(0, 16, 0, 0);
        actionsLayout.Name = "actionsLayout";
        actionsLayout.RowCount = 1;
        actionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        actionsLayout.Size = new Size(690, 60);
        actionsLayout.TabIndex = 31;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(494, 0);
        cancelButton.Margin = new Padding(0, 0, 8, 0);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(92, 32);
        cancelButton.TabIndex = 0;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += cancelButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(594, 0);
        saveButton.Margin = new Padding(0);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(96, 32);
        saveButton.TabIndex = 1;
        saveButton.Text = "Save ";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(24, 24);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(124, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Upsert Concessionaire";
        // 
        // UpsertConcessionaireForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(779, 683);
        Controls.Add(rootLayout);
        MinimumSize = new Size(760, 720);
        Name = "UpsertConcessionaireForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Upsert Concessionaire";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        tableLayoutPanel6.ResumeLayout(false);
        tableLayoutPanel6.PerformLayout();
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel5.PerformLayout();
        actionsLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private TextBox accountNoTextBox;
    private Label nameLabel;
    private TextBox nameTextBox;
    private TextBox tinTextBox;
    private Label addressLabel;
    private TextBox addressTextBox;
    private Label zoneLabel;
    private Label serviceLabel;
    private ComboBox serviceComboBox;
    private Label meterLabel;
    private TextBox meterNumberTextBox;
    private Label firstReadingDateLabel;
    private DateTimePicker firstReadingDatePicker;
    private Label scfTotalLabel;
    private TextBox scfTotalTextBox;
    private TextBox scfMonthlyTextBox;
    private CheckBox taxExemptedCheckBox;
    private CheckBox dueExemptedCheckBox;
    private CheckBox discountedCheckBox;
    private CheckBox notBillableCheckBox;
    private FlowLayoutPanel flowLayoutPanel1;
    private Label accountNoLabel;
    private TableLayoutPanel tableLayoutPanel1;
    private Label tinLabel;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel4;
    private Label scfMonthlyLabel;
    private ComboBox zoneComboBox;
    private TableLayoutPanel tableLayoutPanel6;
    private TableLayoutPanel tableLayoutPanel5;
    private Label collectionStatusFieldLabel;
    private ComboBox statusComboBox;
    private Label statusLabel;
    private TableLayoutPanel actionsLayout;
    private Button cancelButton;
    private Button saveButton;
    private CheckBox serviceTypeCheckBox;
    private CheckBox zoneCheckBox;
}