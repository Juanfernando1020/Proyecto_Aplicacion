using Aplicacion.Pages.Route.List;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Route.Detail.Module
{
    internal static class RouteDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<RouteDetailsPage, ViewModel.RouteDetails>();
        }
    }
}
 