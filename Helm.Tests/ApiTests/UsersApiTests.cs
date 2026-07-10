using Helm.Api;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Application.Users.Commands;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace Helm.Tests.ApiTests
{
    public class UsersApiTests: IClassFixture<NoAuthWebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        private readonly NoAuthWebApplicationFactory<Program> _factory;
        private string apiUri = "/api/v1/Users";
        private string apiRolesUri = "/api/v1/UserRoles";
        public UsersApiTests(
    NoAuthWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
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
        private async Task<HttpResponseMessage?> CreateAsync(string login, string name)
        {
            CreateUserCommand request = new()
            {
                Login = login,
                Name = name,
            };
            var response = await _client.PostAsJsonAsync(apiUri, request);
            return response;
        }
        private async Task<int> CreateOneUser()
        {
            string name = Guid.NewGuid().ToString();
            string login = Guid.NewGuid().ToString();
            List<int> roles = [];
            var response = await CreateAsync(login, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<UserRoleDTO>();
            Assert.NotNull(content);
            int originalId = content.Id;
            return content.Id;
        }
        private async Task<int> CreateOneUserRole()
        {
            string Name = Guid.NewGuid().ToString();
            string Description = Guid.NewGuid().ToString();
            CreateUserRoleRequest request = new CreateUserRoleRequest { Name = Name, Description = Description };
            var response = await _client.PostAsJsonAsync(apiRolesUri, request);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<UserRoleDTO>();
            Assert.NotNull(content);
            int originalId = content.Id;
            return content.Id;
        }
        private async Task<HttpResponseMessage> DeleteAsync(int? id)
        {
            var response = await _client.DeleteAsync($"{apiUri}/{id}");
            return response;
        }
        [Fact]
        public async Task UsersApi_GET_ShouldWorkCorrectly()
        {
            List<UserDTO>? getResponse = await GetAsync();
            Assert.NotNull(getResponse);
        }
        [Fact]
        public async Task UsersApi_POST_ShouldWorkCorrectly()
        {
            string name = Guid.NewGuid().ToString();
            string login = Guid.NewGuid().ToString();
            List<int> roles = [];
            var response = await CreateAsync(login, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            response = await CreateAsync(login, name);
            Assert.NotNull(response);
            Assert.Equal(409, (int)response.StatusCode);
        }
        private async Task<HttpResponseMessage> UpdateAsync(int id, string Name, string Login)
        {
            var request = new UpdateUserCommand { Id = id, Name = Name, Login = Login };
            return await _client.PutAsJsonAsync(apiUri, request);
        }
        private async Task<HttpResponseMessage> UpdateStatusAsync(int id, bool enabled)
        {
            var request = new UpdateUserStatusCommand { Enabled = enabled };
            return await _client.PutAsJsonAsync($"{apiUri}/{id}/status", request);
        }
        private async Task UpdateUserHappyPath(int id, string Name, string Login)
        {
            var response = await UpdateAsync(id, Name, Login);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<UserDTO>();
            Assert.NotNull(content);
            Assert.Equal(id, content.Id);
            Assert.Equal(Name, content.Name);
            Assert.Equal(Login, content.Login);
        }
        [Fact]
        public async Task UsersApi_PUT_ShouldWorkCorrectly()
        {
            int originalId = await CreateOneUser();
            string newName = Guid.NewGuid().ToString();
            string newLogin = Guid.NewGuid().ToString();
            await UpdateUserHappyPath(originalId, newName, newLogin);
            await UpdateUserHappyPath(originalId, newName, newLogin);//idempotency PUT
            var response = await UpdateAsync(Int32.MaxValue, newName, newLogin);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
        [Fact]
        public async Task UsersApiStatus_PUT_ShouldWorkCorrectly()
        {
            int originalId = await CreateOneUser();
            bool status = false;
            var response = await UpdateStatusAsync(originalId, status);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            Assert.Equal(status, content.Enabled);
            status = true;
            response = await UpdateStatusAsync(originalId, status);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            content = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            Assert.Equal(status, content.Enabled);
            response = await UpdateStatusAsync(Int32.MaxValue, status);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);

        }
        [Fact]
        public async Task UsersApi_DELETE_ShouldWorkCorrectly()
        {
            int originalId = await CreateOneUser();
            var response = await DeleteAsync(originalId);
            Assert.NotNull(response);
            Assert.Equal(204, (int)response.StatusCode);
            response = await DeleteAsync(originalId);
            Assert.NotNull(response);
            Assert.Equal(204, (int)response.StatusCode);
            response = await DeleteAsync(int.MaxValue);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
        [Fact]
        public async Task UsersApi_AssignRoles_ShouldWorkCorrectly()
        {
            int userId = await CreateOneUser();
            int roleId = await CreateOneUserRole();
            var response = await _client.PutAsJsonAsync($"{apiUri}/{userId}/role/{roleId}","",TestContext.Current.CancellationToken);
            Assert.Equal(200, (int)response.StatusCode);
            UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(user);
            Assert.NotNull(user.Roles);
            Assert.NotEmpty(user.Roles);
            Assert.Contains(roleId, user.Roles);
            response = await _client.PutAsJsonAsync($"{apiUri}/{userId}/role/{roleId}", "", TestContext.Current.CancellationToken);
            Assert.Equal(409, (int)response.StatusCode);
            int secondRoleId = await CreateOneUserRole();
            response = await _client.PutAsJsonAsync($"{apiUri}/{userId}/role/{secondRoleId}", "", TestContext.Current.CancellationToken);
            Assert.Equal(200, (int)response.StatusCode);
            user = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(user);
            Assert.NotNull(user.Roles);
            Assert.NotEmpty(user.Roles);
            Assert.Contains(secondRoleId, user.Roles);
            Assert.Equal(2, user.Roles.Count);
            response = await _client.DeleteAsync($"{apiUri}/{userId}/role/{secondRoleId}",  TestContext.Current.CancellationToken);
            Assert.Equal(200, (int)response.StatusCode);
            user = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(user);
            Assert.NotNull(user.Roles);
            Assert.NotEmpty(user.Roles);
            Assert.Contains(roleId, user.Roles);
            Assert.DoesNotContain(secondRoleId, user.Roles);
            Assert.Single(user.Roles);
        }
        [Fact]
        public async Task UsersApi_ReplaceRoles_ShouldWorkCorrectly()
        {
            int userId = await CreateOneUser();
            List<int> roles = new List<int>() { 1, 2 };
            ReplaceUserRoleCommand request = new() { Roles = roles , UserId = userId};
            var response = await _client.PutAsJsonAsync($"{apiUri}/role", request, TestContext.Current.CancellationToken);
            UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(user);
            Assert.NotNull(user.Roles);
            Assert.NotEmpty(user.Roles);
            Assert.Contains(1, user.Roles);
            Assert.Contains(2, user.Roles);
            response = await _client.PutAsJsonAsync($"{apiUri}/role", request, TestContext.Current.CancellationToken); //indempotency check
            user = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(user);
            Assert.NotNull(user.Roles);
            Assert.NotEmpty(user.Roles);
            Assert.Contains(1, user.Roles);
            Assert.Contains(2, user.Roles);
            roles = new List<int>() { 1,  };
            request = new () { Roles = roles, UserId = userId };
            response = await _client.PutAsJsonAsync($"{apiUri}/role", request, TestContext.Current.CancellationToken);
            user = await response.Content.ReadFromJsonAsync<UserDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(user);
            Assert.NotNull(user.Roles);
            Assert.NotEmpty(user.Roles);
            Assert.Contains(1, user.Roles);
            Assert.DoesNotContain(2, user.Roles);
            request = new() { Roles = roles, UserId = int.MaxValue };
            response = await _client.PutAsJsonAsync($"{apiUri}/role", request, TestContext.Current.CancellationToken); 
            Assert.Equal(404, (int)response.StatusCode);
        }
    }
}
