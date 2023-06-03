using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Client.Specifications
{
    internal class ClientsByInstallmentDateNowSpecification : SpecificationBase<Clients>
    {
        public override Expression<Func<Clients, bool>> ToExpression()
            => client => client.IsActive;
    }
}
