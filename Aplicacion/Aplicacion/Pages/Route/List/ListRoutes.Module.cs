using Aplicacion.Pages.Route.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Route.List.Module
{
    internal static class ListRoutes
    {
        internal static void Initialize()
        {
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<ListRoutesPage, ViewModel.ListRoutes>();
        }
    }
}
