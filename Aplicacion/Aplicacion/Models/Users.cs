using System;
using System.Collections.Generic;

namespace Aplicacion.Models
{
    public class Users
    {
        public Users(Guid id, string name, string phone, string password, int role, Branches branch = null, bool isActive = true, Users admin = null)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Password = password;
            Role = role;
            Branch = branch;
            IsActive = isActive;
            Admin = admin;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public Branches Branch { get; set; }
        public bool IsActive { get; set; }

        public Users Admin{ get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Users))
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
            int hashCode = -426589422;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Branches>.Default.GetHashCode(Branch);
            hashCode = hashCode * -1521134295 + IsActive.GetHashCode();
            return hashCode;
        }
    }
}
