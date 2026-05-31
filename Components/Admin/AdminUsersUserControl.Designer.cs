namespace WDBS_2026.Components.Admin
{
    partial class AdminUsersUserControl
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Panel gridPanel;
        private TableLayoutPanel gridLayout;
        private Label usersLabel;
        private FlowLayoutPanel searchLayout;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button addUserButton;
        private Button deleteUserButton;
        private Button refreshButton;
        private DataGridView usersGrid;
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
            gridPanel = new Panel();
            gridLayout = new TableLayoutPanel();
            usersLabel = new Label();
            searchLayout = new FlowLayoutPanel();
            searchTextBox = new TextBox();
            searchButton = new Button();
            addUserButton = new Button();
            deleteUserButton = new Button();
            refreshButton = new Button();
            usersGrid = new DataGridView();
            statusLabel = new Label();
            rootLayout.SuspendLayout();
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
            rootLayout.Controls.Add(gridPanel, 0, 1);
            rootLayout.Controls.Add(statusLabel, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(0, 4, 0, 0);
            rootLayout.RowCount = 3;
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
            // gridPanel
            // 
            gridPanel.Controls.Add(gridLayout);
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.Location = new Point(0, 31);
            gridPanel.Margin = new Padding(0, 12, 0, 0);
            gridPanel.Name = "gridPanel";
            gridPanel.Padding = new Padding(12);
            gridPanel.Size = new Size(1120, 677);
            gridPanel.TabIndex = 1;
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
            gridLayout.Size = new Size(1096, 653);
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
            searchLayout.Controls.Add(addUserButton);
            searchLayout.Controls.Add(deleteUserButton);
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
            // addUserButton
            // 
            addUserButton.Location = new Point(432, 0);
            addUserButton.Margin = new Padding(0, 0, 8, 0);
            addUserButton.Name = "addUserButton";
            addUserButton.Size = new Size(112, 32);
            addUserButton.TabIndex = 2;
            addUserButton.Text = "Add User";
            addUserButton.UseVisualStyleBackColor = true;
            addUserButton.Click += addUserButton_Click;
            // 
            // deleteUserButton
            // 
            deleteUserButton.Location = new Point(552, 0);
            deleteUserButton.Margin = new Padding(0, 0, 8, 0);
            deleteUserButton.Name = "deleteUserButton";
            deleteUserButton.Size = new Size(112, 32);
            deleteUserButton.TabIndex = 3;
            deleteUserButton.Text = "Delete User";
            deleteUserButton.UseVisualStyleBackColor = true;
            deleteUserButton.Click += deleteUserButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(672, 0);
            refreshButton.Margin = new Padding(0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(96, 32);
            refreshButton.TabIndex = 4;
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
            usersGrid.Size = new Size(1096, 585);
            usersGrid.TabIndex = 2;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(0, 708);
            statusLabel.Margin = new Padding(0, 3, 0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(120, 15);
            statusLabel.TabIndex = 2;
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
            gridPanel.ResumeLayout(false);
            gridLayout.ResumeLayout(false);
            gridLayout.PerformLayout();
            searchLayout.ResumeLayout(false);
            searchLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)usersGrid).EndInit();
            ResumeLayout(false);
        }
    }
}
