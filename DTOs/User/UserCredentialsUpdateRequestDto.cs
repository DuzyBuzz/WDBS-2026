namespace WDBS_2026.DTOs.User;

public sealed class UserCredentialsUpdateRequestDto
{
    public int UserId { get; init; }

    public string Username { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public string CurrentPassword { get; init; } = string.Empty;

    public string NewPassword { get; init; } = string.Empty;

    public string ConfirmNewPassword { get; init; } = string.Empty;
}