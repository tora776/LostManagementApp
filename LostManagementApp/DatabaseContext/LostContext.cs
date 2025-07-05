using Microsoft.EntityFrameworkCore;
using LostManagementApp.Models;

namespace LostManagementApp.DatabaseContext
{
    public class LostContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Lost> Lost { get; set; } = null!;
        public DbSet<Login> Login { get; set; } = null!;
        public LostContext(DbContextOptions<LostContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
