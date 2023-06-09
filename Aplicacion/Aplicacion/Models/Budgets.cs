﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Budgets
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Users User { get; set; }
    }
}
