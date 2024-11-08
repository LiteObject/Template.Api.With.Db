using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TemplateApiDb.Data.Contexts;

namespace TemplateApiDb.Api.Integration.Test
{
    public class DatabaseFixture : IDisposable
    {
        public UsersDbContext Context { get; private set; }

        //private readonly ITestOutputHelper _output;

        public DatabaseFixture()
        {
            // this._output = output;

            IConfigurationRoot config = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", false, true)
                        .Build();

            /* DbContextOptions<UsersDbContext> options = new DbContextOptionsBuilder<UsersDbContext>()
                .LogTo(m => Console.WriteLine(m), Microsoft.Extensions.Logging.LogLevel.Information)
                .UseSqlite()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;  */

            Context = new();

            _ = Context.Database.EnsureDeleted();
            _ = Context.Database.EnsureCreated();

            Context.Users.AddRange(
                     new Domain.Entities.User { Id = 1, Username = new("testuser1"), FirstName = new("Test1"), LastName = new("User1"), Email = new("test.user@email.com"), PhoneNumber = new("972-000-0000") },
                            new Domain.Entities.User { Id = 2, Username = new("testuser2"), FirstName = new("Test2"), LastName = new("User2"), Email = new("test2.user2@email.com"), PhoneNumber = new("214-000-0000") }
                    );

            _ = Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Database.CloseConnection();
                Context.Dispose();
            }
        }
    }
}
