using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.API.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Test.API.DAL
{
    public class TestContext : DbContext
	{
		public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

		public DbSet<Account> Accounts { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Call> Calls { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Document> Documents { get; set; }

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

            // Required properties
            modelBuilder.Entity<Account>().Property(e => e.Name).IsRequired();

            #endregion

			#region Contacts

            // Soft delete query filter
            modelBuilder.Entity<Contact>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Contact>().ToTable("Contacts");

            // Required properties
            modelBuilder.Entity<Contact>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<Contact>().Property(e => e.LastName).IsRequired();

            #endregion

			#region Calls

            // Soft delete query filter
            modelBuilder.Entity<Call>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Call>().ToTable("Calls");

            // Required properties
            modelBuilder.Entity<Call>().Property(e => e.Date).IsRequired();

            #endregion

			#region Notes

            // Soft delete query filter
            modelBuilder.Entity<Note>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Note>().ToTable("Notes");

            // Required properties
            modelBuilder.Entity<Note>().Property(e => e.Title).IsRequired();

            #endregion

			#region Documents

            // Soft delete query filter
            modelBuilder.Entity<Document>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Document>().ToTable("Documents");

            // Required properties
            modelBuilder.Entity<Document>().Property(e => e.Name).IsRequired();

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
					entry.Entity.GetType() == typeof(Account) ||					entry.Entity.GetType() == typeof(Contact) ||					entry.Entity.GetType() == typeof(Call) ||					entry.Entity.GetType() == typeof(Note) ||					entry.Entity.GetType() == typeof(Document)				)
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
					entry.Entity.GetType() == typeof(Account) ||					entry.Entity.GetType() == typeof(Contact) ||					entry.Entity.GetType() == typeof(Call) ||					entry.Entity.GetType() == typeof(Note) ||					entry.Entity.GetType() == typeof(Document)				)
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
