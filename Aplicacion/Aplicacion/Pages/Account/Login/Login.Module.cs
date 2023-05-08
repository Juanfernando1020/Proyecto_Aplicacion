using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Account.Login.Module
{
    internal static class Login
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<LoginPage, ViewModel.Login>();
        }

        internal static Page CreatePage() => ViewsManager.CreateView<LoginPage>(null);

        private static void InitializeDependencyPages()
        {
        }
    }
}
