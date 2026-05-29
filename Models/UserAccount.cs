namespace WDBS_2026.Models;

public sealed class UserAccount
{
    public int UserId { get; init; }

    public string Username { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public UserRole Role { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedAt { get; init; }
}