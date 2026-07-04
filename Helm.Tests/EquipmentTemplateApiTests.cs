using Helm.Api;
using Helm.Core.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Core.Application.Users.Commands;
using Helm.Core.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;


namespace Helm.Tests
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
        [Fact]
        public async Task EquipmentTemplateApi_GET_HappyPath()
        {
            List<EquipmentTemplateDTO>? getResponse = await GetAsync();
            Assert.NotNull(getResponse);
        }
    }
}
