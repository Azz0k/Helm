using Helm.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public string? Hash { get; set; }
        public required string Name { get; set; }
        public string? ADLogin { get; set; }
        public int Version { get; set; }
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
        public UserRole Role { get; set; }
    }
}
