using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.AddLoanClient.Module
{
    internal static class AddLoanClient
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<AddLoanClientPage, ViewModel.AddLoanClient>();
        }
        internal static Page CreateAddLoanClientPage()
        {
            return ViewsManager.CreateView<AddLoanClientPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
