using WDBS_2026.Models;

namespace WDBS_2026.DTOs.Auth;

public sealed class AuthenticatedUserDto
{
    public int UserId { get; init; }

    public string Username { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public UserRole Role { get; init; }
}