using Aplicacion.Config;
using Aplicacion.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Client.List.ViewModel
{
    internal class ClientList : PageViewModelBase
    {
        #region Propeties
        public ICommand GoToCreateClientCommand => new Command(async () => await GoToCreateClientController());
        public ICommand SelectOptionCommand => new Command<Clients>(async (Clients client) => await SelectOptionController(client));
        #endregion

        #region Methods
        private async Task GoToCreateClientController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Create.ClientCreatePage>();
            IsBusy = false;
        }
        private async Task SelectOptionController(Clients client)
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Client, client);

            await NavigationService.NavigateToAsync<Details.ClientDetailsPage>(parameters: parameters);
            IsBusy= false;
        }
        #endregion

    }
}
