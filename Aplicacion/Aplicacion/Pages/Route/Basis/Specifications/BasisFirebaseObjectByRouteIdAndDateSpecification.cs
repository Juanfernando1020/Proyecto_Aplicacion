using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Firebase.Database;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Basis.Specifications
{
    internal class BasisFirebaseObjectByRouteIdAndDateSpecification : SpecificationBase<FirebaseObject<Basises>>
    {
        private readonly DateTime _date;
        private readonly Guid _routeId;

        public BasisFirebaseObjectByRouteIdAndDateSpecification(Guid routeId, DateTime date)
        {
            _routeId = routeId;
            _date = date;
        }

        public override Expression<Func<FirebaseObject<Basises>, bool>> ToExpression()
            => basis => basis.Object != null && (basis.Object.Route == _routeId && basis.Object.Date.Date == _date.Date);
    }
}
