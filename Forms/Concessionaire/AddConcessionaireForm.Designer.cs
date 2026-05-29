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
        fieldsLayout = new TableLayoutPanel();
        statusLabel = new Label();
        buttonPanel = new FlowLayoutPanel();
        cancelButton = new Button();
        saveButton = new Button();
        optionsPanel = new FlowLayoutPanel();
        taxExemptedCheckBox = new CheckBox();
        dueExemptedCheckBox = new CheckBox();
        discountedCheckBox = new CheckBox();
        scfMonthlyTextBox = new TextBox();
        scfMonthlyLabel = new Label();
        scfTotalTextBox = new TextBox();
        scfTotalLabel = new Label();
        firstReadingDatePicker = new DateTimePicker();
        firstReadingDateLabel = new Label();
        meterNumberTextBox = new TextBox();
        meterLabel = new Label();
        serviceComboBox = new ComboBox();
        serviceLabel = new Label();
        zoneComboBox = new ComboBox();
        zoneLabel = new Label();
        addressTextBox = new TextBox();
        addressLabel = new Label();
        tinTextBox = new TextBox();
        tinLabel = new Label();
        nameTextBox = new TextBox();
        nameLabel = new Label();
        accountNoTextBox = new TextBox();
        accountNoLabel = new Label();
        titleLabel = new Label();
        rootLayout.SuspendLayout();
        fieldsLayout.SuspendLayout();
        buttonPanel.SuspendLayout();
        optionsPanel.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(fieldsLayout, 0, 1);
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(24);
        rootLayout.RowCount = 2;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(760, 720);
        rootLayout.TabIndex = 0;
        // 
        // fieldsLayout
        // 
        fieldsLayout.ColumnCount = 2;
        fieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        fieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        fieldsLayout.Controls.Add(statusLabel, 0, 11);
        fieldsLayout.Controls.Add(buttonPanel, 0, 12);
        fieldsLayout.Controls.Add(optionsPanel, 0, 10);
        fieldsLayout.Controls.Add(scfMonthlyTextBox, 1, 9);
        fieldsLayout.Controls.Add(scfMonthlyLabel, 1, 8);
        fieldsLayout.Controls.Add(scfTotalTextBox, 0, 9);
        fieldsLayout.Controls.Add(scfTotalLabel, 0, 8);
        fieldsLayout.Controls.Add(firstReadingDatePicker, 1, 7);
        fieldsLayout.Controls.Add(firstReadingDateLabel, 1, 6);
        fieldsLayout.Controls.Add(meterNumberTextBox, 0, 7);
        fieldsLayout.Controls.Add(meterLabel, 0, 6);
        fieldsLayout.Controls.Add(serviceComboBox, 1, 5);
        fieldsLayout.Controls.Add(serviceLabel, 1, 4);
        fieldsLayout.Controls.Add(zoneComboBox, 0, 5);
        fieldsLayout.Controls.Add(zoneLabel, 0, 4);
        fieldsLayout.Controls.Add(addressTextBox, 1, 3);
        fieldsLayout.Controls.Add(addressLabel, 1, 2);
        fieldsLayout.Controls.Add(tinTextBox, 0, 3);
        fieldsLayout.Controls.Add(tinLabel, 0, 2);
        fieldsLayout.Controls.Add(nameTextBox, 1, 1);
        fieldsLayout.Controls.Add(nameLabel, 1, 0);
        fieldsLayout.Controls.Add(accountNoTextBox, 0, 1);
        fieldsLayout.Controls.Add(accountNoLabel, 0, 0);
        fieldsLayout.Dock = DockStyle.Fill;
        fieldsLayout.Location = new Point(24, 55);
        fieldsLayout.Margin = new Padding(0);
        fieldsLayout.Name = "fieldsLayout";
        fieldsLayout.RowCount = 13;
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle());
        fieldsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        fieldsLayout.Size = new Size(712, 641);
        fieldsLayout.TabIndex = 1;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        fieldsLayout.SetColumnSpan(statusLabel, 2);
        statusLabel.Location = new Point(0, 385);
        statusLabel.Margin = new Padding(0, 14, 0, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(39, 15);
        statusLabel.TabIndex = 24;
        statusLabel.Text = "Ready";
        // 
        // buttonPanel
        // 
        buttonPanel.AutoSize = true;
        buttonPanel.Controls.Add(cancelButton);
        buttonPanel.Controls.Add(saveButton);
        fieldsLayout.SetColumnSpan(buttonPanel, 2);
        buttonPanel.FlowDirection = FlowDirection.RightToLeft;
        buttonPanel.Location = new Point(0, 414);
        buttonPanel.Margin = new Padding(0, 14, 0, 0);
        buttonPanel.Name = "buttonPanel";
        buttonPanel.Size = new Size(176, 32);
        buttonPanel.TabIndex = 23;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(88, 0);
        cancelButton.Margin = new Padding(0);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(88, 32);
        cancelButton.TabIndex = 1;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += cancelButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(0, 0);
        saveButton.Margin = new Padding(0, 0, 8, 0);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(80, 32);
        saveButton.TabIndex = 0;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // optionsPanel
        // 
        optionsPanel.AutoSize = true;
        optionsPanel.Controls.Add(taxExemptedCheckBox);
        optionsPanel.Controls.Add(dueExemptedCheckBox);
        optionsPanel.Controls.Add(discountedCheckBox);
        fieldsLayout.SetColumnSpan(optionsPanel, 2);
        optionsPanel.Location = new Point(0, 352);
        optionsPanel.Margin = new Padding(0, 14, 0, 0);
        optionsPanel.Name = "optionsPanel";
        optionsPanel.Size = new Size(291, 19);
        optionsPanel.TabIndex = 22;
        // 
        // taxExemptedCheckBox
        // 
        taxExemptedCheckBox.AutoSize = true;
        taxExemptedCheckBox.Location = new Point(0, 0);
        taxExemptedCheckBox.Margin = new Padding(0, 0, 16, 0);
        taxExemptedCheckBox.Name = "taxExemptedCheckBox";
        taxExemptedCheckBox.Size = new Size(100, 19);
        taxExemptedCheckBox.TabIndex = 0;
        taxExemptedCheckBox.Text = "Tax Exempted";
        taxExemptedCheckBox.UseVisualStyleBackColor = true;
        // 
        // dueExemptedCheckBox
        // 
        dueExemptedCheckBox.AutoSize = true;
        dueExemptedCheckBox.Location = new Point(116, 0);
        dueExemptedCheckBox.Margin = new Padding(0, 0, 16, 0);
        dueExemptedCheckBox.Name = "dueExemptedCheckBox";
        dueExemptedCheckBox.Size = new Size(102, 19);
        dueExemptedCheckBox.TabIndex = 1;
        dueExemptedCheckBox.Text = "Due Exempted";
        dueExemptedCheckBox.UseVisualStyleBackColor = true;
        // 
        // discountedCheckBox
        // 
        discountedCheckBox.AutoSize = true;
        discountedCheckBox.Location = new Point(234, 0);
        discountedCheckBox.Margin = new Padding(0);
        discountedCheckBox.Name = "discountedCheckBox";
        discountedCheckBox.Size = new Size(57, 19);
        discountedCheckBox.TabIndex = 2;
        discountedCheckBox.Text = "Disc.";
        discountedCheckBox.UseVisualStyleBackColor = true;
        // 
        // scfMonthlyTextBox
        // 
        scfMonthlyTextBox.Dock = DockStyle.Top;
        scfMonthlyTextBox.Location = new Point(368, 315);
        scfMonthlyTextBox.Margin = new Padding(12, 6, 0, 0);
        scfMonthlyTextBox.Name = "scfMonthlyTextBox";
        scfMonthlyTextBox.Size = new Size(344, 23);
        scfMonthlyTextBox.TabIndex = 21;
        // 
        // scfMonthlyLabel
        // 
        scfMonthlyLabel.AutoSize = true;
        scfMonthlyLabel.Location = new Point(368, 294);
        scfMonthlyLabel.Margin = new Padding(12, 14, 0, 0);
        scfMonthlyLabel.Name = "scfMonthlyLabel";
        scfMonthlyLabel.Size = new Size(76, 15);
        scfMonthlyLabel.TabIndex = 20;
        scfMonthlyLabel.Text = "SCF Monthly";
        // 
        // scfTotalTextBox
        // 
        scfTotalTextBox.Dock = DockStyle.Top;
        scfTotalTextBox.Location = new Point(0, 315);
        scfTotalTextBox.Margin = new Padding(0, 6, 12, 0);
        scfTotalTextBox.Name = "scfTotalTextBox";
        scfTotalTextBox.Size = new Size(344, 23);
        scfTotalTextBox.TabIndex = 19;
        // 
        // scfTotalLabel
        // 
        scfTotalLabel.AutoSize = true;
        scfTotalLabel.Location = new Point(0, 294);
        scfTotalLabel.Margin = new Padding(0, 14, 0, 0);
        scfTotalLabel.Name = "scfTotalLabel";
        scfTotalLabel.Size = new Size(92, 15);
        scfTotalLabel.TabIndex = 18;
        scfTotalLabel.Text = "SCF Total Amt.";
        // 
        // firstReadingDatePicker
        // 
        firstReadingDatePicker.Dock = DockStyle.Top;
        firstReadingDatePicker.Format = DateTimePickerFormat.Short;
        firstReadingDatePicker.Location = new Point(368, 255);
        firstReadingDatePicker.Margin = new Padding(12, 6, 0, 0);
        firstReadingDatePicker.Name = "firstReadingDatePicker";
        firstReadingDatePicker.Size = new Size(344, 23);
        firstReadingDatePicker.TabIndex = 17;
        // 
        // firstReadingDateLabel
        // 
        firstReadingDateLabel.AutoSize = true;
        firstReadingDateLabel.Location = new Point(368, 234);
        firstReadingDateLabel.Margin = new Padding(12, 14, 0, 0);
        firstReadingDateLabel.Name = "firstReadingDateLabel";
        firstReadingDateLabel.Size = new Size(101, 15);
        firstReadingDateLabel.TabIndex = 16;
        firstReadingDateLabel.Text = "First Reading Date";
        // 
        // meterNumberTextBox
        // 
        meterNumberTextBox.Dock = DockStyle.Top;
        meterNumberTextBox.Location = new Point(0, 255);
        meterNumberTextBox.Margin = new Padding(0, 6, 12, 0);
        meterNumberTextBox.Name = "meterNumberTextBox";
        meterNumberTextBox.Size = new Size(344, 23);
        meterNumberTextBox.TabIndex = 15;
        // 
        // meterLabel
        // 
        meterLabel.AutoSize = true;
        meterLabel.Location = new Point(0, 234);
        meterLabel.Margin = new Padding(0, 14, 0, 0);
        meterLabel.Name = "meterLabel";
        meterLabel.Size = new Size(84, 15);
        meterLabel.TabIndex = 14;
        meterLabel.Text = "Meter Number";
        // 
        // serviceComboBox
        // 
        serviceComboBox.Dock = DockStyle.Top;
        serviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        serviceComboBox.FormattingEnabled = true;
        serviceComboBox.Location = new Point(368, 195);
        serviceComboBox.Margin = new Padding(12, 6, 0, 0);
        serviceComboBox.Name = "serviceComboBox";
        serviceComboBox.Size = new Size(344, 23);
        serviceComboBox.TabIndex = 13;
        // 
        // serviceLabel
        // 
        serviceLabel.AutoSize = true;
        serviceLabel.Location = new Point(368, 174);
        serviceLabel.Margin = new Padding(12, 14, 0, 0);
        serviceLabel.Name = "serviceLabel";
        serviceLabel.Size = new Size(69, 15);
        serviceLabel.TabIndex = 12;
        serviceLabel.Text = "Service Type";
        // 
        // zoneComboBox
        // 
        zoneComboBox.Dock = DockStyle.Top;
        zoneComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        zoneComboBox.FormattingEnabled = true;
        zoneComboBox.Location = new Point(0, 195);
        zoneComboBox.Margin = new Padding(0, 6, 12, 0);
        zoneComboBox.Name = "zoneComboBox";
        zoneComboBox.Size = new Size(344, 23);
        zoneComboBox.TabIndex = 11;
        // 
        // zoneLabel
        // 
        zoneLabel.AutoSize = true;
        zoneLabel.Location = new Point(0, 174);
        zoneLabel.Margin = new Padding(0, 14, 0, 0);
        zoneLabel.Name = "zoneLabel";
        zoneLabel.Size = new Size(34, 15);
        zoneLabel.TabIndex = 10;
        zoneLabel.Text = "Zone";
        // 
        // addressTextBox
        // 
        addressTextBox.Dock = DockStyle.Top;
        addressTextBox.Location = new Point(368, 135);
        addressTextBox.Margin = new Padding(12, 6, 0, 0);
        addressTextBox.Name = "addressTextBox";
        addressTextBox.Size = new Size(344, 23);
        addressTextBox.TabIndex = 9;
        // 
        // addressLabel
        // 
        addressLabel.AutoSize = true;
        addressLabel.Location = new Point(368, 114);
        addressLabel.Margin = new Padding(12, 14, 0, 0);
        addressLabel.Name = "addressLabel";
        addressLabel.Size = new Size(49, 15);
        addressLabel.TabIndex = 8;
        addressLabel.Text = "Address";
        // 
        // tinTextBox
        // 
        tinTextBox.Dock = DockStyle.Top;
        tinTextBox.Location = new Point(0, 135);
        tinTextBox.Margin = new Padding(0, 6, 12, 0);
        tinTextBox.Name = "tinTextBox";
        tinTextBox.Size = new Size(344, 23);
        tinTextBox.TabIndex = 7;
        // 
        // tinLabel
        // 
        tinLabel.AutoSize = true;
        tinLabel.Location = new Point(0, 114);
        tinLabel.Margin = new Padding(0, 14, 0, 0);
        tinLabel.Name = "tinLabel";
        tinLabel.Size = new Size(25, 15);
        tinLabel.TabIndex = 6;
        tinLabel.Text = "TIN";
        // 
        // nameTextBox
        // 
        nameTextBox.Dock = DockStyle.Top;
        nameTextBox.Location = new Point(368, 75);
        nameTextBox.Margin = new Padding(12, 6, 0, 0);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(344, 23);
        nameTextBox.TabIndex = 5;
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(368, 54);
        nameLabel.Margin = new Padding(12, 14, 0, 0);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(39, 15);
        nameLabel.TabIndex = 4;
        nameLabel.Text = "Name";
        // 
        // accountNoTextBox
        // 
        accountNoTextBox.Dock = DockStyle.Top;
        accountNoTextBox.Location = new Point(0, 75);
        accountNoTextBox.Margin = new Padding(0, 6, 12, 0);
        accountNoTextBox.Name = "accountNoTextBox";
        accountNoTextBox.Size = new Size(344, 23);
        accountNoTextBox.TabIndex = 3;
        // 
        // accountNoLabel
        // 
        accountNoLabel.AutoSize = true;
        accountNoLabel.Location = new Point(0, 54);
        accountNoLabel.Margin = new Padding(0, 14, 0, 0);
        accountNoLabel.Name = "accountNoLabel";
        accountNoLabel.Size = new Size(69, 15);
        accountNoLabel.TabIndex = 2;
        accountNoLabel.Text = "Account No";
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(24, 24);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(109, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Upsert Concessionaire";
        // 
        // UpsertConcessionaireForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(760, 720);
        Controls.Add(rootLayout);
        MinimumSize = new Size(760, 720);
        Name = "UpsertConcessionaireForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Upsert Concessionaire";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        fieldsLayout.ResumeLayout(false);
        fieldsLayout.PerformLayout();
        buttonPanel.ResumeLayout(false);
        optionsPanel.ResumeLayout(false);
        optionsPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private TableLayoutPanel fieldsLayout;
    private Label accountNoLabel;
    private TextBox accountNoTextBox;
    private Label nameLabel;
    private TextBox nameTextBox;
    private Label tinLabel;
    private TextBox tinTextBox;
    private Label addressLabel;
    private TextBox addressTextBox;
    private Label zoneLabel;
    private ComboBox zoneComboBox;
    private Label serviceLabel;
    private ComboBox serviceComboBox;
    private Label meterLabel;
    private TextBox meterNumberTextBox;
    private Label firstReadingDateLabel;
    private DateTimePicker firstReadingDatePicker;
    private Label scfTotalLabel;
    private TextBox scfTotalTextBox;
    private Label scfMonthlyLabel;
    private TextBox scfMonthlyTextBox;
    private FlowLayoutPanel optionsPanel;
    private CheckBox taxExemptedCheckBox;
    private CheckBox dueExemptedCheckBox;
    private CheckBox discountedCheckBox;
    private FlowLayoutPanel buttonPanel;
    private Button saveButton;
    private Button cancelButton;
    private Label statusLabel;
}