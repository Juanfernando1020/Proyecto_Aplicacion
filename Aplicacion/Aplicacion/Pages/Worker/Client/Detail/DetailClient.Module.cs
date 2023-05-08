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
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<DetailClientPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
