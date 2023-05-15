using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Routes
    {
        public Routes(Guid id, string name, Zones zone, bool isActive, Users worker, Users manager)
        {
            Id = id;
            Name = name;
            Zone = zone;
            IsActive = isActive;
            Worker = worker;
            Manager = manager;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Zones Zone { get; set; }
        public bool IsActive { get; set; }
        public Users Worker { get; set; }
        public Users Manager { get; set; }

    }
}
