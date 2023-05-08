using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Worker.Client.Module
{
    internal static class Client
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.CreateNewClient.Initialize();
            List.Module.ListClients.Initialize();
            Detail.Module.DetailClient.Initialize();
            Billing.Module.Billing.Initialize();
            Loan.Module.Loan.Initialize();
            
        }
    }
}
