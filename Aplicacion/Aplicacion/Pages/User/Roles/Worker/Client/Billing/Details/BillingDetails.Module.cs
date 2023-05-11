using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Billing.Details.Module
{
    internal static class BillingDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<BillingDetailsPage, ViewModel.BillingDetails>();
        }
    }
}
