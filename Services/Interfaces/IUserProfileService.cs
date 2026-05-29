using WDBS_2026.DTOs.User;

namespace WDBS_2026.Services.Interfaces;

public interface IUserProfileService
{
    Task<UserCredentialsUpdateResultDto> UpdateCredentialsAsync(
        UserCredentialsUpdateRequestDto request,
        CancellationToken cancellationToken = default);
}