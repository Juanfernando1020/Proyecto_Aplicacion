using Aplicacion.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Client.Specifications
{
    internal class ClientFirebaseObjectByClientIdSpecification : SpecificationBase<FirebaseObject<Clients>>
    {
        private readonly Guid _clientId;

        public ClientFirebaseObjectByClientIdSpecification(Guid clientId)
        {
            _clientId= clientId;
        }
        public override Expression<Func<FirebaseObject<Clients>, bool>> ToExpression() =>
           client => client.Object.Id == _clientId;
    }
}
