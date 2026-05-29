using WDBS_2026.Models;

namespace WDBS_2026.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserAccount?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    Task<UserAccount?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<bool> UsernameExistsAsync(string username, int excludeUserId, CancellationToken cancellationToken = default);

    Task UpdateCredentialsAsync(
        int userId,
        string username,
        string fullName,
        string? newPassword,
        CancellationToken cancellationToken = default);
}