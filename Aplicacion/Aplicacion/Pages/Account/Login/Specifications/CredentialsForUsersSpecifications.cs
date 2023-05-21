using Xamarin.CommonToolkit.Specifications;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Aplicacion.Pages.Account.Login.Specifications
{
    internal class CredentialsForUsersSpecifications : SpecificationBase<Users>
    {
        private readonly Credentials _credentials;
        public CredentialsForUsersSpecifications(Credentials credentials) => _credentials = credentials;
        public override Expression<Func<Users, bool>> ToExpression() =>
            user => user.Phone.Equals(_credentials.Username) && user.Password.Equals(_credentials.Password);
    }
}
