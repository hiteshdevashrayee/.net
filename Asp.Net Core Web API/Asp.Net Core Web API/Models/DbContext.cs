using Asp.Net_Core_Web_API.Interface;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;
using System.Diagnostics;

namespace Asp.Net_Core_Web_API.Models
{
    public class DbContext : IDbContext
    {
        private string? connectionString;
        private readonly IConfiguration configuration;
       

        public DbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            GetConnectionString();
        }
        public string GetConnectionString()
        {            
            connectionString = configuration.GetConnectionString("netcoredb");
            NpgsqlConnectionStringBuilder npgconnectionString = new NpgsqlConnectionStringBuilder(connectionString);
            return npgconnectionString.ToString();
        }

        public DbConnection GetDbConnection(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
