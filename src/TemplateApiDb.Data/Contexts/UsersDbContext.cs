using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TemplateApiDb.Domain.Entities;

namespace TemplateApiDb.Data.Contexts
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath
        {
            get;
        }

        public UsersDbContext()
        {
            Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "Users.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder
                    .UseSqlite($"Data Source={DbPath}")
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging(true)
                    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
               .Property(e => e.FirstName)
               .HasMaxLength(50);

            modelBuilder.Entity<User>()
               .Property(e => e.LastName)
               .HasMaxLength(50);

            modelBuilder.Entity<User>()
               .Property(e => e.PhoneNumber)
               .HasMaxLength(50);

            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}