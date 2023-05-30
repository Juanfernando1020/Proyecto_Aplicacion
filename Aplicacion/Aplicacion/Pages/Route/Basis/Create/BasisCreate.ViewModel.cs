using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Basis.Create.Contracts;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Aplicacion.Pages.Route.Basis.Create
{
    internal class BasisCreate : PopupViewModelBase
    {
        #region Variables

        private readonly IBasisCreateService _basisCreateService;
        private readonly IGenericService<Basises, Guid> _genericService;

        #endregion

        #region Properties

        private Basises _basises;
        public Basises Basis
        {
            get => _basises;
            set => SetProperty(ref _basises, value);
        }

        public ICommand CreateBasisCommand => new AsyncCommand(CreateBasisController);

        #endregion

        #region Methods
        private async Task CreateBasisController()
        {
            IsBusy = true;

            string message = string.Empty;
            if (_basisCreateService.Validate(Basis, out message))
            {
                Basis.Date = DateTime.Now;

                ResultBase result = await _genericService.InsertAsync(Basis);

                if (result.IsSuccess)
                {
                    INavigationParameters parameters = new NavigationParameters();
                    parameters.Add(ArgKeys.Basis, Basis);

                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.InformationMessage));
                    await NavigationPopupService.PopPopupAsync(this, parameters: parameters);
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage(result.Message ?? CommonMessages.Error.InformationMessage));
                }
            }
            else
            {
                await AlertService.ShowAlert(new ErrorMessage(message));
            }

            IsBusy = false;
        }

        private async void ShowInitError(string consoleMessage)
        {
            Console.WriteLine(consoleMessage);
            await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            await NavigationPopupService.PopPopupAsync(this);
        }

        #endregion

        #region Constructor

        public BasisCreate()
        {
            Basis = new Basises()
            {
                Id = Guid.NewGuid(),
            };
            _basisCreateService = new Service.BasisCreate();
            _genericService = GetGenericService<Basises, Guid>();
        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    Basis.Route = route.Id;
                }
            }
            else
            {
                ShowInitError(string.Format(CommonMessages.Console.NullKey, nameof(parameters)));
            }
        }

        #endregion
    }
}
