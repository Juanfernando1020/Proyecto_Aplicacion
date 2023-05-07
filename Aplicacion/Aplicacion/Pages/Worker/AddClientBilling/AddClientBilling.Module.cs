using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.AddClientBilling.Module
{
    internal static class AddClientBilling
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<AddClientBillingPage, ViewModel.AddClientBilling>();
        }
        internal static Page CreateAddClientBilling()
        {
            return ViewsManager.CreateView<AddClientBillingPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
