using System;

namespace Aplicacion.Models
{
    public class Basises
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Cashflows[] CashFlows { get; set; }
        public Guid Route { get; set; }
        public bool IsActive { get; set; }
    }
}
