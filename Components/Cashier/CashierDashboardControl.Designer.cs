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
        subtitleLabel = new Label();
        headingLabel = new Label();
        cardsFlowLayoutPanel = new FlowLayoutPanel();
        collectionCardPanel = new Panel();
        collectionBodyLabel = new Label();
        collectionTitleLabel = new Label();
        scfCollectionCardPanel = new Panel();
        scfCollectionBodyLabel = new Label();
        scfCollectionTitleLabel = new Label();
        recordsCardPanel = new Panel();
        recordsBodyLabel = new Label();
        recordsTitleLabel = new Label();
        layoutPanel.SuspendLayout();
        cardsFlowLayoutPanel.SuspendLayout();
        collectionCardPanel.SuspendLayout();
        scfCollectionCardPanel.SuspendLayout();
        recordsCardPanel.SuspendLayout();
        SuspendLayout();
        // 
        // layoutPanel
        // 
        layoutPanel.ColumnCount = 1;
        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutPanel.Controls.Add(subtitleLabel, 0, 1);
        layoutPanel.Controls.Add(headingLabel, 0, 0);
        layoutPanel.Controls.Add(cardsFlowLayoutPanel, 0, 2);
        layoutPanel.Dock = DockStyle.Fill;
        layoutPanel.Location = new Point(0, 0);
        layoutPanel.Name = "layoutPanel";
        layoutPanel.Padding = new Padding(0, 4, 0, 0);
        layoutPanel.RowCount = 3;
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle());
        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutPanel.Size = new Size(1046, 618);
        layoutPanel.TabIndex = 0;
        // 
        // subtitleLabel
        // 
        subtitleLabel.AutoSize = true;
        subtitleLabel.Location = new Point(3, 41);
        subtitleLabel.MaximumSize = new Size(900, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(44, 19);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "State";
        // 
        // headingLabel
        // 
        headingLabel.AutoSize = true;
        headingLabel.Location = new Point(3, 4);
        headingLabel.Name = "headingLabel";
        headingLabel.Size = new Size(86, 19);
        headingLabel.TabIndex = 0;
        headingLabel.Text = "Cashier Hub";
        // 
        // cardsFlowLayoutPanel
        // 
        cardsFlowLayoutPanel.Controls.Add(collectionCardPanel);
        cardsFlowLayoutPanel.Controls.Add(scfCollectionCardPanel);
        cardsFlowLayoutPanel.Controls.Add(recordsCardPanel);
        cardsFlowLayoutPanel.Dock = DockStyle.Fill;
        cardsFlowLayoutPanel.Location = new Point(0, 84);
        cardsFlowLayoutPanel.Margin = new Padding(0, 24, 0, 0);
        cardsFlowLayoutPanel.Name = "cardsFlowLayoutPanel";
        cardsFlowLayoutPanel.Size = new Size(1046, 534);
        cardsFlowLayoutPanel.TabIndex = 2;
        cardsFlowLayoutPanel.WrapContents = true;
        // 
        // collectionCardPanel
        // 
        collectionCardPanel.Controls.Add(collectionBodyLabel);
        collectionCardPanel.Controls.Add(collectionTitleLabel);
        collectionCardPanel.Location = new Point(0, 0);
        collectionCardPanel.Margin = new Padding(0, 0, 18, 18);
        collectionCardPanel.Name = "collectionCardPanel";
        collectionCardPanel.Size = new Size(300, 170);
        collectionCardPanel.TabIndex = 0;
        // 
        // collectionBodyLabel
        // 
        collectionBodyLabel.AutoSize = true;
        collectionBodyLabel.Location = new Point(24, 62);
        collectionBodyLabel.MaximumSize = new Size(240, 0);
        collectionBodyLabel.Name = "collectionBodyLabel";
        collectionBodyLabel.Size = new Size(239, 57);
        collectionBodyLabel.TabIndex = 1;
        collectionBodyLabel.Text = "Preserve the layout and logic for OR generation, preview, and standard bill collection.";
        // 
        // collectionTitleLabel
        // 
        collectionTitleLabel.AutoSize = true;
        collectionTitleLabel.Location = new Point(24, 24);
        collectionTitleLabel.Name = "collectionTitleLabel";
        collectionTitleLabel.Size = new Size(67, 19);
        collectionTitleLabel.TabIndex = 0;
        collectionTitleLabel.Text = "Collection";
        // 
        // scfCollectionCardPanel
        // 
        scfCollectionCardPanel.Controls.Add(scfCollectionBodyLabel);
        scfCollectionCardPanel.Controls.Add(scfCollectionTitleLabel);
        scfCollectionCardPanel.Location = new Point(318, 0);
        scfCollectionCardPanel.Margin = new Padding(0, 0, 18, 18);
        scfCollectionCardPanel.Name = "scfCollectionCardPanel";
        scfCollectionCardPanel.Size = new Size(300, 170);
        scfCollectionCardPanel.TabIndex = 1;
        // 
        // scfCollectionBodyLabel
        // 
        scfCollectionBodyLabel.AutoSize = true;
        scfCollectionBodyLabel.Location = new Point(24, 62);
        scfCollectionBodyLabel.MaximumSize = new Size(240, 0);
        scfCollectionBodyLabel.Name = "scfCollectionBodyLabel";
        scfCollectionBodyLabel.Size = new Size(227, 57);
        scfCollectionBodyLabel.TabIndex = 1;
        scfCollectionBodyLabel.Text = "Support SCF-specific cashier posting and keep the legacy flow ready for migration.";
        // 
        // scfCollectionTitleLabel
        // 
        scfCollectionTitleLabel.AutoSize = true;
        scfCollectionTitleLabel.Location = new Point(24, 24);
        scfCollectionTitleLabel.Name = "scfCollectionTitleLabel";
        scfCollectionTitleLabel.Size = new Size(93, 19);
        scfCollectionTitleLabel.TabIndex = 0;
        scfCollectionTitleLabel.Text = "SCF Payment";
        // 
        // recordsCardPanel
        // 
        recordsCardPanel.Controls.Add(recordsBodyLabel);
        recordsCardPanel.Controls.Add(recordsTitleLabel);
        recordsCardPanel.Location = new Point(636, 0);
        recordsCardPanel.Margin = new Padding(0, 0, 18, 18);
        recordsCardPanel.Name = "recordsCardPanel";
        recordsCardPanel.Size = new Size(300, 170);
        recordsCardPanel.TabIndex = 2;
        // 
        // recordsBodyLabel
        // 
        recordsBodyLabel.AutoSize = true;
        recordsBodyLabel.Location = new Point(24, 62);
        recordsBodyLabel.MaximumSize = new Size(240, 0);
        recordsBodyLabel.Name = "recordsBodyLabel";
        recordsBodyLabel.Size = new Size(236, 57);
        recordsBodyLabel.TabIndex = 1;
        recordsBodyLabel.Text = "Review collection history, preserve void support, and keep cashier reporting accessible.";
        // 
        // recordsTitleLabel
        // 
        recordsTitleLabel.AutoSize = true;
        recordsTitleLabel.Location = new Point(24, 24);
        recordsTitleLabel.Name = "recordsTitleLabel";
        recordsTitleLabel.Size = new Size(118, 19);
        recordsTitleLabel.TabIndex = 0;
        recordsTitleLabel.Text = "Collection Records";
        // 
        // CashierDashboardControl
        // 
        AutoScaleDimensions = new SizeF(7F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(layoutPanel);
        Name = "CashierDashboardControl";
        Size = new Size(1046, 618);
        layoutPanel.ResumeLayout(false);
        layoutPanel.PerformLayout();
        cardsFlowLayoutPanel.ResumeLayout(false);
        collectionCardPanel.ResumeLayout(false);
        collectionCardPanel.PerformLayout();
        scfCollectionCardPanel.ResumeLayout(false);
        scfCollectionCardPanel.PerformLayout();
        recordsCardPanel.ResumeLayout(false);
        recordsCardPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel layoutPanel;
    private Label headingLabel;
    private Label subtitleLabel;
    private FlowLayoutPanel cardsFlowLayoutPanel;
    private Panel collectionCardPanel;
    private Label collectionTitleLabel;
    private Label collectionBodyLabel;
    private Panel scfCollectionCardPanel;
    private Label scfCollectionTitleLabel;
    private Label scfCollectionBodyLabel;
    private Panel recordsCardPanel;
    private Label recordsTitleLabel;
    private Label recordsBodyLabel;
}