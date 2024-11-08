using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApiDb.Api.DTO;
using Xunit.Abstractions;

namespace TemplateApiDb.Api.Integration.Test
{
    /// <summary>
    /// More on "Integration tests in ASP.NET Core":
    /// https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
    /// </summary>
    [Collection("Database collection")]
    public class UsersControllerTests :
        IClassFixture<DatabaseFixture>,
        IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private ITestOutputHelper _output;
        DatabaseFixture fixture;

        public UsersControllerTests(ITestOutputHelper output, WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture)
        {
            this._output = output;
            this._factory = factory;
            this.fixture = databaseFixture;
        }

        [Fact]
        public async Task Get_Users_Should_Returns200OK()
        {
            _output.WriteLine($">>> Executing test: {nameof(Get_Users_Should_Returns200OK)}");

            // ARRANGE
            HttpClient client = _factory.CreateClient();

            // ACT
            HttpResponseMessage response = await client.GetAsync("/users");
            Stream responseBody = await response.Content.ReadAsStreamAsync();
            List<User>? users = await System.Text.Json.JsonSerializer.DeserializeAsync<List<User>>(responseBody);

            // ASSERT
            _ = response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(users);
            Assert.NotEmpty(users);
            Assert.True(users.Any());
        }

        [Fact]
        public async Task Get_User_By_Id_Should_Returns200OK()
        {
            _output.WriteLine($">>> Executing test: {nameof(Get_Users_Should_Returns200OK)}");

            // ARRANGE
            HttpClient client = _factory.CreateClient();

            // ACT
            HttpResponseMessage response = await client.GetAsync("/users/1");
            Stream responseBody = await response.Content.ReadAsStreamAsync();
            User? user = await System.Text.Json.JsonSerializer.DeserializeAsync<User>(responseBody);

            // ASSERT
            _ = response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(user);
        }
    }
}
