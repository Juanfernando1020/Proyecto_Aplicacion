using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Finance.Expense.List.Module
{
    internal static class ExpenseList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ExpenseListPage, ViewModel.ExpenseList>();
        }
    }
}
