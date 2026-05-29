using WDBS_2026.Models;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Database;

public sealed class DbConnectionContext : IConnectionContext
{
    public void UseLoginConnection()
    {
        DBConfig.SetConnectionStringForLogin();
    }

    public void UseRoleConnection(UserRole role)
    {
        DBConfig.SetConnectionString(role);
    }
}