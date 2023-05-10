using Aplicacion.Pages.Worker.Client.Billing.Module;
using Aplicacion.Pages.Worker.Client.Loan.Module;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Owner.Admin.Module
{
    internal static class Admin
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Create.Module.CreateNewAdmin.Initialize();
        }
    }
}
