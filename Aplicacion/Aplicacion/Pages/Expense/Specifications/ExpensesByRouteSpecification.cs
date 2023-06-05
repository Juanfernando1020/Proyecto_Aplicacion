using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Expense.Specifications
{
    internal class ExpensesByRouteSpecification : SpecificationBase<Expenses>
    {
        private readonly Guid _routeId;

        public ExpensesByRouteSpecification(Guid routeId)
        {
            _routeId = routeId;
        }

        public override Expression<Func<Expenses, bool>> ToExpression()
            => expense => expense.RouteId == _routeId;
    }
}
