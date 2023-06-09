using Aplicacion.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Specifications
{
    internal class UsersFirebaseObjectByUserIdSpecification : SpecificationBase<FirebaseObject<Users>>
    {
        private readonly Guid _userId;

        public UsersFirebaseObjectByUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<FirebaseObject<Users>, bool>> ToExpression()=>
            user => user.Object.Id == _userId;
    }
}
