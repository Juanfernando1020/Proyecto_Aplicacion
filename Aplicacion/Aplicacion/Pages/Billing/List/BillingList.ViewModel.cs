using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm;
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
            await NavigationService.NavigateToAsync<Create.BillingCreatePage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.Billing, billing }
            });
            IsBusy = false;
        }

        private async Task SelectOptionController(Billings billing)
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Details.BillingDetailsPage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.Billing, billing }
            });
            IsBusy = false;
        }
        #endregion
    }
}
