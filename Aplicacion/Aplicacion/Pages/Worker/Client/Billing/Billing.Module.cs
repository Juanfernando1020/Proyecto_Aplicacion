using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Worker.Client.Billing.Module
{
    internal static class Billing
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            BillingProduct.Module.BillingProduct.Initialize();
            List.Module.ListBillings.Initialize();
        }

    }
}
