using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Data
{
  public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext (DbContextOptions<SalesWebMVCContext> options)
            : base(options)
        {
        }
        public DbSet<SalesWebMVC.Models.Department> Department { get; set; }
        public DbSet<SalesWebMVC.Models.Seller> Seller { get; set; }
        public DbSet<SalesWebMVC.Models.SalesRecord> SalesRecords { get; set; }
    }
}
