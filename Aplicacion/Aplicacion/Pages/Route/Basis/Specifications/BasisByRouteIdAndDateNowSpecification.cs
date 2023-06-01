using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Basis.Specifications
{
    internal class BasisByRouteIdAndDateNowSpecification : SpecificationBase<Basises>
    {
        private readonly DateTime _now;
        private readonly Guid _routeId;

        public BasisByRouteIdAndDateNowSpecification(Guid routeId)
        {
            _routeId = routeId;
            _now = DateTime.Now;
        }

        public override Expression<Func<Basises, bool>> ToExpression()
        => basis => basis.Route == _routeId && (basis.Date.Year == _now.Year && basis.Date.Month == _now.Month && basis.Date.Day == _now.Day);
    }
}
