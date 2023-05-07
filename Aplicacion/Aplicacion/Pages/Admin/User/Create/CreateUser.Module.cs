using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Admin.User.Create.Module
{
    internal class CreateUser
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateUserPage, ViewModel.CreateUser>();
        }

        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<CreateUserPage>();
        }

        private static void InitializeDependencyPages()
        {
            
        }
    }
}
