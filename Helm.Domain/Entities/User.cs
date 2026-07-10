using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Domain.Entities
{
    public class User
    {
        public int Id { get; init; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public bool Enabled { get; private set; }
        public bool Deleted {  get; private set; }
        public List<UserRole> Roles { get; private set; }
        public User()
        {
        }
        public User(string login, string name)
        {
            Login = login;
            Name = name;
            Enabled = true;
            Deleted = false;
            Roles = [];
        }
        public bool HasRole(int roleId)
        {
            return Roles.Any(x => x.Id == roleId);
        }
        public bool HasRole(UserRole role)
        {
            return HasRole(role.Id);
        }
        public void Rename (string newName)
        {
            if (Deleted) return;
            if (Name == newName) return;
            Name = newName;
        }
        public void ChangeLogin(string newLogin)
        {
            if (Deleted) return;
            if (Login == newLogin) return;
            Login = newLogin;
        }
        public void SetStatus(bool enabled)
        {
            if (Deleted) return;
            Enabled = enabled;
        }
        public void Delete()
        {
            Deleted = true;
        }
        public void AddRole(UserRole role)
        {
            if (Deleted) return;
            if (!HasRole(role))
            {
                Roles.Add(role);
            }
        }
        public void RemoveRole(UserRole role)
        {
            if (Deleted) return;
            if (HasRole(role))
            {
                Roles.Remove(role);
            }
        }
        public void ClearRoles()
        {
            if (Deleted) return;
            Roles = [];
        }
    }
}
