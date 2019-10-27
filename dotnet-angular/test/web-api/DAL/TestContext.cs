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
		public DbSet<Call> Calls { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Document> Documents { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Todo> Todoes { get; set; }

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

			#region Emails

            // Soft delete query filter
            modelBuilder.Entity<Email>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Email>().ToTable("Emails");

            // Required properties
            modelBuilder.Entity<Email>().Property(e => e.Subject).IsRequired();

            #endregion

			#region Projects

            // Soft delete query filter
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Project>().ToTable("Projects");

            // Required properties
            modelBuilder.Entity<Project>().Property(e => e.Name).IsRequired();

            #endregion

			#region Todoes

            // Soft delete query filter
            modelBuilder.Entity<Todo>().HasQueryFilter(e => e.DeletedOn == null);

            // Table
            modelBuilder.Entity<Todo>().ToTable("Todoes");

            // Required properties

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
					entry.Entity.GetType() == typeof(Call) ||
					entry.Entity.GetType() == typeof(Note) ||
					entry.Entity.GetType() == typeof(Document) ||
					entry.Entity.GetType() == typeof(Email) ||
					entry.Entity.GetType() == typeof(Project) ||
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
					entry.Entity.GetType() == typeof(Call) ||
					entry.Entity.GetType() == typeof(Note) ||
					entry.Entity.GetType() == typeof(Document) ||
					entry.Entity.GetType() == typeof(Email) ||
					entry.Entity.GetType() == typeof(Project) ||
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
