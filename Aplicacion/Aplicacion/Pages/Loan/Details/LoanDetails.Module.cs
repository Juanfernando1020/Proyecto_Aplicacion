using Xamarin.CommonToolkit.Helpers;
using Aplicacion.Pages.Loan.Details;

namespace Aplicacion.Pages.Loan.Details.Module
{
    internal static class LoanDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<LoanDetailsPage, ViewModel.LoanDetails>();
        }
    }
}
