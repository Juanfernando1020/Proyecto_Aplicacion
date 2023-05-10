using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Billing.BillingProduct.Create.Module
{
    internal static class CreateBilling
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateBillingPage, ViewModel.CreateBilling>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<CreateBillingPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
