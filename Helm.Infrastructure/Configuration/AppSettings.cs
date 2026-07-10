using Microsoft.Extensions.Configuration;

namespace Helm.Infrastructure.Configuration
{
    public class AppSettings
    {
        public required string AllowedOrigins { get; init; }
        public required string ConnectionString { get; init; }
        public required string MediatRLicense {  get; init; }
        public required ADFSSettings ADFS {  get; init; }
        
    }
    public static class AppSettingsExtensions
    {
        public static AppSettings GetValidatedAppSettings()
        {
            AppSettings? appSettings = null;
            try
            {
                var settingsBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfiguration config = settingsBuilder.Build();
                appSettings = config.GetSection("Settings").Get<AppSettings>();
            }
            catch  (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error loading appsettings.json");
                Environment.Exit(1);
            }
            if (appSettings == null)
            {
                Console.WriteLine("Settings section's missing in appsettings.json");
                Environment.Exit(1);
            }
            var validator = new AppSettingsValidator(appSettings);
            if (!validator.IsValid)
            {
                foreach (var item in validator.Errors)
                {
                    Console.WriteLine(item.ErrorMessage);
                }
                Environment.Exit(1);
            }
            return appSettings;
        }
    }
}
