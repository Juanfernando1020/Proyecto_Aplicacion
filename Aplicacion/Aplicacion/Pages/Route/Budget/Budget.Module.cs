namespace Aplicacion.Pages.Route.Budget.Module
{
    internal static class Budget
    {
        internal static void Initialize()
        {
            InitializeDependencyPopups();
        }

        private static void InitializeDependencyPopups()
        {
            Add.Module.BudgetAdd.Initialize();
        }
    }
}
