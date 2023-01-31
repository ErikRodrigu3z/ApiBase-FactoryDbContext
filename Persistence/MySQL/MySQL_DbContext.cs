using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Auth;

namespace Persistence.MySQL
{
    // Add-Migration MySql_Migration -context MySQL_DbContext
    // update-database -context MySQL_DbContext
    public class MySQL_DbContext : IdentityDbContext<Users>
    {
        public MySQL_DbContext(DbContextOptions<MySQL_DbContext> options) : base(options)
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
