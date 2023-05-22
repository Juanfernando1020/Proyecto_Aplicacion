using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm;
using Xamarin.Forms;

namespace Aplicacion.Pages.Finance.Expense.List.Module.ViewModel
{
    internal class ExpenseList : ViewModelBase
    {
        #region Propeties
        public ICommand GoToExpenseCreatePageCommand => new Command(async () => await GoToExpenseCreatePageController());
        #endregion

        #region Methods
        private async Task GoToExpenseCreatePageController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Create.ExpenseCreatePage>();
            IsBusy = false;
        }
        #endregion
    }
}
