using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Create.Module
{
    internal static class CreateNewClient
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateNewClientPage, ViewModel.CreateNewClient>();
        }
        private static void InitializeDependencyPages()         
        {
        }
    }
}
