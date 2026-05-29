namespace WDBS_2026.Forms.Report
{
    partial class PrintPreviewForm
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
            actionLayout = new TableLayoutPanel();
            printButton = new Button();
            pdfButton = new Button();
            excelButton = new Button();
            zoomInfoLabel = new Label();
            zoomHintLabel = new Label();
            nextPageButton = new Button();
            pageInfoLabel = new Label();
            pageNumberTextBox = new TextBox();
            previousPageButton = new Button();
            previewHostPanel = new Panel();
            previewControl = new PrintPreviewControl();
            subtitleLabel = new Label();
            titleLabel = new Label();
            rootLayout.SuspendLayout();
            actionLayout.SuspendLayout();
            previewHostPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(actionLayout, 0, 2);
            rootLayout.Controls.Add(previewHostPanel, 0, 3);
            rootLayout.Controls.Add(subtitleLabel, 0, 1);
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(24);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1180, 760);
            rootLayout.TabIndex = 0;
            // 
            // actionLayout
            // 
            actionLayout.ColumnCount = 10;
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.ColumnStyles.Add(new ColumnStyle());
            actionLayout.Controls.Add(printButton, 0, 0);
            actionLayout.Controls.Add(pdfButton, 1, 0);
            actionLayout.Controls.Add(excelButton, 2, 0);
            actionLayout.Controls.Add(zoomInfoLabel, 3, 0);
            actionLayout.Controls.Add(zoomHintLabel, 4, 0);
            actionLayout.Controls.Add(nextPageButton, 9, 0);
            actionLayout.Controls.Add(pageInfoLabel, 8, 0);
            actionLayout.Controls.Add(pageNumberTextBox, 7, 0);
            actionLayout.Controls.Add(previousPageButton, 6, 0);
            actionLayout.Dock = DockStyle.Fill;
            actionLayout.Location = new Point(24, 71);
            actionLayout.Margin = new Padding(0, 14, 0, 0);
            actionLayout.Name = "actionLayout";
            actionLayout.RowCount = 1;
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actionLayout.Size = new Size(1132, 40);
            actionLayout.TabIndex = 2;
            // 
            // printButton
            // 
            printButton.Location = new Point(0, 0);
            printButton.Margin = new Padding(0);
            printButton.Name = "printButton";
            printButton.Size = new Size(104, 36);
            printButton.TabIndex = 0;
            printButton.Text = "Print";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += printButton_Click;
            // 
            // pdfButton
            // 
            pdfButton.Location = new Point(112, 0);
            pdfButton.Margin = new Padding(8, 0, 0, 0);
            pdfButton.Name = "pdfButton";
            pdfButton.Size = new Size(104, 36);
            pdfButton.TabIndex = 1;
            pdfButton.Text = "PDF";
            pdfButton.UseVisualStyleBackColor = true;
            pdfButton.Click += pdfButton_Click;
            // 
            // excelButton
            // 
            excelButton.Location = new Point(224, 0);
            excelButton.Margin = new Padding(8, 0, 0, 0);
            excelButton.Name = "excelButton";
            excelButton.Size = new Size(104, 36);
            excelButton.TabIndex = 2;
            excelButton.Text = "EXCEL";
            excelButton.UseVisualStyleBackColor = true;
            excelButton.Click += excelButton_Click;
            // 
            // zoomInfoLabel
            // 
            zoomInfoLabel.Anchor = AnchorStyles.Left;
            zoomInfoLabel.AutoSize = true;
            zoomInfoLabel.Location = new Point(336, 12);
            zoomInfoLabel.Margin = new Padding(8, 0, 12, 0);
            zoomInfoLabel.Name = "zoomInfoLabel";
            zoomInfoLabel.Size = new Size(66, 15);
            zoomInfoLabel.TabIndex = 3;
            zoomInfoLabel.Text = "Zoom: Auto";
            // 
            // zoomHintLabel
            // 
            zoomHintLabel.Anchor = AnchorStyles.Left;
            zoomHintLabel.AutoSize = true;
            zoomHintLabel.Location = new Point(414, 12);
            zoomHintLabel.Margin = new Padding(0);
            zoomHintLabel.Name = "zoomHintLabel";
            zoomHintLabel.Size = new Size(119, 15);
            zoomHintLabel.TabIndex = 4;
            zoomHintLabel.Text = "Ctrl + Wheel to zoom";
            // 
            // nextPageButton
            // 
                nextPageButton.Location = new Point(1050, 0);
            nextPageButton.Margin = new Padding(8, 0, 0, 0);
            nextPageButton.Name = "nextPageButton";
            nextPageButton.Size = new Size(96, 36);
                nextPageButton.TabIndex = 8;
            nextPageButton.Text = "Next";
            nextPageButton.UseVisualStyleBackColor = true;
            nextPageButton.Click += nextPageButton_Click;
            // 
            // pageInfoLabel
            // 
            pageInfoLabel.Anchor = AnchorStyles.Left;
            pageInfoLabel.AutoSize = true;
                pageInfoLabel.Location = new Point(1017, 12);
            pageInfoLabel.Margin = new Padding(8, 0, 8, 0);
            pageInfoLabel.Name = "pageInfoLabel";
                pageInfoLabel.Size = new Size(23, 15);
                pageInfoLabel.TabIndex = 7;
                pageInfoLabel.Text = "of 1";
                // 
                // pageNumberTextBox
                // 
                pageNumberTextBox.Anchor = AnchorStyles.Left;
                pageNumberTextBox.Location = new Point(953, 8);
                pageNumberTextBox.Margin = new Padding(8, 0, 0, 0);
                pageNumberTextBox.Name = "pageNumberTextBox";
                pageNumberTextBox.Size = new Size(56, 23);
                pageNumberTextBox.TabIndex = 6;
                pageNumberTextBox.Text = "1";
                pageNumberTextBox.TextAlign = HorizontalAlignment.Center;
                pageNumberTextBox.KeyDown += pageNumberTextBox_KeyDown;
                pageNumberTextBox.Leave += pageNumberTextBox_Leave;
            // 
            // previousPageButton
            // 
            previousPageButton.Location = new Point(849, 0);
            previousPageButton.Margin = new Padding(0);
            previousPageButton.Name = "previousPageButton";
            previousPageButton.Size = new Size(96, 36);
            previousPageButton.TabIndex = 5;
            previousPageButton.Text = "Prev";
            previousPageButton.UseVisualStyleBackColor = true;
            previousPageButton.Click += previousPageButton_Click;
            // 
            // previewHostPanel
            // 
            previewHostPanel.Controls.Add(previewControl);
            previewHostPanel.Dock = DockStyle.Fill;
            previewHostPanel.Location = new Point(24, 125);
            previewHostPanel.Margin = new Padding(0, 14, 0, 0);
            previewHostPanel.Name = "previewHostPanel";
            previewHostPanel.Padding = new Padding(12);
            previewHostPanel.Size = new Size(1132, 611);
            previewHostPanel.TabIndex = 3;
            // 
            // previewControl
            // 
            previewControl.Dock = DockStyle.Fill;
            previewControl.Location = new Point(12, 12);
            previewControl.Name = "previewControl";
            previewControl.Size = new Size(1108, 587);
            previewControl.TabIndex = 0;
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(24, 42);
            subtitleLabel.Margin = new Padding(0, 6, 0, 0);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(42, 15);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Period";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(24, 24);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(84, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Report Preview";
            // 
            // PrintPreviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 760);
            Controls.Add(rootLayout);
            MinimumSize = new Size(980, 620);
            Name = "PrintPreviewForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Report Preview";
            WindowState = FormWindowState.Maximized;
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            actionLayout.ResumeLayout(false);
            actionLayout.PerformLayout();
            previewHostPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Label subtitleLabel;
        private TableLayoutPanel actionLayout;
        private Button printButton;
        private Button pdfButton;
        private Button excelButton;
        private Label zoomInfoLabel;
        private Label zoomHintLabel;
        private Button previousPageButton;
        private TextBox pageNumberTextBox;
        private Label pageInfoLabel;
        private Button nextPageButton;
        private Panel previewHostPanel;
        private PrintPreviewControl previewControl;
    }
}