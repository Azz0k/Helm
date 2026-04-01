using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
