using WDBS_2026.Database;
using WDBS_2026.Repositories.MySql;
using WDBS_2026.Services.Auth;
using WDBS_2026.Services.Interfaces;
using WDBS_2026.Services.User;

namespace WDBS_2026.Services;

public sealed class ApplicationComposition
{
    private ApplicationComposition(
        IAuthenticationService authenticationService,
        IUserProfileService userProfileService)
    {
        AuthenticationService = authenticationService;
        UserProfileService = userProfileService;
    }

    public IAuthenticationService AuthenticationService { get; }

    public IUserProfileService UserProfileService { get; }

    public static ApplicationComposition Create()
    {
        var connectionContext = new DbConnectionContext();
        var userRepository = new MySqlUserRepository(connectionContext);
        var authenticationService = new AuthenticationService(userRepository, connectionContext);
        var userProfileService = new UserProfileService(userRepository);

        return new ApplicationComposition(authenticationService, userProfileService);
    }
}