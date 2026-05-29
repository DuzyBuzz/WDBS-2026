namespace WDBS_2026.Forms.User;

partial class UserCredentialsForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserCredentialsForm));
        rootLayout = new TableLayoutPanel();
        credentialsCardPanel = new Panel();
        cardLayout = new TableLayoutPanel();
        buttonLayout = new FlowLayoutPanel();
        cancelButton = new Button();
        saveButton = new Button();
        statusLabel = new Label();
        showPasswordsCheckBox = new CheckBox();
        confirmPasswordTextBox = new TextBox();
        confirmPasswordLabel = new Label();
        newPasswordTextBox = new TextBox();
        newPasswordLabel = new Label();
        currentPasswordTextBox = new TextBox();
        currentPasswordLabel = new Label();
        roleTextBox = new TextBox();
        roleLabel = new Label();
        fullNameTextBox = new TextBox();
        fullNameLabel = new Label();
        usernameTextBox = new TextBox();
        usernameLabel = new Label();
        helpLabel = new Label();
        titleLabel = new Label();
        rootLayout.SuspendLayout();
        credentialsCardPanel.SuspendLayout();
        cardLayout.SuspendLayout();
        buttonLayout.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.BackgroundImage = (Image)resources.GetObject("rootLayout.BackgroundImage");
        rootLayout.BackgroundImageLayout = ImageLayout.Stretch;
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(credentialsCardPanel, 0, 1);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(24);
        rootLayout.RowCount = 3;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        rootLayout.Size = new Size(640, 660);
        rootLayout.TabIndex = 0;
        // 
        // credentialsCardPanel
        // 
        credentialsCardPanel.Anchor = AnchorStyles.None;
        credentialsCardPanel.Controls.Add(cardLayout);
        credentialsCardPanel.Location = new Point(68, 44);
        credentialsCardPanel.Margin = new Padding(0);
        credentialsCardPanel.Name = "credentialsCardPanel";
        credentialsCardPanel.Padding = new Padding(28);
        credentialsCardPanel.Size = new Size(504, 572);
        credentialsCardPanel.TabIndex = 0;
        // 
        // cardLayout
        // 
        cardLayout.ColumnCount = 1;
        cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        cardLayout.Controls.Add(buttonLayout, 0, 15);
        cardLayout.Controls.Add(statusLabel, 0, 14);
        cardLayout.Controls.Add(showPasswordsCheckBox, 0, 13);
        cardLayout.Controls.Add(confirmPasswordTextBox, 0, 12);
        cardLayout.Controls.Add(confirmPasswordLabel, 0, 11);
        cardLayout.Controls.Add(newPasswordTextBox, 0, 10);
        cardLayout.Controls.Add(newPasswordLabel, 0, 9);
        cardLayout.Controls.Add(currentPasswordTextBox, 0, 8);
        cardLayout.Controls.Add(currentPasswordLabel, 0, 7);
        cardLayout.Controls.Add(roleTextBox, 0, 6);
        cardLayout.Controls.Add(roleLabel, 0, 5);
        cardLayout.Controls.Add(fullNameTextBox, 0, 4);
        cardLayout.Controls.Add(fullNameLabel, 0, 3);
        cardLayout.Controls.Add(usernameTextBox, 0, 2);
        cardLayout.Controls.Add(usernameLabel, 0, 1);
        cardLayout.Controls.Add(helpLabel, 0, 16);
        cardLayout.Controls.Add(titleLabel, 0, 0);
        cardLayout.Dock = DockStyle.Fill;
        cardLayout.Location = new Point(28, 28);
        cardLayout.Name = "cardLayout";
        cardLayout.RowCount = 17;
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle());
        cardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        cardLayout.Size = new Size(448, 516);
        cardLayout.TabIndex = 0;
        // 
        // buttonLayout
        // 
        buttonLayout.AutoSize = true;
        buttonLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonLayout.Controls.Add(cancelButton);
        buttonLayout.Controls.Add(saveButton);
        buttonLayout.Dock = DockStyle.Fill;
        buttonLayout.FlowDirection = FlowDirection.RightToLeft;
        buttonLayout.Location = new Point(0, 445);
        buttonLayout.Margin = new Padding(0, 12, 0, 0);
        buttonLayout.Name = "buttonLayout";
        buttonLayout.Size = new Size(448, 32);
        buttonLayout.TabIndex = 15;
        buttonLayout.WrapContents = false;
        // 
        // cancelButton
        // 
        cancelButton.Location = new Point(353, 0);
        cancelButton.Margin = new Padding(0);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(95, 32);
        cancelButton.TabIndex = 0;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += cancelButton_Click;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(258, 0);
        saveButton.Margin = new Padding(10, 0, 0, 0);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(95, 32);
        saveButton.TabIndex = 1;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(0, 418);
        statusLabel.Margin = new Padding(0, 14, 0, 0);
        statusLabel.MaximumSize = new Size(448, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(0, 15);
        statusLabel.TabIndex = 14;
        // 
        // showPasswordsCheckBox
        // 
        showPasswordsCheckBox.AutoSize = true;
        showPasswordsCheckBox.Location = new Point(0, 385);
        showPasswordsCheckBox.Margin = new Padding(0, 10, 0, 0);
        showPasswordsCheckBox.Name = "showPasswordsCheckBox";
        showPasswordsCheckBox.Size = new Size(113, 19);
        showPasswordsCheckBox.TabIndex = 13;
        showPasswordsCheckBox.Text = "Show passwords";
        showPasswordsCheckBox.UseVisualStyleBackColor = true;
        showPasswordsCheckBox.CheckedChanged += showPasswordsCheckBox_CheckedChanged;
        // 
        // confirmPasswordTextBox
        // 
        confirmPasswordTextBox.Dock = DockStyle.Top;
        confirmPasswordTextBox.Location = new Point(0, 352);
        confirmPasswordTextBox.Margin = new Padding(0, 6, 0, 0);
        confirmPasswordTextBox.Name = "confirmPasswordTextBox";
        confirmPasswordTextBox.Size = new Size(448, 23);
        confirmPasswordTextBox.TabIndex = 12;
        confirmPasswordTextBox.UseSystemPasswordChar = true;
        // 
        // confirmPasswordLabel
        // 
        confirmPasswordLabel.AutoSize = true;
        confirmPasswordLabel.Location = new Point(0, 331);
        confirmPasswordLabel.Margin = new Padding(0, 16, 0, 0);
        confirmPasswordLabel.Name = "confirmPasswordLabel";
        confirmPasswordLabel.Size = new Size(104, 15);
        confirmPasswordLabel.TabIndex = 11;
        confirmPasswordLabel.Text = "Confirm password";
        // 
        // newPasswordTextBox
        // 
        newPasswordTextBox.Dock = DockStyle.Top;
        newPasswordTextBox.Location = new Point(0, 292);
        newPasswordTextBox.Margin = new Padding(0, 6, 0, 0);
        newPasswordTextBox.Name = "newPasswordTextBox";
        newPasswordTextBox.Size = new Size(448, 23);
        newPasswordTextBox.TabIndex = 10;
        newPasswordTextBox.UseSystemPasswordChar = true;
        // 
        // newPasswordLabel
        // 
        newPasswordLabel.AutoSize = true;
        newPasswordLabel.Location = new Point(0, 271);
        newPasswordLabel.Margin = new Padding(0, 16, 0, 0);
        newPasswordLabel.Name = "newPasswordLabel";
        newPasswordLabel.Size = new Size(84, 15);
        newPasswordLabel.TabIndex = 9;
        newPasswordLabel.Text = "New password";
        // 
        // currentPasswordTextBox
        // 
        currentPasswordTextBox.Dock = DockStyle.Top;
        currentPasswordTextBox.Location = new Point(0, 232);
        currentPasswordTextBox.Margin = new Padding(0, 6, 0, 0);
        currentPasswordTextBox.Name = "currentPasswordTextBox";
        currentPasswordTextBox.Size = new Size(448, 23);
        currentPasswordTextBox.TabIndex = 8;
        currentPasswordTextBox.UseSystemPasswordChar = true;
        // 
        // currentPasswordLabel
        // 
        currentPasswordLabel.AutoSize = true;
        currentPasswordLabel.Location = new Point(0, 211);
        currentPasswordLabel.Margin = new Padding(0, 16, 0, 0);
        currentPasswordLabel.Name = "currentPasswordLabel";
        currentPasswordLabel.Size = new Size(100, 15);
        currentPasswordLabel.TabIndex = 7;
        currentPasswordLabel.Text = "Current password";
        // 
        // roleTextBox
        // 
        roleTextBox.Dock = DockStyle.Top;
        roleTextBox.Location = new Point(0, 172);
        roleTextBox.Margin = new Padding(0, 6, 0, 0);
        roleTextBox.Name = "roleTextBox";
        roleTextBox.ReadOnly = true;
        roleTextBox.Size = new Size(448, 23);
        roleTextBox.TabIndex = 6;
        // 
        // roleLabel
        // 
        roleLabel.AutoSize = true;
        roleLabel.Location = new Point(0, 151);
        roleLabel.Margin = new Padding(0, 16, 0, 0);
        roleLabel.Name = "roleLabel";
        roleLabel.Size = new Size(30, 15);
        roleLabel.TabIndex = 5;
        roleLabel.Text = "Role";
        // 
        // fullNameTextBox
        // 
        fullNameTextBox.Dock = DockStyle.Top;
        fullNameTextBox.Location = new Point(0, 112);
        fullNameTextBox.Margin = new Padding(0, 6, 0, 0);
        fullNameTextBox.Name = "fullNameTextBox";
        fullNameTextBox.Size = new Size(448, 23);
        fullNameTextBox.TabIndex = 4;
        // 
        // fullNameLabel
        // 
        fullNameLabel.AutoSize = true;
        fullNameLabel.Location = new Point(0, 91);
        fullNameLabel.Margin = new Padding(0, 16, 0, 0);
        fullNameLabel.Name = "fullNameLabel";
        fullNameLabel.Size = new Size(59, 15);
        fullNameLabel.TabIndex = 3;
        fullNameLabel.Text = "Full name";
        // 
        // usernameTextBox
        // 
        usernameTextBox.Dock = DockStyle.Top;
        usernameTextBox.Location = new Point(0, 52);
        usernameTextBox.Margin = new Padding(0, 6, 0, 0);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.Size = new Size(448, 23);
        usernameTextBox.TabIndex = 2;
        // 
        // usernameLabel
        // 
        usernameLabel.AutoSize = true;
        usernameLabel.Location = new Point(0, 31);
        usernameLabel.Margin = new Padding(0, 16, 0, 0);
        usernameLabel.Name = "usernameLabel";
        usernameLabel.Size = new Size(60, 15);
        usernameLabel.TabIndex = 1;
        usernameLabel.Text = "Username";
        // 
        // helpLabel
        // 
        helpLabel.AutoSize = true;
        helpLabel.Location = new Point(0, 489);
        helpLabel.Margin = new Padding(0, 12, 0, 0);
        helpLabel.MaximumSize = new Size(448, 0);
        helpLabel.Name = "helpLabel";
        helpLabel.Size = new Size(255, 15);
        helpLabel.TabIndex = 16;
        helpLabel.Text = "Role is read-only. Current password is required.";
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(0, 0);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(92, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "User Credentials";
        // 
        // UserCredentialsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(640, 660);
        Controls.Add(rootLayout);
        MinimumSize = new Size(580, 620);
        Name = "UserCredentialsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "User Credentials";
        rootLayout.ResumeLayout(false);
        credentialsCardPanel.ResumeLayout(false);
        cardLayout.ResumeLayout(false);
        cardLayout.PerformLayout();
        buttonLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Panel credentialsCardPanel;
    private TableLayoutPanel cardLayout;
    private Label titleLabel;
    private Label usernameLabel;
    private TextBox usernameTextBox;
    private Label fullNameLabel;
    private TextBox fullNameTextBox;
    private Label roleLabel;
    private TextBox roleTextBox;
    private Label currentPasswordLabel;
    private TextBox currentPasswordTextBox;
    private Label newPasswordLabel;
    private TextBox newPasswordTextBox;
    private Label confirmPasswordLabel;
    private TextBox confirmPasswordTextBox;
    private CheckBox showPasswordsCheckBox;
    private Label statusLabel;
    private FlowLayoutPanel buttonLayout;
    private Button saveButton;
    private Button cancelButton;
    private Label helpLabel;
}