﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Cashflows
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
    }
}