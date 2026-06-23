using FluentValidation;
using Helm.Core.Application.Common.Behaviours;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Infrastructure.Configuration;
using Helm.Core.Infrastructure.Contexts;
using Helm.Core.Infrastructure.Repositories;
using Helm.Razor;
using Helm.Razor.MyFeature.Pages;
using Helm.Core.Infrastructure.Renderer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Helm.Tests")]
namespace Helm.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var settignsBuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
            IConfiguration config = settignsBuilder.Build();
            AppSettings? appSettings = config.GetSection("Settings").Get<AppSettings>();
            var validator = new AppSettingsValidator(appSettings);
            if (!validator.IsValid)
            {
                foreach (var item in validator.Errors )
                {
                    Console.WriteLine(item.ErrorMessage);
                }
                return;
            }
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{appSettings?.ADFS?.ADFSDomain}.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(new []
                {
                    "Helm.API",
                    "Helm.Core"
                });

                cfg.LicenseKey = appSettings?.MediatRLicense;
            });
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddValidatorsFromAssembly(typeof(GetUsersQueryHandler).Assembly);
            builder.Services.AddMediatR(cfg =>
            {
                cfg.LicenseKey = appSettings?.MediatRLicense;
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQueryHandler).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            });
            builder.Services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = "wwwroot";
            });
            builder.Services.AddControllers();
            builder.Services.AddMvcCore().AddRazorViewEngine();
                //.AddApplicationPart(typeof(Page1Model).Assembly); ;
            builder.Services.AddTransient<Helm.Core.Infrastructure.Renderer.IHtmlRenderer, Helm.Core.Infrastructure.Renderer.HtmlRenderer>();
            builder.Services.AddDbContext<PostgresDBContext>(options => options.UseNpgsql(appSettings?.ConnectionString));
            builder.Services.AddScoped<IUserRepository,PostgresUserRepository>();
            builder.Services.AddScoped<IUserRoleRepository,PostgresUserRoleRepository>();
            builder.Services.AddScoped<IUserContext, UserContext>();
            builder.Services.AddScoped<IAuthorizationService, PostgresAuthorizationService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{appSettings?.ADFS?.ADFSDomain}.well-known/openid-configuration";
                    options.Audience = appSettings?.ADFS?.ADFSAudience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = appSettings?.ADFS?.ADFSIssuer,
                        IssuerSigningKeys = openIdConfig.SigningKeys
                    };
                });
            
            builder.Services.AddAuthorization();
            if (appSettings?.AllowedOrigins != null)
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("FrontEnd", policy =>
                    {
                        policy.WithOrigins(appSettings?.AllowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
                });
            }
            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseStaticFiles();
            if (appSettings?.AllowedOrigins != null)
            {
                app.UseCors("FrontEnd");
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";
            });
            app.Run();
        }
        
    }
}
