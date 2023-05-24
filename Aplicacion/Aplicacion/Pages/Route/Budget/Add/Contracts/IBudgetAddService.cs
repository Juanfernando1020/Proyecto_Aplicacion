using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;

namespace Aplicacion.Pages.Route.Budget.Add.Contracts
{
    internal interface IBudgetAddService
    {
        void Validate(Budgets budget);
    }
}
