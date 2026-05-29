using WDBS_2026.DTOs.Auth;

namespace WDBS_2026.Services.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResultDto> AuthenticateAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}