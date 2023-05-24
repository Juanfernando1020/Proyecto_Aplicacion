using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.List.ViewModel
{
    internal class LoanList : PageViewModelBase
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

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Loan, loan);

            await NavigationService.NavigateToAsync<Details.LoanDetailsPage>(parameters: parameters);
            IsBusy = false;
        }


        #endregion
    }
}
