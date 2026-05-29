namespace WDBS_2026.Components.Admin;

partial class AdminDashboardControl
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
        usersCardPanel = new Panel();
        usersBodyLabel = new Label();
        usersTitleLabel = new Label();
        settingsCardPanel = new Panel();
        settingsBodyLabel = new Label();
        settingsTitleLabel = new Label();
        servicesCardPanel = new Panel();
        servicesBodyLabel = new Label();
        servicesTitleLabel = new Label();
        zonesCardPanel = new Panel();
        zonesBodyLabel = new Label();
        zonesTitleLabel = new Label();
        layoutPanel.SuspendLayout();
        cardsFlowLayoutPanel.SuspendLayout();
        usersCardPanel.SuspendLayout();
        settingsCardPanel.SuspendLayout();
        servicesCardPanel.SuspendLayout();
        zonesCardPanel.SuspendLayout();
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
        headingLabel.Size = new Size(136, 19);
        headingLabel.TabIndex = 0;
        headingLabel.Text = "Administration Hub";
        // 
        // cardsFlowLayoutPanel
        // 
        cardsFlowLayoutPanel.Controls.Add(usersCardPanel);
        cardsFlowLayoutPanel.Controls.Add(settingsCardPanel);
        cardsFlowLayoutPanel.Controls.Add(servicesCardPanel);
        cardsFlowLayoutPanel.Controls.Add(zonesCardPanel);
        cardsFlowLayoutPanel.Dock = DockStyle.Fill;
        cardsFlowLayoutPanel.Location = new Point(0, 84);
        cardsFlowLayoutPanel.Margin = new Padding(0, 24, 0, 0);
        cardsFlowLayoutPanel.Name = "cardsFlowLayoutPanel";
        cardsFlowLayoutPanel.Size = new Size(1046, 534);
        cardsFlowLayoutPanel.TabIndex = 2;
        cardsFlowLayoutPanel.WrapContents = true;
        // 
        // usersCardPanel
        // 
        usersCardPanel.Controls.Add(usersBodyLabel);
        usersCardPanel.Controls.Add(usersTitleLabel);
        usersCardPanel.Location = new Point(0, 0);
        usersCardPanel.Margin = new Padding(0, 0, 18, 18);
        usersCardPanel.Name = "usersCardPanel";
        usersCardPanel.Size = new Size(300, 170);
        usersCardPanel.TabIndex = 0;
        // 
        // usersBodyLabel
        // 
        usersBodyLabel.AutoSize = true;
        usersBodyLabel.Location = new Point(24, 62);
        usersBodyLabel.MaximumSize = new Size(240, 0);
        usersBodyLabel.Name = "usersBodyLabel";
        usersBodyLabel.Size = new Size(226, 57);
        usersBodyLabel.TabIndex = 1;
        usersBodyLabel.Text = "Create, activate, and maintain admin, biller, and cashier accounts from a single module.";
        // 
        // usersTitleLabel
        // 
        usersTitleLabel.AutoSize = true;
        usersTitleLabel.Location = new Point(24, 24);
        usersTitleLabel.Name = "usersTitleLabel";
        usersTitleLabel.Size = new Size(100, 19);
        usersTitleLabel.TabIndex = 0;
        usersTitleLabel.Text = "User Accounts";
        // 
        // settingsCardPanel
        // 
        settingsCardPanel.Controls.Add(settingsBodyLabel);
        settingsCardPanel.Controls.Add(settingsTitleLabel);
        settingsCardPanel.Location = new Point(318, 0);
        settingsCardPanel.Margin = new Padding(0, 0, 18, 18);
        settingsCardPanel.Name = "settingsCardPanel";
        settingsCardPanel.Size = new Size(300, 170);
        settingsCardPanel.TabIndex = 1;
        // 
        // settingsBodyLabel
        // 
        settingsBodyLabel.AutoSize = true;
        settingsBodyLabel.Location = new Point(24, 62);
        settingsBodyLabel.MaximumSize = new Size(240, 0);
        settingsBodyLabel.Name = "settingsBodyLabel";
        settingsBodyLabel.Size = new Size(228, 57);
        settingsBodyLabel.TabIndex = 1;
        settingsBodyLabel.Text = "Review discount, tax, due-date, and penalty settings that influence billing behavior.";
        // 
        // settingsTitleLabel
        // 
        settingsTitleLabel.AutoSize = true;
        settingsTitleLabel.Location = new Point(24, 24);
        settingsTitleLabel.Name = "settingsTitleLabel";
        settingsTitleLabel.Size = new Size(111, 19);
        settingsTitleLabel.TabIndex = 0;
        settingsTitleLabel.Text = "System Settings";
        // 
        // servicesCardPanel
        // 
        servicesCardPanel.Controls.Add(servicesBodyLabel);
        servicesCardPanel.Controls.Add(servicesTitleLabel);
        servicesCardPanel.Location = new Point(636, 0);
        servicesCardPanel.Margin = new Padding(0, 0, 18, 18);
        servicesCardPanel.Name = "servicesCardPanel";
        servicesCardPanel.Size = new Size(300, 170);
        servicesCardPanel.TabIndex = 2;
        // 
        // servicesBodyLabel
        // 
        servicesBodyLabel.AutoSize = true;
        servicesBodyLabel.Location = new Point(24, 62);
        servicesBodyLabel.MaximumSize = new Size(240, 0);
        servicesBodyLabel.Name = "servicesBodyLabel";
        servicesBodyLabel.Size = new Size(232, 57);
        servicesBodyLabel.TabIndex = 1;
        servicesBodyLabel.Text = "Maintain service types, pipe sizes, minimum rates, and progressive billing brackets.";
        // 
        // servicesTitleLabel
        // 
        servicesTitleLabel.AutoSize = true;
        servicesTitleLabel.Location = new Point(24, 24);
        servicesTitleLabel.Name = "servicesTitleLabel";
        servicesTitleLabel.Size = new Size(100, 19);
        servicesTitleLabel.TabIndex = 0;
        servicesTitleLabel.Text = "Service Rates";
        // 
        // zonesCardPanel
        // 
        zonesCardPanel.Controls.Add(zonesBodyLabel);
        zonesCardPanel.Controls.Add(zonesTitleLabel);
        zonesCardPanel.Location = new Point(0, 188);
        zonesCardPanel.Margin = new Padding(0, 0, 18, 18);
        zonesCardPanel.Name = "zonesCardPanel";
        zonesCardPanel.Size = new Size(300, 170);
        zonesCardPanel.TabIndex = 3;
        // 
        // zonesBodyLabel
        // 
        zonesBodyLabel.AutoSize = true;
        zonesBodyLabel.Location = new Point(24, 62);
        zonesBodyLabel.MaximumSize = new Size(240, 0);
        zonesBodyLabel.Name = "zonesBodyLabel";
        zonesBodyLabel.Size = new Size(228, 57);
        zonesBodyLabel.TabIndex = 1;
        zonesBodyLabel.Text = "Organize concessionaires by service zone and keep operational grouping accurate for billing.";
        // 
        // zonesTitleLabel
        // 
        zonesTitleLabel.AutoSize = true;
        zonesTitleLabel.Location = new Point(24, 24);
        zonesTitleLabel.Name = "zonesTitleLabel";
        zonesTitleLabel.Size = new Size(47, 19);
        zonesTitleLabel.TabIndex = 0;
        zonesTitleLabel.Text = "Zones";
        // 
        // AdminDashboardControl
        // 
        AutoScaleDimensions = new SizeF(7F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(layoutPanel);
        Name = "AdminDashboardControl";
        Size = new Size(1046, 618);
        layoutPanel.ResumeLayout(false);
        layoutPanel.PerformLayout();
        cardsFlowLayoutPanel.ResumeLayout(false);
        usersCardPanel.ResumeLayout(false);
        usersCardPanel.PerformLayout();
        settingsCardPanel.ResumeLayout(false);
        settingsCardPanel.PerformLayout();
        servicesCardPanel.ResumeLayout(false);
        servicesCardPanel.PerformLayout();
        zonesCardPanel.ResumeLayout(false);
        zonesCardPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel layoutPanel;
    private Label headingLabel;
    private Label subtitleLabel;
    private FlowLayoutPanel cardsFlowLayoutPanel;
    private Panel usersCardPanel;
    private Label usersTitleLabel;
    private Label usersBodyLabel;
    private Panel settingsCardPanel;
    private Label settingsTitleLabel;
    private Label settingsBodyLabel;
    private Panel servicesCardPanel;
    private Label servicesTitleLabel;
    private Label servicesBodyLabel;
    private Panel zonesCardPanel;
    private Label zonesTitleLabel;
    private Label zonesBodyLabel;
}