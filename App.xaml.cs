using System.Windows;
using LibraryTRY3.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryTRY3
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlite("Data Source=MyLocalDatabase.db");

            using (var context = new MyDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
