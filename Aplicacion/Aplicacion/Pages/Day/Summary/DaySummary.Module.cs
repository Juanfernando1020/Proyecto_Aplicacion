using Aplicacion.Pages.Client.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Day.Summary.Module
{
    internal static class DaySummary
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<DaySummaryPage, ViewModel.DaySummary>();
        }
    }
}
