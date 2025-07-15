using Microsoft.EntityFrameworkCore;
using LostManagementApp.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;

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
            // appsettings.jsonから接続文字列を取得してPostgreSQLに接続する
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); ;
            }
        }
    }
}
