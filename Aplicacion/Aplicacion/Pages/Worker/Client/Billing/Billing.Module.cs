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
            Create.Module.CreateBilling.Initialize();
            Detail.Module.DetailBilling.Initialize();
            List.Module.ListBilling.Initialize();
        }

    }
}
