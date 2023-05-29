using System;

namespace Aplicacion.Models
{
    public class Basises
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Guid Route { get; set; }
    }
}
