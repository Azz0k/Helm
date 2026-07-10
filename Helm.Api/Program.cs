using Helm.Application.Interfaces;
using Helm.Core.Infrastructure.Configuration;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Helm.Tests")]
namespace Helm.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppSettings appSettings = AppSettingsExtensions.GetValidatedAppSettings();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddApplication(appSettings);
            builder.Services.AddInfrastructure(appSettings);
            builder.Services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = "wwwroot";
            });
            builder.Services.AddControllers();
            builder.Services.AddMvcCore().AddRazorViewEngine();
            await builder.Services.AddAdfsAuthentication(appSettings);
            builder.Services.AddAuthorization();
            builder.Services.AddCorsPolicy(appSettings.AllowedOrigins);
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
