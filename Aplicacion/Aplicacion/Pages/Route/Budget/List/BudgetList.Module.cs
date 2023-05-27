using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Route.Budget.List.Module
{
    internal static class BudgetList
    {
        internal static void Initialize()
        {
            RegisterView();
        }

        private static void RegisterView()
        {
            ViewsManager.RegisterView<BudgetListView, ViewModel.BudgetList>();
        }
    }
}
