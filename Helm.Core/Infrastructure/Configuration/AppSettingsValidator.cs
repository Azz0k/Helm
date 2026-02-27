using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Helm.Core.Infrastructure.Configuration
{

    public class AppSettingsValidator
    {
        private AppSettings? appSettings;
        public bool valid = false;
        public string error = "";
        public AppSettingsValidator(AppSettings? appSettings)
        {
            this.appSettings = appSettings; 
        }
        public AppSettingsValidator Validate()
        {
            valid = true;
            if (appSettings == null)
            {
                valid = false;
                error = "Нет appsettings.json или неверный формат файла";
                return this;
            }
            if (appSettings.AllowedOrigins == null)
            {
                valid = false;
                error = "Не заполнен AllowedOrigins";

            }
            if (appSettings.ConnectionString == null)
            {
                error = "Не заполнен ConnectionString";
                valid = false;
            }
            if (appSettings.JWTSecretCode == null)
            {
                error ="Не заполнен JWTSecretCode";
                valid = false;
            }
            return this;
        }
    }
}
