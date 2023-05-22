using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.Forms;

namespace Aplicacion.Pages.Billing.List.ViewModel
{
    internal class BillingList : ViewModelBase
    {
        #region Propeties
        public ICommand GoToCreateBillingCommand => new Command<Billings>(async (Billings billing) => await GoToCreateBillingController(billing));
        public ICommand SelectOptionCommand => new Command<Billings>(async (Billings billing) => await SelectOptionController(billing));

        #endregion

        #region Methods
        private async Task GoToCreateBillingController(Billings billing)
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Role, billing);

            await NavigationService.NavigateToAsync<Create.BillingCreatePage>(parameters: parameters);
            IsBusy = false;
        }

        private async Task SelectOptionController(Billings billing)
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Role, billing);

            await NavigationService.NavigateToAsync<Details.BillingDetailsPage>(parameters: parameters);
            IsBusy = false;
        }
        #endregion
    }
}
