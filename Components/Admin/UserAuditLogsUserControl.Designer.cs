namespace WDBS_2026.Components.Admin
{
    partial class UserAuditLogsUserControl
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
            filterLayout = new TableLayoutPanel();
            userSearchLabel = new Label();
            userSearchTextBox = new TextBox();
            roleFilterLabel = new Label();
            roleFilterComboBox = new ComboBox();
            actionTypeFilterLabel = new Label();
            actionTypeFilterComboBox = new ComboBox();
            moduleFilterLabel = new Label();
            moduleFilterComboBox = new ComboBox();
            severityFilterLabel = new Label();
            severityFilterComboBox = new ComboBox();
            dateFromLabel = new Label();
            dateFromPicker = new DateTimePicker();
            dateToLabel = new Label();
            dateToPicker = new DateTimePicker();
            buttonPanel = new FlowLayoutPanel();
            refreshButton = new Button();
            printButton = new Button();
            clearButton = new Button();
            searchButton = new Button();
            auditLogsGrid = new DataGridView();
            footerLayout = new TableLayoutPanel();
            statusLabel = new Label();
            paginationLayout = new TableLayoutPanel();
            previousPageButton = new Button();
            pageInfoLabel = new Label();
            nextPageButton = new Button();
            rootLayout.SuspendLayout();
            filterLayout.SuspendLayout();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)auditLogsGrid).BeginInit();
            footerLayout.SuspendLayout();
            paginationLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(filterLayout, 0, 1);
            rootLayout.Controls.Add(auditLogsGrid, 0, 2);
            rootLayout.Controls.Add(footerLayout, 0, 3);
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
            rootLayout.Size = new Size(1100, 700);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(0, 4);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(90, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "User Audit Logs";
            // 
            // filterLayout
            // 
            filterLayout.ColumnCount = 15;
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filterLayout.Controls.Add(userSearchLabel, 0, 0);
            filterLayout.Controls.Add(userSearchTextBox, 1, 0);
            filterLayout.Controls.Add(roleFilterLabel, 2, 0);
            filterLayout.Controls.Add(roleFilterComboBox, 3, 0);
            filterLayout.Controls.Add(actionTypeFilterLabel, 4, 0);
            filterLayout.Controls.Add(actionTypeFilterComboBox, 5, 0);
            filterLayout.Controls.Add(moduleFilterLabel, 6, 0);
            filterLayout.Controls.Add(moduleFilterComboBox, 7, 0);
            filterLayout.Controls.Add(severityFilterLabel, 8, 0);
            filterLayout.Controls.Add(severityFilterComboBox, 9, 0);
            filterLayout.Controls.Add(dateFromLabel, 10, 0);
            filterLayout.Controls.Add(dateFromPicker, 11, 0);
            filterLayout.Controls.Add(dateToLabel, 12, 0);
            filterLayout.Controls.Add(dateToPicker, 13, 0);
            filterLayout.Controls.Add(buttonPanel, 14, 0);
            filterLayout.Dock = DockStyle.Fill;
            filterLayout.Location = new Point(0, 27);
            filterLayout.Margin = new Padding(0, 8, 0, 0);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 1;
            filterLayout.RowStyles.Add(new RowStyle());
            filterLayout.Size = new Size(1100, 30);
            filterLayout.TabIndex = 1;
            // 
            // userSearchLabel
            // 
            userSearchLabel.Anchor = AnchorStyles.Left;
            userSearchLabel.AutoSize = true;
            userSearchLabel.Location = new Point(0, 7);
            userSearchLabel.Margin = new Padding(0, 0, 6, 0);
            userSearchLabel.Name = "userSearchLabel";
            userSearchLabel.Size = new Size(71, 15);
            userSearchLabel.TabIndex = 0;
            userSearchLabel.Text = "User Search";
            // 
            // userSearchTextBox
            // 
            userSearchTextBox.Dock = DockStyle.Fill;
            userSearchTextBox.Location = new Point(77, 0);
            userSearchTextBox.Margin = new Padding(0, 0, 8, 0);
            userSearchTextBox.Name = "userSearchTextBox";
            userSearchTextBox.PlaceholderText = "Username or Full Name";
            userSearchTextBox.Size = new Size(162, 23);
            userSearchTextBox.TabIndex = 1;
            // 
            // roleFilterLabel
            // 
            roleFilterLabel.Anchor = AnchorStyles.Left;
            roleFilterLabel.AutoSize = true;
            roleFilterLabel.Location = new Point(277, 7);
            roleFilterLabel.Margin = new Padding(0, 0, 6, 0);
            roleFilterLabel.Name = "roleFilterLabel";
            roleFilterLabel.Size = new Size(30, 15);
            roleFilterLabel.TabIndex = 2;
            roleFilterLabel.Text = "Role";
            // 
            // roleFilterComboBox
            // 
            roleFilterComboBox.Dock = DockStyle.Fill;
            roleFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            roleFilterComboBox.FormattingEnabled = true;
            roleFilterComboBox.Location = new Point(313, 0);
            roleFilterComboBox.Margin = new Padding(0, 0, 8, 0);
            roleFilterComboBox.Name = "roleFilterComboBox";
            roleFilterComboBox.Size = new Size(82, 23);
            roleFilterComboBox.TabIndex = 3;
            // 
            // actionTypeFilterLabel
            // 
            actionTypeFilterLabel.Anchor = AnchorStyles.Left;
            actionTypeFilterLabel.AutoSize = true;
            actionTypeFilterLabel.Location = new Point(433, 7);
            actionTypeFilterLabel.Margin = new Padding(0, 0, 6, 0);
            actionTypeFilterLabel.Name = "actionTypeFilterLabel";
            actionTypeFilterLabel.Size = new Size(70, 15);
            actionTypeFilterLabel.TabIndex = 4;
            actionTypeFilterLabel.Text = "Action Type";
            // 
            // actionTypeFilterComboBox
            // 
            actionTypeFilterComboBox.Dock = DockStyle.Fill;
            actionTypeFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            actionTypeFilterComboBox.FormattingEnabled = true;
            actionTypeFilterComboBox.Location = new Point(509, 0);
            actionTypeFilterComboBox.Margin = new Padding(0, 0, 8, 0);
            actionTypeFilterComboBox.Name = "actionTypeFilterComboBox";
            actionTypeFilterComboBox.Size = new Size(92, 23);
            actionTypeFilterComboBox.TabIndex = 5;
            // 
            // moduleFilterLabel
            // 
            moduleFilterLabel.Anchor = AnchorStyles.Left;
            moduleFilterLabel.AutoSize = true;
            moduleFilterLabel.Location = new Point(629, 7);
            moduleFilterLabel.Margin = new Padding(0, 0, 6, 0);
            moduleFilterLabel.Name = "moduleFilterLabel";
            moduleFilterLabel.Size = new Size(48, 15);
            moduleFilterLabel.TabIndex = 6;
            moduleFilterLabel.Text = "Module";
            // 
            // moduleFilterComboBox
            // 
            moduleFilterComboBox.Dock = DockStyle.Fill;
            moduleFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            moduleFilterComboBox.FormattingEnabled = true;
            moduleFilterComboBox.Location = new Point(683, 0);
            moduleFilterComboBox.Margin = new Padding(0, 0, 8, 0);
            moduleFilterComboBox.Name = "moduleFilterComboBox";
            moduleFilterComboBox.Size = new Size(82, 23);
            moduleFilterComboBox.TabIndex = 7;
            // 
            // severityFilterLabel
            // 
            severityFilterLabel.Anchor = AnchorStyles.Left;
            severityFilterLabel.AutoSize = true;
            severityFilterLabel.Location = new Point(793, 7);
            severityFilterLabel.Margin = new Padding(0, 0, 6, 0);
            severityFilterLabel.Name = "severityFilterLabel";
            severityFilterLabel.Size = new Size(46, 15);
            severityFilterLabel.TabIndex = 10;
            severityFilterLabel.Text = "Severity";
            // 
            // severityFilterComboBox
            // 
            severityFilterComboBox.Dock = DockStyle.Fill;
            severityFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            severityFilterComboBox.FormattingEnabled = true;
            severityFilterComboBox.Location = new Point(845, 0);
            severityFilterComboBox.Margin = new Padding(0, 0, 8, 0);
            severityFilterComboBox.Name = "severityFilterComboBox";
            severityFilterComboBox.Size = new Size(82, 23);
            severityFilterComboBox.TabIndex = 11;
            // 
            // dateFromLabel
            // 
            dateFromLabel.Anchor = AnchorStyles.Left;
            dateFromLabel.AutoSize = true;
            dateFromLabel.Location = new Point(935, 7);
            dateFromLabel.Margin = new Padding(0, 0, 6, 0);
            dateFromLabel.Name = "dateFromLabel";
            dateFromLabel.Size = new Size(38, 15);
            dateFromLabel.TabIndex = 12;
            dateFromLabel.Text = "From:";
            // 
            // dateFromPicker
            // 
            dateFromPicker.CustomFormat = "yyyy-MM-dd";
            dateFromPicker.Format = DateTimePickerFormat.Custom;
            dateFromPicker.Location = new Point(979, 0);
            dateFromPicker.Margin = new Padding(0, 0, 8, 0);
            dateFromPicker.Name = "dateFromPicker";
            dateFromPicker.Size = new Size(100, 23);
            dateFromPicker.TabIndex = 13;
            // 
            // dateToLabel
            // 
            dateToLabel.Anchor = AnchorStyles.Left;
            dateToLabel.AutoSize = true;
            dateToLabel.Location = new Point(1087, 7);
            dateToLabel.Margin = new Padding(0, 0, 6, 0);
            dateToLabel.Name = "dateToLabel";
            dateToLabel.Size = new Size(22, 15);
            dateToLabel.TabIndex = 14;
            dateToLabel.Text = "To:";
            // 
            // dateToPicker
            // 
            dateToPicker.CustomFormat = "yyyy-MM-dd";
            dateToPicker.Format = DateTimePickerFormat.Custom;
            dateToPicker.Location = new Point(1115, 0);
            dateToPicker.Margin = new Padding(0, 0, 8, 0);
            dateToPicker.Name = "dateToPicker";
            dateToPicker.Size = new Size(100, 23);
            dateToPicker.TabIndex = 15;
            // 
            // buttonPanel
            // 
            buttonPanel.AutoSize = true;
            buttonPanel.Controls.Add(refreshButton);
            buttonPanel.Controls.Add(printButton);
            buttonPanel.Controls.Add(clearButton);
            buttonPanel.Controls.Add(searchButton);
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(1218, 0);
            buttonPanel.Margin = new Padding(0, 0, 0, 0);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(312, 30);
            buttonPanel.TabIndex = 16;
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(232, 0);
            refreshButton.Margin = new Padding(0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(80, 30);
            refreshButton.TabIndex = 3;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = true;
            // 
            // printButton
            // 
            printButton.Location = new Point(127, 0);
            printButton.Margin = new Padding(0, 0, 8, 0);
            printButton.Name = "printButton";
            printButton.Size = new Size(97, 30);
            printButton.TabIndex = 2;
            printButton.Text = "Print Report";
            printButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(59, 0);
            clearButton.Margin = new Padding(0, 0, 8, 0);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(60, 30);
            clearButton.TabIndex = 1;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(0, 0);
            searchButton.Margin = new Padding(0, 0, 8, 0);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(70, 30);
            searchButton.TabIndex = 0;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            // 
            // auditLogsGrid
            // 
            auditLogsGrid.AllowUserToAddRows = false;
            auditLogsGrid.AllowUserToDeleteRows = false;
            auditLogsGrid.AllowUserToResizeRows = false;
            auditLogsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            auditLogsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            auditLogsGrid.Dock = DockStyle.Fill;
            auditLogsGrid.Location = new Point(0, 57);
            auditLogsGrid.Margin = new Padding(0);
            auditLogsGrid.MultiSelect = false;
            auditLogsGrid.Name = "auditLogsGrid";
            auditLogsGrid.ReadOnly = true;
            auditLogsGrid.RowHeadersWidth = 51;
            auditLogsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            auditLogsGrid.Size = new Size(1100, 597);
            auditLogsGrid.TabIndex = 2;
            // 
            // footerLayout
            // 
            footerLayout.ColumnCount = 1;
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            footerLayout.Controls.Add(statusLabel, 0, 0);
            footerLayout.Controls.Add(paginationLayout, 0, 1);
            footerLayout.Dock = DockStyle.Fill;
            footerLayout.Location = new Point(0, 666);
            footerLayout.Margin = new Padding(0, 12, 0, 0);
            footerLayout.Name = "footerLayout";
            footerLayout.RowCount = 2;
            footerLayout.RowStyles.Add(new RowStyle());
            footerLayout.RowStyles.Add(new RowStyle());
            footerLayout.Size = new Size(1100, 34);
            footerLayout.TabIndex = 3;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(0, 0);
            statusLabel.Margin = new Padding(0, 0, 0, 4);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 0;
            statusLabel.Text = "Ready";
            // 
            // paginationLayout
            // 
            paginationLayout.ColumnCount = 4;
            paginationLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            paginationLayout.ColumnStyles.Add(new ColumnStyle());
            paginationLayout.ColumnStyles.Add(new ColumnStyle());
            paginationLayout.ColumnStyles.Add(new ColumnStyle());
            paginationLayout.Controls.Add(previousPageButton, 1, 0);
            paginationLayout.Controls.Add(pageInfoLabel, 2, 0);
            paginationLayout.Controls.Add(nextPageButton, 3, 0);
            paginationLayout.Dock = DockStyle.Fill;
            paginationLayout.Location = new Point(0, 19);
            paginationLayout.Margin = new Padding(0);
            paginationLayout.Name = "paginationLayout";
            paginationLayout.RowCount = 1;
            paginationLayout.RowStyles.Add(new RowStyle());
            paginationLayout.Size = new Size(1100, 15);
            paginationLayout.TabIndex = 1;
            // 
            // previousPageButton
            // 
            previousPageButton.Location = new Point(908, 0);
            previousPageButton.Margin = new Padding(6, 0, 6, 0);
            previousPageButton.Name = "previousPageButton";
            previousPageButton.Size = new Size(80, 23);
            previousPageButton.TabIndex = 0;
            previousPageButton.Text = "< Previous";
            previousPageButton.UseVisualStyleBackColor = true;
            // 
            // pageInfoLabel
            // 
            pageInfoLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pageInfoLabel.AutoSize = true;
            pageInfoLabel.Location = new Point(1000, 4);
            pageInfoLabel.Margin = new Padding(6, 0, 6, 0);
            pageInfoLabel.Name = "pageInfoLabel";
            pageInfoLabel.Size = new Size(24, 15);
            pageInfoLabel.TabIndex = 1;
            pageInfoLabel.Text = "1/1";
            // 
            // nextPageButton
            // 
            nextPageButton.Location = new Point(1030, 0);
            nextPageButton.Margin = new Padding(0);
            nextPageButton.Name = "nextPageButton";
            nextPageButton.Size = new Size(70, 23);
            nextPageButton.TabIndex = 2;
            nextPageButton.Text = "Next >";
            nextPageButton.UseVisualStyleBackColor = true;
            // 
            // UserAuditLogsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UserAuditLogsUserControl";
            Size = new Size(1100, 700);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            filterLayout.ResumeLayout(false);
            filterLayout.PerformLayout();
            buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)auditLogsGrid).EndInit();
            footerLayout.ResumeLayout(false);
            footerLayout.PerformLayout();
            paginationLayout.ResumeLayout(false);
            paginationLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private TableLayoutPanel filterLayout;
        private Label userSearchLabel;
        private TextBox userSearchTextBox;
        private Label roleFilterLabel;
        private ComboBox roleFilterComboBox;
        private Label actionTypeFilterLabel;
        private ComboBox actionTypeFilterComboBox;
        private Label moduleFilterLabel;
        private ComboBox moduleFilterComboBox;
        private Label severityFilterLabel;
        private ComboBox severityFilterComboBox;
        private Label dateFromLabel;
        private DateTimePicker dateFromPicker;
        private Label dateToLabel;
        private DateTimePicker dateToPicker;
        private FlowLayoutPanel buttonPanel;
        private Button searchButton;
        private Button clearButton;
        private Button printButton;
        private Button refreshButton;
        private DataGridView auditLogsGrid;
        private TableLayoutPanel footerLayout;
        private Label statusLabel;
        private TableLayoutPanel paginationLayout;
        private Button previousPageButton;
        private Label pageInfoLabel;
        private Button nextPageButton;
    }
}
