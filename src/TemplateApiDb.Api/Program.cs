using Serilog;
using TemplateApiDb.Data.Contexts;

namespace TemplateApiDb.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _ = builder.Host.UseSerilog((context, services, configuration) =>
            {
                _ = configuration.ReadFrom.Configuration(context.Configuration, "Serilog");
            });

            _ = builder.Configuration.AddEnvironmentVariables();
            _ = builder.Services.AddDbContext<UsersDbContext>();
            _ = builder.Services.AddAutoMapper(typeof(Program));
            _ = builder.Services.AddControllers();
            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TemplateApiDb.Api",
                    Version = "v1",
                    Description = "Description: Basic API (with DB) template"
                });
            });
            _ = builder.Services.AddHealthChecks();

            WebApplication app = builder.Build();

            if (!app.Environment.IsProduction())
            {
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "TemplateApiDb.Api";
                });

                _ = app.MapFallback(() => Results.Redirect("/swagger"));
            }

            SetupDatabaseWithDummyData(app);

            _ = app.UseHttpsRedirection();

            _ = app.UseAuthorization();

            _ = app.MapControllers();
            _ = app.MapHealthChecks("/health").AllowAnonymous();

            app.Run();
        }

        private static void SetupDatabaseWithDummyData(WebApplication app)
        {
            using IServiceScope serviceScope = app.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            using UsersDbContext usersDbContext = serviceProvider.GetRequiredService<UsersDbContext>();
            _ = usersDbContext.Database.EnsureDeleted();
            _ = usersDbContext.Database.EnsureCreated();

            usersDbContext.Users.AddRange(
                new Domain.Entities.User { Id = 1, Username = new("testuser1"), FirstName = new("Test1"), LastName = new("User1"), Email = new("test.user@email.com"), PhoneNumber = new("972-000-0000") },
                 new Domain.Entities.User { Id = 2, Username = new("testuser2"), FirstName = new("Test2"), LastName = new("User2"), Email = new("test2.user2@email.com"), PhoneNumber = new("214-000-0000") }
                );
            _ = usersDbContext.SaveChanges();
        }
    }
}