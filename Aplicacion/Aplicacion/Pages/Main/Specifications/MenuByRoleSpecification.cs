using Aplicacion.Common.Specifications;
using Aplicacion.Models;
using Aplicacion.Pages.User.Roles.Enums;
using System;
using System.Linq.Expressions;

namespace Aplicacion.Pages.Main.Specifications
{
    internal class MenuByRoleSpecification : SpecificationBase<Menu>
    {
        private RolesEnum _role;

        public MenuByRoleSpecification(RolesEnum role)
        {
            _role = role;
        }

        public override Expression<Func<Menu, bool>> ToExpression() =>
            menu => menu.Role.Equals((int)_role);
    }
}
