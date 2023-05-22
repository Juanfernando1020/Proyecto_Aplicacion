using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.User.Popups.Pickers.UserBySpecification.Module
{
    internal class UserBySpecification
    {
        internal static void Initialize()
        {
            RegisterPopup();
        }

        private static void RegisterPopup()
        {
            ViewsManager.RegisterPopup<UserBySpecificationPopup, ViewModel.UserBySpecification>();
        }
    }
}
