using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Billing.List.Module
{
    internal static class ListBillings
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListBillingsPage, ViewModel.ListBillings>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
