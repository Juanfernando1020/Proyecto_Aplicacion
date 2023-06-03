using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Specifications
{
    internal class LoansByDateSpecifications : SpecificationBase<Loans>
    {
        private readonly DateTime _date;

        public LoansByDateSpecifications(DateTime date)
        {
            _date = date;
        }

        public override Expression<Func<Loans, bool>> ToExpression()
            => loan => loan.IsActive && loan.Date.Day == _date.Day;
    }
}
