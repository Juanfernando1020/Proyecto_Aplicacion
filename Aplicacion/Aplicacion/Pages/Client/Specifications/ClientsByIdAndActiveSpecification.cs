using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Firebase.Database;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Client.Specifications
{
    internal class ClientsByIdAndActiveSpecification : SpecificationBase<FirebaseObject<Clients>>
    {
        private readonly Guid _clientId;
        private readonly bool _requiredActiveState;

        public ClientsByIdAndActiveSpecification(Guid clientId, bool requiredActiveState)
        {
            _clientId = clientId;
            _requiredActiveState = requiredActiveState;
        }

        public override Expression<Func<FirebaseObject<Clients>, bool>> ToExpression()
            => firebaseObject => firebaseObject.Object.IsActive == _requiredActiveState && firebaseObject.Object.Id == _clientId;
    }
}
