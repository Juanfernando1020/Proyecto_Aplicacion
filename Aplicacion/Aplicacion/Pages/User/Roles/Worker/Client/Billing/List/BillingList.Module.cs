using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Billing.List.Module
{
    internal static class BillingList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<BillingListPage, ViewModel.BillingList>();
        }
    }
}
