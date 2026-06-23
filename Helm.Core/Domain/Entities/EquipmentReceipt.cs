using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Entities
{
    public class EquipmentReceipt
    {
        public int Id { get; set; }
        public EquipmentTemplate Template { get; init; }
        public User CreatedBy { get; init; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public string IssuedBy { get; private set; }
        public bool Returned {  get; private set; } = false;
        public DateTimeOffset? ReturnedAt { get; private set; } = null;
        public User? AcceptedBy { get; private set; } = null;
        public DateTimeOffset? LastModifiedAt { get; private set; } = null;
        public User? LastModifiedBy { get; private set; } = null;
        public IReadOnlyList<Equipment> Equipment { get; private set; }
        public EquipmentReceipt(User user, EquipmentTemplate template, string issuedBy, List<Equipment> equipment)
        {
            CreatedBy = user;
            Template = template;
            IssuedBy = issuedBy;
            Equipment = equipment;
            foreach (var item in Equipment)
            {
                item.Issue(user, issuedBy);
            }
        }
        public EquipmentReceipt()
        {

        }
        public void ReIssue(User user,  string issuedBy)
        {
            if (IssuedBy == issuedBy) return;
            IssuedBy = issuedBy;
            Returned = false;
            AcceptedBy = null;
            ReturnedAt = null;
            LastModifiedAt = DateTimeOffset.UtcNow; ;
            LastModifiedBy = user;
            foreach (var item in Equipment)
            {
                item.Issue(user, issuedBy);
            }
        }
        public void Return(User user, List<int> lostItems)
        {
            Returned = true;
            AcceptedBy = user;
            ReturnedAt = DateTimeOffset.UtcNow;
            foreach (var item in Equipment)
            {
                item.Return(user);
            }
            foreach (var itemId in lostItems)
            {
                Equipment.FirstOrDefault(i => i.Id == itemId)?.Lost(user);
            }
        }

    }
}
