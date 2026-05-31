namespace WDBS_2026.Forms.Auditing
{
    partial class AuditingForm
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
            if (disposing && components != null)
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
            detailsCard = new Panel();
            detailsTable = new TableLayoutPanel();
            logIdCaptionLabel = new Label();
            logIdTextBox = new TextBox();
            loggedAtCaptionLabel = new Label();
            loggedAtTextBox = new TextBox();
            fullNameCaptionLabel = new Label();
            fullNameTextBox = new TextBox();
            usernameCaptionLabel = new Label();
            usernameTextBox = new TextBox();
            roleCaptionLabel = new Label();
            roleTextBox = new TextBox();
            actionTypeCaptionLabel = new Label();
            actionTypeTextBox = new TextBox();
            moduleCaptionLabel = new Label();
            moduleTextBox = new TextBox();
            entityNameCaptionLabel = new Label();
            entityNameTextBox = new TextBox();
            entityIdCaptionLabel = new Label();
            entityIdTextBox = new TextBox();
            severityCaptionLabel = new Label();
            severityTextBox = new TextBox();
            descriptionCaptionLabel = new Label();
            descriptionTextBox = new TextBox();
            activitySummaryCaptionLabel = new Label();
            activitySummaryTextBox = new TextBox();
            subtitleLabel = new Label();
            titleLabel = new Label();
            detailsCard.SuspendLayout();
            detailsTable.SuspendLayout();
            SuspendLayout();
            // 
            // detailsCard
            // 
            detailsCard.Controls.Add(detailsTable);
            detailsCard.Controls.Add(subtitleLabel);
            detailsCard.Controls.Add(titleLabel);
            detailsCard.Dock = DockStyle.Fill;
            detailsCard.Location = new Point(0, 0);
            detailsCard.Name = "detailsCard";
            detailsCard.Padding = new Padding(24);
            detailsCard.Size = new Size(860, 560);
            detailsCard.TabIndex = 0;
            // 
            // detailsTable
            // 
            detailsTable.ColumnCount = 1;
            detailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailsTable.Controls.Add(logIdCaptionLabel, 0, 0);
            detailsTable.Controls.Add(logIdTextBox, 0, 1);
            detailsTable.Controls.Add(loggedAtCaptionLabel, 0, 2);
            detailsTable.Controls.Add(loggedAtTextBox, 0, 3);
            detailsTable.Controls.Add(fullNameCaptionLabel, 0, 4);
            detailsTable.Controls.Add(fullNameTextBox, 0, 5);
            detailsTable.Controls.Add(usernameCaptionLabel, 0, 6);
            detailsTable.Controls.Add(usernameTextBox, 0, 7);
            detailsTable.Controls.Add(roleCaptionLabel, 0, 8);
            detailsTable.Controls.Add(roleTextBox, 0, 9);
            detailsTable.Controls.Add(actionTypeCaptionLabel, 0, 10);
            detailsTable.Controls.Add(actionTypeTextBox, 0, 11);
            detailsTable.Controls.Add(moduleCaptionLabel, 0, 12);
            detailsTable.Controls.Add(moduleTextBox, 0, 13);
            detailsTable.Controls.Add(entityNameCaptionLabel, 0, 14);
            detailsTable.Controls.Add(entityNameTextBox, 0, 15);
            detailsTable.Controls.Add(entityIdCaptionLabel, 0, 16);
            detailsTable.Controls.Add(entityIdTextBox, 0, 17);
            detailsTable.Controls.Add(severityCaptionLabel, 0, 18);
            detailsTable.Controls.Add(severityTextBox, 0, 19);
            detailsTable.Controls.Add(descriptionCaptionLabel, 0, 20);
            detailsTable.Controls.Add(descriptionTextBox, 0, 21);
            detailsTable.Controls.Add(activitySummaryCaptionLabel, 0, 22);
            detailsTable.Controls.Add(activitySummaryTextBox, 0, 23);
            detailsTable.Dock = DockStyle.Fill;
            detailsTable.Location = new Point(24, 90);
            detailsTable.Margin = new Padding(0);
            detailsTable.Name = "detailsTable";
            detailsTable.RowCount = 24;
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle());
            detailsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailsTable.Size = new Size(812, 446);
            detailsTable.TabIndex = 2;
            // 
            // logIdCaptionLabel
            // 
            logIdCaptionLabel.AutoSize = true;
            logIdCaptionLabel.Location = new Point(0, 0);
            logIdCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            logIdCaptionLabel.Name = "logIdCaptionLabel";
            logIdCaptionLabel.Size = new Size(48, 20);
            logIdCaptionLabel.TabIndex = 0;
            logIdCaptionLabel.Text = "Log ID";
            // 
            // logIdTextBox
            // 
            logIdTextBox.Dock = DockStyle.Fill;
            logIdTextBox.Location = new Point(0, 26);
            logIdTextBox.Margin = new Padding(0, 0, 0, 10);
            logIdTextBox.Name = "logIdTextBox";
            logIdTextBox.ReadOnly = true;
            logIdTextBox.Size = new Size(812, 27);
            logIdTextBox.TabIndex = 1;
            // 
            // loggedAtCaptionLabel
            // 
            loggedAtCaptionLabel.AutoSize = true;
            loggedAtCaptionLabel.Location = new Point(0, 63);
            loggedAtCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            loggedAtCaptionLabel.Name = "loggedAtCaptionLabel";
            loggedAtCaptionLabel.Size = new Size(76, 20);
            loggedAtCaptionLabel.TabIndex = 2;
            loggedAtCaptionLabel.Text = "Date/Time";
            // 
            // loggedAtTextBox
            // 
            loggedAtTextBox.Dock = DockStyle.Fill;
            loggedAtTextBox.Location = new Point(0, 89);
            loggedAtTextBox.Margin = new Padding(0, 0, 0, 10);
            loggedAtTextBox.Name = "loggedAtTextBox";
            loggedAtTextBox.ReadOnly = true;
            loggedAtTextBox.Size = new Size(812, 27);
            loggedAtTextBox.TabIndex = 3;
            // 
            // fullNameCaptionLabel
            // 
            fullNameCaptionLabel.AutoSize = true;
            fullNameCaptionLabel.Location = new Point(0, 126);
            fullNameCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            fullNameCaptionLabel.Name = "fullNameCaptionLabel";
            fullNameCaptionLabel.Size = new Size(76, 20);
            fullNameCaptionLabel.TabIndex = 4;
            fullNameCaptionLabel.Text = "Full Name";
            // 
            // fullNameTextBox
            // 
            fullNameTextBox.Dock = DockStyle.Fill;
            fullNameTextBox.Location = new Point(0, 152);
            fullNameTextBox.Margin = new Padding(0, 0, 0, 10);
            fullNameTextBox.Name = "fullNameTextBox";
            fullNameTextBox.ReadOnly = true;
            fullNameTextBox.Size = new Size(812, 27);
            fullNameTextBox.TabIndex = 5;
            // 
            // usernameCaptionLabel
            // 
            usernameCaptionLabel.AutoSize = true;
            usernameCaptionLabel.Location = new Point(0, 189);
            usernameCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            usernameCaptionLabel.Name = "usernameCaptionLabel";
            usernameCaptionLabel.Size = new Size(75, 20);
            usernameCaptionLabel.TabIndex = 6;
            usernameCaptionLabel.Text = "Username";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Dock = DockStyle.Fill;
            usernameTextBox.Location = new Point(0, 215);
            usernameTextBox.Margin = new Padding(0, 0, 0, 10);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.ReadOnly = true;
            usernameTextBox.Size = new Size(812, 27);
            usernameTextBox.TabIndex = 7;
            // 
            // roleCaptionLabel
            // 
            roleCaptionLabel.AutoSize = true;
            roleCaptionLabel.Location = new Point(0, 252);
            roleCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            roleCaptionLabel.Name = "roleCaptionLabel";
            roleCaptionLabel.Size = new Size(39, 20);
            roleCaptionLabel.TabIndex = 8;
            roleCaptionLabel.Text = "Role";
            // 
            // roleTextBox
            // 
            roleTextBox.Dock = DockStyle.Fill;
            roleTextBox.Location = new Point(0, 278);
            roleTextBox.Margin = new Padding(0, 0, 0, 10);
            roleTextBox.Name = "roleTextBox";
            roleTextBox.ReadOnly = true;
            roleTextBox.Size = new Size(812, 27);
            roleTextBox.TabIndex = 9;
            // 
            // actionTypeCaptionLabel
            // 
            actionTypeCaptionLabel.AutoSize = true;
            actionTypeCaptionLabel.Location = new Point(0, 315);
            actionTypeCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            actionTypeCaptionLabel.Name = "actionTypeCaptionLabel";
            actionTypeCaptionLabel.Size = new Size(84, 20);
            actionTypeCaptionLabel.TabIndex = 10;
            actionTypeCaptionLabel.Text = "Action Type";
            // 
            // actionTypeTextBox
            // 
            actionTypeTextBox.Dock = DockStyle.Fill;
            actionTypeTextBox.Location = new Point(0, 341);
            actionTypeTextBox.Margin = new Padding(0, 0, 0, 10);
            actionTypeTextBox.Name = "actionTypeTextBox";
            actionTypeTextBox.ReadOnly = true;
            actionTypeTextBox.Size = new Size(812, 27);
            actionTypeTextBox.TabIndex = 11;
            // 
            // moduleCaptionLabel
            // 
            moduleCaptionLabel.AutoSize = true;
            moduleCaptionLabel.Location = new Point(0, 378);
            moduleCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            moduleCaptionLabel.Name = "moduleCaptionLabel";
            moduleCaptionLabel.Size = new Size(59, 20);
            moduleCaptionLabel.TabIndex = 12;
            moduleCaptionLabel.Text = "Module";
            // 
            // moduleTextBox
            // 
            moduleTextBox.Dock = DockStyle.Fill;
            moduleTextBox.Location = new Point(0, 404);
            moduleTextBox.Margin = new Padding(0, 0, 0, 10);
            moduleTextBox.Name = "moduleTextBox";
            moduleTextBox.ReadOnly = true;
            moduleTextBox.Size = new Size(812, 27);
            moduleTextBox.TabIndex = 13;
            // 
            // entityNameCaptionLabel
            // 
            entityNameCaptionLabel.AutoSize = true;
            entityNameCaptionLabel.Location = new Point(0, 441);
            entityNameCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            entityNameCaptionLabel.Name = "entityNameCaptionLabel";
            entityNameCaptionLabel.Size = new Size(89, 20);
            entityNameCaptionLabel.TabIndex = 14;
            entityNameCaptionLabel.Text = "Entity Name";
            // 
            // entityNameTextBox
            // 
            entityNameTextBox.Dock = DockStyle.Fill;
            entityNameTextBox.Location = new Point(0, 467);
            entityNameTextBox.Margin = new Padding(0, 0, 0, 10);
            entityNameTextBox.Name = "entityNameTextBox";
            entityNameTextBox.ReadOnly = true;
            entityNameTextBox.Size = new Size(812, 27);
            entityNameTextBox.TabIndex = 15;
            // 
            // entityIdCaptionLabel
            // 
            entityIdCaptionLabel.AutoSize = true;
            entityIdCaptionLabel.Location = new Point(0, 504);
            entityIdCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            entityIdCaptionLabel.Name = "entityIdCaptionLabel";
            entityIdCaptionLabel.Size = new Size(66, 20);
            entityIdCaptionLabel.TabIndex = 16;
            entityIdCaptionLabel.Text = "Entity ID";
            // 
            // entityIdTextBox
            // 
            entityIdTextBox.Dock = DockStyle.Fill;
            entityIdTextBox.Location = new Point(0, 530);
            entityIdTextBox.Margin = new Padding(0, 0, 0, 10);
            entityIdTextBox.Name = "entityIdTextBox";
            entityIdTextBox.ReadOnly = true;
            entityIdTextBox.Size = new Size(812, 27);
            entityIdTextBox.TabIndex = 17;
            // 
            // severityCaptionLabel
            // 
            severityCaptionLabel.AutoSize = true;
            severityCaptionLabel.Location = new Point(0, 567);
            severityCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            severityCaptionLabel.Name = "severityCaptionLabel";
            severityCaptionLabel.Size = new Size(58, 20);
            severityCaptionLabel.TabIndex = 18;
            severityCaptionLabel.Text = "Severity";
            // 
            // severityTextBox
            // 
            severityTextBox.Dock = DockStyle.Fill;
            severityTextBox.Location = new Point(0, 593);
            severityTextBox.Margin = new Padding(0, 0, 0, 10);
            severityTextBox.Name = "severityTextBox";
            severityTextBox.ReadOnly = true;
            severityTextBox.Size = new Size(812, 27);
            severityTextBox.TabIndex = 19;
            // 
            // descriptionCaptionLabel
            // 
            descriptionCaptionLabel.AutoSize = true;
            descriptionCaptionLabel.Location = new Point(0, 630);
            descriptionCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            descriptionCaptionLabel.Name = "descriptionCaptionLabel";
            descriptionCaptionLabel.Size = new Size(85, 20);
            descriptionCaptionLabel.TabIndex = 20;
            descriptionCaptionLabel.Text = "Description";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Dock = DockStyle.Fill;
            descriptionTextBox.Location = new Point(0, 656);
            descriptionTextBox.Margin = new Padding(0, 0, 0, 10);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ReadOnly = true;
            descriptionTextBox.ScrollBars = ScrollBars.Vertical;
            descriptionTextBox.Size = new Size(812, 74);
            descriptionTextBox.TabIndex = 21;
            // 
            // activitySummaryCaptionLabel
            // 
            activitySummaryCaptionLabel.AutoSize = true;
            activitySummaryCaptionLabel.Location = new Point(0, 740);
            activitySummaryCaptionLabel.Margin = new Padding(0, 0, 0, 6);
            activitySummaryCaptionLabel.Name = "activitySummaryCaptionLabel";
            activitySummaryCaptionLabel.Size = new Size(120, 20);
            activitySummaryCaptionLabel.TabIndex = 22;
            activitySummaryCaptionLabel.Text = "Activity Summary";
            // 
            // activitySummaryTextBox
            // 
            activitySummaryTextBox.Dock = DockStyle.Fill;
            activitySummaryTextBox.Location = new Point(0, 766);
            activitySummaryTextBox.Margin = new Padding(0);
            activitySummaryTextBox.Multiline = true;
            activitySummaryTextBox.Name = "activitySummaryTextBox";
            activitySummaryTextBox.ReadOnly = true;
            activitySummaryTextBox.ScrollBars = ScrollBars.Vertical;
            activitySummaryTextBox.Size = new Size(812, 88);
            activitySummaryTextBox.TabIndex = 23;
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(24, 54);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(266, 20);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Audit trail details for the selected activity.";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(24, 24);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(92, 20);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Audit Details";
            // 
            // AuditingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 560);
            Controls.Add(detailsCard);
            MinimumSize = new Size(760, 520);
            Name = "AuditingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Audit Details";
            detailsCard.ResumeLayout(false);
            detailsCard.PerformLayout();
            detailsTable.ResumeLayout(false);
            detailsTable.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel detailsCard;
        private Label titleLabel;
        private Label subtitleLabel;
        private TableLayoutPanel detailsTable;
        private Label logIdCaptionLabel;
        private TextBox logIdTextBox;
        private Label loggedAtCaptionLabel;
        private TextBox loggedAtTextBox;
        private Label fullNameCaptionLabel;
        private TextBox fullNameTextBox;
        private Label usernameCaptionLabel;
        private TextBox usernameTextBox;
        private Label roleCaptionLabel;
        private TextBox roleTextBox;
        private Label actionTypeCaptionLabel;
        private TextBox actionTypeTextBox;
        private Label moduleCaptionLabel;
        private TextBox moduleTextBox;
        private Label entityNameCaptionLabel;
        private TextBox entityNameTextBox;
        private Label entityIdCaptionLabel;
        private TextBox entityIdTextBox;
        private Label severityCaptionLabel;
        private TextBox severityTextBox;
        private Label descriptionCaptionLabel;
        private TextBox descriptionTextBox;
        private Label activitySummaryCaptionLabel;
        private TextBox activitySummaryTextBox;
    }
}
