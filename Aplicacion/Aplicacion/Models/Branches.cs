using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Branches
    {
        public Branches(Guid id, string name, Companies company,  bool isActive = true)
        {
            Id = id;
            Name = name;
            Company = company;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Companies Company { get; set; }
        public bool IsActive { get; set; }
    }
}
