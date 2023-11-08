using System;
using Safqah.Shared.BaseClases;
using Safqah.Shared.Enums;

namespace Safqah.Payment.Entites
{
    public class PaymentTransaction : CreationAuditedEntity<Guid>
    {
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentSource PaymentSource { get; set; }
        public PaymentCardType PaymentCardType { get; set; }
        public decimal Amount { get; set; }
    }   
}
