using Xamarin.CommonToolkit.Specifications;
using Aplicacion.Models;
using System;
using System.Linq.Expressions;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UserByIdSpecification : SpecificationBase<Users>
    {
        private readonly Guid _userId;

        public UserByIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Users, bool>> ToExpression()
            => user => user.Id.Equals(_userId);
    }
}
