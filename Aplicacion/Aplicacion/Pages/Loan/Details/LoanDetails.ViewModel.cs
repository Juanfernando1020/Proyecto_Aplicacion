using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Details.ViewModel
{
    internal class LoanDetails : ViewModelBase
    {
        #region Propeties
        public ICommand GoToCreateBillingCommand => new Command<Billings>(async (Billings billing) => await GoToCreateBillingController(billing));

        #endregion

        #region Methods
        private async Task GoToCreateBillingController(Billings billing)
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Billing.List.BillingListPage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.Billing, billing}
            });
            IsBusy = false;
        }

        #endregion
    }
}
