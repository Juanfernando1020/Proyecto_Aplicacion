using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.List.Module
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
