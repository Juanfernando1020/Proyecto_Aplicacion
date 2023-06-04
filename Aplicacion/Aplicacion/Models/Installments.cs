using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Installments
    {
        public Guid Id { get; set; }
        public DateTime PaymenDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
