using Helm.Api;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.ApiModels.User;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace Helm.Tests
{
    public class UsersApiTests: IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private string apiUri = "/api/v1/Users";
        public UsersApiTests(
    CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "TestScheme";
                        options.DefaultChallengeScheme = "TestScheme";
                    })
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestScheme", options => { });
                });
            })
        .CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
            DoMigrate();
        }
        private void DoMigrate()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<PostgresDBContext>();
                db.Database.Migrate();
            }
        }
        private async Task<List<UserDTO>?> GetAsync()
        {
            var response = await _client.GetAsync(apiUri);
            var content = await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            return content;
        }
        [Fact]
        public async Task UsersApi_GET_ShouldWorkCorrectly()
        {
            List<UserDTO>? getResponse = await GetAsync();
            Assert.NotNull(getResponse);
            Assert.Empty(getResponse);
        }
    }
}
