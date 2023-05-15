using Aplicacion.Common.Helpers;

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
            ViewsManager.RegisterView<ClientDetailsPage, ViewModel.ClientDetails>();
        }
    }
}
