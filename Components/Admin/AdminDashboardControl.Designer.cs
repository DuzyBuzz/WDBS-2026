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
        roleDashboardsTabControl = new TabControl();
        billerTabPage = new TabPage();
        billerDashboardHostPanel = new Panel();
        cashierTabPage = new TabPage();
        cashierDashboardHostPanel = new Panel();
        roleDashboardsTabControl.SuspendLayout();
        billerTabPage.SuspendLayout();
        cashierTabPage.SuspendLayout();
        SuspendLayout();
        // 
        // roleDashboardsTabControl
        // 
        roleDashboardsTabControl.Controls.Add(billerTabPage);
        roleDashboardsTabControl.Controls.Add(cashierTabPage);
        roleDashboardsTabControl.Dock = DockStyle.Fill;
        roleDashboardsTabControl.Location = new Point(0, 0);
        roleDashboardsTabControl.Margin = new Padding(0, 19, 0, 0);
        roleDashboardsTabControl.Name = "roleDashboardsTabControl";
        roleDashboardsTabControl.SelectedIndex = 0;
        roleDashboardsTabControl.Size = new Size(1046, 488);
        roleDashboardsTabControl.TabIndex = 2;
        // 
        // billerTabPage
        // 
        billerTabPage.Controls.Add(billerDashboardHostPanel);
        billerTabPage.Location = new Point(4, 24);
        billerTabPage.Margin = new Padding(3, 2, 3, 2);
        billerTabPage.Name = "billerTabPage";
        billerTabPage.Padding = new Padding(3, 2, 3, 2);
        billerTabPage.Size = new Size(1038, 460);
        billerTabPage.TabIndex = 0;
        billerTabPage.Text = "Biller Dashboard";
        billerTabPage.UseVisualStyleBackColor = true;
        // 
        // billerDashboardHostPanel
        // 
        billerDashboardHostPanel.Dock = DockStyle.Fill;
        billerDashboardHostPanel.Location = new Point(3, 2);
        billerDashboardHostPanel.Margin = new Padding(3, 2, 3, 2);
        billerDashboardHostPanel.Name = "billerDashboardHostPanel";
        billerDashboardHostPanel.Size = new Size(1032, 456);
        billerDashboardHostPanel.TabIndex = 0;
        // 
        // cashierTabPage
        // 
        cashierTabPage.Controls.Add(cashierDashboardHostPanel);
        cashierTabPage.Location = new Point(4, 24);
        cashierTabPage.Margin = new Padding(3, 2, 3, 2);
        cashierTabPage.Name = "cashierTabPage";
        cashierTabPage.Padding = new Padding(3, 2, 3, 2);
        cashierTabPage.Size = new Size(1038, 414);
        cashierTabPage.TabIndex = 1;
        cashierTabPage.Text = "Cashier Dashboard";
        cashierTabPage.UseVisualStyleBackColor = true;
        // 
        // cashierDashboardHostPanel
        // 
        cashierDashboardHostPanel.Dock = DockStyle.Fill;
        cashierDashboardHostPanel.Location = new Point(3, 2);
        cashierDashboardHostPanel.Margin = new Padding(3, 2, 3, 2);
        cashierDashboardHostPanel.Name = "cashierDashboardHostPanel";
        cashierDashboardHostPanel.Size = new Size(1032, 410);
        cashierDashboardHostPanel.TabIndex = 0;
        // 
        // AdminDashboardControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(roleDashboardsTabControl);
        Margin = new Padding(3, 2, 3, 2);
        Name = "AdminDashboardControl";
        Size = new Size(1046, 488);
        roleDashboardsTabControl.ResumeLayout(false);
        billerTabPage.ResumeLayout(false);
        cashierTabPage.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private TabControl roleDashboardsTabControl;
    private TabPage billerTabPage;
    private Panel billerDashboardHostPanel;
    private TabPage cashierTabPage;
    private Panel cashierDashboardHostPanel;
}