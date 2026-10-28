using Microsoft.EntityFrameworkCore;

namespace BogusDataCreate.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Local SQL Server (LocalDB is installed with Visual Studio)
            options.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=BogusDemoDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
