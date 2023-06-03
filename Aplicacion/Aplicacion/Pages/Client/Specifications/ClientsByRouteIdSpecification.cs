using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Client.Specifications
{
    internal class ClientsByRouteIdSpecification : SpecificationBase<Clients>
    {
        private readonly Guid _routeId;

        public ClientsByRouteIdSpecification(Guid routeId)
        {
            _routeId = routeId;
        }

        public override Expression<Func<Clients, bool>> ToExpression()
            => client => client.IsActive && client.Route == _routeId;
    }
}
