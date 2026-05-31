namespace WDBS_2026.Forms.Auditing
{
    partial class AuditingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

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
            components = new System.ComponentModel.Container();
            
            var detailsCard = new Panel();
            var detailsTable = new TableLayoutPanel();
            var actionTextBox = new TextBox();
            var actionCaptionLabel = new Label();
            var roleTextBox = new TextBox();
            var roleCaptionLabel = new Label();
            var usernameTextBox = new TextBox();
            var usernameCaptionLabel = new Label();
            var fullNameTextBox = new TextBox();
            var fullNameCaptionLabel = new Label();
            var loggedAtTextBox = new TextBox();
            var loggedAtCaptionLabel = new Label();
            var logIdTextBox = new TextBox();
            var logIdCaptionLabel = new Label();
            var subtitleLabel = new Label();
            var titleLabel = new Label();

            this.detailsCard = detailsCard;
            this.detailsTable = detailsTable;
            this.actionTextBox = actionTextBox;
            this.actionCaptionLabel = actionCaptionLabel;
            this.roleTextBox = roleTextBox;
            this.roleCaptionLabel = roleCaptionLabel;
            this.usernameTextBox = usernameTextBox;
            this.usernameCaptionLabel = usernameCaptionLabel;
            this.fullNameTextBox = fullNameTextBox;
            this.fullNameCaptionLabel = fullNameCaptionLabel;
            this.loggedAtTextBox = loggedAtTextBox;
            this.loggedAtCaptionLabel = loggedAtCaptionLabel;
            this.logIdTextBox = logIdTextBox;
            this.logIdCaptionLabel = logIdCaptionLabel;
            this.subtitleLabel = subtitleLabel;
            this.titleLabel = titleLabel;

            detailsCard.SuspendLayout();
            detailsTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // detailsCard
            // 
            detailsCard.Controls.Add(detailsTable);
            detailsCard.Controls.Add(subtitleLabel);
            detailsCard.Controls.Add(titleLabel);
            detailsCard.Dock = DockStyle.Fill;
            detailsCard.Location = new Point(0, 0);
            detailsCard.Name = "detailsCard";
            detailsCard.Padding = new Padding(22);
            detailsCard.Size = new Size(860, 560);
            detailsCard.TabIndex = 0;
            
            // 
            // detailsTable
            // 
            detailsTable.ColumnCount = 1;
            detailsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailsTable.Controls.Add(actionTextBox, 0, 11);
            detailsTable.Controls.Add(actionCaptionLabel, 0, 10);
            detailsTable.Controls.Add(roleTextBox, 0, 9);
            detailsTable.Controls.Add(roleCaptionLabel, 0, 8);
            detailsTable.Controls.Add(usernameTextBox, 0, 7);
            detailsTable.Controls.Add(usernameCaptionLabel, 0, 6);
            detailsTable.Controls.Add(fullNameTextBox, 0, 5);
            detailsTable.Controls.Add(fullNameCaptionLabel, 0, 4);
            detailsTable.Controls.Add(loggedAtTextBox, 0, 3);
            detailsTable.Controls.Add(loggedAtCaptionLabel, 0, 2);
            detailsTable.Controls.Add(logIdTextBox, 0, 1);
            detailsTable.Controls.Add(logIdCaptionLabel, 0, 0);
            detailsTable.Dock = DockStyle.Fill;
            detailsTable.Location = new Point(22, 86);
            detailsTable.Name = "detailsTable";
            detailsTable.RowCount = 12;
            for (int i = 0; i < 11; i++)
            {
                detailsTable.RowStyles.Add(new RowStyle());
            }
            detailsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailsTable.Size = new Size(816, 452);
            detailsTable.TabIndex = 2;
            // 
            // actionTextBox
            // 
            actionTextBox.Dock = DockStyle.Fill;
            actionTextBox.Location = new Point(3, 367);
            actionTextBox.Multiline = true;
            actionTextBox.Name = "actionTextBox";
            actionTextBox.ReadOnly = true;
            actionTextBox.ScrollBars = ScrollBars.Vertical;
            actionTextBox.Size = new Size(810, 82);
            actionTextBox.TabIndex = 11;
            // 
            // actionCaptionLabel
            // 
            actionCaptionLabel.AutoSize = true;
            actionCaptionLabel.Location = new Point(3, 344);
            actionCaptionLabel.Margin = new Padding(3, 14, 3, 4);
            actionCaptionLabel.Name = "actionCaptionLabel";
            actionCaptionLabel.Size = new Size(53, 20);
            actionCaptionLabel.TabIndex = 10;
            actionCaptionLabel.Text = "Action";
            // 
            // roleTextBox
            // 
            roleTextBox.Dock = DockStyle.Top;
            roleTextBox.Location = new Point(3, 300);
            roleTextBox.Name = "roleTextBox";
            roleTextBox.ReadOnly = true;
            roleTextBox.Size = new Size(810, 27);
            roleTextBox.TabIndex = 9;
            // 
            // roleCaptionLabel
            // 
            roleCaptionLabel.AutoSize = true;
            roleCaptionLabel.Location = new Point(3, 276);
            roleCaptionLabel.Margin = new Padding(3, 14, 3, 4);
            roleCaptionLabel.Name = "roleCaptionLabel";
            roleCaptionLabel.Size = new Size(39, 20);
            roleCaptionLabel.TabIndex = 8;
            roleCaptionLabel.Text = "Role";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Dock = DockStyle.Top;
            usernameTextBox.Location = new Point(3, 232);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.ReadOnly = true;
            usernameTextBox.Size = new Size(810, 27);
            usernameTextBox.TabIndex = 7;
            // 
            // usernameCaptionLabel
            // 
            usernameCaptionLabel.AutoSize = true;
            usernameCaptionLabel.Location = new Point(3, 208);
            usernameCaptionLabel.Margin = new Padding(3, 14, 3, 4);
            usernameCaptionLabel.Name = "usernameCaptionLabel";
            usernameCaptionLabel.Size = new Size(75, 20);
            usernameCaptionLabel.TabIndex = 6;
            usernameCaptionLabel.Text = "Username";
            // 
            // fullNameTextBox
            // 
            fullNameTextBox.Dock = DockStyle.Top;
            fullNameTextBox.Location = new Point(3, 164);
            fullNameTextBox.Name = "fullNameTextBox";
            fullNameTextBox.ReadOnly = true;
            fullNameTextBox.Size = new Size(810, 27);
            fullNameTextBox.TabIndex = 5;
            // 
            // fullNameCaptionLabel
            // 
            fullNameCaptionLabel.AutoSize = true;
            fullNameCaptionLabel.Location = new Point(3, 140);
            fullNameCaptionLabel.Margin = new Padding(3, 14, 3, 4);
            fullNameCaptionLabel.Name = "fullNameCaptionLabel";
            fullNameCaptionLabel.Size = new Size(76, 20);
            fullNameCaptionLabel.TabIndex = 4;
            fullNameCaptionLabel.Text = "Full Name";
            // 
            // loggedAtTextBox
            // 
            loggedAtTextBox.Dock = DockStyle.Top;
            loggedAtTextBox.Location = new Point(3, 96);
            loggedAtTextBox.Name = "loggedAtTextBox";
            loggedAtTextBox.ReadOnly = true;
            loggedAtTextBox.Size = new Size(810, 27);
            loggedAtTextBox.TabIndex = 3;
            // 
            // loggedAtCaptionLabel
            // 
            loggedAtCaptionLabel.AutoSize = true;
            loggedAtCaptionLabel.Location = new Point(3, 72);
            loggedAtCaptionLabel.Margin = new Padding(3, 14, 3, 4);
            loggedAtCaptionLabel.Name = "loggedAtCaptionLabel";
            loggedAtCaptionLabel.Size = new Size(76, 20);
            loggedAtCaptionLabel.TabIndex = 2;
            loggedAtCaptionLabel.Text = "Date/Time";
            // 
            // logIdTextBox
            // 
            logIdTextBox.Dock = DockStyle.Top;
            logIdTextBox.Location = new Point(3, 28);
            logIdTextBox.Name = "logIdTextBox";
            logIdTextBox.ReadOnly = true;
            logIdTextBox.Size = new Size(810, 27);
            logIdTextBox.TabIndex = 1;
            // 
            // logIdCaptionLabel
            // 
            logIdCaptionLabel.AutoSize = true;
            logIdCaptionLabel.Location = new Point(3, 0);
            logIdCaptionLabel.Margin = new Padding(3, 0, 3, 4);
            logIdCaptionLabel.Name = "logIdCaptionLabel";
            logIdCaptionLabel.Size = new Size(48, 20);
            logIdCaptionLabel.TabIndex = 0;
            logIdCaptionLabel.Text = "Log ID";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(24, 52);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(266, 20);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Audit trail details for the selected activity.";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(22, 22);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(92, 20);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Audit Details";
            // 
            // AuditingForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(860, 560);
            this.Controls.Add(detailsCard);
            this.MinimumSize = new Size(760, 520);
            this.Name = "AuditingForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Audit Details";
            
            detailsCard.ResumeLayout(false);
            detailsCard.PerformLayout();
            detailsTable.ResumeLayout(false);
            detailsTable.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel detailsCard = null!;
        private Label titleLabel = null!;
        private Label subtitleLabel = null!;
        private TableLayoutPanel detailsTable = null!;
        private Label logIdCaptionLabel = null!;
        private TextBox logIdTextBox = null!;
        private Label loggedAtCaptionLabel = null!;
        private TextBox loggedAtTextBox = null!;
        private Label fullNameCaptionLabel = null!;
        private TextBox fullNameTextBox = null!;
        private Label usernameCaptionLabel = null!;
        private TextBox usernameTextBox = null!;
        private Label roleCaptionLabel = null!;
        private TextBox roleTextBox = null!;
        private Label actionCaptionLabel = null!;
        private TextBox actionTextBox = null!;
    }
}