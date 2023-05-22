using Aplicacion.Pages.Billing.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Route.Create.Module 
{
    internal static class CreateRoute
    {
        internal static void Initialize()
        {
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<CreateRoutePage, ViewModel.CreateRoute>();
        }
    }
}
