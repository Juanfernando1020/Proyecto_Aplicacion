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
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int InterestRate { get; set; }
        public int InstallmentType { get; set; }
        public int InstallmentsQuantity { get; set; }
        public Installments[] Installments { get; set; }
        public decimal Surcharge { get; set; }
        public int SurchargeDays { get; set; }
        public string Observations { get; set; }
        public bool IsActive { get; set; }
        
    }
}
