using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Branches
    {
        public Branches(Guid id, string name, Guid fk_Company, DateTime auditCreationDate, DateTime auditUpdateDate, bool isActive = true)
        {
            Id = id;
            Name = name;
            this.fk_Company = fk_Company;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid fk_Company { get; set; }
        public bool IsActive { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; } 
    }
}
