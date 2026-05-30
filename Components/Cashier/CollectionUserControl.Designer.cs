namespace WDBS_2026.Components.Cashier
{
    partial class CollectionUserControl
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
            pagerLayout = new TableLayoutPanel();
            statusLabel = new Label();
            pageInfoLabel = new Label();
            nextPageButton = new Button();
            previousPageButton = new Button();
            collectionGrid = new DataGridView();
            filterLayout = new TableLayoutPanel();
            printReportButton = new Button();
            clearButton = new Button();
            searchButton = new Button();
            searchTextBox = new TextBox();
            periodPicker = new DateTimePicker();
            periodLabel = new Label();
            modeComboBox = new ComboBox();
            modeLabel = new Label();
            titleLabel = new Label();
            rootLayout.SuspendLayout();
            pagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)collectionGrid).BeginInit();
            filterLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(pagerLayout, 0, 3);
            rootLayout.Controls.Add(collectionGrid, 0, 2);
            rootLayout.Controls.Add(filterLayout, 0, 1);
            rootLayout.Controls.Add(titleLabel, 0, 0);
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
            // pagerLayout
            // 
            pagerLayout.ColumnCount = 4;
            pagerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pagerLayout.ColumnStyles.Add(new ColumnStyle());
            pagerLayout.ColumnStyles.Add(new ColumnStyle());
            pagerLayout.ColumnStyles.Add(new ColumnStyle());
            pagerLayout.Controls.Add(statusLabel, 0, 0);
            pagerLayout.Controls.Add(pageInfoLabel, 1, 0);
            pagerLayout.Controls.Add(nextPageButton, 3, 0);
            pagerLayout.Controls.Add(previousPageButton, 2, 0);
            pagerLayout.Dock = DockStyle.Fill;
            pagerLayout.Location = new Point(0, 659);
            pagerLayout.Margin = new Padding(0, 12, 0, 0);
            pagerLayout.Name = "pagerLayout";
            pagerLayout.RowCount = 1;
            pagerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pagerLayout.Size = new Size(1100, 41);
            pagerLayout.TabIndex = 3;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(0, 13);
            statusLabel.Margin = new Padding(0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 0;
            statusLabel.Text = "Ready";
            // 
            // pageInfoLabel
            // 
            pageInfoLabel.Anchor = AnchorStyles.None;
            pageInfoLabel.AutoSize = true;
            pageInfoLabel.Location = new Point(826, 13);
            pageInfoLabel.Margin = new Padding(10, 0, 10, 0);
            pageInfoLabel.Name = "pageInfoLabel";
            pageInfoLabel.Size = new Size(88, 15);
            pageInfoLabel.TabIndex = 1;
            pageInfoLabel.Text = "Page 1 of 1  •  0";
            // 
            // nextPageButton
            // 
            nextPageButton.Location = new Point(1012, 0);
            nextPageButton.Margin = new Padding(0);
            nextPageButton.Name = "nextPageButton";
            nextPageButton.Size = new Size(88, 32);
            nextPageButton.TabIndex = 3;
            nextPageButton.Text = "Next";
            nextPageButton.UseVisualStyleBackColor = true;
            nextPageButton.Click += nextPageButton_Click;
            // 
            // previousPageButton
            // 
            previousPageButton.Location = new Point(924, 0);
            previousPageButton.Margin = new Padding(0);
            previousPageButton.Name = "previousPageButton";
            previousPageButton.Size = new Size(88, 32);
            previousPageButton.TabIndex = 2;
            previousPageButton.Text = "Previous";
            previousPageButton.UseVisualStyleBackColor = true;
            previousPageButton.Click += previousPageButton_Click;
            // 
            // collectionGrid
            // 
            collectionGrid.AllowUserToOrderColumns = true;
            collectionGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            collectionGrid.Dock = DockStyle.Fill;
            collectionGrid.Location = new Point(0, 73);
            collectionGrid.Margin = new Padding(0);
            collectionGrid.Name = "collectionGrid";
            collectionGrid.Size = new Size(1100, 574);
            collectionGrid.TabIndex = 2;
            // 
            // filterLayout
            // 
            filterLayout.ColumnCount = 8;
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.ColumnStyles.Add(new ColumnStyle());
            filterLayout.Controls.Add(printReportButton, 7, 0);
            filterLayout.Controls.Add(clearButton, 6, 0);
            filterLayout.Controls.Add(searchButton, 5, 0);
            filterLayout.Controls.Add(searchTextBox, 4, 0);
            filterLayout.Controls.Add(periodPicker, 3, 0);
            filterLayout.Controls.Add(periodLabel, 2, 0);
            filterLayout.Controls.Add(modeComboBox, 1, 0);
            filterLayout.Controls.Add(modeLabel, 0, 0);
            filterLayout.Dock = DockStyle.Fill;
            filterLayout.Location = new Point(0, 31);
            filterLayout.Margin = new Padding(0, 12, 0, 12);
            filterLayout.Name = "filterLayout";
            filterLayout.RowCount = 1;
            filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            filterLayout.Size = new Size(1100, 30);
            filterLayout.TabIndex = 1;
            filterLayout.Paint += filterLayout_Paint;
            // 
            // printReportButton
            // 
            printReportButton.Location = new Point(970, 0);
            printReportButton.Margin = new Padding(8, 0, 0, 0);
            printReportButton.Name = "printReportButton";
            printReportButton.Size = new Size(130, 30);
            printReportButton.TabIndex = 7;
            printReportButton.Text = "Print Report";
            printReportButton.UseVisualStyleBackColor = true;
            printReportButton.Click += printReportButton_Click;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(862, 0);
            clearButton.Margin = new Padding(8, 0, 0, 0);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(100, 30);
            clearButton.TabIndex = 6;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(754, 0);
            searchButton.Margin = new Padding(8, 0, 0, 0);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(100, 30);
            searchButton.TabIndex = 5;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Dock = DockStyle.Fill;
            searchTextBox.Location = new Point(467, 0);
            searchTextBox.Margin = new Padding(8, 0, 0, 0);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search OR Number, Invoice Number, Account No., or Payor";
            searchTextBox.Size = new Size(279, 23);
            searchTextBox.TabIndex = 4;
            searchTextBox.KeyDown += searchTextBox_KeyDown;
            // 
            // periodPicker
            // 
            periodPicker.Format = DateTimePickerFormat.Short;
            periodPicker.Location = new Point(236, 3);
            periodPicker.Name = "periodPicker";
            periodPicker.Size = new Size(220, 23);
            periodPicker.TabIndex = 3;
            periodPicker.ValueChanged += periodPicker_ValueChanged;
            // 
            // periodLabel
            // 
            periodLabel.Anchor = AnchorStyles.Left;
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(145, 7);
            periodLabel.Margin = new Padding(12, 0, 0, 0);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(88, 15);
            periodLabel.TabIndex = 2;
            periodLabel.Text = "Collection Date";
            // 
            // modeComboBox
            // 
            modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modeComboBox.FormattingEnabled = true;
            modeComboBox.Items.AddRange(new object[] { "Daily", "Monthly" });
            modeComboBox.Location = new Point(41, 3);
            modeComboBox.Name = "modeComboBox";
            modeComboBox.Size = new Size(89, 23);
            modeComboBox.TabIndex = 1;
            modeComboBox.SelectedIndexChanged += modeComboBox_SelectedIndexChanged;
            // 
            // modeLabel
            // 
            modeLabel.Anchor = AnchorStyles.Left;
            modeLabel.AutoSize = true;
            modeLabel.Location = new Point(0, 7);
            modeLabel.Margin = new Padding(0);
            modeLabel.Name = "modeLabel";
            modeLabel.Size = new Size(38, 15);
            modeLabel.TabIndex = 0;
            modeLabel.Text = "Mode";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(0, 4);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Collection Reports";
            // 
            // CollectionUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "CollectionUserControl";
            Size = new Size(1100, 700);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            pagerLayout.ResumeLayout(false);
            pagerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)collectionGrid).EndInit();
            filterLayout.ResumeLayout(false);
            filterLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private TableLayoutPanel filterLayout;
        private Label modeLabel;
        private ComboBox modeComboBox;
        private Label periodLabel;
        private DateTimePicker periodPicker;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button clearButton;
        private Button printReportButton;
        private DataGridView collectionGrid;
        private TableLayoutPanel pagerLayout;
        private Label statusLabel;
        private Label pageInfoLabel;
        private Button previousPageButton;
        private Button nextPageButton;
    }
}
