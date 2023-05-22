using Xamarin.CommonToolkit.Helpers;
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
            ViewsManager.RegisterPage<LoginPage, ViewModel.Login>();
        }

        private static void InitializeDependencyPages()
        {
        }
    }
}
