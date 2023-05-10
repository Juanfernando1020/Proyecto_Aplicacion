using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Billing.BillingProduct.Detail.Module
{
    internal static class DetailBilling
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<DetailBillingPage, ViewModel.DetailBilling>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
