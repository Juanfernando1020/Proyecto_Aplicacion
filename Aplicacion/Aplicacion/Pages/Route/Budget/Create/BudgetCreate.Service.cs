using Aplicacion.Pages.Route.Budget.Create.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Config.Messages;
using Aplicacion.Models;

namespace Aplicacion.Pages.Route.Budget.Create.Service
{
    internal class BudgetCreate : IBudgetCreateService
    {
        public bool Validate(Budgets budget, out string message)
        {
            if (budget.Id == Guid.Empty)
            {
                message = CommonMessages.Error.InformationMessage;
                return false;
            }

            if (budget.Amount <= 0)
            {
                message = "La cantidad debe ser mayor a 0";
                return false;
            }

            if (budget.Admin == null)
            {
                message = "Debes elegir un administrador.";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
