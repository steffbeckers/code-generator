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

		public DbSet<Country> Countries { get; set; }
		public DbSet<RelationType> RelationTypes { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<WorkOrder> WorkOrders { get; set; }

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

			#region Countries

            // Soft delete query filter
            modelBuilder.Entity<Country>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Country>().ToTable("Countries");

			// Key
			modelBuilder.Entity<Country>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Country>().Property(e => e.Name).IsRequired();

            // User
            modelBuilder.Entity<Country>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Country>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region RelationTypes

            // Soft delete query filter
            modelBuilder.Entity<RelationType>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<RelationType>().ToTable("RelationTypes");

			// Key
			modelBuilder.Entity<RelationType>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<RelationType>().Property(e => e.Name).IsRequired();

            // User
            modelBuilder.Entity<RelationType>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RelationType>()
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

			#region Contacts

            // Soft delete query filter
            modelBuilder.Entity<Contact>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Contact>().ToTable("Contacts");

			// Key
			modelBuilder.Entity<Contact>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Contact>().Property(e => e.LastName).IsRequired();
            modelBuilder.Entity<Contact>().Property(e => e.AccountId).IsRequired();

            // User
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Contact>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

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

            // User
            modelBuilder.Entity<Account>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasOne(x => x.ModifiedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

			#region WorkOrders

            // Soft delete query filter
            modelBuilder.Entity<WorkOrder>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<WorkOrder>().ToTable("WorkOrders");

			// Key
			modelBuilder.Entity<WorkOrder>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<WorkOrder>().Property(e => e.Date).IsRequired();
            modelBuilder.Entity<WorkOrder>().Property(e => e.AccountId).IsRequired();

            // User
            modelBuilder.Entity<WorkOrder>()
                .HasOne(x => x.CreatedByUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkOrder>()
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
					entry.Entity.GetType() == typeof(Country) ||
					entry.Entity.GetType() == typeof(RelationType) ||
					entry.Entity.GetType() == typeof(Address) ||
					entry.Entity.GetType() == typeof(Contact) ||
					entry.Entity.GetType() == typeof(Account) ||
					entry.Entity.GetType() == typeof(WorkOrder)
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
					entry.Entity.GetType() == typeof(Country) ||
					entry.Entity.GetType() == typeof(RelationType) ||
					entry.Entity.GetType() == typeof(Address) ||
					entry.Entity.GetType() == typeof(Contact) ||
					entry.Entity.GetType() == typeof(Account) ||
					entry.Entity.GetType() == typeof(WorkOrder)
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
					    entry.Entity.GetType() == typeof(Country) ||
					    entry.Entity.GetType() == typeof(RelationType) ||
					    entry.Entity.GetType() == typeof(Address) ||
					    entry.Entity.GetType() == typeof(Contact) ||
					    entry.Entity.GetType() == typeof(Account) ||
					    entry.Entity.GetType() == typeof(WorkOrder)
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
