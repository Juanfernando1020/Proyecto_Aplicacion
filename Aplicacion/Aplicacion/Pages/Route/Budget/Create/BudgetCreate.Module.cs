using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Route.Budget.Create.Module
{
    internal static class BudgetCreate
    {
        internal static void Initialize()
        {
            RegisterView();
        }

        private static void RegisterView()
        {
            ViewsManager.RegisterPopup<BudgetCreatePopup, ViewModel.BudgetCreate>();
        }
    }
}
