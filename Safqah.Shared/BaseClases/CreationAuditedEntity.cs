using System;

namespace Safqah.Shared.BaseClases
{
    public class CreationAuditedEntity<TKey> : BaseEntity<TKey>
    {
        public DateTime CreatedAt { set; get; }
        public string CreatorId { set; get; }

        public void AddCreator(string id)
        {
            CreatorId = id;
            CreatedAt = DateTime.Now;
        }
        
    }
}