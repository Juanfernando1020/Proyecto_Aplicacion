using Aplicacion.Pages.Worker.Client.Billing.Module;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Worker.Finance.Expense.Module
{
    internal static class Expense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Create.Module.CreateExpense.Initialize();
            List.Module.ListExpense.Initialize();
        }
    }
}
