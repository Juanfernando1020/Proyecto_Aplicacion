using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Branches
    {
        public Branches(Guid id, DateTime auditCreationDate, DateTime auditUpdateDate, bool isActive)
        {
            Id = id;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; } 
    }
}
