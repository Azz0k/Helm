namespace Helm.Domain.Entities
{
    public class EquipmentTemplate
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string RenderTemplateKey { get; init; }
        public bool Enabled { get; private set; } = true;
        public bool Deleted { get; private set; } = false;
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public User CreatedBy { get; init; }
        public DateTimeOffset? LastModifiedAt { get; private set; } = null;
        public User? LastModifiedBy { get; private set; } = null;
        public DateTimeOffset? DeletedAt { get; private set; } = null;
        public User? DeletedBy { get; private set; } = null;
        public EquipmentTemplate(User user, string name, string renderTemplateKey, string? description) 
        {
            CreatedBy = user;
            Name = name;
            RenderTemplateKey = renderTemplateKey;
            Description = description ?? string.Empty;
        }
        public EquipmentTemplate(){

        }
        public void Update(User user, string? name, string? description, bool? enabled)
        {
            if (Deleted) return;
            Name = name ?? Name;
            Description = description ?? Description;
            Enabled = enabled ?? Enabled;
            SetModified(user);
        }
        public void Delete(User user)
        {
            if (Deleted) return;
            Deleted = true;
            DeletedBy = user;
            DeletedAt = DateTimeOffset.UtcNow;
        }
        private void SetModified(User user)
        {
            LastModifiedBy = user;
            LastModifiedAt = DateTimeOffset.UtcNow;
        }
        public void Enable(User user)
        {
            if (Deleted) return;
            if (Enabled) return;
            Enabled = true;
            SetModified(user);
        }
        public void Disable(User user)
        {
            if (Deleted) return;
            if (!Enabled) return;
            Enabled = false;
            SetModified(user);
        }
        public void Rename(User user, string name)
        {
            if (Deleted) return;
            if (name == Name) return;
            Name = name;
            SetModified(user);
        }
        public void SetDescription(User user, string description)
        {
            if (Deleted) return;
            if (description == Description) return;
            Description = description;
            SetModified(user);
        }
    }
}
