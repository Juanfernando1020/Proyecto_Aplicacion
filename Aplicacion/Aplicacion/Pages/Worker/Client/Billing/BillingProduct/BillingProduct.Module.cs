using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Worker.Client.Billing.BillingProduct.Module
{
    internal static class BillingProduct
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Create.Module.CreateBilling.Initialize();
            Detail.Module.DetailBilling.Initialize();
        }
    }
}
