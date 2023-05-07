using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Account.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.ViewAllClients.Module
{
    internal static class ViewAllClients
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ViewAllClientsPage, ViewModel.ViewAllClients>();
        }

        internal static Page CreateViewAllClients()
        {
            return ViewsManager.CreateView<ViewAllClientsPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
