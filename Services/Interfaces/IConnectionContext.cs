using WDBS_2026.Models;

namespace WDBS_2026.Services.Interfaces;

public interface IConnectionContext
{
    void UseLoginConnection();

    void UseRoleConnection(UserRole role);
}