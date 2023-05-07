using Aplicacion.Common.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.ViewExpense.Module
{
    internal static class ViewExpense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ViewExpensePage, ViewModel.ViewExpense>();
        }

        internal static Page CreateViewExpensePage()
        {
            return ViewsManager.CreateView<ViewExpensePage>();
        }
        private static void InitializeDependencyPages()
        {
        }


    }
}
