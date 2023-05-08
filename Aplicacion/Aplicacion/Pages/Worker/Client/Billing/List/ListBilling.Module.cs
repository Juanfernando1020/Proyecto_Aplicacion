using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Billing.Detail;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Billing.List.Module
{
    internal static class ListBilling
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListBillingPage, ViewModel.ListBilling>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<ListBillingPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
