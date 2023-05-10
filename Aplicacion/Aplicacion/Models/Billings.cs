using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Billings
    {
        public Billings(Guid id, Guid fk_Installments, DateTime paymentDate, decimal amount, decimal advance, decimal advanceDelay, int status, DateTime auditCreationDate, DateTime auditUpdateDate)
        {
            Id = id;
            this.fk_Installments = fk_Installments;
            PaymentDate = paymentDate;
            Amount = amount;
            Advance = advance;
            AdvanceDelay = advanceDelay;
            Status = status;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
        }

        public Guid Id { get; set; }
        public Guid fk_Installments { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Advance { get; set; }
        public decimal AdvanceDelay { get; set; }
        public int Status { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }
    }
}
