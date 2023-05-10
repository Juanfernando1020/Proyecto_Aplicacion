using Aplicacion.Common.Specifications;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Aplicacion.Pages.Account.Login.Specifications
{
    internal class CredentialsForAdministratorSpecifications : SpecificationBase<Users>
    {
        private readonly Credentials _credentials;
        public CredentialsForAdministratorSpecifications(Credentials credentials) => _credentials = credentials;
        public override Expression<Func<Users, bool>> ToExpression() => 
            Users => Users.Phone.Equals(_credentials.Username) && Users.Password.Equals(_credentials.Password);
    }
}
