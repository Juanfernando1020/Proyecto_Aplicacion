using Aplicacion.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    internal class AllUsersSpecification : SpecificationBase<Users>
    {
        public AllUsersSpecification() 
        {
           
        }

        public override Expression<Func<Users, bool>> ToExpression()
            => user => true;
    }
}
