using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Installment.Models
{
    internal class InstallmentListFilter
    {
        public string Title { get; set; }
        public SpecificationBase<InstallmentExtension> Specification { get; set; }
    }
}
