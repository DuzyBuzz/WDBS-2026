namespace WDBS_2026.Forms.Billing;

partial class BillingReportParametersForm
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
        contentPanel = new Panel();
        contentLayout = new TableLayoutPanel();
        buttonLayout = new TableLayoutPanel();
        cancelButton = new Button();
        okButton = new Button();
        yearUpDown = new NumericUpDown();
        yearLabel = new Label();
        monthComboBox = new ComboBox();
        monthLabel = new Label();
        dayPicker = new DateTimePicker();
        dayLabel = new Label();
        helpLabel = new Label();
        titleLabel = new Label();
        rootLayout.SuspendLayout();
        contentPanel.SuspendLayout();
        contentLayout.SuspendLayout();
        buttonLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)yearUpDown).BeginInit();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(contentPanel, 0, 1);
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(24);
        rootLayout.RowCount = 2;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(432, 274);
        rootLayout.TabIndex = 0;
        // 
        // contentPanel
        // 
        contentPanel.Controls.Add(contentLayout);
        contentPanel.Dock = DockStyle.Fill;
        contentPanel.Location = new Point(24, 55);
        contentPanel.Margin = new Padding(0, 16, 0, 0);
        contentPanel.Name = "contentPanel";
        contentPanel.Size = new Size(384, 195);
        contentPanel.TabIndex = 1;
        // 
        // contentLayout
        // 
        contentLayout.ColumnCount = 2;
        contentLayout.ColumnStyles.Add(new ColumnStyle());
        contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        contentLayout.Controls.Add(buttonLayout, 0, 4);
        contentLayout.Controls.Add(yearUpDown, 1, 3);
        contentLayout.Controls.Add(yearLabel, 0, 3);
        contentLayout.Controls.Add(monthComboBox, 1, 2);
        contentLayout.Controls.Add(monthLabel, 0, 2);
        contentLayout.Controls.Add(dayPicker, 1, 1);
        contentLayout.Controls.Add(dayLabel, 0, 1);
        contentLayout.Controls.Add(helpLabel, 0, 0);
        contentLayout.Dock = DockStyle.Fill;
        contentLayout.Location = new Point(0, 0);
        contentLayout.Margin = new Padding(0);
        contentLayout.Name = "contentLayout";
        contentLayout.Padding = new Padding(18);
        contentLayout.RowCount = 5;
        contentLayout.RowStyles.Add(new RowStyle());
        contentLayout.RowStyles.Add(new RowStyle());
        contentLayout.RowStyles.Add(new RowStyle());
        contentLayout.RowStyles.Add(new RowStyle());
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        contentLayout.Size = new Size(384, 195);
        contentLayout.TabIndex = 0;
        // 
        // buttonLayout
        // 
        buttonLayout.ColumnCount = 3;
        contentLayout.SetColumnSpan(buttonLayout, 2);
        buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        buttonLayout.ColumnStyles.Add(new ColumnStyle());
        buttonLayout.ColumnStyles.Add(new ColumnStyle());
        buttonLayout.Controls.Add(cancelButton, 1, 0);
        buttonLayout.Controls.Add(okButton, 2, 0);
        buttonLayout.Dock = DockStyle.Fill;
        buttonLayout.Location = new Point(18, 145);
        buttonLayout.Margin = new Padding(0, 16, 0, 0);
        buttonLayout.Name = "buttonLayout";
        buttonLayout.RowCount = 1;
        buttonLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        buttonLayout.Size = new Size(348, 32);
        buttonLayout.TabIndex = 7;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(168, 0);
        cancelButton.Margin = new Padding(0, 0, 8, 0);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(84, 32);
        cancelButton.TabIndex = 0;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += cancelButton_Click;
        // 
        // okButton
        // 
        okButton.Location = new Point(260, 0);
        okButton.Margin = new Padding(0);
        okButton.Name = "okButton";
        okButton.Size = new Size(88, 32);
        okButton.TabIndex = 1;
        okButton.Text = "Continue";
        okButton.UseVisualStyleBackColor = true;
        okButton.Click += okButton_Click;
        // 
        // yearUpDown
        // 
        yearUpDown.Location = new Point(117, 106);
        yearUpDown.Margin = new Padding(12, 6, 0, 0);
        yearUpDown.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
        yearUpDown.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
        yearUpDown.Name = "yearUpDown";
        yearUpDown.Size = new Size(120, 23);
        yearUpDown.TabIndex = 6;
        yearUpDown.Value = new decimal(new int[] { 2026, 0, 0, 0 });
        // 
        // yearLabel
        // 
        yearLabel.Anchor = AnchorStyles.Left;
        yearLabel.AutoSize = true;
        yearLabel.Location = new Point(0, 110);
        yearLabel.Margin = new Padding(0, 6, 0, 0);
        yearLabel.Name = "yearLabel";
        yearLabel.Size = new Size(29, 15);
        yearLabel.TabIndex = 5;
        yearLabel.Text = "Year";
        // 
        // monthComboBox
        // 
        monthComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        monthComboBox.FormattingEnabled = true;
        monthComboBox.Location = new Point(117, 77);
        monthComboBox.Margin = new Padding(12, 6, 0, 0);
        monthComboBox.Name = "monthComboBox";
        monthComboBox.Size = new Size(200, 23);
        monthComboBox.TabIndex = 4;
        // 
        // monthLabel
        // 
        monthLabel.Anchor = AnchorStyles.Left;
        monthLabel.AutoSize = true;
        monthLabel.Location = new Point(0, 81);
        monthLabel.Margin = new Padding(0, 6, 0, 0);
        monthLabel.Name = "monthLabel";
        monthLabel.Size = new Size(43, 15);
        monthLabel.TabIndex = 3;
        monthLabel.Text = "Month";
        // 
        // dayPicker
        // 
        dayPicker.Format = DateTimePickerFormat.Short;
        dayPicker.Location = new Point(117, 48);
        dayPicker.Margin = new Padding(12, 6, 0, 0);
        dayPicker.Name = "dayPicker";
        dayPicker.Size = new Size(120, 23);
        dayPicker.TabIndex = 2;
        // 
        // dayLabel
        // 
        dayLabel.Anchor = AnchorStyles.Left;
        dayLabel.AutoSize = true;
        dayLabel.Location = new Point(0, 52);
        dayLabel.Margin = new Padding(0, 6, 0, 0);
        dayLabel.Name = "dayLabel";
        dayLabel.Size = new Size(69, 15);
        dayLabel.TabIndex = 1;
        dayLabel.Text = "Billing Date";
        // 
        // helpLabel
        // 
        helpLabel.AutoSize = true;
        contentLayout.SetColumnSpan(helpLabel, 2);
        helpLabel.Location = new Point(18, 18);
        helpLabel.Margin = new Padding(0);
        helpLabel.Name = "helpLabel";
        helpLabel.Size = new Size(268, 15);
        helpLabel.TabIndex = 0;
        helpLabel.Text = "Select the report period to preview or export.";
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(24, 24);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(114, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Billing Report Filter";
        // 
        // BillingReportParametersForm
        // 
        AcceptButton = okButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new Size(432, 274);
        Controls.Add(rootLayout);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "BillingReportParametersForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Billing Report Parameters";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        contentPanel.ResumeLayout(false);
        contentLayout.ResumeLayout(false);
        contentLayout.PerformLayout();
        buttonLayout.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)yearUpDown).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Panel contentPanel;
    private TableLayoutPanel contentLayout;
    private Label helpLabel;
    private Label dayLabel;
    private DateTimePicker dayPicker;
    private Label monthLabel;
    private ComboBox monthComboBox;
    private Label yearLabel;
    private NumericUpDown yearUpDown;
    private TableLayoutPanel buttonLayout;
    private Button cancelButton;
    private Button okButton;
}