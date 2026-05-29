using WDBS_2026.DTOs.Auth;
using WDBS_2026.Repositories.Interfaces;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Services.Auth;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConnectionContext _connectionContext;

    public AuthenticationService(IUserRepository userRepository, IConnectionContext connectionContext)
    {
        _userRepository = userRepository;
        _connectionContext = connectionContext;
    }

    public async Task<LoginResultDto> AuthenticateAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return LoginResultDto.Failure("Enter both username and password.");
        }

        var user = await _userRepository.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
        if (user is null)
        {
            return LoginResultDto.Failure("The username or password is incorrect.");
        }

        if (!user.IsActive)
        {
            return LoginResultDto.Failure("This account is inactive. Please contact an administrator.");
        }

        if (!string.Equals(user.Password, request.Password, StringComparison.Ordinal))
        {
            return LoginResultDto.Failure("The username or password is incorrect.");
        }

        try
        {
            _connectionContext.UseRoleConnection(user.Role);
        }
        catch (Exception ex)
        {
            return LoginResultDto.Failure("The role-specific database connection could not be prepared: " + ex.Message);
        }

        var authenticatedUser = new AuthenticatedUserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role
        };

        return LoginResultDto.Success(authenticatedUser, $"Welcome back, {user.FullName}.");
    }
}