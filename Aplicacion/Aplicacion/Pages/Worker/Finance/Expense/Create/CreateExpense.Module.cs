using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Finance.Expense.Create.Module
{
    internal static class CreateExpense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateExpensePage, ViewModel.CreateExpense>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
