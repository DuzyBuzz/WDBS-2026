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
            searchLabel = new Label();
            searchTextBox = new TextBox();
            searchButton = new Button();
            clearButton = new Button();
            dateFromLabel = new Label();
            dateFromPicker = new DateTimePicker();
            dateToLabel = new Label();
            dateToPicker = new DateTimePicker();
            printButton = new Button();
            refreshButton = new Button();
            auditLogsGrid = new DataGridView();
            footerLayout = new TableLayoutPanel();
            statusLabel = new Label();
            paginationLayout = new TableLayoutPanel();
            previousPageButton = new Button();
            pageInfoLabel = new Label();
            nextPageButton = new Button();
            
            rootLayout.SuspendLayout();
            filterLayout.SuspendLayout();
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
            titleLabel.Size = new Size(106, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "User Audit Logs";
            
            // 
            // filterLayout
            // 
            filterLayout.ColumnCount = 10;
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.Controls.Add(searchLabel, 0, 0);
            filterLayout.Controls.Add(searchTextBox, 1, 0);
            filterLayout.Controls.Add(searchButton, 2, 0);
            filterLayout.Controls.Add(clearButton, 3, 0);
            filterLayout.Controls.Add(dateFromLabel, 4, 0);
            filterLayout.Controls.Add(dateFromPicker, 5, 0);
            filterLayout.Controls.Add(dateToLabel, 6, 0);
            filterLayout.Controls.Add(dateToPicker, 7, 0);
            filterLayout.Controls.Add(printButton, 8, 0);
            filterLayout.Controls.Add(refreshButton, 9, 0);
            filterLayout.Dock = DockStyle.Fill;
            filterLayout.Location = new Point(0, 27);
            filterLayout.Margin = new Padding(0, 8, 0, 0);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 1;
            filterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            filterLayout.Size = new Size(1100, 35);
            filterLayout.TabIndex = 1;
            
            // 
            // searchLabel
            // 
            searchLabel.Anchor = AnchorStyles.Left;
            searchLabel.AutoSize = true;
            searchLabel.Location = new Point(0, 10);
            searchLabel.Margin = new Padding(0, 0, 6, 0);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new Size(45, 15);
            searchLabel.TabIndex = 0;
            searchLabel.Text = "Search:";
            
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(51, 6);
            searchTextBox.Margin = new Padding(0, 0, 6, 0);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(140, 23);
            searchTextBox.TabIndex = 1;
            
            // 
            // searchButton
            // 
            searchButton.Location = new Point(197, 6);
            searchButton.Margin = new Padding(0, 0, 6, 0);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(70, 23);
            searchButton.TabIndex = 2;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            
            // 
            // clearButton
            // 
            clearButton.Location = new Point(273, 6);
            clearButton.Margin = new Padding(0, 0, 12, 0);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(60, 23);
            clearButton.TabIndex = 3;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            
            // 
            // dateFromLabel
            // 
            dateFromLabel.Anchor = AnchorStyles.Left;
            dateFromLabel.AutoSize = true;
            dateFromLabel.Location = new Point(345, 10);
            dateFromLabel.Margin = new Padding(0, 0, 6, 0);
            dateFromLabel.Name = "dateFromLabel";
            dateFromLabel.Size = new Size(38, 15);
            dateFromLabel.TabIndex = 4;
            dateFromLabel.Text = "From:";
            
            // 
            // dateFromPicker
            // 
            dateFromPicker.CustomFormat = "yyyy-MM-dd";
            dateFromPicker.Format = DateTimePickerFormat.Custom;
            dateFromPicker.Location = new Point(389, 6);
            dateFromPicker.Margin = new Padding(0, 0, 6, 0);
            dateFromPicker.Name = "dateFromPicker";
            dateFromPicker.Size = new Size(110, 23);
            dateFromPicker.TabIndex = 5;
            
            // 
            // dateToLabel
            // 
            dateToLabel.Anchor = AnchorStyles.Left;
            dateToLabel.AutoSize = true;
            dateToLabel.Location = new Point(505, 10);
            dateToLabel.Margin = new Padding(0, 0, 6, 0);
            dateToLabel.Name = "dateToLabel";
            dateToLabel.Size = new Size(22, 15);
            dateToLabel.TabIndex = 6;
            dateToLabel.Text = "To:";
            
            // 
            // dateToPicker
            // 
            dateToPicker.CustomFormat = "yyyy-MM-dd";
            dateToPicker.Format = DateTimePickerFormat.Custom;
            dateToPicker.Location = new Point(533, 6);
            dateToPicker.Margin = new Padding(0, 0, 6, 0);
            dateToPicker.Name = "dateToPicker";
            dateToPicker.Size = new Size(110, 23);
            dateToPicker.TabIndex = 7;
            
            // 
            // printButton
            // 
            printButton.Anchor = AnchorStyles.Right;
            printButton.Location = new Point(972, 6);
            printButton.Margin = new Padding(0, 0, 6, 0);
            printButton.Name = "printButton";
            printButton.Size = new Size(105, 23);
            printButton.TabIndex = 8;
            printButton.Text = "Print Report";
            printButton.UseVisualStyleBackColor = true;
            
            // 
            // refreshButton
            // 
            refreshButton.Anchor = AnchorStyles.Right;
            refreshButton.Location = new Point(1020, 6);
            refreshButton.Margin = new Padding(0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(80, 23);
            refreshButton.TabIndex = 9;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = true;
            
            // 
            // auditLogsGrid
            // 
            auditLogsGrid.AllowUserToAddRows = false;
            auditLogsGrid.AllowUserToDeleteRows = false;
            auditLogsGrid.AllowUserToResizeRows = false;
            auditLogsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            auditLogsGrid.Dock = DockStyle.Fill;
            auditLogsGrid.Location = new Point(0, 62);
            auditLogsGrid.Margin = new Padding(0);
            auditLogsGrid.MultiSelect = false;
            auditLogsGrid.Name = "auditLogsGrid";
            auditLogsGrid.ReadOnly = true;
            auditLogsGrid.RowHeadersWidth = 51;
            auditLogsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            auditLogsGrid.Size = new Size(1100, 604);
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
            statusLabel.Size = new Size(68, 15);
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
            previousPageButton.Location = new Point(853, 0);
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
            pageInfoLabel.Location = new Point(945, 4);
            pageInfoLabel.Margin = new Padding(6, 0, 6, 0);
            pageInfoLabel.Name = "pageInfoLabel";
            pageInfoLabel.Size = new Size(24, 15);
            pageInfoLabel.TabIndex = 1;
            pageInfoLabel.Text = "1/1";
            
            // 
            // nextPageButton
            // 
            nextPageButton.Location = new Point(977, 0);
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
        private Label searchLabel;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button clearButton;
        private Label dateFromLabel;
        private DateTimePicker dateFromPicker;
        private Label dateToLabel;
        private DateTimePicker dateToPicker;
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
