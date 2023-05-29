using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Basis.Specifications
{
    internal class BasisByRouteIdSpecification : SpecificationBase<Basises>
    {
        private readonly Guid _routeId;

        public BasisByRouteIdSpecification(Guid routeId)
        {
            _routeId = routeId;
        }

        public override Expression<Func<Basises, bool>> ToExpression()
        => basis => basis.Route == _routeId;
    }
}
