using WDBS_2026.Forms;
using WDBS_2026.Services;

namespace WDBS_2026;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += static (_, args) => AppDiagnostics.ReportException("UI thread", args.Exception);
        AppDomain.CurrentDomain.UnhandledException += static (_, args) =>
        {
            Exception exception = args.ExceptionObject as Exception
                ?? new Exception($"Non-Exception unhandled object: {args.ExceptionObject}");
            AppDiagnostics.ReportException("AppDomain", exception);
        };
        TaskScheduler.UnobservedTaskException += static (_, args) =>
        {
            AppDiagnostics.ReportException("Unobserved task", args.Exception);
            args.SetObserved();
        };

        ApplicationConfiguration.Initialize();

        try
        {
            var composition = ApplicationComposition.Create();
            System.Windows.Forms.Application.Run(new LoginForm(
                composition.AuthenticationService,
                composition.UserProfileService));
        }
        catch (Exception ex)
        {
            AppDiagnostics.ReportException("Startup", ex);
        }
    }
}

internal static class AppDiagnostics
{
    private static readonly object SyncLock = new();

    private static int _isShowingErrorDialog;

    internal static void ReportException(string context, Exception exception, bool showDialog = true)
    {
        try
        {
            WriteException(context, exception);
        }
        catch
        {
            // Logging must never crash the application.
        }

        if (!showDialog)
        {
            return;
        }

        if (Interlocked.CompareExchange(ref _isShowingErrorDialog, 1, 0) != 0)
        {
            return;
        }

        try
        {
            MessageBox.Show(
                "An unexpected error occurred. The application will keep running when possible.\n\n"
                + exception.Message,
                "Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            Interlocked.Exchange(ref _isShowingErrorDialog, 0);
        }
    }

    private static void WriteException(string context, Exception exception)
    {
        string directory = Path.Combine(AppContext.BaseDirectory, "artifacts", "validation");
        Directory.CreateDirectory(directory);

        string logPath = Path.Combine(directory, "runtime-errors.log");
        string logEntry =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {context}{Environment.NewLine}"
            + $"Type: {exception.GetType().FullName}{Environment.NewLine}"
            + $"Message: {exception.Message}{Environment.NewLine}"
            + $"StackTrace:{Environment.NewLine}{exception.StackTrace}{Environment.NewLine}{Environment.NewLine}";

        lock (SyncLock)
        {
            File.AppendAllText(logPath, logEntry);
        }
    }
}