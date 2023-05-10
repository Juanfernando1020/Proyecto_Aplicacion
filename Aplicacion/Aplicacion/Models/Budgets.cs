using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    internal class Budgets
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public Guid fk_Route { get; set; }
    }
}
