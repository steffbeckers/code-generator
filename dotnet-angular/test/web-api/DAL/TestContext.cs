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

		public DbSet<Account> Accounts { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Todo> Todos { get; set; }

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

			#region Contacts

            // Soft delete query filter
            modelBuilder.Entity<Contact>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Contact>().ToTable("Contacts");

			// Key
			modelBuilder.Entity<Contact>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Contact>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<Contact>().Property(e => e.LastName).IsRequired();

            #endregion

			#region Addresses

            // Soft delete query filter
            modelBuilder.Entity<Address>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Address>().ToTable("Addresses");

			// Key
			modelBuilder.Entity<Address>().HasKey(e => e.Id);

            // Required properties

            #endregion

			#region Notes

            // Soft delete query filter
            modelBuilder.Entity<Note>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Note>().ToTable("Notes");

			// Key
			modelBuilder.Entity<Note>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Note>().Property(e => e.Title).IsRequired();

            #endregion

			#region Todos

            // Soft delete query filter
            modelBuilder.Entity<Todo>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
			modelBuilder.Entity<Todo>().ToTable("Todos");

			// Key
			modelBuilder.Entity<Todo>().HasKey(e => e.Id);

            // Required properties
            modelBuilder.Entity<Todo>().Property(e => e.Title).IsRequired();

            #endregion
		}

		public override int SaveChanges()
        {
            SoftDeleteLogic();
            TimeStampsLogic();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SoftDeleteLogic();
            TimeStampsLogic();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SoftDeleteLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
					entry.Entity.GetType() == typeof(Account) ||
					entry.Entity.GetType() == typeof(Contact) ||
					entry.Entity.GetType() == typeof(Address) ||
					entry.Entity.GetType() == typeof(Note) ||
					entry.Entity.GetType() == typeof(Todo)
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

        private void TimeStampsLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                // Models that have soft delete
                if (
					entry.Entity.GetType() == typeof(Account) ||
					entry.Entity.GetType() == typeof(Contact) ||
					entry.Entity.GetType() == typeof(Address) ||
					entry.Entity.GetType() == typeof(Note) ||
					entry.Entity.GetType() == typeof(Todo)
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
