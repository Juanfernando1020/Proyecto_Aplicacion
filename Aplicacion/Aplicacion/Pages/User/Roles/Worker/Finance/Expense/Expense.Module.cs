namespace Aplicacion.Pages.User.Roles.Worker.Finance.Expense.Module
{
    internal static class Expense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Create.Module.ExpenseCreate.Initialize();
            List.Module.ExpenseList.Initialize();
        }
    }
}
