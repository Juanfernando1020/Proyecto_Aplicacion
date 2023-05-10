using Aplicacion.Common.Helpers;
using System;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Profile.Module
{
    internal static class MainProfile
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<MainProfilePage, ViewModel.MainProfile>();
        }

        private static void InitializeDependencyPages()
        {
        }
    }
}
