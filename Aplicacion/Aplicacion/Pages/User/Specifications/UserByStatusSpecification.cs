using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    class UserByStatusSpecification : SpecificationBase<Users>
    {
        private readonly bool _status;

        public UserByStatusSpecification(bool status)
        {
            _status = status;
        }

        public override Expression<Func<Users, bool>> ToExpression()
        => user => user.IsActive.Equals((bool)_status);
    }
}
