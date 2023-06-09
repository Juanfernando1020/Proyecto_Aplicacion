using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UserByAdminIdSpecification : SpecificationBase<Users>
    {
        private readonly Guid _adminId;
        public UserByAdminIdSpecification(Guid adminId)
        {
            _adminId = adminId;
        }

        public override Expression<Func<Users, bool>> ToExpression()
            => user => user.Id.Equals((Guid) _adminId);
    }
}
