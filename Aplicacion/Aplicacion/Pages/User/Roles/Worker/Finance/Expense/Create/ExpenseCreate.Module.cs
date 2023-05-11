using Aplicacion.Common.Helpers;


namespace Aplicacion.Pages.User.Roles.Worker.Finance.Expense.Create.Module
{
    internal static class ExpenseCreate
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ExpenseCreatePage, ViewModel.ExpenseCreate>();
        }
    }
}
