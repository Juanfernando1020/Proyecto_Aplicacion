﻿using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Create.Module
{
    internal static class ClientCreate
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ClientCreatePage, ViewModel.ClientCreate>();
        }
    }
}
