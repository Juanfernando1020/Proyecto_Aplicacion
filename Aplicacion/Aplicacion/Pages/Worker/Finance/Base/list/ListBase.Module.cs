using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Finance.Expense.Create;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Finance.Base.list.Module
{
    internal static class ListBase
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListBasePage, ViewModel.ListBase>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<ListBasePage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
