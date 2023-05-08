using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Billing.Create.Module
{
    internal static class CreateBilling
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateBillingPage, ViewModel.CreateBilling>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<CreateBillingPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
