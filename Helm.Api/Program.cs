using Helm.Core.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Threading;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Helm.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var settignsBuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
            IConfiguration config = settignsBuilder.Build();
            AppSettings? appSettings = config.GetSection("Settings").Get<AppSettings>();
            var validator = new AppSettingsValidator(appSettings).Validate();
            if (!validator.valid)
            {
                Console.WriteLine(validator.error);
                return;
            }
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{appSettings?.ADFS?.ADFSDomain}.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = "wwwroot";
            });
            builder.Services.AddControllers();
            
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
