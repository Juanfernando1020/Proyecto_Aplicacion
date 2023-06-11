using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;
using static Xamarin.Essentials.Permissions;

namespace Aplicacion.Pages.User.Specifications
{
    class UserByDateSpecification : SpecificationBase<Users>
    {
        private readonly DateTime _date;
        public UserByDateSpecification(DateTime date)
        {
            _date = date;
        }
        public override Expression<Func<Users, bool>> ToExpression()
         => user => user.NextPaymentDate.Date.Equals((DateTime)_date);
    }
}
