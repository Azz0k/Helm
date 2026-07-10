using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Infrastructure.Configuration
{
    public class ADFSSettings
    {
        public required string ADFSDomain { get; init; }
        public required string ADFSAudience { get; init; }
        public required string ADFSIssuer { get; init; }
    }
}
