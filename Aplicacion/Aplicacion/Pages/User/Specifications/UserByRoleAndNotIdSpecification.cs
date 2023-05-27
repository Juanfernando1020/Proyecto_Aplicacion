using Xamarin.CommonToolkit.Specifications;
using Aplicacion.Models;
using System;
using System.Linq.Expressions;
using Aplicacion.Pages.User.Enums;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UserByRoleAndNotIdSpecification : SpecificationBase<Users>
    {
        private readonly Guid _userId;
        private readonly RolesEnum _role;

        public UserByRoleAndNotIdSpecification(Guid userId, RolesEnum role)
        {
            _userId = userId;
            _role = role;
        }

        public override Expression<Func<Users, bool>> ToExpression()
            => user => user.Id != _userId && user.Role == (int)_role;
    }
}
