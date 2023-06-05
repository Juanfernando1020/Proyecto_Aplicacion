
using System;

namespace Aplicacion.Models
{
    internal class Expenses
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid RouteId { get; set; }
    }
}
