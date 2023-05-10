using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Clients
    {
        public Clients(Guid id, string name, string phone, string address, Guid fk_Route, DateTime auditCreationDate, DateTime auditUpdateDate, bool isActive)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
            this.fk_Route = fk_Route;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Guid fk_Route { get; set; }
        public bool IsActive { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }

    }
}
