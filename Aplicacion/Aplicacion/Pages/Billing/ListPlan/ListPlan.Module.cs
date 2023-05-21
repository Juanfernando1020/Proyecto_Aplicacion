using Aplicacion.Pages.Billing.List;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Billing.ListPlan.Module
{
    class ListPlan
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListPlanPage, ViewModel.ListPlan>();
        }
    }
}
