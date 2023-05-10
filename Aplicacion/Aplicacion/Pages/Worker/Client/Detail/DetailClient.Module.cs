using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Detail.Module
{
    internal static class DetailClient
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<DetailClientPage, ViewModel.DetailClient>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
