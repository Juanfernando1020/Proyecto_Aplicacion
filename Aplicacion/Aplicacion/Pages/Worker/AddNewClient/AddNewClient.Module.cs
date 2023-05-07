using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Account.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.AddNewClient.Module
{
    internal static class AddNewClient
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<AddNewClientPage, ViewModel.AddNewClient>();
        }
        internal static Page CreateAddNewClientPage()
        {
            return ViewsManager.CreateView<AddNewClientPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
