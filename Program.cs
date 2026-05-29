using WDBS_2026.Forms;
using WDBS_2026.Services;

namespace WDBS_2026;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
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
            MessageBox.Show(
                "The application could not start.\n\n" + ex.Message,
                "Startup Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}