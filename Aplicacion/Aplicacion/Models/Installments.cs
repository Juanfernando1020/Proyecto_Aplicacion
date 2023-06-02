using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Installments
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public Guid Loan { get; set; }
        public Fees[] Fees { get; set; }
        public bool IsActive { get; set; }
    }
}
