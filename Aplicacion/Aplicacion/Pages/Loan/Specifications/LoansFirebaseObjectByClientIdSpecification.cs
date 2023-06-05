using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Firebase.Database;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Specifications
{
    internal class LoansFirebaseObjectByClientIdSpecification : SpecificationBase<FirebaseObject<Loans>>
    {
        private readonly Guid _clientId;

        public LoansFirebaseObjectByClientIdSpecification(Guid clientId)
        {
            _clientId = clientId;
        }

        public override Expression<Func<FirebaseObject<Loans>, bool>> ToExpression()
            => loan => loan.Object.ClientId == _clientId;
    }
}
