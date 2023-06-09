using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Specifications
{
    internal class LoansByClientIdAndDateSpecification : SpecificationBase<Loans>
    {
        private readonly Guid _clientId;
        private readonly DateTime _dateTime;

        public LoansByClientIdAndDateSpecification(Guid clientId, DateTime dateTime)
        {
            _clientId = clientId;
            _dateTime = dateTime;
        }

        public override Expression<Func<Loans, bool>> ToExpression()
            => loan => loan.ClientId == _clientId && loan.Date.Date == _dateTime.Date;
    }
}
