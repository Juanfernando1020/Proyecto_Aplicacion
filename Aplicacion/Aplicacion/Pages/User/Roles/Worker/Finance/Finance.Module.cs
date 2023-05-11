namespace Aplicacion.Pages.User.Roles.Worker.Finance.Module
{
    internal static class Finance
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Expense.Module.Expense.Initialize();
        }
    }
}
