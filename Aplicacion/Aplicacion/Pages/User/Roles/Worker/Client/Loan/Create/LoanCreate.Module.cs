using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Loan.Create.Module
{
    internal static class LoanCreate
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<LoanCreatePage, ViewModel.LoanCreate>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
