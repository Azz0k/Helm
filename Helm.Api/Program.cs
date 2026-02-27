using Helm.Core.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Logging;

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
            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });
            
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
