using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Client.Create.Module;
using Aplicacion.Pages.Main;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm;
using Xamarin.Forms;

namespace Aplicacion.Pages.Client.List.ViewModel
{
    internal class ClientList : ViewModelBase
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
            await NavigationService.NavigateToAsync<Details.ClientDetailsPage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.Client, client }
            });
            IsBusy= false;
        }
        #endregion

    }
}
