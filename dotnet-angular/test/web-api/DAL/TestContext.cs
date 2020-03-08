using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL
{
    public class TestContext : IdentityDbContext<
        User,
        IdentityRole<Guid>,
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>
    >
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

		public DbSet<Product> Products { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartProduct> CartProduct { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderState> OrderStates { get; set; }
		public DbSet<Address> Addresses { get; set; }

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
                e.HasKey(key => new { key.UserId, key.RoleId });
            });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e =>
            {
                e.ToTable("UserLogins");
                // In case you changed the TKey type
                e.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(e => e.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(e =>
            {
                e.ToTable("UserTokens");
                // In case you changed the TKey type
                e.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

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

            // User
            modelBuilder.Entity<Product>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region Carts

            // Soft delete query filter
            modelBuilder.Entity<Cart>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Cart>().ToTable("Carts");

			// Key
			modelBuilder.Entity<Cart>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Cart>().Property(e => e.Name).IsRequired();

            // User
            modelBuilder.Entity<Cart>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cart>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region CartProduct

            // Soft delete query filter
            modelBuilder.Entity<CartProduct>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<CartProduct>().ToTable("CartProduct");

			// Key
			modelBuilder.Entity<CartProduct>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<CartProduct>().Property(e => e.Quantity).IsRequired();
            modelBuilder.Entity<CartProduct>().Property(e => e.Price).IsRequired();
            modelBuilder.Entity<CartProduct>().Property(e => e.CartId).IsRequired();
            modelBuilder.Entity<CartProduct>().Property(e => e.ProductId).IsRequired();

            // User
            modelBuilder.Entity<CartProduct>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartProduct>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region Orders

            // Soft delete query filter
            modelBuilder.Entity<Order>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Order>().ToTable("Orders");

			// Key
			modelBuilder.Entity<Order>().HasKey(e => e.Id);

            // Required properties

            // User
            modelBuilder.Entity<Order>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region OrderStates

            // Soft delete query filter
            modelBuilder.Entity<OrderState>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<OrderState>().ToTable("OrderStates");

			// Key
			modelBuilder.Entity<OrderState>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<OrderState>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<OrderState>().Property(e => e.OrderId).IsRequired();

            // User
            modelBuilder.Entity<OrderState>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderState>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region Addresses

            // Soft delete query filter
            modelBuilder.Entity<Address>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Address>().ToTable("Addresses");

			// Key
			modelBuilder.Entity<Address>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Address>().Property(e => e.Street).IsRequired();
            modelBuilder.Entity<Address>().Property(e => e.Number).IsRequired();
            modelBuilder.Entity<Address>().Property(e => e.PostalCode).IsRequired();
            modelBuilder.Entity<Address>().Property(e => e.City).IsRequired();

            // User
            modelBuilder.Entity<Address>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Address>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

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
					entry.Entity.GetType() == typeof(Product) ||
					entry.Entity.GetType() == typeof(Cart) ||
					entry.Entity.GetType() == typeof(CartProduct) ||
					entry.Entity.GetType() == typeof(Order) ||
					entry.Entity.GetType() == typeof(OrderState) ||
					entry.Entity.GetType() == typeof(Address)
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
					entry.Entity.GetType() == typeof(Cart) ||
					entry.Entity.GetType() == typeof(CartProduct) ||
					entry.Entity.GetType() == typeof(Order) ||
					entry.Entity.GetType() == typeof(OrderState) ||
					entry.Entity.GetType() == typeof(Address)
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
					    entry.Entity.GetType() == typeof(Product) ||
					    entry.Entity.GetType() == typeof(Cart) ||
					    entry.Entity.GetType() == typeof(CartProduct) ||
					    entry.Entity.GetType() == typeof(Order) ||
					    entry.Entity.GetType() == typeof(OrderState) ||
					    entry.Entity.GetType() == typeof(Address)
                    )
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.CurrentValues["CreatedByUserId"] = userId;
                                entry.CurrentValues["ModifiedByUserId"] = userId;
                                break;
                            case EntityState.Modified:
                                entry.CurrentValues["ModifiedByUserId"] = userId;
                                break;
                        }
                    }
                }
            }
        }
	}
}
