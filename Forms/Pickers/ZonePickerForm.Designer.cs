namespace WDBS_2026.Forms.Pickers
{
    partial class ZonePickerForm
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
            zoneLabel = new Label();
            zoneComboBox = new ComboBox();
            actionLayout = new TableLayoutPanel();
            cancelButton = new Button();
            selectButton = new Button();
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
            rootLayout.Controls.Add(zoneLabel, 0, 2);
            rootLayout.Controls.Add(zoneComboBox, 0, 3);
            rootLayout.Controls.Add(actionLayout, 0, 4);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(24);
            rootLayout.RowCount = 5;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.Size = new Size(460, 288);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(24, 24);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(144, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Print Latest Reading Sheet";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(24, 47);
            subtitleLabel.Margin = new Padding(0, 8, 0, 0);
            subtitleLabel.MaximumSize = new Size(380, 0);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(362, 15);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Select the zone to generate and print the latest meter reading sheet.";
            // 
            // zoneLabel
            // 
            zoneLabel.AutoSize = true;
            zoneLabel.Location = new Point(24, 86);
            zoneLabel.Margin = new Padding(0, 24, 0, 0);
            zoneLabel.Name = "zoneLabel";
            zoneLabel.Size = new Size(34, 15);
            zoneLabel.TabIndex = 2;
            zoneLabel.Text = "Zone";
            // 
            // zoneComboBox
            // 
            zoneComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            zoneComboBox.FormattingEnabled = true;
            zoneComboBox.Location = new Point(24, 109);
            zoneComboBox.Margin = new Padding(0, 8, 0, 0);
            zoneComboBox.Name = "zoneComboBox";
            zoneComboBox.Size = new Size(412, 23);
            zoneComboBox.TabIndex = 3;
            // 
            // actionLayout
            // 
            actionLayout.ColumnCount = 3;
            actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.Controls.Add(cancelButton, 1, 0);
            actionLayout.Controls.Add(selectButton, 2, 0);
            actionLayout.Dock = DockStyle.Fill;
            actionLayout.Location = new Point(24, 156);
            actionLayout.Margin = new Padding(0, 24, 0, 0);
            actionLayout.Name = "actionLayout";
            actionLayout.RowCount = 1;
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actionLayout.Size = new Size(412, 108);
            actionLayout.TabIndex = 4;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(200, 0);
            cancelButton.Margin = new Padding(0, 0, 8, 0);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(96, 36);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // selectButton
            // 
            selectButton.Location = new Point(304, 0);
            selectButton.Margin = new Padding(0);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(108, 36);
            selectButton.TabIndex = 1;
            selectButton.Text = "Print Sheet";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += selectButton_Click;
            // 
            // ZonePickerForm
            // 
            AcceptButton = selectButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(460, 288);
            Controls.Add(rootLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ZonePickerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Zone";
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            actionLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Label subtitleLabel;
        private Label zoneLabel;
        private ComboBox zoneComboBox;
        private TableLayoutPanel actionLayout;
        private Button cancelButton;
        private Button selectButton;
    }
}