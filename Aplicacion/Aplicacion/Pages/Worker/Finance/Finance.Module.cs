using Aplicacion.Pages.Worker.Client.Billing.Module;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Worker.Finance.Module
{
    internal static class Finance
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
        Expense.Module.Expense.Initialize();
        }
    }
}
