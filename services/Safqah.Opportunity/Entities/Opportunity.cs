using Safqah.Shared.BaseClases;
using System;

namespace Safqah.Opportunities.Entities
{
    public class Opportunity : CreationAuditedEntity<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal InvestedAmount { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
