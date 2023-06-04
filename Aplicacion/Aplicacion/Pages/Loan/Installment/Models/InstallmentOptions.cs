using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Pages.Loan.Installment.Enums;

namespace Aplicacion.Pages.Loan.Installment.Models
{
    internal class InstallmentOptions
    {
        public string Title { get; set; }
        public InstallmentTypeEnum InstallmentType { get; set; }
        public int Days { get; set; }
    }
}
