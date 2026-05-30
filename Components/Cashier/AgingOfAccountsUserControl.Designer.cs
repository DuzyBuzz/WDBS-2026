namespace WDBS_2026.Components.Cashier
{
    partial class AgingOfAccountsUserControl
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
            agingDateLabel = new Label();
            agingDatePicker = new DateTimePicker();
            searchLabel = new Label();
            searchTextBox = new TextBox();
            searchButton = new Button();
            clearButton = new Button();
            refreshButton = new Button();
            printReportButton = new Button();
            agingGrid = new DataGridView();
            footerLayout = new TableLayoutPanel();
            statusLabel = new Label();
            rootLayout.SuspendLayout();
            filterLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)agingGrid).BeginInit();
            footerLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(filterLayout, 0, 1);
            rootLayout.Controls.Add(agingGrid, 0, 2);
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
            titleLabel.Size = new Size(160, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Aging of Accounts Summary";
            // 
            // filterLayout
            // 
            filterLayout.ColumnCount = 9;
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.Controls.Add(agingDateLabel, 0, 0);
            filterLayout.Controls.Add(agingDatePicker, 1, 0);
            filterLayout.Controls.Add(searchLabel, 2, 0);
            filterLayout.Controls.Add(searchTextBox, 3, 0);
            filterLayout.Controls.Add(searchButton, 4, 0);
            filterLayout.Controls.Add(clearButton, 5, 0);
            filterLayout.Controls.Add(refreshButton, 6, 0);
            filterLayout.Controls.Add(printReportButton, 8, 0);
            filterLayout.Dock = DockStyle.Fill;
            filterLayout.Location = new Point(0, 31);
            filterLayout.Margin = new Padding(0, 12, 0, 12);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 1;
            filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            filterLayout.Size = new Size(1100, 30);
            filterLayout.TabIndex = 1;
            // 
            // agingDateLabel
            // 
            agingDateLabel.Anchor = AnchorStyles.Left;
            agingDateLabel.AutoSize = true;
            agingDateLabel.Location = new Point(0, 7);
            agingDateLabel.Margin = new Padding(0);
            agingDateLabel.Name = "agingDateLabel";
            agingDateLabel.Size = new Size(66, 15);
            agingDateLabel.TabIndex = 0;
            agingDateLabel.Text = "Aging Date";
            // 
            // agingDatePicker
            // 
            agingDatePicker.Format = DateTimePickerFormat.Short;
            agingDatePicker.Location = new Point(69, 3);
            agingDatePicker.Name = "agingDatePicker";
            agingDatePicker.Size = new Size(130, 23);
            agingDatePicker.TabIndex = 1;
            agingDatePicker.ValueChanged += agingDatePicker_ValueChanged;
            // 
            // searchLabel
            // 
            searchLabel.Anchor = AnchorStyles.Left;
            searchLabel.AutoSize = true;
            searchLabel.Location = new Point(214, 7);
            searchLabel.Margin = new Padding(12, 0, 0, 0);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new Size(42, 15);
            searchLabel.TabIndex = 2;
            searchLabel.Text = "Search";
            // 
            // searchTextBox
            // 
            searchTextBox.Dock = DockStyle.Fill;
            searchTextBox.Location = new Point(259, 0);
            searchTextBox.Margin = new Padding(3, 0, 0, 0);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search Account No. or Concessionaire";
            searchTextBox.Size = new Size(466, 23);
            searchTextBox.TabIndex = 3;
            searchTextBox.KeyDown += searchTextBox_KeyDown;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(733, 0);
            searchButton.Margin = new Padding(8, 0, 0, 0);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(85, 30);
            searchButton.TabIndex = 4;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(826, 0);
            clearButton.Margin = new Padding(8, 0, 0, 0);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(70, 30);
            clearButton.TabIndex = 5;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(904, 0);
            refreshButton.Margin = new Padding(8, 0, 0, 0);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(70, 30);
            refreshButton.TabIndex = 6;
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // printReportButton
            // 
            printReportButton.Location = new Point(980, 0);
            printReportButton.Margin = new Padding(6, 0, 0, 0);
            printReportButton.Name = "printReportButton";
            printReportButton.Size = new Size(120, 30);
            printReportButton.TabIndex = 8;
            printReportButton.Text = "Print Report";
            printReportButton.UseVisualStyleBackColor = true;
            printReportButton.Click += printReportButton_Click;
            // 
            // agingGrid
            // 
            agingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            agingGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            agingGrid.Dock = DockStyle.Fill;
            agingGrid.Location = new Point(0, 73);
            agingGrid.Margin = new Padding(0);
            agingGrid.Name = "agingGrid";
            agingGrid.Size = new Size(1100, 589);
            agingGrid.TabIndex = 2;
            // 
            // footerLayout
            // 
            footerLayout.ColumnCount = 1;
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            footerLayout.Controls.Add(statusLabel, 0, 0);
            footerLayout.Dock = DockStyle.Fill;
            footerLayout.Location = new Point(0, 674);
            footerLayout.Margin = new Padding(0, 12, 0, 0);
            footerLayout.Name = "footerLayout";
            footerLayout.RowCount = 1;
            footerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            footerLayout.Size = new Size(1100, 26);
            footerLayout.TabIndex = 3;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(0, 5);
            statusLabel.Margin = new Padding(0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 0;
            statusLabel.Text = "Ready";
            // 
            // AgingOfAccountsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "AgingOfAccountsUserControl";
            Size = new Size(1100, 700);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            filterLayout.ResumeLayout(false);
            filterLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)agingGrid).EndInit();
            footerLayout.ResumeLayout(false);
            footerLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private TableLayoutPanel filterLayout;
        private Label agingDateLabel;
        private DateTimePicker agingDatePicker;
        private Label searchLabel;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button clearButton;
        private Button refreshButton;
        private Button printReportButton;
        private DataGridView agingGrid;
        private TableLayoutPanel footerLayout;
        private Label statusLabel;
    }
}
