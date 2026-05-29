using System.Runtime.ExceptionServices;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Forms.Report;

internal partial class ReportOperationProgressForm : Form
{
    private Func<IProgress<ReportOperationProgress>, Task>? _operation;
    private Exception? _operationException;
    private bool _operationStarted;

    public ReportOperationProgressForm()
    {
        InitializeComponent();
        ApplyTheme();
    }

    private ReportOperationProgressForm(
        string title,
        string initialMessage,
        Func<IProgress<ReportOperationProgress>, Task> operation)
        : this()
    {
        Text = title;
        titleLabel.Text = title;
        _operation = operation;
        UpdateProgress(new ReportOperationProgress(0, initialMessage));
    }

    public static void Run(
        IWin32Window owner,
        string title,
        string initialMessage,
        Func<IProgress<ReportOperationProgress>, Task> operation)
    {
        using var form = new ReportOperationProgressForm(title, initialMessage, operation);
        form.ShowDialog(owner);

        if (form._operationException is not null)
        {
            ExceptionDispatchInfo.Capture(form._operationException).Throw();
        }
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (_operationStarted || _operation is null)
        {
            return;
        }

        _operationStarted = true;
        var progress = new Progress<ReportOperationProgress>(UpdateProgress);

        try
        {
            await _operation(progress);
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            _operationException = ex;
            DialogResult = DialogResult.Abort;
        }
        finally
        {
            Close();
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_operationStarted && DialogResult == DialogResult.None)
        {
            e.Cancel = true;
            return;
        }

        base.OnFormClosing(e);
    }

    private void ApplyTheme()
    {
        AppTheme.ApplyFormSurface(this);
        AppTheme.ApplyPageTitle(titleLabel);
        AppTheme.ApplySubtitle(statusLabel);
        percentLabel.ForeColor = AppTheme.BodyTextColor;
        percentLabel.Font = AppTheme.SectionFont;
        progressBar.Style = ProgressBarStyle.Continuous;
    }

    private void UpdateProgress(ReportOperationProgress progress)
    {
        int percentage = Math.Clamp(progress.Percentage, 0, 100);
        progressBar.Value = percentage;
        percentLabel.Text = $"{percentage}%";
        statusLabel.Text = string.IsNullOrWhiteSpace(progress.Message)
            ? "Working..."
            : progress.Message;
    }
}