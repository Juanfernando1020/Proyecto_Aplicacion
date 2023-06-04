using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Firebase.Database;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Specifications
{
    internal class RoutesByIdAndActiveStateSpecification : SpecificationBase<FirebaseObject<Routes>>
    {
        private readonly Guid _routeId;
        private readonly bool _requiredActiveState;

        public RoutesByIdAndActiveStateSpecification(Guid routeId, bool requiredActiveState)
        {
            _routeId = routeId;
            _requiredActiveState = requiredActiveState;
        }

        public override Expression<Func<FirebaseObject<Routes>, bool>> ToExpression()
            => firebaseObject => firebaseObject.Object.IsActive == _requiredActiveState && firebaseObject.Object.Id == _routeId;
    }
}
