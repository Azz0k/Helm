using Helm.Api;
using Helm.Application.Equipment.Equipment.Commands;
using Helm.Application.Equipment.Equipment.Queries;
using Helm.Application.UserRoles.Queries;
using Helm.Application.Users.Commands;
using Helm.Application.Users.Queries;
using Helm.Core.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Net.Http.Json;
using Xunit;

namespace Helm.Tests.ApiTests
{
    public class EquipmentApiTests : IClassFixture<NoAuthWebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        private readonly NoAuthWebApplicationFactory<Program> _factory;
        private string apiUri = "/api/v1/Equipment";
        public EquipmentApiTests(
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
                ISender sender = scopedServices.GetRequiredService<ISender>();
                sender.Send(new CreateUserCommand() { Login = "Test user", Name = "Test user" });
            }
        }
        private async Task<List<EquipmentDTO>?> GetAsync()
        {
            var response = await _client.GetAsync(apiUri);
            var content = await response.Content.ReadFromJsonAsync<List<EquipmentDTO>>();
            return content;
        }
        private async Task<HttpResponseMessage?> CreateAsync(string name, bool isBulk)
        {
            CreateEquipmentRequest request = new() { Name = name, IsBulk = isBulk };
            var response = await _client.PostAsJsonAsync(apiUri, request);
            return response;
        }
        private async Task<HttpResponseMessage?> RenameAsync(string name, int id)
        {
            RenameEquipmentRequest request = new() { Name = name, Id = id };
            var response = await _client.PutAsJsonAsync(apiUri, request);
            return response;
        }
        private async Task<HttpResponseMessage?> IssueAsync(string issuedBy, int id)
        {
            IssueEquipmentRequest request = new() { IssuedBy = issuedBy, Id = id };
            var response = await _client.PutAsJsonAsync($"{apiUri}/issue", request);
            return response;
        }
        private async Task<HttpResponseMessage?> ReturnAsync(int id)
        {
            ReturnEquipmentCommand request = new() { Id = id };
            var response = await _client.PutAsJsonAsync($"{apiUri}/return", request);
            return response;
        }
        private async Task<HttpResponseMessage?> LoseAsync(int id)
        {
            LoseEquipmentCommand request = new() { Id = id };
            var response = await _client.PutAsJsonAsync($"{apiUri}/lose", request);
            return response;
        }
        [Fact]
        public async Task EquipmentApi_GET_HappyPath()
        {
            List<EquipmentDTO>? getResponse = await GetAsync();
            Assert.NotNull(getResponse);
        }
        [Fact]
        public async Task EquipmentApi_POST_HappyPath()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, false);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_POST_DuplicateEntity_ReturnsConflict()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, false);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            response = await CreateAsync(name, false);
            Assert.NotNull(response);
            Assert.Equal(409, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Rename_HappyPath()
        {
            string name = Guid.NewGuid().ToString();
            var response = await RenameAsync(name, 1);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            response = await RenameAsync(name, 1); //Idempotency PUT
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Rename_UnknownEntity_ReturnsNotFound()
        {
            string name = Guid.NewGuid().ToString();
            var response = await RenameAsync(name, int.MaxValue);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Issue_HappyPath()
        {
            string issuedBy = Guid.NewGuid().ToString();
            var response = await IssueAsync(issuedBy, 1);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            response = await IssueAsync(issuedBy, 1); //Idempotency PUT
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Issue_UnknownEntity_ReturnsNotFound()
        {
            string name = Guid.NewGuid().ToString();
            var response = await IssueAsync(name, int.MaxValue);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Return_HappyPath()
        {
            var response = await ReturnAsync(1);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            response = await ReturnAsync(1); //Idempotency PUT
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Return_UnknownEntity_ReturnsNotFound()
        {
            var response = await ReturnAsync(int.MaxValue);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Lose_HappyPath()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, false);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            EquipmentDTO? equipment = await response.Content.ReadFromJsonAsync<EquipmentDTO>();
            Assert.NotNull(equipment);
            response = await LoseAsync(equipment.Id);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            response = await LoseAsync(equipment.Id); //Idempotency PUT
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentApi_PUT_Lose_UnknownEntity_ReturnsNotFound()
        {
            var response = await LoseAsync(int.MaxValue);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
    }
    class CreateEquipmentRequest
    {
        public required string Name { get; set; }
        public bool? IsBulk { get; set; } = null;

    }
    class RenameEquipmentRequest
    {
        public required string Name { get; set; }
        public required int Id { get; set; }
    }
    class IssueEquipmentRequest
    {
        public required string IssuedBy { get; set; }
        public required int Id { get; set; }
    }
    
}
