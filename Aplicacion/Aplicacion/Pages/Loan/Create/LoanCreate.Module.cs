using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Loan.Create.Module
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
            ViewsManager.RegisterPage<LoanCreatePage, ViewModel.LoanCreate>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
