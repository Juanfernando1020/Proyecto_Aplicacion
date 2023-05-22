using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm;
using Xamarin.Forms;

namespace Aplicacion.Pages.Client.Details.ViewModel
{
    internal class ClientDetails : ViewModelBase
    {
        #region Propeties
        public ICommand GoToListLoanCommand => new Command(async () => await GoToListLoanController());

        #endregion

        #region Methods
        private async Task GoToListLoanController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Loan.List.LoanListPage >();
            IsBusy = false;
        }

        #endregion
    }
}
