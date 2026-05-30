namespace WDBS_2026.Forms.Collection
{
    partial class BillPreviewForm
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
            titleLabel = new Label();
            subtitleLabel = new Label();
            previewTextBox = new RichTextBox();
            actionLayout = new TableLayoutPanel();
            cancelButton = new Button();
            confirmButton = new Button();
            rootLayout.SuspendLayout();
            actionLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(subtitleLabel, 0, 1);
            rootLayout.Controls.Add(previewTextBox, 0, 2);
            rootLayout.Controls.Add(actionLayout, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(16);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.Size = new Size(840, 600);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(16, 16);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(124, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Collection Bill Preview";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(16, 39);
            subtitleLabel.Margin = new Padding(0, 8, 0, 0);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(351, 15);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Review details below. Confirm to post this collection transaction.";
            // 
            // previewTextBox
            // 
            previewTextBox.Dock = DockStyle.Fill;
            previewTextBox.Location = new Point(16, 72);
            previewTextBox.Margin = new Padding(0, 18, 0, 0);
            previewTextBox.Name = "previewTextBox";
            previewTextBox.ReadOnly = true;
            previewTextBox.Size = new Size(808, 470);
            previewTextBox.TabIndex = 2;
            previewTextBox.Text = "";
            previewTextBox.WordWrap = false;
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
            actionLayout.Location = new Point(16, 560);
            actionLayout.Margin = new Padding(0, 18, 0, 0);
            actionLayout.Name = "actionLayout";
            actionLayout.RowCount = 1;
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actionLayout.Size = new Size(808, 24);
            actionLayout.TabIndex = 3;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(560, 0);
            cancelButton.Margin = new Padding(0, 0, 8, 0);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(116, 24);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // confirmButton
            // 
            confirmButton.Location = new Point(684, 0);
            confirmButton.Margin = new Padding(0);
            confirmButton.Name = "confirmButton";
            confirmButton.Size = new Size(124, 24);
            confirmButton.TabIndex = 1;
            confirmButton.Text = "Confirm Payment";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += confirmButton_Click;
            // 
            // BillPreviewForm
            // 
            AcceptButton = confirmButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(840, 600);
            Controls.Add(rootLayout);
            MinimizeBox = false;
            MinimumSize = new Size(760, 520);
            Name = "BillPreviewForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bill Preview";
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            actionLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Label subtitleLabel;
        private RichTextBox previewTextBox;
        private TableLayoutPanel actionLayout;
        private Button cancelButton;
        private Button confirmButton;
    }
}