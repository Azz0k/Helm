using Helm.Api;
using Helm.Core.Application.UserRoles.Queries;
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
    public class UserRolesAPITests: IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private string apiUri = "/api/v1/UserRoles";
        public UserRolesAPITests(
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
        private async Task<List<UserRoleDTO>?> GetAsync()
        {
            var response = await _client.GetAsync(apiUri);
            var content = await response.Content.ReadFromJsonAsync<List<UserRoleDTO>>();
            Assert.Equal(200, (int)response.StatusCode);
            return content;
        }
        private async Task CreateAsync()
        {
            await CreateAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
        private async Task<HttpResponseMessage?> CreateAsync(string? Name, string? Description)
        {
            CreateUserRoleRequest request = new CreateUserRoleRequest { Name = Name, Description = Description };
            var response = await _client.PostAsJsonAsync(apiUri, request);
            return response;
        }
        [Fact]
        public async Task UsersRolesApi_GET_ShouldWorkCorrectly()
        {
            List<UserRoleDTO>? getResponse = await GetAsync();
            Assert.NotNull(getResponse);
            await CreateAsync();
            getResponse = await GetAsync();
            Assert.NotNull(getResponse);
            Assert.NotEmpty(getResponse);
        }
        [Fact]
        public async Task UsersRolesApi_POST_ShouldWorkCorrectly()
        {
            string name = Guid.NewGuid().ToString();
            string description = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, description);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            response = await CreateAsync(name, description);
            Assert.NotNull(response);
            Assert.Equal(409, (int)response.StatusCode);
            response = await CreateAsync("   ", description);
            Assert.NotNull(response);
            Assert.Equal(400, (int)response.StatusCode);
            response = await CreateAsync(null, description);
            Assert.NotNull(response);
            Assert.Equal(400, (int)response.StatusCode);
        }
    }
    public class CreateUserRoleRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
