using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Infrastructure.Configuration
{
    public class ADFSSettings
    {
        public string? ADFSDomain { get; set; }
        public string? ADFSAudience { get; set; }
        public string? ADFSIssuer { get; set; }
    }
}
