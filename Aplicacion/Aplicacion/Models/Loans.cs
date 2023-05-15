using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Aplicacion.Models
{
    public class Loans
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime LoanDate { get; set; }
        public decimal Price { get; set; }
        public int AmountPayments { get; set; }
        public int InterestRate { get; set; }
        public DateTime StarDatePayment { get; set; }
        public int ProofPayment{ get; set; }
        public int delay { get; set; }
        public int DaysLate { get; set; }
        public decimal DelayValueroperty { get; set; }
        public string Observations { get; set; }
        public Clients Client { get; set; }
        public Guid fk_Budget { get; set; }
        public bool IsActive { get; set; }
        
    }
}
