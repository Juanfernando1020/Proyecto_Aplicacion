using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;

namespace Aplicacion.Pages.Client.Models
{
    public class ClientExtended
    {
        public Clients Client { get; set; }
        public decimal LoansQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PayedAmount { get; set; }
        public int PayedPercentage { get; set; }
        public decimal RestAmount { get; set; }
        public int RestPercentage { get; set; }
    }
}
