using Helm.Core.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Helm.Api
{
    public class Program
    {
        public static void Main(string[] args)
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
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = "wwwroot";
            });
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).
            AddOpenIdConnect(options =>
            {
                options.ClientSecret = "O1gvoqODqZi8Nv_K__3Mfz36fZh_VmUlx6f4EBT0";
                options.Authority = "https://fs.energo.local/adfs/.well-known/openid-configuration";
                options.ClientId = "7aec8875-80fe-4998-94b8-d5f817b4bf5b";
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.ResponseType = OpenIdConnectResponseType.Code;

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.MapInboundClaims = false;

            });
                
            builder.Services.AddAuthorization();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontEnd", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); 
                });
            });
            builder.Services.AddControllers();
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseCors("FrontEnd");
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
