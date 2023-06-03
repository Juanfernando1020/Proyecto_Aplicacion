using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Aplicacion.Pages.Client.Create.ViewModel
{
    internal class ClientCreate : PageViewModelBase
    {
        #region Variables

        private readonly IGenericService<Clients, Guid> _genericService;

        #endregion

        #region Properties

        private Clients _client;
        public Clients Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        public ICommand CreateClientCommand => new AsyncCommand(CreateClientController);

        #endregion

        #region Methods

        private async Task CreateClientController()
        {
            IsBusy = true;

            bool isValid = await ValidateClient();

            if (isValid)
            {
                ResultBase result = await _genericService.InsertAsync(Client);

                if (result.IsSuccess)
                {
                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                    await NavigationService.PopAsync();
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                }
            }

            IsBusy = false;
        }

        private async Task<bool> ValidateClient()
        {
            if (string.IsNullOrEmpty(Client.Name))
            {
                await AlertService.ShowAlert(new WarningMessage("Debes asignarle un nombre."));
                return false;
            }

            if (string.IsNullOrEmpty(Client.Phone))
            {
                await AlertService.ShowAlert(new WarningMessage("Debes asignarle un teléfono."));
                return false;
            }

            if (string.IsNullOrEmpty(Client.Address))
            {
                await AlertService.ShowAlert(new WarningMessage("Debes asignarle una dirección."));
                return false;
            }

            return true;
        }

        #endregion

        #region Constructor

        public ClientCreate()
        {
            Client = new Clients()
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Loans = new Loans[]{}
            };

            _genericService = GetGenericService<Clients, Guid>();
        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad(parameters);
        }

        #endregion

        #region OnLoad

        private void OnLoad(INavigationParameters parameters)
        {
            if (Aplicacion.Module.App.RouteInfo is Routes route)
            {
                Client.Route = route.Id;
            }
        }

        #endregion
    }
}
