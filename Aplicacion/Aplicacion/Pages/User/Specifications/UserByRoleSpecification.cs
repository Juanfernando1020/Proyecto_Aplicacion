using Xamarin.CommonToolkit.Specifications;
using Aplicacion.Models;
using System;
using System.Linq.Expressions;
using Aplicacion.Pages.User.Enums;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UserByRoleSpecification : SpecificationBase<Users>
    {
        private readonly RolesEnum _role;

        public UserByRoleSpecification(RolesEnum role)
        {
            _role = role;
        }

        public override Expression<Func<Users, bool>> ToExpression()
            => user => user.Role.Equals((int)_role);
    }
}
