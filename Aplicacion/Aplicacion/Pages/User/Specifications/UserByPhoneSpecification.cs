using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UserByPhoneSpecification : SpecificationBase<Users>
    {
        private readonly string _phone;

        public UserByPhoneSpecification(string phone)
        {
            _phone = phone;
        }

        public override Expression<Func<Users, bool>> ToExpression()
          => user => user.Phone.Equals((string)_phone);
    }
}
