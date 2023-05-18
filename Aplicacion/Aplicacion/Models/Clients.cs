﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Clients
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int AmountOfLoans { get; set; }
        public Routes Routes { get; set; }
        public bool IsActive { get; set; }

    }
}
