using System.ComponentModel;

namespace WDBS_2026.Forms.Pickers;

internal static class VoidReasonPromptDialog
{
    public static bool TryGetReason(IWin32Window owner, string title, string prompt, out string reason)
    {
        reason = string.Empty;
        string enteredReason = string.Empty;

        using var form = new Form
        {
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MinimizeBox = false,
            MaximizeBox = false,
            ShowInTaskbar = false,
            Width = 560,
            Height = 330
        };

        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        {
            AppTheme.ApplyFormSurface(form);
        }

        var promptLabel = new Label
        {
            Dock = DockStyle.Top,
            Height = 44,
            Text = prompt,
            AutoEllipsis = true,
            Font = AppTheme.SectionFont,
            ForeColor = AppTheme.BodyTextColor,
            Padding = new Padding(12, 12, 12, 0)
        };

        var reasonTextBox = new TextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Font = AppTheme.BodyFont,
            ForeColor = AppTheme.BodyTextColor,
            BorderStyle = BorderStyle.FixedSingle
        };

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            FlowDirection = FlowDirection.RightToLeft,
            Height = 54,
            Padding = new Padding(12, 10, 12, 10)
        };

        var cancelButton = new Button
        {
            Text = "Cancel",
            Width = 100,
            DialogResult = DialogResult.Cancel
        };
        AppTheme.ApplySeverityButton(cancelButton, ButtonSeverity.Neutral);

        var confirmButton = new Button
        {
            Text = "Confirm Void",
            Width = 120,
            DialogResult = DialogResult.OK
        };
        AppTheme.ApplySeverityButton(confirmButton, ButtonSeverity.Danger);

        confirmButton.Click += (_, args) =>
        {
            enteredReason = reasonTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(enteredReason))
            {
                return;
            }

            MessageBox.Show(
                form,
                "Please enter a reason for voiding.",
                "Void Reason Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            form.DialogResult = DialogResult.None;
            reasonTextBox.Focus();
        };

        buttonPanel.Controls.Add(cancelButton);
        buttonPanel.Controls.Add(confirmButton);

        var contentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12, 0, 12, 0)
        };
        contentPanel.Controls.Add(reasonTextBox);

        form.Controls.Add(contentPanel);
        form.Controls.Add(buttonPanel);
        form.Controls.Add(promptLabel);

        form.AcceptButton = confirmButton;
        form.CancelButton = cancelButton;

        if (form.ShowDialog(owner) != DialogResult.OK)
        {
            return false;
        }

        reason = enteredReason;
        return true;
    }
}
