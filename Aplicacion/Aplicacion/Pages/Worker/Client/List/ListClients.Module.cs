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
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<ListClientsPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
