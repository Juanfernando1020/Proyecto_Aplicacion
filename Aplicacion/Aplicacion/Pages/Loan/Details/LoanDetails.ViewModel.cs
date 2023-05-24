using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Details.ViewModel
{
    internal class LoanDetails : PageViewModelBase
    {
        #region Propeties
        public ICommand GoToCreateBillingCommand => new Command<Billings>(async (Billings billing) => await GoToCreateBillingController(billing));

        #endregion

        #region Methods
        private async Task GoToCreateBillingController(Billings billing)
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Role, billing);

            await NavigationService.NavigateToAsync<Billing.List.BillingListPage>(parameters: parameters);
            IsBusy = false;
        }

        #endregion
    }
}
