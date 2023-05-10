using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.List.Module
{
    internal static class ListClients
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListClientsPage, ViewModel.ListClients>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
