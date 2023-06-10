using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Expense.Specifications
{
    internal class ExpensesByRouteAndDateSpecification : SpecificationBase<Expenses>
    {
        private readonly Guid _routeId;
        private readonly DateTime _date;
        public ExpensesByRouteAndDateSpecification(Guid routeId, DateTime date)
        {
            _routeId = routeId;
            _date = date;
            
        }

        public override Expression<Func<Expenses, bool>> ToExpression()
            => expense => expense.RouteId == _routeId && expense.Date.Date == _date.Date;
    }
}
