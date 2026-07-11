using Helm.Api;
using Helm.Application.Equipment.EquipmentTemplate.Commands;
using Helm.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Application.Users.Commands;
using Helm.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Testing.Platform.Extensions.TestFramework;
using System.Net.Http.Json;
using Xunit;


namespace Helm.Tests.ApiTests
{
    public class EquipmentTemplateApiTests : IClassFixture<NoAuthWebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        private readonly NoAuthWebApplicationFactory<Program> _factory;
        private string apiUri = "/api/v1/EquipmentTemplates";
        public EquipmentTemplateApiTests(
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
        private async Task<List<EquipmentTemplateDTO>?> GetAsync()
        {
            var response = await _client.GetAsync(apiUri);
            var content = await response.Content.ReadFromJsonAsync<List<EquipmentTemplateDTO>>();
            return content;
        }
        private async Task<HttpResponseMessage?> CreateAsync(string name, string description, string key)
        {
            CreateEquipmentTemplateCommand request = new() { Name = name, Description = description, RenderTemplateKey = key };
            var response = await _client.PostAsJsonAsync(apiUri, request);
            return response;
        }
        private async Task<HttpResponseMessage?> UpdateAsync(int id, string name, string description, bool enabled)
        {
            UpdateEquipmentTemplateCommand request = new() { Id = id, Name = name, Description = description, Enabled = enabled };
            var response = await _client.PutAsJsonAsync(apiUri, request);
            return response;
        }
        [Fact]
        public async Task EquipmentTemplateApi_GET_HappyPath()
        {
            List<EquipmentTemplateDTO>? getResponse = await GetAsync();
            Assert.NotNull(getResponse);
        }
        
        [Fact]
        public async Task EquipmentTemplateApi_POST_HappyPath()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, name, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<EquipmentTemplateDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            Assert.Equal(name, content.Name);
            Assert.Equal(name, content.Description);
        }
        [Fact]
        public async Task EquipmentTemplateApi_POST_DuplicateEntity_ReturnsConflict()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, name, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            response = await CreateAsync(name, name, name);
            Assert.NotNull(response);
            Assert.Equal(409, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentTemplateApi_PUT_HappyPath()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, name, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<EquipmentTemplateDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            int id = content.Id;
            bool newStatus = false;
            name = Guid.NewGuid().ToString();
            response = await UpdateAsync(id, name, name, newStatus);
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            content = await response.Content.ReadFromJsonAsync<EquipmentTemplateDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            Assert.Equal(name, content.Name);
            Assert.Equal(name, content.Description);
            Assert.Equal(id, content.Id);
            Assert.Equal(newStatus, content.Enabled);
        }
        [Fact]
        public async Task EquipmentTemplateApi_PUT_NotFound()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, name, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<EquipmentTemplateDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            bool newStatus = false;
            name = Guid.NewGuid().ToString();
            response = await UpdateAsync(int.MaxValue, name, name, newStatus);
            Assert.NotNull(response);
            Assert.Equal(404, (int)response.StatusCode);
        }
        [Fact]
        public async Task EquipmentTemplateApi_PUT_Conflict()
        {
            string name = Guid.NewGuid().ToString();
            var response = await CreateAsync(name, name, name);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            string newName = Guid.NewGuid().ToString();
            response = await CreateAsync(newName, newName, newName);
            Assert.NotNull(response);
            Assert.Equal(201, (int)response.StatusCode);
            var content = await response.Content.ReadFromJsonAsync<EquipmentTemplateDTO>(TestContext.Current.CancellationToken);
            Assert.NotNull(content);
            int id = content.Id;
            response = await UpdateAsync(id, name, name, false);
            Assert.NotNull(response);
            Assert.Equal(409, (int)response.StatusCode);
        }
    }
}
