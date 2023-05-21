using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Specifications
{
    internal class RoutesByUserIdSpecification : SpecificationBase<Routes>
    {
        private readonly Guid _userId;

        public RoutesByUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Routes, bool>> ToExpression() =>
            route => route.Worker.Id.Equals(_userId) || route.Manager.Id.Equals(_userId) || route.Admins.Any(admin => admin.Id.Equals(_userId));
    }
}
