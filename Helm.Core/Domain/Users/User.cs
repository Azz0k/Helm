using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string ADLogin { get; set; }
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
        public UserRole Role { get; set; }
    }
}
