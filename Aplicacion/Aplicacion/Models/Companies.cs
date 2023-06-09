﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Companies
    {
        public Companies(Guid id, string name, Users owner, bool isActive = true)
        {
            Id = id;
            Name = name;
            Owner = owner;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Users Owner { get; set; }
        public bool IsActive { get; set; }
    }
}
