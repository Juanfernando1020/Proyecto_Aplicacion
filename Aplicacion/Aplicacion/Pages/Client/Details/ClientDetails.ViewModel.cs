using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Module;
using Aplicacion.Pages.Client.Channels;
using Aplicacion.Pages.Client.Specifications;
using Aplicacion.Pages.Loan.Specifications;
using Aplicacion.Pages.User.Specifications;
using Xamarin.CommonToolkit.Helpers;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Aplicacion.Module;
using Aplicacion.Pages.User.Enums;

namespace Aplicacion.Pages.Client.Details.ViewModel
{
    internal class ClientDetails : PageViewModelBase, ILoadLoanListViewChannel
    {
        #region Variables

        private readonly IGenericService<Loans, Guid> _genericLoanService;
        private readonly IGenericService<Clients, Guid> _genericClientService;

        #endregion

        #region Propeties

        private Clients _client;
        public Clients Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        public ICommand CallClientCommand => new AsyncCommand(CallClientController);
        public ICommand DelateClientCommand => new AsyncCommand(DeleteClientController);

        #endregion

        #region Methods

        private async Task DeleteClientController()
        {
            if (Aplicacion.Module.App.UserInfo.Role == 1)
            {
                IsBusy = true;
                Clients updatedClient = new Clients(Client.Id, Client.IDImage, Client.Name, Client.Phone, Client.Address, Client.Route, false);
                Guid clientId = Client.Id;
                ResultBase resultUpdate = await _genericClientService.UpdateAsync(new ClientFirebaseObjectByClientIdSpecification(clientId), Client.Id, updatedClient);

                if (resultUpdate.IsSuccess)
                {
                    Client = updatedClient;
                    await AlertService.ShowAlert(new SuccessMessage("Cliente eliminado correctamente"));
                    //Agregar Navegacion al menu principal.
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage("Error al eliminar el cliente"));
                }

                IsBusy = false;
            }
            else
            {
                await AlertService.ShowAlert(new InfoMessage("No tiene permiso para realizar esta acción"));
            }
        }
        private async Task CallClientController()
        {
            try
            {
                PhoneDialer.Open(Client.Phone);
            }
            catch (ArgumentNullException ex)
            {
                await AlertService.ShowAlert(new WarningMessage("No se ha encontrado un número asociado a este client"));
            }
            catch (FeatureNotSupportedException ex)
            {
                await AlertService.ShowAlert(new WarningMessage(CommonMessages.Warning.Unsupported));
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handler(ex);
                await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            }
        }

        #endregion

        #region constructor

        public ClientDetails()
        {
            _genericLoanService = GetGenericService<Loans, Guid>();
            _genericClientService = GetGenericService<Clients,Guid>();
        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);

            await OnLoad(parameters);
        }

        #endregion

        #region OnLoan

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Client] is Clients client)
                {
                    Client = client;

                    ResultBase<IEnumerable<Loans>> result = await _genericLoanService.GetAllAsync(new LoansByClientIdSpecification(client.Id));

                    if (result.IsSuccess)
                    {
                        if (result.Data is IEnumerable<Loans> routes)
                        {
                            INavigationParameters loanListParameters = new NavigationParameters();
                            loanListParameters.Add(ArgKeys.Client, client);
                            loanListParameters.Add(ArgKeys.Loans, routes.ToArray());

                            MessagingCenter.Send<ILoadLoanListViewChannel, INavigationParameters>(this, nameof(ILoadLoanListViewChannel), loanListParameters);
                        }
                    }
                    else
                    {
                        await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(parameters)), CommonMessages.Error.InformationMessage);
                    }
                }
                else
                {
                    await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(parameters)), CommonMessages.Error.InformationMessage);
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.NullKey, nameof(parameters)), CommonMessages.Error.InformationMessage);
            }
        }

        #endregion
    }
}
