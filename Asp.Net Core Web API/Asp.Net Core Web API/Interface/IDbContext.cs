using System.Data.Common;

namespace Asp.Net_Core_Web_API.Interface
{
    public interface IDbContext
    {
        string GetConnectionString();
        DbConnection GetDbConnection(string connectionString);
    }
}
