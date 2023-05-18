using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.User.Details.Module
{
    internal static class UserDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<UserDetailsPage, ViewModel.UserDetails>();
        }
    }
}
