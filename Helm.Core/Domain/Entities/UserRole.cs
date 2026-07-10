using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Entities
{
    public class UserRole
    {
        public int Id { get; init; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public List<User> Users { get; } = new List<User>();
        public UserRole()
        {
        }
        public UserRole(string Name, string? Description)
        {
            this.Name = Name;
            this.Description = Description;
        }

        public void Rename(string Name)
        {
            if (this.Name == Name) return;
            this.Name = Name;
        }
        public void ChangeDescription(string Description)
        {
            if (this.Description == Description) return;
            this.Description = Description;
        }
    }
}
