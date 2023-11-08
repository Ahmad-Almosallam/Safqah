using Safqah.Shared.BaseClases;
using Safqah.Shared.Enums;
using System;

namespace Safqah.Payment.Models
{
    public class PaymentTransactionModel : CreationAuditedEntity<Guid>
    {
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentCardType PaymentCardType { get; set; }
        public decimal Amount { get; set; }
    }
}
