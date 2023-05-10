using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Routes
    {
        public Routes(Guid id, string name, int neighborhood, Guid fk_User, DateTime auditCreationDate, DateTime auditUpdateDate, bool isActive)
        {
            Id = id;
            Name = name;
            Neighborhood = neighborhood;
            this.fk_User = fk_User;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Neighborhood { get; set; }
        public Guid fk_User { get; set; }
        public bool IsActive { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }

    }
}
