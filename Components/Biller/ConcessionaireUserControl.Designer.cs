namespace WDBS_2026.Components.Biller;

partial class ConcessionaireUserControl
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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        rootLayout = new TableLayoutPanel();
        pagerLayout = new TableLayoutPanel();
        statusLabel = new Label();
        pageInfoLabel = new Label();
        nextPageButton = new Button();
        previousPageButton = new Button();
        concessionaireGrid = new DataGridView();
        searchLayout = new TableLayoutPanel();
        statusFilterLabel = new Label();
        statusFilterComboBox = new ComboBox();
        searchTextBox = new TextBox();
        searchButton = new Button();
        clearButton = new Button();
        addConcessionaireButton = new Button();
        titleLabel = new Label();
        rootLayout.SuspendLayout();
        pagerLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireGrid).BeginInit();
        searchLayout.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(pagerLayout, 0, 3);
        rootLayout.Controls.Add(concessionaireGrid, 0, 2);
        rootLayout.Controls.Add(searchLayout, 0, 1);
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
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
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
        pagerLayout.TabIndex = 4;
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
        // concessionaireGrid
        // 
        concessionaireGrid.AllowUserToAddRows = false;
        concessionaireGrid.AllowUserToDeleteRows = false;
        concessionaireGrid.AllowUserToOrderColumns = true;
        concessionaireGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        concessionaireGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        concessionaireGrid.Dock = DockStyle.Fill;
        concessionaireGrid.Location = new Point(0, 73);
        concessionaireGrid.Margin = new Padding(0);
        concessionaireGrid.MultiSelect = false;
        concessionaireGrid.Name = "concessionaireGrid";
        concessionaireGrid.ReadOnly = true;
        concessionaireGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        concessionaireGrid.Size = new Size(1100, 574);
        concessionaireGrid.TabIndex = 3;
        // 
        // searchLayout
        // 
        searchLayout.ColumnCount = 6;
        searchLayout.ColumnStyles.Add(new ColumnStyle());
        searchLayout.ColumnStyles.Add(new ColumnStyle());
        searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        searchLayout.ColumnStyles.Add(new ColumnStyle());
        searchLayout.ColumnStyles.Add(new ColumnStyle());
        searchLayout.ColumnStyles.Add(new ColumnStyle());
        searchLayout.Controls.Add(statusFilterLabel, 0, 0);
        searchLayout.Controls.Add(statusFilterComboBox, 1, 0);
        searchLayout.Controls.Add(searchTextBox, 2, 0);
        searchLayout.Controls.Add(searchButton, 3, 0);
        searchLayout.Controls.Add(clearButton, 4, 0);
        searchLayout.Controls.Add(addConcessionaireButton, 5, 0);
        searchLayout.Dock = DockStyle.Fill;
        searchLayout.Location = new Point(0, 31);
        searchLayout.Margin = new Padding(0, 12, 0, 12);
        searchLayout.Name = "searchLayout";
        searchLayout.RowCount = 1;
        searchLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        searchLayout.Size = new Size(1100, 30);
        searchLayout.TabIndex = 2;
        // 
        // statusFilterLabel
        // 
        statusFilterLabel.Anchor = AnchorStyles.Left;
        statusFilterLabel.AutoSize = true;
        statusFilterLabel.Location = new Point(0, 7);
        statusFilterLabel.Margin = new Padding(0);
        statusFilterLabel.Name = "statusFilterLabel";
        statusFilterLabel.Size = new Size(39, 15);
        statusFilterLabel.TabIndex = 0;
        statusFilterLabel.Text = "Status";
        // 
        // statusFilterComboBox
        // 
        statusFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        statusFilterComboBox.FormattingEnabled = true;
        statusFilterComboBox.Items.AddRange(new object[] { "All", "Active", "Disconnected" });
        statusFilterComboBox.Location = new Point(47, 3);
        statusFilterComboBox.Margin = new Padding(8, 3, 0, 3);
        statusFilterComboBox.Name = "statusFilterComboBox";
        statusFilterComboBox.Size = new Size(130, 23);
        statusFilterComboBox.TabIndex = 1;
        statusFilterComboBox.SelectedIndexChanged += statusFilterComboBox_SelectedIndexChanged;
        // 
        // searchTextBox
        // 
        searchTextBox.Dock = DockStyle.Fill;
        searchTextBox.Location = new Point(185, 0);
        searchTextBox.Margin = new Padding(8, 0, 0, 0);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Search Account No or Concessionaire Name";
        searchTextBox.Size = new Size(591, 23);
        searchTextBox.TabIndex = 2;
        searchTextBox.KeyDown += searchTextBox_KeyDown;
        // 
        // searchButton
        // 
        searchButton.Location = new Point(784, 0);
        searchButton.Margin = new Padding(8, 0, 0, 0);
        searchButton.Name = "searchButton";
        searchButton.Size = new Size(100, 30);
        searchButton.TabIndex = 3;
        searchButton.Text = "Search";
        searchButton.UseVisualStyleBackColor = true;
        searchButton.Click += searchButton_Click;
        // 
        // clearButton
        // 
        clearButton.Location = new Point(892, 0);
        clearButton.Margin = new Padding(8, 0, 0, 0);
        clearButton.Name = "clearButton";
        clearButton.Size = new Size(100, 30);
        clearButton.TabIndex = 4;
        clearButton.Text = "Clear";
        clearButton.UseVisualStyleBackColor = true;
        clearButton.Click += clearButton_Click;
        // 
        // addConcessionaireButton
        // 
        addConcessionaireButton.Location = new Point(1000, 0);
        addConcessionaireButton.Margin = new Padding(8, 0, 0, 0);
        addConcessionaireButton.Name = "addConcessionaireButton";
        addConcessionaireButton.Size = new Size(100, 30);
        addConcessionaireButton.TabIndex = 5;
        addConcessionaireButton.Text = "Add";
        addConcessionaireButton.UseVisualStyleBackColor = true;
        addConcessionaireButton.Click += addConcessionaireButton_Click;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(0, 4);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(161, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Concessionaire Management";
        // 
        // ConcessionaireUserControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(rootLayout);
        Name = "ConcessionaireUserControl";
        Size = new Size(1100, 700);
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        pagerLayout.ResumeLayout(false);
        pagerLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)concessionaireGrid).EndInit();
        searchLayout.ResumeLayout(false);
        searchLayout.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private TableLayoutPanel searchLayout;
    private Label statusFilterLabel;
    private ComboBox statusFilterComboBox;
    private TextBox searchTextBox;
    private Button searchButton;
    private Button clearButton;
    private Button addConcessionaireButton;
    private DataGridView concessionaireGrid;
    private TableLayoutPanel pagerLayout;
    private Label statusLabel;
    private Label pageInfoLabel;
    private Button previousPageButton;
    private Button nextPageButton;
}
