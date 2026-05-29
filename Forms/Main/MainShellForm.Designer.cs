namespace WDBS_2026.Forms;

partial class MainShellForm
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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainShellForm));
        navigationPanel = new Panel();
        navigationLayout = new TableLayoutPanel();
        navigationLogoPictureBox = new PictureBox();
        dashboardButton = new Button();
        roleButtonsFlowPanel = new FlowLayoutPanel();
        profileButton = new Button();
        logoutButton = new Button();
        contentHostPanel = new Panel();
        navigationToolTip = new ToolTip(components);
        navigationPanel.SuspendLayout();
        navigationLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)navigationLogoPictureBox).BeginInit();
        SuspendLayout();
        // 
        // navigationPanel
        // 
        navigationPanel.Controls.Add(navigationLayout);
        navigationPanel.Dock = DockStyle.Left;
        navigationPanel.Location = new Point(0, 0);
        navigationPanel.Margin = new Padding(0);
        navigationPanel.Name = "navigationPanel";
        navigationPanel.Padding = new Padding(20, 24, 20, 24);
        navigationPanel.Size = new Size(296, 768);
        navigationPanel.TabIndex = 0;
        // 
        // navigationLayout
        // 
        navigationLayout.ColumnCount = 1;
        navigationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        navigationLayout.Controls.Add(navigationLogoPictureBox, 0, 0);
        navigationLayout.Controls.Add(dashboardButton, 0, 1);
        navigationLayout.Controls.Add(roleButtonsFlowPanel, 0, 2);
        navigationLayout.Controls.Add(profileButton, 0, 4);
        navigationLayout.Controls.Add(logoutButton, 0, 5);
        navigationLayout.Dock = DockStyle.Fill;
        navigationLayout.Location = new Point(20, 24);
        navigationLayout.Margin = new Padding(3, 2, 3, 2);
        navigationLayout.Name = "navigationLayout";
        navigationLayout.RowCount = 6;
        navigationLayout.RowStyles.Add(new RowStyle());
        navigationLayout.RowStyles.Add(new RowStyle());
        navigationLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        navigationLayout.RowStyles.Add(new RowStyle());
        navigationLayout.RowStyles.Add(new RowStyle());
        navigationLayout.RowStyles.Add(new RowStyle());
        navigationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        navigationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        navigationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        navigationLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        navigationLayout.Size = new Size(256, 720);
        navigationLayout.TabIndex = 0;
        // 
        // navigationLogoPictureBox
        // 
        navigationLogoPictureBox.Anchor = AnchorStyles.None;
        navigationLogoPictureBox.Location = new Point(100, 0);
        navigationLogoPictureBox.Margin = new Padding(0, 0, 0, 8);
        navigationLogoPictureBox.Name = "navigationLogoPictureBox";
        navigationLogoPictureBox.Size = new Size(56, 56);
        navigationLogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        navigationLogoPictureBox.TabIndex = 8;
        navigationLogoPictureBox.TabStop = false;
        // 
        // dashboardButton
        // 
        dashboardButton.Dock = DockStyle.Top;
        dashboardButton.Location = new Point(0, 92);
        dashboardButton.Margin = new Padding(0, 28, 0, 0);
        dashboardButton.Name = "dashboardButton";
        dashboardButton.Size = new Size(256, 42);
        dashboardButton.TabIndex = 4;
        dashboardButton.Text = "Dashboard";
        dashboardButton.UseVisualStyleBackColor = true;
        dashboardButton.Click += dashboardButton_Click;
        // 
        // roleButtonsFlowPanel
        // 
        roleButtonsFlowPanel.AutoSize = true;
        roleButtonsFlowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        roleButtonsFlowPanel.Dock = DockStyle.Top;
        roleButtonsFlowPanel.FlowDirection = FlowDirection.TopDown;
        roleButtonsFlowPanel.Location = new Point(0, 142);
        roleButtonsFlowPanel.Margin = new Padding(0, 8, 0, 0);
        roleButtonsFlowPanel.Name = "roleButtonsFlowPanel";
        roleButtonsFlowPanel.Size = new Size(256, 0);
        roleButtonsFlowPanel.TabIndex = 7;
        roleButtonsFlowPanel.WrapContents = false;
        // 
        // profileButton
        // 
        profileButton.Dock = DockStyle.Fill;
        profileButton.Location = new Point(0, 636);
        profileButton.Margin = new Padding(0, 0, 0, 8);
        profileButton.Name = "profileButton";
        profileButton.Size = new Size(256, 34);
        profileButton.TabIndex = 5;
        profileButton.Text = "Profile";
        profileButton.UseVisualStyleBackColor = true;
        profileButton.Click += profileButton_Click;
        // 
        // logoutButton
        // 
        logoutButton.Dock = DockStyle.Fill;
        logoutButton.Location = new Point(0, 678);
        logoutButton.Margin = new Padding(0);
        logoutButton.Name = "logoutButton";
        logoutButton.Size = new Size(256, 42);
        logoutButton.TabIndex = 6;
        logoutButton.Text = "Log Out";
        logoutButton.UseVisualStyleBackColor = true;
        logoutButton.Click += logoutButton_Click;
        // 
        // contentHostPanel
        // 
        contentHostPanel.AutoScroll = true;
        contentHostPanel.Dock = DockStyle.Fill;
        contentHostPanel.Location = new Point(296, 0);
        contentHostPanel.Margin = new Padding(0);
        contentHostPanel.Name = "contentHostPanel";
        contentHostPanel.Padding = new Padding(24);
        contentHostPanel.Size = new Size(1070, 768);
        contentHostPanel.TabIndex = 1;
        // 
        // MainShellForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
        BackgroundImageLayout = ImageLayout.Stretch;
        ClientSize = new Size(1366, 768);
        Controls.Add(contentHostPanel);
        Controls.Add(navigationPanel);
        Margin = new Padding(3, 2, 3, 2);
        MinimumSize = new Size(980, 620);
        Name = "MainShellForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Water District Billing System";
        WindowState = FormWindowState.Maximized;
        navigationPanel.ResumeLayout(false);
        navigationLayout.ResumeLayout(false);
        navigationLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)navigationLogoPictureBox).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel navigationPanel;
    private TableLayoutPanel navigationLayout;
    private FlowLayoutPanel roleButtonsFlowPanel;
    private Button profileButton;
    private Button logoutButton;
    private Panel contentHostPanel;
    private PictureBox navigationLogoPictureBox;
    private ToolTip navigationToolTip;
    private Button dashboardButton;
}