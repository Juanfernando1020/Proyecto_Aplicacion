using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Route.Basis.Create.Contracts
{
    public interface IBasisCreateService
    {
        bool Validate(Basises basis, out string message);
    }
}
