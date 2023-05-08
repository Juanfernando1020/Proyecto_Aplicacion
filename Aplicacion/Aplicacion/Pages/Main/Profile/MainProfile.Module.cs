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

        internal static Page CreatePage() => ViewsManager.CreateView<MainProfilePage>();

        private static void InitializeDependencyPages()
        {
        }
    }
}
