using Helm.Core.Application.Common.Behaviours;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Infrastructure.Configuration;
using Helm.Core.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;

namespace Helm.Tests
{
    public  class NoAuthWebApplicationFactory <TProgram> : CustomWebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var builder = new ConfigurationBuilder().AddJsonFile($"HelperTestsAppSettings.json");
                IConfiguration config = builder.Build();
                string dbConn = config["ConnectionString"];

                var mediatRServices = services.SingleOrDefault(
                      s => (s.ImplementationType?.FullName ?? "").Contains("AuthorizationBehaviour"));
                if (mediatRServices != null)
                {
                    services.Remove(mediatRServices);
                }
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(IDbContextOptionsConfiguration<PostgresDBContext>));
                services.Remove(dbContextDescriptor);
                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new NpgsqlConnection(dbConn);
                    connection.Open();
                    return connection;
                });
                services.AddDbContext<PostgresDBContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseNpgsql(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
