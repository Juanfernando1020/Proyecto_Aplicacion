using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Loan.List.Module
{
    internal static class LoanList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<LoanListPage, ViewModel.LoanList>();
        }
    }
}
