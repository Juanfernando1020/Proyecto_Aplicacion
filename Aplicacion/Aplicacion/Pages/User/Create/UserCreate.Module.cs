using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.User.Create.Module
{
    internal class UserCreate
    {
        internal static void Initialize()
        {
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<UserCreatePage, ViewModel.UserCreate>();
        }
    }
}
