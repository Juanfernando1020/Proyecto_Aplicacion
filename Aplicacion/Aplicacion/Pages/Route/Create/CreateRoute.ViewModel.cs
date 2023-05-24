using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Contracts;
using Aplicacion.Pages.User.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Pages.User.Enums;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.Forms;
using Aplicacion.Pages.User.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Mvvm.ViewModels;

namespace Aplicacion.Pages.Route.Create.ViewModel
{
    internal class CreateRoute : PageViewModelBase
    {
        #region Variables
        private readonly IRouteService _routeService;
        private readonly IUserService _userService;
        #endregion

        #region Properties
        private Routes route;
        public Routes Route
        { 
            get => route;
            set
            {
                SetProperty(ref route, value);
            }
        }

        private Users _worker;
        public Users Worker
        {
            get => _worker;
            set => SetProperty(ref _worker, value);
        }

        public ICommand OpenUserBySpecificationPopupCommand => new Command(async () => await OpenUserBySpecificationPopupController());
        public ICommand CreateCommand => new Command(async () => await CreateController());
        #endregion

        #region Methods

        private async Task OpenUserBySpecificationPopupController()
        {
            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Specification, new UserByRoleSpecification(RolesEnum.Worker));

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.User.UserBySpecification, parameters: parameters);
        }
        private async Task CreateController()
        {
            IsBusy = true;
            ResultBase result = await _routeService.CreateAsync(Route);

            if (result.IsSuccess)
            {
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Route, Route);

                await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.InformationMessage));
                await NavigationService.PopAsync(parameters: parameters);
            }
            else
            {
                await AlertService.ShowAlert(new WarningMessage(result.Message));
            }
            IsBusy = false;
        }
        #endregion

        #region Constructor
        public CreateRoute()
        {
            Route = new Routes(Guid.NewGuid(), string.Empty, string.Empty, true, null, null);

            _routeService = new Service.Route(new Repository.Route());
            _userService = new User.Service.User(new User.Repository.User());
        }
        #endregion

        #region Overrides
        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }

        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);

            if (parameters != null)
            {
                Worker = parameters[ArgKeys.Worker] as Users;
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Worker):
                    Route.Worker = Worker;
                    break;
            }
        }

        #endregion

        #region OnLoad
        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;
            if (parameters.ContainsKey(ArgKeys.User))
            {
                object arg = parameters[ArgKeys.User];

                if (arg is Users user)
                {
                    Route.Manager = user;
                }
                else
                {
                    await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(ArgKeys.User)));
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(ArgKeys.User)));
            }
            IsBusy = false;
        }

        private async Task ShowErrorResult(string message)
        {
            Console.WriteLine(message);
            await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            await NavigationService.PopAsync();
        }
        #endregion
    }
}
