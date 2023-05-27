using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;

namespace Aplicacion.Pages.Route.Budget.Create.Contracts
{
    internal interface IBudgetCreateService
    {
        bool Validate(Budgets budget, out string message);
    }
}
