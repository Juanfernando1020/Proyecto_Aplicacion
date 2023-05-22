using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Billing.Details.Module
{
    internal static class BillingDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<BillingDetailsPage, ViewModel.BillingDetails>();
        }
    }
}
