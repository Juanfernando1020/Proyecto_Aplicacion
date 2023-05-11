using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Loan.Details.Module
{
    internal static class LoanDetails
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<LoanDetailsPage, ViewModel.LoanDetails>();
        }
    }
}
