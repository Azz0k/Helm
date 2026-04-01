using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Infrastructure.Configuration
{
    public class AppSettings
    {
        public string? AllowedOrigins { get; set; }
        public string? ConnectionString { get; set; }
        public string? JWTSecretCode { get; set; }
        public string? MediatRLicense {  get; set; }
        public ADFSSettings? ADFS {  get; set; }
    }
}
