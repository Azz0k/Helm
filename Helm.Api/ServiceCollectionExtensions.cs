using FluentValidation;
using Helm.Core.Application.Common.Behaviours;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Infrastructure.Configuration;
using Helm.Core.Infrastructure.Contexts;
using Helm.Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Helm.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCorsPolicy(this IServiceCollection services, string allowedOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEnd", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }
        public static async Task AddAdfsAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                $"{appSettings.ADFS?.ADFSDomain}.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{appSettings.ADFS?.ADFSDomain}.well-known/openid-configuration";
                    options.Audience = appSettings.ADFS?.ADFSAudience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = appSettings.ADFS?.ADFSIssuer,
                        IssuerSigningKeys = openIdConfig.SigningKeys
                    };
                });
        }
        public static void AddApplication(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    "Helm.API",
                    "Helm.Core"
                });

                cfg.LicenseKey = appSettings.MediatRLicense;
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(typeof(GetUsersQueryHandler).Assembly);
            services.AddMediatR(cfg =>
            {
                cfg.LicenseKey = appSettings.MediatRLicense;
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQueryHandler).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            });
        }
        public static void AddInfrastructure(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddTransient<Helm.Core.Infrastructure.Renderer.IHtmlRenderer, Helm.Core.Infrastructure.Renderer.HtmlRenderer>();
            services.AddDbContext<PostgresDBContext>(options => options.UseNpgsql(appSettings.ConnectionString));
            services.AddScoped<IUserRepository, PostgresUserRepository>();
            services.AddScoped<IUserRoleRepository, PostgresUserRoleRepository>();
            services.AddScoped<IEquipmentRepository, PostgresEquipmentRepository>();
            services.AddScoped<IEquipmentTemplateRepository, PostgresEquipmentTemplateRepository>();
            services.AddScoped<IAuthorizationService, PostgresAuthorizationService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
        }
        
    }
}
