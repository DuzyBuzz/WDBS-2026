namespace WDBS_2026.Components.Admin
{
    partial class AdminUsersUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            titleLabel = new Label();
            formPanel = new Panel();
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
            addUserButton = new Button();
            updateUserButton = new Button();
            deleteUserButton = new Button();
            clearFormButton = new Button();
            gridPanel = new Panel();
            gridLayout = new TableLayoutPanel();
            usersLabel = new Label();
            searchLayout = new FlowLayoutPanel();
            searchTextBox = new TextBox();
            searchButton = new Button();
            refreshButton = new Button();
            usersGrid = new DataGridView();
            statusLabel = new Label();
            rootLayout.SuspendLayout();
            formPanel.SuspendLayout();
            formLayout.SuspendLayout();
            buttonLayout.SuspendLayout();
            gridPanel.SuspendLayout();
            gridLayout.SuspendLayout();
            searchLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)usersGrid).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(formPanel, 0, 1);
            rootLayout.Controls.Add(gridPanel, 0, 2);
            rootLayout.Controls.Add(statusLabel, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(0, 4, 0, 0);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.Size = new Size(1120, 720);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(0, 4);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(80, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Admin Users";
            // 
            // formPanel
            // 
            formPanel.Controls.Add(formLayout);
            formPanel.Dock = DockStyle.Fill;
            formPanel.Location = new Point(0, 31);
            formPanel.Margin = new Padding(0, 12, 0, 0);
            formPanel.Name = "formPanel";
            formPanel.Padding = new Padding(12);
            formPanel.Size = new Size(1120, 109);
            formPanel.TabIndex = 1;
            // 
            // formLayout
            // 
            formLayout.ColumnCount = 10;
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.ColumnStyles.Add(new ColumnStyle());
            formLayout.Controls.Add(usernameLabel, 0, 0);
            formLayout.Controls.Add(usernameTextBox, 1, 0);
            formLayout.Controls.Add(fullNameLabel, 2, 0);
            formLayout.Controls.Add(fullNameTextBox, 3, 0);
            formLayout.Controls.Add(passwordLabel, 4, 0);
            formLayout.Controls.Add(passwordTextBox, 5, 0);
            formLayout.Controls.Add(roleLabel, 6, 0);
            formLayout.Controls.Add(roleComboBox, 7, 0);
            formLayout.Controls.Add(isActiveLabel, 8, 0);
            formLayout.Controls.Add(isActiveCheckBox, 9, 0);
            formLayout.Controls.Add(buttonLayout, 0, 1);
            formLayout.Dock = DockStyle.Fill;
            formLayout.Location = new Point(12, 12);
            formLayout.Margin = new Padding(0);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 2;
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.RowStyles.Add(new RowStyle());
            formLayout.Size = new Size(1096, 85);
            formLayout.TabIndex = 0;
            // 
            // usernameLabel
            // 
            usernameLabel.Anchor = AnchorStyles.Left;
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(0, 7);
            usernameLabel.Margin = new Padding(0, 0, 8, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(63, 15);
            usernameLabel.TabIndex = 0;
            usernameLabel.Text = "Username";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Dock = DockStyle.Fill;
            usernameTextBox.Location = new Point(71, 0);
            usernameTextBox.Margin = new Padding(0, 0, 12, 0);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(212, 23);
            usernameTextBox.TabIndex = 1;
            // 
            // fullNameLabel
            // 
            fullNameLabel.Anchor = AnchorStyles.Left;
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new Point(295, 7);
            fullNameLabel.Margin = new Padding(0, 0, 8, 0);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new Size(61, 15);
            fullNameLabel.TabIndex = 2;
            fullNameLabel.Text = "Full Name";
            // 
            // fullNameTextBox
            // 
            fullNameTextBox.Dock = DockStyle.Fill;
            fullNameTextBox.Location = new Point(364, 0);
            fullNameTextBox.Margin = new Padding(0, 0, 12, 0);
            fullNameTextBox.Name = "fullNameTextBox";
            fullNameTextBox.Size = new Size(212, 23);
            fullNameTextBox.TabIndex = 3;
            // 
            // passwordLabel
            // 
            passwordLabel.Anchor = AnchorStyles.Left;
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(588, 7);
            passwordLabel.Margin = new Padding(0, 0, 8, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(57, 15);
            passwordLabel.TabIndex = 4;
            passwordLabel.Text = "Password";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Dock = DockStyle.Fill;
            passwordTextBox.Location = new Point(653, 0);
            passwordTextBox.Margin = new Padding(0, 0, 12, 0);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(212, 23);
            passwordTextBox.TabIndex = 5;
            // 
            // roleLabel
            // 
            roleLabel.Anchor = AnchorStyles.Left;
            roleLabel.AutoSize = true;
            roleLabel.Location = new Point(877, 7);
            roleLabel.Margin = new Padding(0, 0, 8, 0);
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
            roleComboBox.Location = new Point(915, 0);
            roleComboBox.Margin = new Padding(0, 0, 12, 0);
            roleComboBox.Name = "roleComboBox";
            roleComboBox.Size = new Size(145, 23);
            roleComboBox.TabIndex = 7;
            // 
            // isActiveLabel
            // 
            isActiveLabel.Anchor = AnchorStyles.Left;
            isActiveLabel.AutoSize = true;
            isActiveLabel.Location = new Point(1072, 7);
            isActiveLabel.Margin = new Padding(0, 0, 8, 0);
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
            isActiveCheckBox.Location = new Point(1121, 7);
            isActiveCheckBox.Margin = new Padding(0);
            isActiveCheckBox.Name = "isActiveCheckBox";
            isActiveCheckBox.Size = new Size(15, 14);
            isActiveCheckBox.TabIndex = 9;
            isActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // buttonLayout
            // 
            formLayout.SetColumnSpan(buttonLayout, 10);
            buttonLayout.Controls.Add(addUserButton);
            buttonLayout.Controls.Add(updateUserButton);
            buttonLayout.Controls.Add(deleteUserButton);
            buttonLayout.Controls.Add(clearFormButton);
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.Location = new Point(0, 33);
            buttonLayout.Margin = new Padding(0, 10, 0, 0);
            buttonLayout.Name = "buttonLayout";
            buttonLayout.Size = new Size(1136, 52);
            buttonLayout.TabIndex = 10;
            // 
            // addUserButton
            // 
            addUserButton.Location = new Point(0, 0);
            addUserButton.Margin = new Padding(0, 0, 8, 0);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(112, 32);
            addUserButton.TabIndex = 0;
            addUserButton.Text = "Add User";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += addUserButton_Click;
            // 
            // updateUserButton
            // 
            updateUserButton.Location = new Point(120, 0);
            updateUserButton.Margin = new Padding(0, 0, 8, 0);
            updateUserButton.Name = "updateUserButton";
            updateUserButton.Size = new Size(112, 32);
            updateUserButton.TabIndex = 1;
            updateUserButton.Text = "Update User";
            updateUserButton.UseVisualStyleBackColor = true;
            updateUserButton.Click += updateUserButton_Click;
            // 
            // deleteUserButton
            // 
            deleteUserButton.Location = new Point(240, 0);
            deleteUserButton.Margin = new Padding(0, 0, 8, 0);
            deleteUserButton.Name = "deleteUserButton";
            deleteUserButton.Size = new Size(112, 32);
            deleteUserButton.TabIndex = 2;
            deleteUserButton.Text = "Delete User";
            deleteUserButton.UseVisualStyleBackColor = true;
            deleteUserButton.Click += deleteUserButton_Click;
            // 
            // clearFormButton
            // 
            clearFormButton.Location = new Point(360, 0);
            clearFormButton.Margin = new Padding(0);
            clearFormButton.Name = "clearFormButton";
            clearFormButton.Size = new Size(112, 32);
            clearFormButton.TabIndex = 3;
            clearFormButton.Text = "Clear Form";
            clearFormButton.UseVisualStyleBackColor = true;
            clearFormButton.Click += clearFormButton_Click;
            // 
            // gridPanel
            // 
            gridPanel.Controls.Add(gridLayout);
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.Location = new Point(0, 152);
            gridPanel.Margin = new Padding(0, 12, 0, 0);
            gridPanel.Name = "gridPanel";
            gridPanel.Padding = new Padding(12);
            gridPanel.Size = new Size(1120, 544);
            gridPanel.TabIndex = 2;
            // 
            // gridLayout
            // 
            gridLayout.ColumnCount = 1;
            gridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            gridLayout.Controls.Add(usersLabel, 0, 0);
            gridLayout.Controls.Add(searchLayout, 0, 1);
            gridLayout.Controls.Add(usersGrid, 0, 2);
            gridLayout.Dock = DockStyle.Fill;
            gridLayout.Location = new Point(12, 12);
            gridLayout.Margin = new Padding(0);
            gridLayout.Name = "gridLayout";
            gridLayout.RowCount = 3;
            gridLayout.RowStyles.Add(new RowStyle());
            gridLayout.RowStyles.Add(new RowStyle());
            gridLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            gridLayout.Size = new Size(1096, 520);
            gridLayout.TabIndex = 0;
            // 
            // usersLabel
            // 
            usersLabel.AutoSize = true;
            usersLabel.Location = new Point(0, 0);
            usersLabel.Margin = new Padding(0);
            usersLabel.Name = "usersLabel";
            usersLabel.Size = new Size(35, 15);
            usersLabel.TabIndex = 0;
            usersLabel.Text = "Users";
            // 
            // searchLayout
            // 
            searchLayout.Controls.Add(searchTextBox);
            searchLayout.Controls.Add(searchButton);
            searchLayout.Controls.Add(refreshButton);
            searchLayout.Dock = DockStyle.Fill;
            searchLayout.Location = new Point(0, 23);
            searchLayout.Margin = new Padding(0, 8, 0, 0);
            searchLayout.Name = "searchLayout";
            searchLayout.Size = new Size(1096, 33);
            searchLayout.TabIndex = 1;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(0, 4);
            searchTextBox.Margin = new Padding(0, 4, 8, 0);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search username or full name...";
            searchTextBox.Size = new Size(320, 23);
            searchTextBox.TabIndex = 0;
            searchTextBox.KeyDown += searchTextBox_KeyDown;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(328, 0);
            searchButton.Margin = new Padding(0, 0, 8, 0);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(96, 32);
            searchButton.TabIndex = 1;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(432, 0);
            refreshButton.Margin = new Padding(0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(96, 32);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // usersGrid
            // 
            usersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usersGrid.Dock = DockStyle.Fill;
            usersGrid.Location = new Point(0, 68);
            usersGrid.Margin = new Padding(0, 12, 0, 0);
            usersGrid.Name = "usersGrid";
            usersGrid.Size = new Size(1096, 452);
            usersGrid.TabIndex = 2;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(0, 699);
            statusLabel.Margin = new Padding(0, 3, 0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(120, 15);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Manage user accounts.";
            // 
            // AdminUsersUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "AdminUsersUserControl";
            Size = new Size(1120, 720);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            formPanel.ResumeLayout(false);
            formLayout.ResumeLayout(false);
            formLayout.PerformLayout();
            buttonLayout.ResumeLayout(false);
            gridPanel.ResumeLayout(false);
            gridLayout.ResumeLayout(false);
            gridLayout.PerformLayout();
            searchLayout.ResumeLayout(false);
            searchLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)usersGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Panel formPanel;
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
        private Button addUserButton;
        private Button updateUserButton;
        private Button deleteUserButton;
        private Button clearFormButton;
        private Panel gridPanel;
        private TableLayoutPanel gridLayout;
        private Label usersLabel;
        private FlowLayoutPanel searchLayout;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button refreshButton;
        private DataGridView usersGrid;
        private Label statusLabel;
    }
}
