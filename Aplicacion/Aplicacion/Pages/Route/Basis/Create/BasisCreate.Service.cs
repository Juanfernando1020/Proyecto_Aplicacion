using Aplicacion.Pages.Route.Basis.Create.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Config;

namespace Aplicacion.Pages.Route.Basis.Create.Service
{
    internal class BasisCreate : IBasisCreateService
    {
        public bool Validate(Basises basis, out string message)
        {
            if (basis == null)
            {
                message = CommonMessages.Error.InformationMessage;
                return false;
            }

            if (basis.Amount <= RoutesConfig.MIN_AMOUNT)
            {
                message = "La cantidad debe ser mayor a 0";
                return false;
            }

            message = string.Empty;

            return true;
        }
    }
}
