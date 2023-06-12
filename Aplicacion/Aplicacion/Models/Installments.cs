using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Installments
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime PaymenDate { get; set; }
        public decimal Amount { get; set; }
        public decimal DiferenceAmount { get; set; }
        public int Status { get; set; }
    }
}
