using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Main.Module
{
    internal static class Main
    {
        internal static void Initialize()
        {
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<MainPage, ViewModel.Main>();
        }

    }
}
