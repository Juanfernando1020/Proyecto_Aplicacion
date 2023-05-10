using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Users
    {
        public Users(Guid id, string name, string phone, string password, int role, Guid fk_Branch, DateTime auditCreationDate, DateTime auditUpdateDate)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Password = password;
            Role = role;
            this.fk_Branch = fk_Branch;
            AuditCreationDate = auditCreationDate;
            AuditUpdateDate = auditUpdateDate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; } 
        public string Password { get; set; }
        public int Role { get; set; }
        public Guid fk_Branch { get; set; }
        public DateTime AuditCreationDate { get; set; }
        public DateTime AuditUpdateDate { get; set; }
    }
}
