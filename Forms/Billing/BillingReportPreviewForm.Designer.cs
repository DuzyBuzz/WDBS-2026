namespace WDBS_2026.Forms.Billing;

partial class BillingReportPreviewForm
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
        actionLayout = new TableLayoutPanel();
        closeButton = new Button();
        excelButton = new Button();
        pdfButton = new Button();
        printButton = new Button();
        pageInfoLabel = new Label();
        nextPageButton = new Button();
        previousPageButton = new Button();
        previewHostPanel = new Panel();
        previewControl = new PrintPreviewControl();
        subtitleLabel = new Label();
        titleLabel = new Label();
        rootLayout.SuspendLayout();
        actionLayout.SuspendLayout();
        previewHostPanel.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(actionLayout, 0, 2);
        rootLayout.Controls.Add(previewHostPanel, 0, 3);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(24);
        rootLayout.RowCount = 4;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Size = new Size(1180, 760);
        rootLayout.TabIndex = 0;
        // 
        // actionLayout
        // 
        actionLayout.ColumnCount = 8;
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.Controls.Add(closeButton, 7, 0);
        actionLayout.Controls.Add(excelButton, 6, 0);
        actionLayout.Controls.Add(pdfButton, 5, 0);
        actionLayout.Controls.Add(printButton, 4, 0);
        actionLayout.Controls.Add(pageInfoLabel, 3, 0);
        actionLayout.Controls.Add(nextPageButton, 2, 0);
        actionLayout.Controls.Add(previousPageButton, 1, 0);
        actionLayout.Dock = DockStyle.Fill;
        actionLayout.Location = new Point(24, 71);
        actionLayout.Margin = new Padding(0, 14, 0, 0);
        actionLayout.Name = "actionLayout";
        actionLayout.RowCount = 1;
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        actionLayout.Size = new Size(1132, 40);
        actionLayout.TabIndex = 2;
        // 
        // closeButton
        // 
        closeButton.Location = new Point(1036, 0);
        closeButton.Margin = new Padding(8, 0, 0, 0);
        closeButton.Name = "closeButton";
        closeButton.Size = new Size(96, 36);
        closeButton.TabIndex = 3;
        closeButton.Text = "Close";
        closeButton.UseVisualStyleBackColor = true;
        closeButton.Click += closeButton_Click;
        // 
        // excelButton
        // 
        excelButton.Location = new Point(924, 0);
        excelButton.Margin = new Padding(8, 0, 0, 0);
        excelButton.Name = "excelButton";
        excelButton.Size = new Size(104, 36);
        excelButton.TabIndex = 2;
        excelButton.Text = "EXCEL";
        excelButton.UseVisualStyleBackColor = true;
        excelButton.Click += excelButton_Click;
        // 
        // pdfButton
        // 
        pdfButton.Location = new Point(812, 0);
        pdfButton.Margin = new Padding(8, 0, 0, 0);
        pdfButton.Name = "pdfButton";
        pdfButton.Size = new Size(104, 36);
        pdfButton.TabIndex = 1;
        pdfButton.Text = "PDF";
        pdfButton.UseVisualStyleBackColor = true;
        pdfButton.Click += pdfButton_Click;
        // 
        // printButton
        // 
        printButton.Location = new Point(700, 0);
        printButton.Margin = new Padding(0);
        printButton.Name = "printButton";
        printButton.Size = new Size(104, 36);
        printButton.TabIndex = 0;
        printButton.Text = "Print";
        printButton.UseVisualStyleBackColor = true;
        printButton.Click += printButton_Click;
        // 
        // pageInfoLabel
        // 
        pageInfoLabel.Anchor = AnchorStyles.Left;
        pageInfoLabel.AutoSize = true;
        pageInfoLabel.Location = new Point(698, 12);
        pageInfoLabel.Margin = new Padding(8, 0, 8, 0);
        pageInfoLabel.Name = "pageInfoLabel";
        pageInfoLabel.Size = new Size(66, 15);
        pageInfoLabel.TabIndex = 4;
        pageInfoLabel.Text = "Page 1 of 1";
        // 
        // nextPageButton
        // 
        nextPageButton.Location = new Point(588, 0);
        nextPageButton.Margin = new Padding(8, 0, 0, 0);
        nextPageButton.Name = "nextPageButton";
        nextPageButton.Size = new Size(96, 36);
        nextPageButton.TabIndex = 5;
        nextPageButton.Text = "Next";
        nextPageButton.UseVisualStyleBackColor = true;
        nextPageButton.Click += nextPageButton_Click;
        // 
        // previousPageButton
        // 
        previousPageButton.Location = new Point(484, 0);
        previousPageButton.Margin = new Padding(0);
        previousPageButton.Name = "previousPageButton";
        previousPageButton.Size = new Size(96, 36);
        previousPageButton.TabIndex = 6;
        previousPageButton.Text = "Prev";
        previousPageButton.UseVisualStyleBackColor = true;
        previousPageButton.Click += previousPageButton_Click;
        // 
        // previewHostPanel
        // 
        previewHostPanel.Controls.Add(previewControl);
        previewHostPanel.Dock = DockStyle.Fill;
        previewHostPanel.Location = new Point(24, 125);
        previewHostPanel.Margin = new Padding(0, 14, 0, 0);
        previewHostPanel.Name = "previewHostPanel";
        previewHostPanel.Padding = new Padding(12);
        previewHostPanel.Size = new Size(1132, 611);
        previewHostPanel.TabIndex = 3;
        // 
        // previewControl
        // 
        previewControl.Dock = DockStyle.Fill;
        previewControl.Location = new Point(12, 12);
        previewControl.Name = "previewControl";
        previewControl.Size = new Size(1108, 587);
        previewControl.TabIndex = 0;
        // 
        // subtitleLabel
        // 
        subtitleLabel.AutoSize = true;
        subtitleLabel.Location = new Point(24, 42);
        subtitleLabel.Margin = new Padding(0, 6, 0, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(73, 15);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "Billing Date:";
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(24, 24);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(112, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Billing Report Preview";
        // 
        // BillingReportPreviewForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1180, 760);
        Controls.Add(rootLayout);
        MinimumSize = new Size(980, 620);
        Name = "BillingReportPreviewForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Billing Report Preview";
        WindowState = FormWindowState.Maximized;
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        actionLayout.ResumeLayout(false);
        previewHostPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
    private TableLayoutPanel actionLayout;
    private Button printButton;
    private Button pdfButton;
    private Button excelButton;
    private Label pageInfoLabel;
    private Button nextPageButton;
    private Button previousPageButton;
    private Button closeButton;
    private Panel previewHostPanel;
    private PrintPreviewControl previewControl;
}