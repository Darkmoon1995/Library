using Microsoft.EntityFrameworkCore;
using LibraryTRY3.Models;
using Microsoft.Extensions.Logging;

namespace LibraryTRY3.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BooksDetail> BooksDetails { get; set; }
        public DbSet<LibraryStructure> LibraryStructures { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Borrow> Borrow { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public MyDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlite("Data Source=MyLocalDatabase.db")
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

    }
}
