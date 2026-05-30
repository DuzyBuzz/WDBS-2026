namespace WDBS_2026.Forms.Pickers;

partial class CollectionPickerForm
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
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        subtitleLabel = new Label();
        filterLayout = new TableLayoutPanel();
        searchTextBox = new TextBox();
        searchButton = new Button();
        refreshButton = new Button();
        grid = new DataGridView();
        actionLayout = new TableLayoutPanel();
        cancelButton = new Button();
        confirmButton = new Button();
        rootLayout.SuspendLayout();
        filterLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        actionLayout.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(filterLayout, 0, 2);
        rootLayout.Controls.Add(grid, 0, 3);
        rootLayout.Controls.Add(actionLayout, 0, 4);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(18);
        rootLayout.RowCount = 5;
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.RowStyles.Add(new RowStyle());
        rootLayout.Size = new Size(1080, 640);
        rootLayout.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(18, 18);
        titleLabel.Margin = new Padding(0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(96, 15);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Void Collection";
        // 
        // subtitleLabel
        // 
        subtitleLabel.AutoSize = true;
        subtitleLabel.Location = new Point(18, 41);
        subtitleLabel.Margin = new Padding(0, 8, 0, 0);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(314, 15);
        subtitleLabel.TabIndex = 1;
        subtitleLabel.Text = "Pick a posted collection record to mark as voided record.";
        // 
        // filterLayout
        // 
        filterLayout.ColumnCount = 3;
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        filterLayout.ColumnStyles.Add(new ColumnStyle());
        filterLayout.ColumnStyles.Add(new ColumnStyle());
        filterLayout.Controls.Add(searchTextBox, 0, 0);
        filterLayout.Controls.Add(searchButton, 1, 0);
        filterLayout.Controls.Add(refreshButton, 2, 0);
        filterLayout.Dock = DockStyle.Fill;
        filterLayout.Location = new Point(18, 80);
        filterLayout.Margin = new Padding(0, 24, 0, 0);
        filterLayout.Name = "filterLayout";
        filterLayout.RowCount = 1;
        filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        filterLayout.Size = new Size(1044, 38);
        filterLayout.TabIndex = 2;
        // 
        // searchTextBox
        // 
        searchTextBox.Dock = DockStyle.Fill;
        searchTextBox.Location = new Point(0, 7);
        searchTextBox.Margin = new Padding(0, 7, 8, 0);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Search OR, account no, concessionaire, invoice...";
        searchTextBox.Size = new Size(770, 23);
        searchTextBox.TabIndex = 0;
        searchTextBox.KeyDown += searchTextBox_KeyDown;
        // 
        // searchButton
        // 
        searchButton.Location = new Point(778, 0);
        searchButton.Margin = new Padding(0, 0, 8, 0);
        searchButton.Name = "searchButton";
        searchButton.Size = new Size(126, 36);
        searchButton.TabIndex = 1;
        searchButton.Text = "Search";
        searchButton.UseVisualStyleBackColor = true;
        searchButton.Click += searchButton_Click;
        // 
        // refreshButton
        // 
        refreshButton.Location = new Point(912, 0);
        refreshButton.Margin = new Padding(0);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(132, 36);
        refreshButton.TabIndex = 2;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += refreshButton_Click;
        // 
        // grid
        // 
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(18, 136);
        grid.Margin = new Padding(0, 18, 0, 0);
        grid.Name = "grid";
        grid.Size = new Size(1044, 438);
        grid.TabIndex = 3;
        // 
        // actionLayout
        // 
        actionLayout.ColumnCount = 3;
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.ColumnStyles.Add(new ColumnStyle());
        actionLayout.Controls.Add(cancelButton, 1, 0);
        actionLayout.Controls.Add(confirmButton, 2, 0);
        actionLayout.Dock = DockStyle.Fill;
        actionLayout.Location = new Point(18, 592);
        actionLayout.Margin = new Padding(0, 18, 0, 0);
        actionLayout.Name = "actionLayout";
        actionLayout.RowCount = 1;
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        actionLayout.Size = new Size(1044, 30);
        actionLayout.TabIndex = 4;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(792, 0);
        cancelButton.Margin = new Padding(0, 0, 8, 0);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(120, 30);
        cancelButton.TabIndex = 0;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // confirmButton
        // 
        confirmButton.Location = new Point(920, 0);
        confirmButton.Margin = new Padding(0);
        confirmButton.Name = "confirmButton";
        confirmButton.Size = new Size(124, 30);
        confirmButton.TabIndex = 1;
        confirmButton.Text = "Select To Void";
        confirmButton.UseVisualStyleBackColor = true;
        confirmButton.Click += confirmButton_Click;
        // 
        // CollectionPickerForm
        // 
        AcceptButton = searchButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new Size(1080, 640);
        Controls.Add(rootLayout);
        MinimizeBox = false;
        MinimumSize = new Size(860, 520);
        Name = "CollectionPickerForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Collection Picker";
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        filterLayout.ResumeLayout(false);
        filterLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        actionLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel rootLayout;
    private Label titleLabel;
    private Label subtitleLabel;
    private TableLayoutPanel filterLayout;
    private TextBox searchTextBox;
    private Button searchButton;
    private Button refreshButton;
    private DataGridView grid;
    private TableLayoutPanel actionLayout;
    private Button cancelButton;
    private Button confirmButton;
}
