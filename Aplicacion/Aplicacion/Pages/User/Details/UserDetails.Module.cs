using Aplicacion.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.User.Details.Module
{
    internal static class UserDetails
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<UserDetailsPage, ViewModel.UserDetails>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
