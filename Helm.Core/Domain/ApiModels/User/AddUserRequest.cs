using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.ApiModels.User
{
    public class AddUserRequest
    {
        public required string Login { get; set; }
        public string? Password { get; set; }
        public required string Name { get; set; }
        public string? ADLogin { get; set; }
        public bool Enabled { get; set; }
        public required List<string> Role { get; set; }
    }
}
