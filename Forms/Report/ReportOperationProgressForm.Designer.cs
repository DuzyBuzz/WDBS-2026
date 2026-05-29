namespace WDBS_2026.Forms.Report;

partial class ReportOperationProgressForm
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
        titleLabel = new Label();
        statusLabel = new Label();
        progressBar = new ProgressBar();
        percentLabel = new Label();
        rootLayout.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 2;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.ColumnStyles.Add(new ColumnStyle());
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(statusLabel, 0, 1);
        rootLayout.Controls.Add(progressBar, 0, 2);
        rootLayout.Controls.Add(percentLabel, 1, 2);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(20);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.Size = new Size(500, 156);
        rootLayout.TabIndex = 0;
        rootLayout.SetColumnSpan(titleLabel, 2);
        rootLayout.SetColumnSpan(statusLabel, 2);
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(20, 20);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(116, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Preparing Report...";
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(20, 47);
        statusLabel.Margin = new Padding(0, 12, 0, 0);
        statusLabel.MaximumSize = new Size(440, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(59, 15);
        statusLabel.TabIndex = 1;
        statusLabel.Text = "Working...";
        // 
        // progressBar
        // 
        progressBar.Dock = DockStyle.Fill;
        progressBar.Location = new Point(20, 86);
        progressBar.Margin = new Padding(0, 24, 12, 0);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(407, 24);
        progressBar.TabIndex = 2;
        // 
        // percentLabel
        // 
        percentLabel.Anchor = AnchorStyles.Left;
        percentLabel.AutoSize = true;
        percentLabel.Location = new Point(439, 90);
        percentLabel.Margin = new Padding(0);
        percentLabel.Name = "percentLabel";
        percentLabel.Size = new Size(26, 15);
        percentLabel.TabIndex = 3;
        percentLabel.Text = "0%";
        // 
        // ReportOperationProgressForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(500, 156);
        ControlBox = false;
        Controls.Add(rootLayout);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReportOperationProgressForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Working";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label statusLabel;
    private ProgressBar progressBar;
    private Label percentLabel;
}