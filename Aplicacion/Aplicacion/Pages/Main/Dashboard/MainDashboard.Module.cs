using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Dashboard.Module
{
    internal static class MainDashboard
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<MainDashboardPage, ViewModel.MainDashboard>();
        }

        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<MainDashboardPage>();
        }

        private static void InitializeDependencyPages()
        {
        }
    }
}
