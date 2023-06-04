using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Fees
    {
        public Guid Id { get; set; }
        public Guid InstallmentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
