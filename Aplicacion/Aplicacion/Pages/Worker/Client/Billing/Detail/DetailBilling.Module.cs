using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Billing.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Billing.Detail.Module
{
    internal static class DetailBilling
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<DetailBillingPage, ViewModel.DetailBilling>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<DetailBillingPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
