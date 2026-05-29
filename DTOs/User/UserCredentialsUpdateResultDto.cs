using WDBS_2026.DTOs.Auth;

namespace WDBS_2026.DTOs.User;

public sealed class UserCredentialsUpdateResultDto
{
    public bool IsSuccess { get; private init; }

    public string Message { get; private init; } = string.Empty;

    public AuthenticatedUserDto? User { get; private init; }

    public static UserCredentialsUpdateResultDto Success(AuthenticatedUserDto user, string message)
    {
        return new UserCredentialsUpdateResultDto
        {
            IsSuccess = true,
            Message = message,
            User = user
        };
    }

    public static UserCredentialsUpdateResultDto Failure(string message)
    {
        return new UserCredentialsUpdateResultDto
        {
            IsSuccess = false,
            Message = message
        };
    }
}