using Aplicacion.Models;
using Aplicacion.Pages.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    class UserByDateAndRoleSpecification : SpecificationBase<Users>
    {
        private readonly DateTime _date;
        private readonly RolesEnum _role;

        public UserByDateAndRoleSpecification(DateTime date, RolesEnum role)
        {
            _date = date;
            _role = role;
        }

        public override Expression<Func<Users, bool>> ToExpression()
           => user => user.NextPaymentDate.Date < DateTime.Now.Date && user.Role == (int)_role;

    }
}
