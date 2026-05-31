namespace WDBS_2026.Forms.User
{
    partial class UserUpserForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private TableLayoutPanel formLayout;
        private Label usernameLabel;
        private TextBox usernameTextBox;
        private Label fullNameLabel;
        private TextBox fullNameTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Label roleLabel;
        private ComboBox roleComboBox;
        private Label isActiveLabel;
        private CheckBox isActiveCheckBox;
        private FlowLayoutPanel buttonLayout;
        private Button saveButton;
        private Button cancelButton;
        private Label statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            titleLabel = new Label();
            formLayout = new TableLayoutPanel();
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            fullNameLabel = new Label();
            fullNameTextBox = new TextBox();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            roleLabel = new Label();
            roleComboBox = new ComboBox();
            isActiveLabel = new Label();
            isActiveCheckBox = new CheckBox();
            buttonLayout = new FlowLayoutPanel();
            saveButton = new Button();
            cancelButton = new Button();
            statusLabel = new Label();
            rootLayout.SuspendLayout();
            formLayout.SuspendLayout();
            buttonLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(formLayout, 0, 1);
            rootLayout.Controls.Add(buttonLayout, 0, 2);
            rootLayout.Controls.Add(statusLabel, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(16);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.Size = new Size(640, 360);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(16, 16);
            titleLabel.Margin = new Padding(0, 0, 0, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(77, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Add User";
            // 
            // formLayout
            // 
            formLayout.ColumnCount = 2;
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            formLayout.Controls.Add(usernameLabel, 0, 0);
            formLayout.Controls.Add(usernameTextBox, 1, 0);
            formLayout.Controls.Add(fullNameLabel, 0, 1);
            formLayout.Controls.Add(fullNameTextBox, 1, 1);
            formLayout.Controls.Add(passwordLabel, 0, 2);
            formLayout.Controls.Add(passwordTextBox, 1, 2);
            formLayout.Controls.Add(roleLabel, 0, 3);
            formLayout.Controls.Add(roleComboBox, 1, 3);
            formLayout.Controls.Add(isActiveLabel, 0, 4);
            formLayout.Controls.Add(isActiveCheckBox, 1, 4);
            formLayout.Dock = DockStyle.Fill;
            formLayout.Location = new Point(16, 43);
            formLayout.Margin = new Padding(0, 0, 0, 12);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 5;
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.Size = new Size(608, 225);
            formLayout.TabIndex = 1;
            // 
            // usernameLabel
            // 
            usernameLabel.Anchor = AnchorStyles.Left;
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(0, 7);
            usernameLabel.Margin = new Padding(0, 0, 12, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(63, 15);
            usernameLabel.TabIndex = 0;
            usernameLabel.Text = "Username";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Dock = DockStyle.Fill;
            usernameTextBox.Location = new Point(83, 0);
            usernameTextBox.Margin = new Padding(0, 0, 0, 8);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(525, 23);
            usernameTextBox.TabIndex = 1;
            // 
            // fullNameLabel
            // 
            fullNameLabel.Anchor = AnchorStyles.Left;
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new Point(0, 38);
            fullNameLabel.Margin = new Padding(0, 0, 12, 0);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new Size(61, 15);
            fullNameLabel.TabIndex = 2;
            fullNameLabel.Text = "Full Name";
            // 
            // fullNameTextBox
            // 
            fullNameTextBox.Dock = DockStyle.Fill;
            fullNameTextBox.Location = new Point(83, 31);
            fullNameTextBox.Margin = new Padding(0, 0, 0, 8);
            fullNameTextBox.Name = "fullNameTextBox";
            fullNameTextBox.Size = new Size(525, 23);
            fullNameTextBox.TabIndex = 3;
            // 
            // passwordLabel
            // 
            passwordLabel.Anchor = AnchorStyles.Left;
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(0, 69);
            passwordLabel.Margin = new Padding(0, 0, 12, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(57, 15);
            passwordLabel.TabIndex = 4;
            passwordLabel.Text = "Password";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Dock = DockStyle.Fill;
            passwordTextBox.Location = new Point(83, 62);
            passwordTextBox.Margin = new Padding(0, 0, 0, 8);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(525, 23);
            passwordTextBox.TabIndex = 5;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // roleLabel
            // 
            roleLabel.Anchor = AnchorStyles.Left;
            roleLabel.AutoSize = true;
            roleLabel.Location = new Point(0, 100);
            roleLabel.Margin = new Padding(0, 0, 12, 0);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new Size(30, 15);
            roleLabel.TabIndex = 6;
            roleLabel.Text = "Role";
            // 
            // roleComboBox
            // 
            roleComboBox.Dock = DockStyle.Fill;
            roleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            roleComboBox.FormattingEnabled = true;
            roleComboBox.Location = new Point(83, 93);
            roleComboBox.Margin = new Padding(0, 0, 0, 8);
            roleComboBox.Name = "roleComboBox";
            roleComboBox.Size = new Size(525, 23);
            roleComboBox.TabIndex = 7;
            // 
            // isActiveLabel
            // 
            isActiveLabel.Anchor = AnchorStyles.Left;
            isActiveLabel.AutoSize = true;
            isActiveLabel.Location = new Point(0, 131);
            isActiveLabel.Margin = new Padding(0, 0, 12, 0);
            isActiveLabel.Name = "isActiveLabel";
            isActiveLabel.Size = new Size(41, 15);
            isActiveLabel.TabIndex = 8;
            isActiveLabel.Text = "Active";
            // 
            // isActiveCheckBox
            // 
            isActiveCheckBox.Anchor = AnchorStyles.Left;
            isActiveCheckBox.AutoSize = true;
            isActiveCheckBox.Checked = true;
            isActiveCheckBox.CheckState = CheckState.Checked;
            isActiveCheckBox.Location = new Point(83, 130);
            isActiveCheckBox.Margin = new Padding(0, 0, 0, 8);
            isActiveCheckBox.Name = "isActiveCheckBox";
            isActiveCheckBox.Size = new Size(15, 14);
            isActiveCheckBox.TabIndex = 9;
            isActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // buttonLayout
            // 
            buttonLayout.AutoSize = true;
            buttonLayout.Controls.Add(saveButton);
            buttonLayout.Controls.Add(cancelButton);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(16, 280);
            buttonLayout.Margin = new Padding(0, 0, 0, 8);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.Size = new Size(608, 40);
            buttonLayout.TabIndex = 2;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(0, 0);
            saveButton.Margin = new Padding(0, 0, 8, 0);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(112, 32);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(120, 0);
            cancelButton.Margin = new Padding(0);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(112, 32);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(16, 328);
            statusLabel.Margin = new Padding(0, 0, 0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            statusLabel.TabIndex = 3;
            // 
            // UserUpserForm
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(640, 360);
            Controls.Add(rootLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserUpserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add User";
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            formLayout.ResumeLayout(false);
            formLayout.PerformLayout();
            buttonLayout.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}