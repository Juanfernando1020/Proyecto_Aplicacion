using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.User.List.Module
{
    internal static class UserList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<UserListPage, ViewModel.UserList>();
        }
    }
}
