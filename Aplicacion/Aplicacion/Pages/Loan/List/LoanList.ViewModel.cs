using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.MVVM;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.List.ViewModel
{
    internal class LoanList : ViewModelBase
    {
        #region Propeties
        public ICommand GoToCreateLoanCommand => new Command(async () => await GoToCreateLoanController());
        #endregion

        #region Methods
        private async Task GoToCreateLoanController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync < Create.LoanCreatePage>();
            IsBusy = false;
        }
        #endregion
    }
}
