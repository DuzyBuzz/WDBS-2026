namespace WDBS_2026.Forms.Concessionaire
{
    partial class InitializeSCFForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            contentPanel = new Panel();
            contentLayout = new TableLayoutPanel();
            actionsLayout = new TableLayoutPanel();
            cancelButton = new Button();
            saveButton = new Button();
            statusLabel = new Label();
            fieldsLayout = new TableLayoutPanel();
            balanceAmountTextBox = new TextBox();
            balanceAmountLabel = new Label();
            monthlyAmountTextBox = new TextBox();
            monthlyAmountLabel = new Label();
            totalAmountTextBox = new TextBox();
            totalAmountLabel = new Label();
            concessionaireNameTextBox = new TextBox();
            concessionaireNameLabel = new Label();
            accountNoTextBox = new TextBox();
            accountNoLabel = new Label();
            titleLabel = new Label();
            rootLayout.SuspendLayout();
            contentPanel.SuspendLayout();
            contentLayout.SuspendLayout();
            actionsLayout.SuspendLayout();
            fieldsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(contentPanel, 0, 1);
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(24);
            rootLayout.RowCount = 2;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(544, 381);
            rootLayout.TabIndex = 0;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(contentLayout);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(24, 55);
            contentPanel.Margin = new Padding(0, 16, 0, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(496, 302);
            contentPanel.TabIndex = 1;
            // 
            // contentLayout
            // 
            contentLayout.ColumnCount = 1;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            contentLayout.Controls.Add(actionsLayout, 0, 2);
            contentLayout.Controls.Add(statusLabel, 0, 1);
            contentLayout.Controls.Add(fieldsLayout, 0, 0);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(0, 0);
            contentLayout.Margin = new Padding(0);
            contentLayout.Name = "contentLayout";
            contentLayout.Padding = new Padding(18);
            contentLayout.RowCount = 3;
            contentLayout.RowStyles.Add(new RowStyle());
            contentLayout.RowStyles.Add(new RowStyle());
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentLayout.Size = new Size(496, 302);
            contentLayout.TabIndex = 0;
            // 
            // actionsLayout
            // 
            actionsLayout.ColumnCount = 3;
            actionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actionsLayout.ColumnStyles.Add(new ColumnStyle());
            actionsLayout.ColumnStyles.Add(new ColumnStyle());
            actionsLayout.Controls.Add(cancelButton, 1, 0);
            actionsLayout.Controls.Add(saveButton, 2, 0);
            actionsLayout.Dock = DockStyle.Fill;
            actionsLayout.Location = new Point(18, 224);
            actionsLayout.Margin = new Padding(0, 16, 0, 0);
            actionsLayout.Name = "actionsLayout";
            actionsLayout.RowCount = 1;
            actionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actionsLayout.Size = new Size(460, 60);
            actionsLayout.TabIndex = 2;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(264, 0);
            cancelButton.Margin = new Padding(0, 0, 8, 0);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(92, 32);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(364, 0);
            saveButton.Margin = new Padding(0);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(96, 32);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save SCF";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(18, 193);
            statusLabel.Margin = new Padding(0, 12, 0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(82, 15);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Loading SCF...";
            // 
            // fieldsLayout
            // 
            fieldsLayout.ColumnCount = 2;
            fieldsLayout.ColumnStyles.Add(new ColumnStyle());
            fieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fieldsLayout.Controls.Add(balanceAmountTextBox, 1, 4);
            fieldsLayout.Controls.Add(balanceAmountLabel, 0, 4);
            fieldsLayout.Controls.Add(monthlyAmountTextBox, 1, 3);
            fieldsLayout.Controls.Add(monthlyAmountLabel, 0, 3);
            fieldsLayout.Controls.Add(totalAmountTextBox, 1, 2);
            fieldsLayout.Controls.Add(totalAmountLabel, 0, 2);
            fieldsLayout.Controls.Add(concessionaireNameTextBox, 1, 1);
            fieldsLayout.Controls.Add(concessionaireNameLabel, 0, 1);
            fieldsLayout.Controls.Add(accountNoTextBox, 1, 0);
            fieldsLayout.Controls.Add(accountNoLabel, 0, 0);
            fieldsLayout.Dock = DockStyle.Top;
            fieldsLayout.Location = new Point(18, 18);
            fieldsLayout.Margin = new Padding(0);
            fieldsLayout.Name = "fieldsLayout";
            fieldsLayout.RowCount = 5;
            fieldsLayout.RowStyles.Add(new RowStyle());
            fieldsLayout.RowStyles.Add(new RowStyle());
            fieldsLayout.RowStyles.Add(new RowStyle());
            fieldsLayout.RowStyles.Add(new RowStyle());
            fieldsLayout.RowStyles.Add(new RowStyle());
            fieldsLayout.Size = new Size(460, 163);
            fieldsLayout.TabIndex = 0;
            // 
            // balanceAmountTextBox
            // 
            balanceAmountTextBox.Dock = DockStyle.Fill;
            balanceAmountTextBox.Location = new Point(134, 116);
            balanceAmountTextBox.Margin = new Padding(12, 6, 0, 0);
            balanceAmountTextBox.Name = "balanceAmountTextBox";
            balanceAmountTextBox.PlaceholderText = "0.00";
            balanceAmountTextBox.Size = new Size(326, 23);
            balanceAmountTextBox.TabIndex = 9;
            // 
            // balanceAmountLabel
            // 
            balanceAmountLabel.Anchor = AnchorStyles.Left;
            balanceAmountLabel.AutoSize = true;
            balanceAmountLabel.Location = new Point(0, 132);
            balanceAmountLabel.Margin = new Padding(0, 6, 0, 0);
            balanceAmountLabel.Name = "balanceAmountLabel";
            balanceAmountLabel.Size = new Size(118, 15);
            balanceAmountLabel.TabIndex = 8;
            balanceAmountLabel.Text = "SCF Balance Amount";
            // 
            // monthlyAmountTextBox
            // 
            monthlyAmountTextBox.Dock = DockStyle.Fill;
            monthlyAmountTextBox.Location = new Point(134, 87);
            monthlyAmountTextBox.Margin = new Padding(12, 6, 0, 0);
            monthlyAmountTextBox.Name = "monthlyAmountTextBox";
            monthlyAmountTextBox.PlaceholderText = "0.00";
            monthlyAmountTextBox.Size = new Size(326, 23);
            monthlyAmountTextBox.TabIndex = 7;
            // 
            // monthlyAmountLabel
            // 
            monthlyAmountLabel.Anchor = AnchorStyles.Left;
            monthlyAmountLabel.AutoSize = true;
            monthlyAmountLabel.Location = new Point(0, 91);
            monthlyAmountLabel.Margin = new Padding(0, 6, 0, 0);
            monthlyAmountLabel.Name = "monthlyAmountLabel";
            monthlyAmountLabel.Size = new Size(122, 15);
            monthlyAmountLabel.TabIndex = 6;
            monthlyAmountLabel.Text = "SCF Monthly Amount";
            // 
            // totalAmountTextBox
            // 
            totalAmountTextBox.Dock = DockStyle.Fill;
            totalAmountTextBox.Location = new Point(134, 58);
            totalAmountTextBox.Margin = new Padding(12, 6, 0, 0);
            totalAmountTextBox.Name = "totalAmountTextBox";
            totalAmountTextBox.PlaceholderText = "0.00";
            totalAmountTextBox.Size = new Size(326, 23);
            totalAmountTextBox.TabIndex = 5;
            // 
            // totalAmountLabel
            // 
            totalAmountLabel.Anchor = AnchorStyles.Left;
            totalAmountLabel.AutoSize = true;
            totalAmountLabel.Location = new Point(0, 62);
            totalAmountLabel.Margin = new Padding(0, 6, 0, 0);
            totalAmountLabel.Name = "totalAmountLabel";
            totalAmountLabel.Size = new Size(102, 15);
            totalAmountLabel.TabIndex = 4;
            totalAmountLabel.Text = "SCF Total Amount";
            // 
            // concessionaireNameTextBox
            // 
            concessionaireNameTextBox.Dock = DockStyle.Fill;
            concessionaireNameTextBox.Location = new Point(134, 29);
            concessionaireNameTextBox.Margin = new Padding(12, 6, 0, 0);
            concessionaireNameTextBox.Name = "concessionaireNameTextBox";
            concessionaireNameTextBox.Size = new Size(326, 23);
            concessionaireNameTextBox.TabIndex = 3;
            // 
            // concessionaireNameLabel
            // 
            concessionaireNameLabel.Anchor = AnchorStyles.Left;
            concessionaireNameLabel.AutoSize = true;
            concessionaireNameLabel.Location = new Point(0, 33);
            concessionaireNameLabel.Margin = new Padding(0, 6, 0, 0);
            concessionaireNameLabel.Name = "concessionaireNameLabel";
            concessionaireNameLabel.Size = new Size(122, 15);
            concessionaireNameLabel.TabIndex = 2;
            concessionaireNameLabel.Text = "Concessionaire Name";
            // 
            // accountNoTextBox
            // 
            accountNoTextBox.Dock = DockStyle.Fill;
            accountNoTextBox.Location = new Point(134, 0);
            accountNoTextBox.Margin = new Padding(12, 0, 0, 0);
            accountNoTextBox.Name = "accountNoTextBox";
            accountNoTextBox.Size = new Size(326, 23);
            accountNoTextBox.TabIndex = 1;
            // 
            // accountNoLabel
            // 
            accountNoLabel.Anchor = AnchorStyles.Left;
            accountNoLabel.AutoSize = true;
            accountNoLabel.Location = new Point(0, 4);
            accountNoLabel.Margin = new Padding(0);
            accountNoLabel.Name = "accountNoLabel";
            accountNoLabel.Size = new Size(71, 15);
            accountNoLabel.TabIndex = 0;
            accountNoLabel.Text = "Account No";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(24, 24);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(73, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Initialize SCF";
            // 
            // InitializeSCFForm
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(544, 381);
            Controls.Add(rootLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InitializeSCFForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Initialize SCF";
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            contentPanel.ResumeLayout(false);
            contentLayout.ResumeLayout(false);
            contentLayout.PerformLayout();
            actionsLayout.ResumeLayout(false);
            fieldsLayout.ResumeLayout(false);
            fieldsLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Panel contentPanel;
        private TableLayoutPanel contentLayout;
        private TableLayoutPanel fieldsLayout;
        private Label accountNoLabel;
        private TextBox accountNoTextBox;
        private Label concessionaireNameLabel;
        private TextBox concessionaireNameTextBox;
        private Label totalAmountLabel;
        private TextBox totalAmountTextBox;
        private Label monthlyAmountLabel;
        private TextBox monthlyAmountTextBox;
        private Label balanceAmountLabel;
        private TextBox balanceAmountTextBox;
        private Label statusLabel;
        private TableLayoutPanel actionsLayout;
        private Button cancelButton;
        private Button saveButton;
    }
}