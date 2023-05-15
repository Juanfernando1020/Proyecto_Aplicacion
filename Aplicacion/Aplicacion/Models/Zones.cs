using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Zones
    {
        public Zones(Guid id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
