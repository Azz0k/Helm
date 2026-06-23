using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public bool IsIssued { get; private set; } = false;
        public string IssuedBy { get; private set; } = string.Empty;
        public bool IsLost { get; private set; } = false;
        public bool IsBulk { get; init; } = false;
        public User CreatedBy { get; init; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastModifiedAt { get; private set; } = null;
        public User? LastModifiedBy { get; private set; } = null;
        public Equipment(User CreatedBy, string Name, bool IsBulk = false)
        {
            this.CreatedBy = CreatedBy;
            this.Name = Name;
            this.IsBulk = IsBulk;
        }
        public Equipment()
        {

        }
        private void SetModified(User user)
        {
            LastModifiedBy = user;
            LastModifiedAt = DateTimeOffset.UtcNow;
        }
        public void Rename (User user, string newName)
        {
            if (IsLost) return;
            Name = newName;
            SetModified(user);  
        }
        public void Issue (User user, string issuedBy)
        {
            if (IsLost) return;
            if (IsBulk) return;
            IsIssued = true;
            IssuedBy = issuedBy;
            SetModified(user);
        }
        public void Return(User user)
        {
            if (IsLost) return;
            if (IsBulk) return;
            if (!IsIssued) return;
            IsIssued = false; 
            SetModified(user);
        }
        public void Lost(User user)
        {
            if (IsLost) return;
            if (IsBulk) return;
            IsIssued = false;
            IsLost = true;
            SetModified(user);
        }

    }
}
