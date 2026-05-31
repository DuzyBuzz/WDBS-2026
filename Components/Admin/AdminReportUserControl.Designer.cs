namespace WDBS_2026.Components.Admin
{
    partial class AdminReportUserControl
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
            reportsTabControl = new TabControl();
            billingTabPage = new TabPage();
            billingHostPanel = new Panel();
            collectionTabPage = new TabPage();
            collectionHostPanel = new Panel();
            agingTabPage = new TabPage();
            agingHostPanel = new Panel();
            agingScfTabPage = new TabPage();
            agingScfHostPanel = new Panel();
            rootLayout.SuspendLayout();
            reportsTabControl.SuspendLayout();
            billingTabPage.SuspendLayout();
            collectionTabPage.SuspendLayout();
            agingTabPage.SuspendLayout();
            agingScfTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(reportsTabControl, 0, 1);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(0, 4, 0, 0);
            rootLayout.RowCount = 2;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1100, 700);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(0, 4);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(47, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Reports";
            // 
            // reportsTabControl
            // 
            reportsTabControl.Controls.Add(billingTabPage);
            reportsTabControl.Controls.Add(collectionTabPage);
            reportsTabControl.Controls.Add(agingTabPage);
            reportsTabControl.Controls.Add(agingScfTabPage);
            reportsTabControl.Dock = DockStyle.Fill;
            reportsTabControl.Location = new Point(0, 31);
            reportsTabControl.Margin = new Padding(0, 12, 0, 0);
            reportsTabControl.Name = "reportsTabControl";
            reportsTabControl.SelectedIndex = 0;
            reportsTabControl.Size = new Size(1100, 669);
            reportsTabControl.TabIndex = 1;
            // 
            // billingTabPage
            // 
            billingTabPage.Controls.Add(billingHostPanel);
            billingTabPage.Location = new Point(4, 24);
            billingTabPage.Name = "billingTabPage";
            billingTabPage.Padding = new Padding(3);
            billingTabPage.Size = new Size(1092, 641);
            billingTabPage.TabIndex = 1;
            billingTabPage.Text = "Billing";
            billingTabPage.UseVisualStyleBackColor = true;
            // 
            // billingHostPanel
            // 
            billingHostPanel.Dock = DockStyle.Fill;
            billingHostPanel.Location = new Point(3, 3);
            billingHostPanel.Margin = new Padding(0);
            billingHostPanel.Name = "billingHostPanel";
            billingHostPanel.Size = new Size(1086, 635);
            billingHostPanel.TabIndex = 0;
            // 
            // collectionTabPage
            // 
            collectionTabPage.Controls.Add(collectionHostPanel);
            collectionTabPage.Location = new Point(4, 24);
            collectionTabPage.Name = "collectionTabPage";
            collectionTabPage.Padding = new Padding(3);
            collectionTabPage.Size = new Size(1092, 641);
            collectionTabPage.TabIndex = 2;
            collectionTabPage.Text = "Collection";
            collectionTabPage.UseVisualStyleBackColor = true;
            // 
            // collectionHostPanel
            // 
            collectionHostPanel.Dock = DockStyle.Fill;
            collectionHostPanel.Location = new Point(3, 3);
            collectionHostPanel.Margin = new Padding(0);
            collectionHostPanel.Name = "collectionHostPanel";
            collectionHostPanel.Size = new Size(1086, 635);
            collectionHostPanel.TabIndex = 0;
            // 
            // agingTabPage
            // 
            agingTabPage.Controls.Add(agingHostPanel);
            agingTabPage.Location = new Point(4, 24);
            agingTabPage.Name = "agingTabPage";
            agingTabPage.Padding = new Padding(3);
            agingTabPage.Size = new Size(1092, 641);
            agingTabPage.TabIndex = 3;
            agingTabPage.Text = "Aging of Accounts";
            agingTabPage.UseVisualStyleBackColor = true;
            // 
            // agingHostPanel
            // 
            agingHostPanel.Dock = DockStyle.Fill;
            agingHostPanel.Location = new Point(3, 3);
            agingHostPanel.Margin = new Padding(0);
            agingHostPanel.Name = "agingHostPanel";
            agingHostPanel.Size = new Size(1086, 635);
            agingHostPanel.TabIndex = 0;
            // 
            // agingScfTabPage
            // 
            agingScfTabPage.Controls.Add(agingScfHostPanel);
            agingScfTabPage.Location = new Point(4, 24);
            agingScfTabPage.Name = "agingScfTabPage";
            agingScfTabPage.Padding = new Padding(3);
            agingScfTabPage.Size = new Size(1092, 641);
            agingScfTabPage.TabIndex = 4;
            agingScfTabPage.Text = "Aging of SCF";
            agingScfTabPage.UseVisualStyleBackColor = true;
            // 
            // agingScfHostPanel
            // 
            agingScfHostPanel.Dock = DockStyle.Fill;
            agingScfHostPanel.Location = new Point(3, 3);
            agingScfHostPanel.Margin = new Padding(0);
            agingScfHostPanel.Name = "agingScfHostPanel";
            agingScfHostPanel.Size = new Size(1086, 635);
            agingScfHostPanel.TabIndex = 0;
            // 
            // AdminReportUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "AdminReportUserControl";
            Size = new Size(1100, 700);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            reportsTabControl.ResumeLayout(false);
            billingTabPage.ResumeLayout(false);
            collectionTabPage.ResumeLayout(false);
            agingTabPage.ResumeLayout(false);
            agingScfTabPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private TabControl reportsTabControl;
        private TabPage billingTabPage;
        private Panel billingHostPanel;
        private TabPage collectionTabPage;
        private Panel collectionHostPanel;
        private TabPage agingTabPage;
        private Panel agingHostPanel;
        private TabPage agingScfTabPage;
        private Panel agingScfHostPanel;
    }
}
