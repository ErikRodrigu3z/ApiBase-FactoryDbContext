using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models.Auth;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistence.EF
{
    // Add-Migration MSSQL_Migration -context MSSQL_DbContext
    // update-database -context MSSQL_DbContext
    public class MSSQL_DbContext : IdentityDbContext<Users>
    {
        public MSSQL_DbContext(DbContextOptions<MSSQL_DbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "User", NormalizedName = "USER" }
            );
        }

        //DbSets
        public DbSet<Test> Test { get; set; }

    }
}
