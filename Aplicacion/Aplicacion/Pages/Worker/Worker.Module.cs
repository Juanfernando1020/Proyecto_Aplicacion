using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Worker.Module
{
    internal static class Worker
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            ViewExpense.Module.ViewExpense.Initialize();
            AddNewExpense.Module.AddNewExpense.Initialize();
            AddNewClient.Module.AddNewClient.Initialize();
            ViewAllClients.Module.ViewAllClients.Initialize();
            AddLoanClient.Module.AddLoanClient.Initialize();
            AddClientBilling.Module.AddClientBilling.Initialize();
        }
    }
}
