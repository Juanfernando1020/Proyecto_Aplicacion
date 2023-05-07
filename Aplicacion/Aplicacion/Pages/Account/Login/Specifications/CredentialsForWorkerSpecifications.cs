using Aplicacion.Common.Specifications;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Aplicacion.Pages.Account.Login.Specifications
{
    internal class CredentialsForWorkerSpecifications : SpecificationBase<Trabajador>
    {
        private readonly Credentials _credentials;
        public CredentialsForWorkerSpecifications(Credentials credentials) => _credentials = credentials;
        public override Expression<Func<Trabajador, bool>> ToExpression() => 
            worker => worker.Phone.Equals(_credentials.Username) && worker.Password.Equals(_credentials.Password);
    }
}
