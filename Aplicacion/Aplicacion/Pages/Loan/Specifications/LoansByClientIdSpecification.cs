using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Specifications
{
    internal class LoansByClientIdSpecification : SpecificationBase<Loans>
    {
        private readonly Guid _clientId;

        public LoansByClientIdSpecification(Guid clientId)
        {
            _clientId = clientId;
        }

        public override Expression<Func<Loans, bool>> ToExpression()
            => loan => loan.ClientId == _clientId;
    }
}
