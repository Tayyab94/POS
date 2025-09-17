using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class POSDbContext: DbContext
    {

        public POSDbContext():base("name=POSDbConnectionstring")
        {

            //string dbname = @"Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbName.mdf;Integrated Security=true;";
            //Optional Initializer  
            Database.SetInitializer(new CreateDatabaseIfNotExists<POSDbContext>());
        }
        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }


        public DbSet<Product> Products { get; set; }

        public DbSet<Customer>Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Fluent API configurations go here
            modelBuilder.Entity<City>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Category>()
             .Property(c => c.name)
             .IsRequired()
             .HasMaxLength(50);


            modelBuilder.Entity<OrderDetail>()
                .HasRequired(s => s.Order).WithMany(s => s.OrderDetails)
                .HasForeignKey(S => S.OrderId).WillCascadeOnDelete(true);

        }
    }
}
