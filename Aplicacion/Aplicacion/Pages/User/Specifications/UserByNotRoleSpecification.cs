using Aplicacion.Models;
using Aplicacion.Pages.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UserByNotRoleSpecification : SpecificationBase<Users>
    {
        private readonly RolesEnum _role;

        public  UserByNotRoleSpecification(RolesEnum role)
        {
            _role = role;
        }

        public override Expression<Func<Users, bool>> ToExpression()
            => user => user.Role != (int)_role;
    }
}
