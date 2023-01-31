using Microsoft.EntityFrameworkCore;
using Persistence.EF;
using Persistence.MySQL;

namespace Persistence
{
    public class DbContextFactory
    {
        private readonly string _connectionString;
        public DbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MSSQL_DbContext Create_EF_DbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MSSQL_DbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            return new MSSQL_DbContext(optionsBuilder.Options);
        }

        public MySQL_DbContext Create_MySQL_DbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MySQL_DbContext>();
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));

            return new MySQL_DbContext(optionsBuilder.Options);
        }


    }
}
