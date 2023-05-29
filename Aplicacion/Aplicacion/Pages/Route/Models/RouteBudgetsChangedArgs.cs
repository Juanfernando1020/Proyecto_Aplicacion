using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;

namespace Aplicacion.Pages.Route.Models
{
    internal class RouteBudgetsChangedArgs
    {
        public bool IsDeleted { get; set; }
        public Budgets Budget { get; set; }

        public RouteBudgetsChangedArgs(bool isDeleted, Budgets budget)
        {
            IsDeleted = isDeleted;
            Budget = budget;
        }
    }
}
