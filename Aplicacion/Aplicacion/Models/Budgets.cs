using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Budgets
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid Admin { get; set; }
    }
}
