using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Test.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Test.API.DAL
{
    public class TestContext : IdentityDbContext<User>
	{
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

		public TestContext(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration
        ) : base()
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

		public DbSet<Account> Accounts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<ProductDetail> ProductDetails { get; set; }
		public DbSet<ProductSupplier> ProductSupplier { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestContext"));
            }
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Identity

            modelBuilder.Entity<User>(e => e.ToTable("Users"));
            modelBuilder.Entity<IdentityRole<Guid>>(e => e.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(e =>
            {
                e.ToTable("UserRoles");
                // In case you changed the TKey type
                // e.HasKey(key => new { key.UserId, key.RoleId });
            });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e =>
            {
                e.ToTable("UserLogins");
                // In case you changed the TKey type
                //e.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(e => e.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(e =>
            {
                e.ToTable("UserTokens");
                // In case you changed the TKey type
                //e.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

            #endregion

			#region Accounts

            // Soft delete query filter
            modelBuilder.Entity<Account>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Account>().ToTable("Accounts");

			// Key
			modelBuilder.Entity<Account>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Account>().Property(e => e.Name).IsRequired();

            #endregion

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
            modelBuilder.Entity<ProductSupplier>().Property(e => e.ProductId).IsRequired();
            modelBuilder.Entity<ProductSupplier>().Property(e => e.SupplierId).IsRequired();

            #endregion
		}

		public override int SaveChanges()
        {
            SoftDeleteLogic();
            TimestampsLogic();
            UserInfoDataLogic();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SoftDeleteLogic();
            TimestampsLogic();
            UserInfoDataLogic();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SoftDeleteLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
					entry.Entity.GetType() == typeof(Account) ||
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
					entry.Entity.GetType() == typeof(Account) ||
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

        private void UserInfoDataLogic()
        {
            string userIdString = this.httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdString))
            {
                Guid userId = Guid.Parse(userIdString);

                foreach (var entry in ChangeTracker.Entries())
                {
                    Type entityType = entry.Entity.GetType();
                    if (
					    entry.Entity.GetType() == typeof(Account) ||
					    entry.Entity.GetType() == typeof(Product) ||
					    entry.Entity.GetType() == typeof(Supplier) ||
					    entry.Entity.GetType() == typeof(ProductDetail) ||
					    entry.Entity.GetType() == typeof(ProductSupplier)
                    )
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.CurrentValues["CreatedUserId"] = userId;
                                entry.CurrentValues["ModifiedUserId"] = userId;
                                break;
                            case EntityState.Modified:
                                entry.CurrentValues["ModifiedUserId"] = userId;
                                break;
                        }
                    }
                }
            }
        }
	}
}
