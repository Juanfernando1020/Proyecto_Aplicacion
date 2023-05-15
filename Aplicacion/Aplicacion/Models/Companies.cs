using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Companies
    {
        public Companies(Guid id, string name, Guid fk_User, DateTime auditCreationDate, DateTime auditUpdateDate, bool isActive = true)
        {
            Id = id;
            Name = name;
            this.fk_User = fk_User;
            IsActive = isActive;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid fk_User { get; set; }
        public bool IsActive { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }
    }
}
