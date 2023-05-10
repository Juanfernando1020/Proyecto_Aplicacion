using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Users
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

        public override bool Equals(object obj)
        {
            if(!(obj is Users))
                return false;

            return Equals((Users)obj);
        }
        public bool Equals(Users obj)
        {
            return (obj.Name == Name) &&
                (obj.Phone == Phone) &&
                (obj.Password == Password) &&
                (obj.Role == Role);
        }

        public override int GetHashCode()
        {
            int hashCode = -156012685;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            hashCode = hashCode * -1521134295 + fk_Branch.GetHashCode();
            hashCode = hashCode * -1521134295 + AuditCreationDate.GetHashCode();
            hashCode = hashCode * -1521134295 + AuditUpdateDate.GetHashCode();
            return hashCode;
        }
    }
}
