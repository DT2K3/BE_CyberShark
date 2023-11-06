using Microsoft.EntityFrameworkCore;
namespace BE_cybershark.Models
{

    namespace BE_cybershark.Models
    {
        public class CyberSharkContext : DbContext
        {
            public DbSet<User> Users { get; set; }

            public CyberSharkContext(DbContextOptions<CyberSharkContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Customize the database schema, if needed
                modelBuilder.Entity<User>().ToTable("Users");

                // Add any additional configurations here
            }
        }
    }
}

//
