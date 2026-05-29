namespace WDBS_2026.DTOs.Auth;

public sealed class LoginResultDto
{
    public bool IsSuccess { get; private init; }

    public string Message { get; private init; } = string.Empty;

    public AuthenticatedUserDto? User { get; private init; }

    public static LoginResultDto Success(AuthenticatedUserDto user, string message)
    {
        return new LoginResultDto
        {
            IsSuccess = true,
            Message = message,
            User = user
        };
    }

    public static LoginResultDto Failure(string message)
    {
        return new LoginResultDto
        {
            IsSuccess = false,
            Message = message
        };
    }
}