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
        public override Expression<Func<Users, bool>> ToExpression()
            => user => user.Admin != null && user.Admin.Id == Aplicacion.Module.App.UserInfo.Id;
    }
}
