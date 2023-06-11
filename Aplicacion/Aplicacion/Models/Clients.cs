using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Clients
    {
        public Clients(Guid id = default, string iDImage = null, string name = null, string phone = null, string address = null, Guid route = default, bool isActive = true)
        {
            Id = id;
            IDImage = iDImage;
            Name = name;
            Phone = phone;
            Address = address;
            Route = route;
            IsActive = isActive;
        }
        public Guid Id { get; set; }
        public string IDImage { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Guid Route { get; set; }
        public bool IsActive { get; set; }

    }
}
