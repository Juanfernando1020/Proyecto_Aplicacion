using Aplicacion.Common.Helpers;
using System;
using Xamarin.Forms;

namespace Aplicacion.Pages.Admin.User.Create.Module
{
    internal class CreateUser
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateUserPage, ViewModel.CreateUser>();
        }

        internal static Page CreatePage() => ViewsManager.CreateView<CreateUserPage>();

        private static void InitializeDependencyPages()
        {
            
        }
    }
}
