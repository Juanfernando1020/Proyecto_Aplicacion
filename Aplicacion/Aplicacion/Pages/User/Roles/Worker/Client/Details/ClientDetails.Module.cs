using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Details.Module
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
