using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Common;

namespace Aplicacion.Pages.Loan.Installment.Arranging
{
    internal class InstallmentByDateArranging : ArrangingBase<Installments>
    {
        protected override Expression<Func<Installments, object>> ToExpression()
            => Installment => Installment.PaymenDate;
    }
}
