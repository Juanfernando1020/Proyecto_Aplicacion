using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Client.Channels;
using Plugin.Media.Abstractions;
using Xamarin.CommonToolkit.Helpers;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Aplicacion.Pages.Client.Details.ViewModel
{
    internal class ClientDetails : PageViewModelBase, ILoadLoanListViewChannel
    {
        #region Propeties

        private Clients _client;
        public Clients Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        public ICommand CallClientCommand => new AsyncCommand(CallClientController);
        public ICommand GoToClientLocationCommand => new AsyncCommand(GoToClientLocationController);

        #endregion

        #region Methods

        private async Task GoToClientLocationController()
        {
            try
            {
                bool canOpen = await Launcher.CanOpenAsync("waze://");
                if (canOpen)
                {
                    await Launcher.OpenAsync($"waze://?q={Uri.EscapeDataString(Client.Address)}&navigate=yes");
                }
                else
                {
                    bool canOpenGoogleMaps = await Launcher.CanOpenAsync("https://www.google.com/maps");

                    if (canOpenGoogleMaps)
                    {
                        await Launcher.OpenAsync($"https://www.google.com/maps?q={Uri.EscapeDataString(Client.Address)}");
                    }
                    else
                    {
                        await AlertService.ShowAlert(new WarningMessage(CommonMessages.Warning.Unavailable));
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                await AlertService.ShowAlert(new WarningMessage("No se ha encontrado una dirección asociada a este client"));
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

                    INavigationParameters loanListParameters = new NavigationParameters();
                    loanListParameters.Add(ArgKeys.Client, client);
                    loanListParameters.Add(ArgKeys.Loans, client.Loans);

                    MessagingCenter.Send<ILoadLoanListViewChannel, INavigationParameters>(this, nameof(ILoadLoanListViewChannel), loanListParameters);
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
