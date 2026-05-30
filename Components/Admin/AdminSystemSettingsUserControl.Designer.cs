namespace WDBS_2026.Components.Admin
{
    partial class AdminSystemSettingsUserControl
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
            settingsCardPanel = new Panel();
            settingsCardLayout = new TableLayoutPanel();
            systemSettingsSectionLabel = new Label();
            settingsInputLayout = new TableLayoutPanel();
            discountPercentLabel = new Label();
            discountPercentTextBox = new TextBox();
            discountThresholdLabel = new Label();
            discountThresholdTextBox = new TextBox();
            penalizeAfterDaysLabel = new Label();
            penalizeAfterDaysTextBox = new TextBox();
            penaltyPercentLabel = new Label();
            penaltyPercentTextBox = new TextBox();
            taxPercentLabel = new Label();
            taxPercentTextBox = new TextBox();
            saveSettingsButton = new Button();
            splitContainer = new SplitContainer();
            servicesPanel = new Panel();
            servicesLayout = new TableLayoutPanel();
            servicesHeaderPanel = new Panel();
            servicesSectionLabel = new Label();
            servicesGrid = new DataGridView();
            zonesPanel = new Panel();
            zonesLayout = new TableLayoutPanel();
            zonesHeaderPanel = new Panel();
            zonesSectionLabel = new Label();
            zonesGrid = new DataGridView();
            statusLabel = new Label();
            rootLayout.SuspendLayout();
            settingsCardPanel.SuspendLayout();
            settingsCardLayout.SuspendLayout();
            settingsInputLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            servicesPanel.SuspendLayout();
            servicesLayout.SuspendLayout();
            servicesHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)servicesGrid).BeginInit();
            zonesPanel.SuspendLayout();
            zonesLayout.SuspendLayout();
            zonesHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)zonesGrid).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(settingsCardPanel, 0, 1);
            rootLayout.Controls.Add(splitContainer, 0, 2);
            rootLayout.Controls.Add(statusLabel, 0, 3);
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
            rootLayout.Size = new Size(1120, 720);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(0, 4);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(147, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Admin System Settings";
            // 
            // settingsCardPanel
            // 
            settingsCardPanel.Controls.Add(settingsCardLayout);
            settingsCardPanel.Dock = DockStyle.Fill;
            settingsCardPanel.Location = new Point(0, 31);
            settingsCardPanel.Margin = new Padding(0, 12, 0, 0);
            settingsCardPanel.Name = "settingsCardPanel";
            settingsCardPanel.Padding = new Padding(14, 12, 14, 12);
            settingsCardPanel.Size = new Size(1120, 125);
            settingsCardPanel.TabIndex = 1;
            // 
            // settingsCardLayout
            // 
            settingsCardLayout.ColumnCount = 1;
            settingsCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            settingsCardLayout.Controls.Add(systemSettingsSectionLabel, 0, 0);
            settingsCardLayout.Controls.Add(settingsInputLayout, 0, 1);
            settingsCardLayout.Dock = DockStyle.Fill;
            settingsCardLayout.Location = new Point(14, 12);
            settingsCardLayout.Margin = new Padding(0);
            settingsCardLayout.Name = "settingsCardLayout";
            settingsCardLayout.RowCount = 2;
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.Size = new Size(1092, 101);
            settingsCardLayout.TabIndex = 0;
            // 
            // systemSettingsSectionLabel
            // 
            systemSettingsSectionLabel.AutoSize = true;
            systemSettingsSectionLabel.Location = new Point(0, 0);
            systemSettingsSectionLabel.Margin = new Padding(0);
            systemSettingsSectionLabel.Name = "systemSettingsSectionLabel";
            systemSettingsSectionLabel.Size = new Size(94, 15);
            systemSettingsSectionLabel.TabIndex = 0;
            systemSettingsSectionLabel.Text = "System Settings";
            // 
            // settingsInputLayout
            // 
            settingsInputLayout.ColumnCount = 6;
            settingsInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            settingsInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            settingsInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            settingsInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            settingsInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            settingsInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            settingsInputLayout.Controls.Add(discountPercentLabel, 0, 0);
            settingsInputLayout.Controls.Add(discountPercentTextBox, 0, 1);
            settingsInputLayout.Controls.Add(discountThresholdLabel, 1, 0);
            settingsInputLayout.Controls.Add(discountThresholdTextBox, 1, 1);
            settingsInputLayout.Controls.Add(penalizeAfterDaysLabel, 2, 0);
            settingsInputLayout.Controls.Add(penalizeAfterDaysTextBox, 2, 1);
            settingsInputLayout.Controls.Add(penaltyPercentLabel, 3, 0);
            settingsInputLayout.Controls.Add(penaltyPercentTextBox, 3, 1);
            settingsInputLayout.Controls.Add(taxPercentLabel, 4, 0);
            settingsInputLayout.Controls.Add(taxPercentTextBox, 4, 1);
            settingsInputLayout.Controls.Add(saveSettingsButton, 5, 1);
            settingsInputLayout.Dock = DockStyle.Fill;
            settingsInputLayout.Location = new Point(0, 23);
            settingsInputLayout.Margin = new Padding(0, 8, 0, 0);
            settingsInputLayout.Name = "settingsInputLayout";
            settingsInputLayout.RowCount = 2;
            settingsInputLayout.RowStyles.Add(new RowStyle());
            settingsInputLayout.RowStyles.Add(new RowStyle());
            settingsInputLayout.Size = new Size(1092, 78);
            settingsInputLayout.TabIndex = 1;
            // 
            // discountPercentLabel
            // 
            discountPercentLabel.AutoSize = true;
            discountPercentLabel.Location = new Point(0, 0);
            discountPercentLabel.Margin = new Padding(0);
            discountPercentLabel.Name = "discountPercentLabel";
            discountPercentLabel.Size = new Size(92, 15);
            discountPercentLabel.TabIndex = 0;
            discountPercentLabel.Text = "Discount Percent";
            // 
            // discountPercentTextBox
            // 
            discountPercentTextBox.Dock = DockStyle.Top;
            discountPercentTextBox.Location = new Point(0, 19);
            discountPercentTextBox.Margin = new Padding(0, 4, 8, 0);
            discountPercentTextBox.Name = "discountPercentTextBox";
            discountPercentTextBox.Size = new Size(188, 23);
            discountPercentTextBox.TabIndex = 5;
            // 
            // discountThresholdLabel
            // 
            discountThresholdLabel.AutoSize = true;
            discountThresholdLabel.Location = new Point(188, 0);
            discountThresholdLabel.Margin = new Padding(0);
            discountThresholdLabel.Name = "discountThresholdLabel";
            discountThresholdLabel.Size = new Size(111, 15);
            discountThresholdLabel.TabIndex = 1;
            discountThresholdLabel.Text = "Discount Threshold";
            // 
            // discountThresholdTextBox
            // 
            discountThresholdTextBox.Dock = DockStyle.Top;
            discountThresholdTextBox.Location = new Point(188, 19);
            discountThresholdTextBox.Margin = new Padding(0, 4, 8, 0);
            discountThresholdTextBox.Name = "discountThresholdTextBox";
            discountThresholdTextBox.Size = new Size(188, 23);
            discountThresholdTextBox.TabIndex = 6;
            // 
            // penalizeAfterDaysLabel
            // 
            penalizeAfterDaysLabel.AutoSize = true;
            penalizeAfterDaysLabel.Location = new Point(376, 0);
            penalizeAfterDaysLabel.Margin = new Padding(0);
            penalizeAfterDaysLabel.Name = "penalizeAfterDaysLabel";
            penalizeAfterDaysLabel.Size = new Size(102, 15);
            penalizeAfterDaysLabel.TabIndex = 2;
            penalizeAfterDaysLabel.Text = "Penalize After Days";
            // 
            // penalizeAfterDaysTextBox
            // 
            penalizeAfterDaysTextBox.Dock = DockStyle.Top;
            penalizeAfterDaysTextBox.Location = new Point(376, 19);
            penalizeAfterDaysTextBox.Margin = new Padding(0, 4, 8, 0);
            penalizeAfterDaysTextBox.Name = "penalizeAfterDaysTextBox";
            penalizeAfterDaysTextBox.Size = new Size(188, 23);
            penalizeAfterDaysTextBox.TabIndex = 7;
            // 
            // penaltyPercentLabel
            // 
            penaltyPercentLabel.AutoSize = true;
            penaltyPercentLabel.Location = new Point(564, 0);
            penaltyPercentLabel.Margin = new Padding(0);
            penaltyPercentLabel.Name = "penaltyPercentLabel";
            penaltyPercentLabel.Size = new Size(88, 15);
            penaltyPercentLabel.TabIndex = 3;
            penaltyPercentLabel.Text = "Penalty Percent";
            // 
            // penaltyPercentTextBox
            // 
            penaltyPercentTextBox.Dock = DockStyle.Top;
            penaltyPercentTextBox.Location = new Point(564, 19);
            penaltyPercentTextBox.Margin = new Padding(0, 4, 8, 0);
            penaltyPercentTextBox.Name = "penaltyPercentTextBox";
            penaltyPercentTextBox.Size = new Size(188, 23);
            penaltyPercentTextBox.TabIndex = 8;
            // 
            // taxPercentLabel
            // 
            taxPercentLabel.AutoSize = true;
            taxPercentLabel.Location = new Point(752, 0);
            taxPercentLabel.Margin = new Padding(0);
            taxPercentLabel.Name = "taxPercentLabel";
            taxPercentLabel.Size = new Size(67, 15);
            taxPercentLabel.TabIndex = 4;
            taxPercentLabel.Text = "Tax Percent";
            // 
            // taxPercentTextBox
            // 
            taxPercentTextBox.Dock = DockStyle.Top;
            taxPercentTextBox.Location = new Point(752, 19);
            taxPercentTextBox.Margin = new Padding(0, 4, 8, 0);
            taxPercentTextBox.Name = "taxPercentTextBox";
            taxPercentTextBox.Size = new Size(188, 23);
            taxPercentTextBox.TabIndex = 9;
            // 
            // saveSettingsButton
            // 
            saveSettingsButton.Anchor = AnchorStyles.Right;
            saveSettingsButton.Location = new Point(952, 16);
            saveSettingsButton.Margin = new Padding(0);
            saveSettingsButton.Name = "saveSettingsButton";
            saveSettingsButton.Size = new Size(140, 28);
            saveSettingsButton.TabIndex = 10;
            saveSettingsButton.Text = "Save All Changes";
            saveSettingsButton.UseVisualStyleBackColor = true;
            saveSettingsButton.Click += saveSettingsButton_Click;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 168);
            splitContainer.Margin = new Padding(0, 12, 0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(servicesPanel);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(zonesPanel);
            splitContainer.Size = new Size(1120, 525);
            splitContainer.SplitterDistance = 725;
            splitContainer.TabIndex = 2;
            // 
            // servicesPanel
            // 
            servicesPanel.Controls.Add(servicesLayout);
            servicesPanel.Dock = DockStyle.Fill;
            servicesPanel.Location = new Point(0, 0);
            servicesPanel.Margin = new Padding(0);
            servicesPanel.Name = "servicesPanel";
            servicesPanel.Padding = new Padding(0, 0, 6, 0);
            servicesPanel.Size = new Size(725, 525);
            servicesPanel.TabIndex = 0;
            // 
            // servicesLayout
            // 
            servicesLayout.ColumnCount = 1;
            servicesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            servicesLayout.Controls.Add(servicesHeaderPanel, 0, 0);
            servicesLayout.Controls.Add(servicesGrid, 0, 1);
            servicesLayout.Dock = DockStyle.Fill;
            servicesLayout.Location = new Point(0, 0);
            servicesLayout.Margin = new Padding(0);
            servicesLayout.Name = "servicesLayout";
            servicesLayout.RowCount = 2;
            servicesLayout.RowStyles.Add(new RowStyle());
            servicesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            servicesLayout.Size = new Size(719, 525);
            servicesLayout.TabIndex = 0;
            // 
            // servicesHeaderPanel
            // 
            servicesHeaderPanel.Controls.Add(servicesSectionLabel);
            servicesHeaderPanel.Dock = DockStyle.Fill;
            servicesHeaderPanel.Location = new Point(0, 0);
            servicesHeaderPanel.Margin = new Padding(0);
            servicesHeaderPanel.Name = "servicesHeaderPanel";
            servicesHeaderPanel.Size = new Size(719, 32);
            servicesHeaderPanel.TabIndex = 0;
            // 
            // servicesSectionLabel
            // 
            servicesSectionLabel.AutoSize = true;
            servicesSectionLabel.Location = new Point(0, 8);
            servicesSectionLabel.Margin = new Padding(0);
            servicesSectionLabel.Name = "servicesSectionLabel";
            servicesSectionLabel.Size = new Size(49, 15);
            servicesSectionLabel.TabIndex = 0;
            servicesSectionLabel.Text = "Services";
            // 
            // servicesGrid
            // 
            servicesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            servicesGrid.Dock = DockStyle.Fill;
            servicesGrid.Location = new Point(0, 32);
            servicesGrid.Margin = new Padding(0);
            servicesGrid.Name = "servicesGrid";
            servicesGrid.Size = new Size(719, 493);
            servicesGrid.TabIndex = 1;
            // 
            // zonesPanel
            // 
            zonesPanel.Controls.Add(zonesLayout);
            zonesPanel.Dock = DockStyle.Fill;
            zonesPanel.Location = new Point(0, 0);
            zonesPanel.Margin = new Padding(0);
            zonesPanel.Name = "zonesPanel";
            zonesPanel.Padding = new Padding(6, 0, 0, 0);
            zonesPanel.Size = new Size(391, 525);
            zonesPanel.TabIndex = 0;
            // 
            // zonesLayout
            // 
            zonesLayout.ColumnCount = 1;
            zonesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            zonesLayout.Controls.Add(zonesHeaderPanel, 0, 0);
            zonesLayout.Controls.Add(zonesGrid, 0, 1);
            zonesLayout.Dock = DockStyle.Fill;
            zonesLayout.Location = new Point(6, 0);
            zonesLayout.Margin = new Padding(0);
            zonesLayout.Name = "zonesLayout";
            zonesLayout.RowCount = 2;
            zonesLayout.RowStyles.Add(new RowStyle());
            zonesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            zonesLayout.Size = new Size(385, 525);
            zonesLayout.TabIndex = 0;
            // 
            // zonesHeaderPanel
            // 
            zonesHeaderPanel.Controls.Add(zonesSectionLabel);
            zonesHeaderPanel.Dock = DockStyle.Fill;
            zonesHeaderPanel.Location = new Point(0, 0);
            zonesHeaderPanel.Margin = new Padding(0);
            zonesHeaderPanel.Name = "zonesHeaderPanel";
            zonesHeaderPanel.Size = new Size(385, 32);
            zonesHeaderPanel.TabIndex = 0;
            // 
            // zonesSectionLabel
            // 
            zonesSectionLabel.AutoSize = true;
            zonesSectionLabel.Location = new Point(0, 8);
            zonesSectionLabel.Margin = new Padding(0);
            zonesSectionLabel.Name = "zonesSectionLabel";
            zonesSectionLabel.Size = new Size(40, 15);
            zonesSectionLabel.TabIndex = 0;
            zonesSectionLabel.Text = "Zones";
            // 
            // zonesGrid
            // 
            zonesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            zonesGrid.Dock = DockStyle.Fill;
            zonesGrid.Location = new Point(0, 32);
            zonesGrid.Margin = new Padding(0);
            zonesGrid.Name = "zonesGrid";
            zonesGrid.Size = new Size(385, 493);
            zonesGrid.TabIndex = 1;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(0, 696);
            statusLabel.Margin = new Padding(0, 3, 0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(117, 15);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Admin settings ready.";
            // 
            // AdminSystemSettingsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "AdminSystemSettingsUserControl";
            Size = new Size(1120, 720);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            settingsCardPanel.ResumeLayout(false);
            settingsCardLayout.ResumeLayout(false);
            settingsCardLayout.PerformLayout();
            settingsInputLayout.ResumeLayout(false);
            settingsInputLayout.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            servicesPanel.ResumeLayout(false);
            servicesLayout.ResumeLayout(false);
            servicesHeaderPanel.ResumeLayout(false);
            servicesHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)servicesGrid).EndInit();
            zonesPanel.ResumeLayout(false);
            zonesLayout.ResumeLayout(false);
            zonesHeaderPanel.ResumeLayout(false);
            zonesHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)zonesGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private Panel settingsCardPanel;
        private TableLayoutPanel settingsCardLayout;
        private Label systemSettingsSectionLabel;
        private TableLayoutPanel settingsInputLayout;
        private Label discountPercentLabel;
        private TextBox discountPercentTextBox;
        private Label discountThresholdLabel;
        private TextBox discountThresholdTextBox;
        private Label penalizeAfterDaysLabel;
        private TextBox penalizeAfterDaysTextBox;
        private Label penaltyPercentLabel;
        private TextBox penaltyPercentTextBox;
        private Label taxPercentLabel;
        private TextBox taxPercentTextBox;
        private Button saveSettingsButton;
        private SplitContainer splitContainer;
        private Panel servicesPanel;
        private TableLayoutPanel servicesLayout;
        private Panel servicesHeaderPanel;
        private Label servicesSectionLabel;
        private DataGridView servicesGrid;
        private Panel zonesPanel;
        private TableLayoutPanel zonesLayout;
        private Panel zonesHeaderPanel;
        private Label zonesSectionLabel;
        private DataGridView zonesGrid;
        private Label statusLabel;
    }
}
