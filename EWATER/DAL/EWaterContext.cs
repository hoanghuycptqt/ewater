using EWATER.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EWATER.DAL
{
    public class EWaterContext: DbContext
    {
        public EWaterContext()
            :base("EWATERDB")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<OrderItem>()
                .HasRequired(t => t.Order)
                .WithMany(m => m.OrderItems)
                .WillCascadeOnDelete(true);
        }
    }
}