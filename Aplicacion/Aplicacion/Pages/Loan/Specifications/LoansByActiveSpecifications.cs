using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Specifications
{
    internal class LoansByActiveSpecifications : SpecificationBase<Loans>
    {
        private readonly bool _activeRequired;

        public LoansByActiveSpecifications(bool activeRequired = true)
        {
            _activeRequired = activeRequired;
        }

        public override Expression<Func<Loans, bool>> ToExpression()
            => loan => loan.IsActive == _activeRequired;
    }
}
