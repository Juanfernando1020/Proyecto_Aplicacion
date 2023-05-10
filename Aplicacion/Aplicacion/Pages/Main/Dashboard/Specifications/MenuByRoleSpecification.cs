using Aplicacion.Common.Specifications;
using Aplicacion.Enums;
using Aplicacion.Models;
using System;
using System.Linq.Expressions;

namespace Aplicacion.Pages.Main.Dashboard.Specifications
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
