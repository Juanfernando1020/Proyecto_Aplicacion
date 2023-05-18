using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Client.List.Module
{
    internal static class ClientList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ClientListPage, ViewModel.ClientList>();
        }
    }
}
