using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
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
        public ICommand SelectOptionCommand => new Command<Loans>(async (Loans loan) => await SelectOptionController(loan));
        #endregion

        #region Methods
        private async Task GoToCreateLoanController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync < Create.LoanCreatePage>();
            IsBusy = false;
        }
        private async Task SelectOptionController(Loans loan)
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Details.LoanDetailsPage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.Loan, loan }
            });
            IsBusy = false;
        }


        #endregion
    }
}
