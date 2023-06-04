using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Installment.Specifications
{
    internal class InstallmentsByPaymentDateAndDateNowSpecification : SpecificationBase<InstallmentExtension>
    {
        public override Expression<Func<InstallmentExtension, bool>> ToExpression()
            => Installment => Installment.Installment.PaymenDate.Date == DateTime.Now.Date;
    }
}
