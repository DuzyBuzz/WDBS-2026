namespace WDBS_2026.Forms;

partial class LoginForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
        loginCardLayout = new TableLayoutPanel();
        logoLayout = new TableLayoutPanel();
        philippinesLogoPictureBox = new PictureBox();
        logoDividerLabel = new Label();
        tubunganLogoPictureBox = new PictureBox();
        signInLabel = new Label();
        signInCaptionLabel = new Label();
        usernameLabel = new Label();
        usernameTextBox = new TextBox();
        passwordLabel = new Label();
        passwordTextBox = new TextBox();
        showPasswordCheckBox = new CheckBox();
        loginButton = new Button();
        statusLabel = new Label();
        updateFooterLayout = new TableLayoutPanel();
        checkForUpdatesButton = new Button();
        loginCardPanel = new Panel();
        rootLayout = new TableLayoutPanel();
        versionLabel = new Label();
        loginCardLayout.SuspendLayout();
        logoLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)philippinesLogoPictureBox).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tubunganLogoPictureBox).BeginInit();
        loginCardPanel.SuspendLayout();
        rootLayout.SuspendLayout();
        SuspendLayout();
        // 
        // loginCardLayout
        // 
        loginCardLayout.AutoSize = true;
        loginCardLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        loginCardLayout.ColumnCount = 1;
        loginCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        loginCardLayout.Controls.Add(updateFooterLayout, 0, 10);
        loginCardLayout.Controls.Add(logoLayout, 0, 0);
        loginCardLayout.Controls.Add(signInLabel, 0, 1);
        loginCardLayout.Controls.Add(signInCaptionLabel, 0, 2);
        loginCardLayout.Controls.Add(usernameLabel, 0, 3);
        loginCardLayout.Controls.Add(usernameTextBox, 0, 4);
        loginCardLayout.Controls.Add(passwordLabel, 0, 5);
        loginCardLayout.Controls.Add(passwordTextBox, 0, 6);
        loginCardLayout.Controls.Add(showPasswordCheckBox, 0, 7);
        loginCardLayout.Controls.Add(loginButton, 0, 8);
        loginCardLayout.Controls.Add(statusLabel, 0, 9);
        loginCardLayout.Dock = DockStyle.Fill;
        loginCardLayout.Location = new Point(32, 30);
        loginCardLayout.Margin = new Padding(3, 2, 3, 2);
        loginCardLayout.Name = "loginCardLayout";
        loginCardLayout.RowCount = 11;
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.RowStyles.Add(new RowStyle());
        loginCardLayout.Size = new Size(396, 400);
        loginCardLayout.TabIndex = 0;
        // 
        // logoLayout
        // 
        logoLayout.ColumnCount = 3;
        logoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
        logoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        logoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
        logoLayout.Controls.Add(philippinesLogoPictureBox, 0, 0);
        logoLayout.Controls.Add(logoDividerLabel, 1, 0);
        logoLayout.Controls.Add(tubunganLogoPictureBox, 2, 0);
        logoLayout.Dock = DockStyle.Fill;
        logoLayout.Location = new Point(0, 0);
        logoLayout.Margin = new Padding(0, 0, 0, 18);
        logoLayout.Name = "logoLayout";
        logoLayout.RowCount = 1;
        logoLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
        logoLayout.Size = new Size(396, 84);
        logoLayout.TabIndex = 0;
        // 
        // philippinesLogoPictureBox
        // 
        philippinesLogoPictureBox.Dock = DockStyle.Fill;
        philippinesLogoPictureBox.Location = new Point(0, 0);
        philippinesLogoPictureBox.Margin = new Padding(0);
        philippinesLogoPictureBox.Name = "philippinesLogoPictureBox";
        philippinesLogoPictureBox.Size = new Size(86, 84);
        philippinesLogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        philippinesLogoPictureBox.TabIndex = 0;
        philippinesLogoPictureBox.TabStop = false;
        // 
        // logoDividerLabel
        // 
        logoDividerLabel.AutoSize = true;
        logoDividerLabel.Dock = DockStyle.Fill;
        logoDividerLabel.Location = new Point(86, 0);
        logoDividerLabel.Margin = new Padding(0);
        logoDividerLabel.Name = "logoDividerLabel";
        logoDividerLabel.Size = new Size(224, 84);
        logoDividerLabel.TabIndex = 1;
        logoDividerLabel.Text = "Tubungan Water District Billing System";
        logoDividerLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tubunganLogoPictureBox
        // 
        tubunganLogoPictureBox.Dock = DockStyle.Fill;
        tubunganLogoPictureBox.Location = new Point(310, 0);
        tubunganLogoPictureBox.Margin = new Padding(0);
        tubunganLogoPictureBox.Name = "tubunganLogoPictureBox";
        tubunganLogoPictureBox.Size = new Size(86, 84);
        tubunganLogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        tubunganLogoPictureBox.TabIndex = 2;
        tubunganLogoPictureBox.TabStop = false;
        // 
        // signInLabel
        // 
        signInLabel.AutoSize = true;
        signInLabel.Location = new Point(0, 102);
        signInLabel.Margin = new Padding(0);
        signInLabel.Name = "signInLabel";
        signInLabel.Size = new Size(81, 15);
        signInLabel.TabIndex = 1;
        signInLabel.Text = "Secure Sign In";
        // 
        // signInCaptionLabel
        // 
        signInCaptionLabel.AutoSize = true;
        signInCaptionLabel.Location = new Point(0, 125);
        signInCaptionLabel.Margin = new Padding(0, 8, 0, 0);
        signInCaptionLabel.MaximumSize = new Size(396, 0);
        signInCaptionLabel.Name = "signInCaptionLabel";
        signInCaptionLabel.Size = new Size(230, 15);
        signInCaptionLabel.TabIndex = 2;
        signInCaptionLabel.Text = "Sign in with your username and password.";
        // 
        // usernameLabel
        // 
        usernameLabel.AutoSize = true;
        usernameLabel.Location = new Point(0, 158);
        usernameLabel.Margin = new Padding(0, 18, 0, 0);
        usernameLabel.Name = "usernameLabel";
        usernameLabel.Size = new Size(60, 15);
        usernameLabel.TabIndex = 3;
        usernameLabel.Text = "Username";
        // 
        // usernameTextBox
        // 
        usernameTextBox.Dock = DockStyle.Top;
        usernameTextBox.Location = new Point(0, 179);
        usernameTextBox.Margin = new Padding(0, 6, 0, 0);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new Size(396, 23);
        usernameTextBox.TabIndex = 1;
        usernameTextBox.KeyDown += credentialsTextBox_KeyDown;
        // 
        // passwordLabel
        // 
        passwordLabel.AutoSize = true;
        passwordLabel.Location = new Point(0, 220);
        passwordLabel.Margin = new Padding(0, 18, 0, 0);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Size = new Size(57, 15);
        passwordLabel.TabIndex = 5;
        passwordLabel.Text = "Password";
        // 
        // passwordTextBox
        // 
        passwordTextBox.Dock = DockStyle.Top;
        passwordTextBox.Location = new Point(0, 241);
        passwordTextBox.Margin = new Padding(0, 6, 0, 0);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.Size = new Size(396, 23);
        passwordTextBox.TabIndex = 6;
        passwordTextBox.UseSystemPasswordChar = true;
        passwordTextBox.KeyDown += credentialsTextBox_KeyDown;
        // 
        // showPasswordCheckBox
        // 
        showPasswordCheckBox.AutoSize = true;
        showPasswordCheckBox.Location = new Point(0, 276);
        showPasswordCheckBox.Margin = new Padding(0, 12, 0, 0);
        showPasswordCheckBox.Name = "showPasswordCheckBox";
        showPasswordCheckBox.Size = new Size(108, 19);
        showPasswordCheckBox.TabIndex = 7;
        showPasswordCheckBox.Text = "Show password";
        showPasswordCheckBox.UseVisualStyleBackColor = true;
        showPasswordCheckBox.CheckedChanged += showPasswordCheckBox_CheckedChanged;
        // 
        // loginButton
        // 
        loginButton.Dock = DockStyle.Top;
        loginButton.Location = new Point(0, 309);
        loginButton.Margin = new Padding(0, 14, 0, 0);
        loginButton.Name = "loginButton";
        loginButton.Size = new Size(396, 42);
        loginButton.TabIndex = 8;
        loginButton.Text = "Login";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += loginButton_Click;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(0, 365);
        statusLabel.Margin = new Padding(0, 14, 0, 0);
        statusLabel.MaximumSize = new Size(396, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(0, 15);
        statusLabel.TabIndex = 9;
        // 
        // updateFooterLayout
        // 
        updateFooterLayout.ColumnCount = 2;
        updateFooterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        updateFooterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 146F));
        updateFooterLayout.Controls.Add(versionLabel, 0, 0);
        updateFooterLayout.Controls.Add(checkForUpdatesButton, 1, 0);
        updateFooterLayout.Dock = DockStyle.Fill;
        updateFooterLayout.Location = new Point(0, 388);
        updateFooterLayout.Margin = new Padding(0, 8, 0, 0);
        updateFooterLayout.Name = "updateFooterLayout";
        updateFooterLayout.RowCount = 1;
        updateFooterLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        updateFooterLayout.Size = new Size(396, 27);
        updateFooterLayout.TabIndex = 10;
        // 
        // checkForUpdatesButton
        // 
        checkForUpdatesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        checkForUpdatesButton.AutoSize = true;
        checkForUpdatesButton.Location = new Point(250, 0);
        checkForUpdatesButton.Margin = new Padding(0);
        checkForUpdatesButton.Name = "checkForUpdatesButton";
        checkForUpdatesButton.Size = new Size(146, 27);
        checkForUpdatesButton.TabIndex = 1;
        checkForUpdatesButton.Text = "Check for Updates";
        checkForUpdatesButton.UseVisualStyleBackColor = true;
        checkForUpdatesButton.Click += checkForUpdatesButton_Click;
        // 
        // loginCardPanel
        // 
        loginCardPanel.Anchor = AnchorStyles.None;
        loginCardPanel.AutoSize = true;
        loginCardPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        loginCardPanel.Controls.Add(loginCardLayout);
        loginCardPanel.Location = new Point(200, 50);
        loginCardPanel.Margin = new Padding(0);
        loginCardPanel.MaximumSize = new Size(460, 0);
        loginCardPanel.Name = "loginCardPanel";
        loginCardPanel.Padding = new Padding(32, 30, 32, 30);
        loginCardPanel.Size = new Size(460, 460);
        loginCardPanel.TabIndex = 0;
        // 
        // rootLayout
        // 
        rootLayout.BackgroundImage = (Image)resources.GetObject("rootLayout.BackgroundImage");
        rootLayout.BackgroundImageLayout = ImageLayout.Stretch;
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(loginCardPanel, 0, 1);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(3, 2, 3, 2);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(32);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        rootLayout.Size = new Size(860, 560);
        rootLayout.TabIndex = 0;
        // 
        // versionLabel
        // 
        versionLabel.AutoSize = true;
        versionLabel.Dock = DockStyle.Fill;
        versionLabel.Location = new Point(0, 0);
        versionLabel.Margin = new Padding(0, 6, 0, 0);
        versionLabel.MaximumSize = new Size(396, 0);
        versionLabel.Name = "versionLabel";
        versionLabel.Size = new Size(250, 27);
        versionLabel.TabIndex = 0;
        versionLabel.TextAlign = ContentAlignment.MiddleLeft;
        versionLabel.Text = "version";
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(860, 560);
        Controls.Add(rootLayout);
        Margin = new Padding(3, 2, 3, 2);
        MinimumSize = new Size(560, 500);
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Water District Billing System";
        Load += LoginForm_Load;
        loginCardLayout.ResumeLayout(false);
        loginCardLayout.PerformLayout();
        logoLayout.ResumeLayout(false);
        logoLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)philippinesLogoPictureBox).EndInit();
        ((System.ComponentModel.ISupportInitialize)tubunganLogoPictureBox).EndInit();
        loginCardPanel.ResumeLayout(false);
        loginCardPanel.PerformLayout();
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private TableLayoutPanel loginCardLayout;
    private TableLayoutPanel logoLayout;
    private PictureBox philippinesLogoPictureBox;
    private Label logoDividerLabel;
    private PictureBox tubunganLogoPictureBox;
    private Label signInLabel;
    private Label signInCaptionLabel;
    private Label usernameLabel;
    private TextBox usernameTextBox;
    private Label passwordLabel;
    private TextBox passwordTextBox;
    private CheckBox showPasswordCheckBox;
    private Button loginButton;
    private Label statusLabel;
    private TableLayoutPanel updateFooterLayout;
    private Button checkForUpdatesButton;
    private Panel loginCardPanel;
    private TableLayoutPanel rootLayout;
    private Label versionLabel;
}