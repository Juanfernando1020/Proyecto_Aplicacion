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
            Create.Module.BudgetCreate.Initialize();
            List.Module.BudgetList.Initialize();
        }
    }
}
