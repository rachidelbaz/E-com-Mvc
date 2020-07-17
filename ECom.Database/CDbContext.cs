using ECom.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Database
{
    public class CDbContext : DbContext
    {
        public CDbContext():base("ECom")
        {
            System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<CDbContext>());

            // the terrible hack
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            this.Configuration.LazyLoadingEnabled = false;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
