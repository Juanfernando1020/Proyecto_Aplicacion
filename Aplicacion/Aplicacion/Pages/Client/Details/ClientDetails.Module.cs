using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Client.Details.Module
{
    internal static class ClientDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<ClientDetailsPage, ViewModel.ClientDetails>();
        }
    }
}
