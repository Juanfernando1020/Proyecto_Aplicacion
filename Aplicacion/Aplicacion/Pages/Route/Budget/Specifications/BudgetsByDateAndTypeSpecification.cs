using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Budget.Enums;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Budget.Specifications
{
    internal class BudgetsByDateAndTypeSpecification : SpecificationBase<Budgets>
    {
        private readonly DateTime _date;
        private readonly BudgetTypes _type;

        public BudgetsByDateAndTypeSpecification(DateTime date, BudgetTypes type)
        {
            _date = date;
            _type = type;
        }

        public override Expression<Func<Budgets, bool>> ToExpression()
            => budget => budget.Date.Date == _date.Date && budget.Type == (int)_type;
    }
}
