namespace Aplicacion.Pages.Finance.Module
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
