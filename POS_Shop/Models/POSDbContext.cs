using POS_Shop.Helpers;
using POS_Shop.Models.AuthModel;
using POS_Shop.Models.LicenseModels;
using POS_Shop.Models.LoanModelsV1;
using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class POSDbContext: DbContext
    {

        public POSDbContext() : base("name=POSDbConnectionstring")
        {
            //string dbname = @"Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbName.mdf;Integrated Security=true;";
            //Optional Initializer  
            Database.SetInitializer(new CreateDatabaseIfNotExists<POSDbContext>());

            //// Performance optimizations
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.AutoDetectChangesEnabled = false;
        }


        //public POSDbContext() : base(DatabasePathManager.GetConnectionString())
        //{
        //    // Disable initializer since you have existing database
        //    Database.SetInitializer<POSDbContext>(null);

        //    // Configuration for better performance
        //    Configuration.LazyLoadingEnabled = false;
        //    Configuration.ProxyCreationEnabled = false;
        //    Configuration.AutoDetectChangesEnabled = true;
        //    Configuration.ValidateOnSaveEnabled = true;

        //    // Set longer timeout for complex queries
        //    Database.CommandTimeout = 180; // 3 minutes
        //}

        /// <summary>
        /// Tests database connection
        /// </summary>
        /// 
        public bool TestConnection()
        {
            try
            {
                Database.Connection.Open();

                bool isConnected = Database.Connection.State == System.Data.ConnectionState.Open;
                Database.Connection.Close();
                return isConnected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get Database Information
        /// </summary>

        public string GetDatabaseInfo()
        {
            try
            {
                Database.Connection.Open();
                string databaseName = Database.Connection.Database;
                string dataSource = Database.Connection.DataSource;
                Database.Connection.Close();
                return $"Database: {databaseName}, Data Source: {dataSource}";
            }
            catch (Exception ex)
            {
                return $"Error retrieving database info: {ex.Message}";
            }
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }


        public DbSet<Product> Products { get; set; }

        public DbSet<Customer>Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // Temporary Tables for Order Processing
        public DbSet<TempOrder> TempOrders { get; set; }
        public DbSet<TempOrderDetail> TempOrderDetails { get; set; }

        public DbSet<AppLicense> Licenses { get; set; }

        public DbSet<AuthUser> Users { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }



        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<SupplierPaymentDetail> SupplierPaymentDetails { get; set; }





        public DbSet<CustomerLedgerEntry> CustomerLedgerEntries { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Purchase relationships
            modelBuilder.Entity<Purchase>()
                .HasRequired(p => p.Supplier)
                .WithMany(s => s.Purchases)
                .HasForeignKey(p => p.SupplierId)
                .WillCascadeOnDelete(false);

            // Configure SupplierPayment relationships
            modelBuilder.Entity<SupplierPayment>()
                .HasRequired(sp => sp.Supplier)
                .WithMany(s => s.SupplierPayments)
                .HasForeignKey(sp => sp.SupplierId)
                .WillCascadeOnDelete(false);

            // Configure SupplierPaymentDetail relationships
            modelBuilder.Entity<SupplierPaymentDetail>()
                .HasRequired(spd => spd.SupplierPayment)
                .WithMany(sp => sp.PaymentDetails)
                .HasForeignKey(spd => spd.SupplierPaymentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierPaymentDetail>()
                .HasRequired(spd => spd.Purchase)
                .WithMany(p => p.PaymentDetails)
                .HasForeignKey(spd => spd.PurchaseId)
                .WillCascadeOnDelete(false);

            // Configure PurchaseItem relationships
            modelBuilder.Entity<PurchaseItem>()
                .HasRequired(pi => pi.Purchase)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.PurchaseId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);


            // Index with specific configuration
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductEnglishName)
                .HasName("IX_Product_EnglishName")
                .IsClustered(false) // Non-clustered index
                .IsUnique(false);   // Allow duplicates

            

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

            // Configure AppLicense entity
            modelBuilder.Entity<AppLicense>()
               .Property(e => e.UserName)
               .IsRequired()
               .HasMaxLength(100);

            modelBuilder.Entity<AppLicense>()
                .Property(e => e.MacAddress)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<AppLicense>()
                .Property(e => e.HardwareId)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<AppLicense>()
                .Property(e => e.LicenseType)
                .IsRequired();

            // Create indexes
            modelBuilder.Entity<AppLicense>()
                .HasIndex(e => e.MacAddress)
                .HasName("IX_MacAddress");

            modelBuilder.Entity<AppLicense>()
                .HasIndex(e => e.HardwareId)
                .HasName("IX_HardwareId");

            modelBuilder.Entity<AppLicense>()
                .HasIndex(e => e.IsActive)
                .HasName("IX_IsActive");



            modelBuilder.Entity<ProductPrice>()
                .HasIndex(p => p.ProductId)
                .HasName("IX_ProductPrices_ProductId")
                .IsClustered(false) // Non-clustered index
                .IsUnique(false);   // Allow duplicates


            modelBuilder.Entity<ProductPrice>()
                .HasIndex(p => p.TypeName)
                .HasName("IX_ProductPrices_TypeName")
                .IsClustered(false) // Non-clustered index
                .IsUnique(false);   // Allow duplicates




            modelBuilder.Entity<Supplier>()
                .HasIndex(p => p.SupplierName)
                .HasName("IX_Supplier_SupplierName")
                .IsClustered(false) // Non-clustered index
                .IsUnique(false);   // Allow duplicates


            modelBuilder.Entity<Supplier>()
                .HasIndex(p => p.ShopName)
                .HasName("IX_Supplier_ShopName")
                .IsClustered(false) // Non-clustered index
                .IsUnique(false);   // Allow duplicates

            modelBuilder.Entity<Supplier>()
                .HasIndex(p => p.ContactNo)
                .HasName("IX_Supplier_ContactNo")
                .IsClustered(false) // Non-clustered index
                .IsUnique(false);   // Allow duplicates

        }
    }
}
