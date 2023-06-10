using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Basis.Specifications
{
    internal class BasisByRouteIdAndDateSpecification : SpecificationBase<Basises>
    {
        private readonly DateTime _date;
        private readonly Guid _routeId;

        public BasisByRouteIdAndDateSpecification(Guid routeId, DateTime date)
        {
            _routeId = routeId;
            _date = date;
        }

        public override Expression<Func<Basises, bool>> ToExpression()
        => basis => basis.Route == _routeId && basis.Date.Date == _date.Date;
    }
}
