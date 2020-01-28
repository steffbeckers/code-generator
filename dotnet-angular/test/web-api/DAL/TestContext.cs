using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL
{
    public class TestContext : DbContext
	{
		public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

		public DbSet<Product> Products { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<ProductDetail> ProductDetails { get; set; }
		public DbSet<ProductSupplier> ProductSupplier { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestContext"));
            }
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			#region Products

            // Soft delete query filter
            modelBuilder.Entity<Product>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Product>().ToTable("Products");

			// Key
			modelBuilder.Entity<Product>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Product>().Property(e => e.Name).IsRequired();

            #endregion

			#region Suppliers

            // Soft delete query filter
            modelBuilder.Entity<Supplier>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Supplier>().ToTable("Suppliers");

			// Key
			modelBuilder.Entity<Supplier>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Supplier>().Property(e => e.Name).IsRequired();

            #endregion

			#region ProductDetails

            // Soft delete query filter
            modelBuilder.Entity<ProductDetail>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<ProductDetail>().ToTable("ProductDetails");

			// Key
			modelBuilder.Entity<ProductDetail>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<ProductDetail>().Property(e => e.Comment).IsRequired();
            modelBuilder.Entity<ProductDetail>().Property(e => e.ProductId).IsRequired();

            #endregion

			#region ProductSupplier

            // Soft delete query filter
            modelBuilder.Entity<ProductSupplier>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<ProductSupplier>().ToTable("ProductSupplier");

			// Key
			modelBuilder.Entity<ProductSupplier>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<ProductSupplier>().Property(e => e.Comment).IsRequired();
            modelBuilder.Entity<ProductSupplier>().Property(e => e.ProductId).IsRequired();
            modelBuilder.Entity<ProductSupplier>().Property(e => e.SupplierId).IsRequired();

            #endregion
		}

		public override int SaveChanges()
        {
            SoftDeleteLogic();
            TimestampsLogic();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SoftDeleteLogic();
            TimestampsLogic();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SoftDeleteLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
					entry.Entity.GetType() == typeof(Product) ||
					entry.Entity.GetType() == typeof(Supplier) ||
					entry.Entity.GetType() == typeof(ProductDetail) ||
					entry.Entity.GetType() == typeof(ProductSupplier)
				)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["DeletedOn"] = null;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["DeletedOn"] = DateTime.Now;
                            break;
                    }
                }
            }
        }

        private void TimestampsLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
					entry.Entity.GetType() == typeof(Product) ||
					entry.Entity.GetType() == typeof(Supplier) ||
					entry.Entity.GetType() == typeof(ProductDetail) ||
					entry.Entity.GetType() == typeof(ProductSupplier)
				)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedOn"] = DateTime.Now;
                            entry.CurrentValues["ModifiedOn"] = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues["ModifiedOn"] = DateTime.Now;
                            break;
                    }
                }
            }
        }
	}
}
