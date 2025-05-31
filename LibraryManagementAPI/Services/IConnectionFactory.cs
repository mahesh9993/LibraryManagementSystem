using System.Data;

namespace LibraryManagementAPI.Services
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
