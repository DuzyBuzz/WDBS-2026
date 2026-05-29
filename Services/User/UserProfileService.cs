using MySql.Data.MySqlClient;
using WDBS_2026.DTOs.Auth;
using WDBS_2026.DTOs.User;
using WDBS_2026.Models;
using WDBS_2026.Repositories.Interfaces;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Services.User;

public sealed class UserProfileService : IUserProfileService
{
    private readonly IUserRepository _userRepository;

    public UserProfileService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserCredentialsUpdateResultDto> UpdateCredentialsAsync(
        UserCredentialsUpdateRequestDto request,
        CancellationToken cancellationToken = default)
    {
        string username = request.Username.Trim();
        string fullName = request.FullName.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(fullName))
        {
            return UserCredentialsUpdateResultDto.Failure("Username and full name are required.");
        }

        if (string.IsNullOrWhiteSpace(request.CurrentPassword))
        {
            return UserCredentialsUpdateResultDto.Failure("Enter your current password to save profile changes.");
        }

        bool passwordChangeRequested =
            !string.IsNullOrWhiteSpace(request.NewPassword) ||
            !string.IsNullOrWhiteSpace(request.ConfirmNewPassword);

        if (passwordChangeRequested)
        {
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return UserCredentialsUpdateResultDto.Failure("Enter the new password.");
            }

            if (request.NewPassword.Length > 50)
            {
                return UserCredentialsUpdateResultDto.Failure("New password must be 50 characters or fewer.");
            }

            if (!string.Equals(request.NewPassword, request.ConfirmNewPassword, StringComparison.Ordinal))
            {
                return UserCredentialsUpdateResultDto.Failure("New password and confirm password do not match.");
            }
        }

        UserAccount? existingUser = await _userRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (existingUser is null)
        {
            return UserCredentialsUpdateResultDto.Failure("User account no longer exists.");
        }

        if (!string.Equals(existingUser.Password, request.CurrentPassword, StringComparison.Ordinal))
        {
            return UserCredentialsUpdateResultDto.Failure("Current password is incorrect.");
        }

        bool usernameTaken = await _userRepository.UsernameExistsAsync(username, request.UserId, cancellationToken);
        if (usernameTaken)
        {
            return UserCredentialsUpdateResultDto.Failure("That username is already in use by another account.");
        }

        try
        {
            await _userRepository.UpdateCredentialsAsync(
                request.UserId,
                username,
                fullName,
                passwordChangeRequested ? request.NewPassword : null,
                cancellationToken);
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return UserCredentialsUpdateResultDto.Failure("That username is already in use by another account.");
        }

        var updatedUser = new AuthenticatedUserDto
        {
            UserId = existingUser.UserId,
            Username = username,
            FullName = fullName,
            Role = existingUser.Role
        };

        string successMessage = passwordChangeRequested
            ? "Credentials and password were updated successfully."
            : "Credentials were updated successfully.";

        return UserCredentialsUpdateResultDto.Success(updatedUser, successMessage);
    }
}