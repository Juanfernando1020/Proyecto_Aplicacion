
using System;

namespace Aplicacion.Models
{
    internal class Expenses
    {
        public Expenses(Guid id, string name, decimal cost, string description, DateTime auditCreationDate, DateTime auditUpdateDate    )
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }
    }
}
