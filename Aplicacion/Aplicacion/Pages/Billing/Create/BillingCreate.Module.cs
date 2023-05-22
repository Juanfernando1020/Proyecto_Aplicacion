using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Billing.Create.Module
{
    internal static class BillingCreate
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<BillingCreatePage, ViewModel.BillingCreate>();
        }
    }
}
