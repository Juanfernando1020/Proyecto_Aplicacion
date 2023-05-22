using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Billing.List.Module
{
    internal static class BillingList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<BillingListPage, ViewModel.BillingList>();
        }
    }
}
