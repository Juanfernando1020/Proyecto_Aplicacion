using Aplicacion.Common.Helpers;

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
            ViewsManager.RegisterView<BillingCreatePage, ViewModel.BillingCreate>();
        }
    }
}
