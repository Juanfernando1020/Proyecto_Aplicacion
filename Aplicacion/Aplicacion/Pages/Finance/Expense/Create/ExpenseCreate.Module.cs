using Xamarin.CommonToolkit.Helpers;


namespace Aplicacion.Pages.Finance.Expense.Create.Module
{
    internal static class ExpenseCreate
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<ExpenseCreatePage, ViewModel.ExpenseCreate>();
        }
    }
}
