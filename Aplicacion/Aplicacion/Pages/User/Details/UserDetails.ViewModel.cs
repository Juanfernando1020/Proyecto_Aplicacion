using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using Aplicacion.Pages.User.Enums;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aplicacion.Pages.Route.Contracts;
using System.Linq;
using Aplicacion.Pages.User.Specifications;
using Xamarin.CommonToolkit.Common;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Aplicacion.Config.Messages;
using Aplicacion.Config.Routes;

namespace Aplicacion.Pages.User.Details.ViewModel
{
    internal class UserDetails : PageViewModelBase, IPopupEvents
    {
        #region Variables
        private readonly IUserService _userService;
        private readonly IRouteService _routeService;
        #endregion

        #region Property
        private INavigationParameters _viewsParamaters;
        public INavigationParameters ViewsParamaters
        {
            get => _viewsParamaters;
            set
            {
                SetProperty(ref _viewsParamaters, value);
            }
        }

        private bool _canCreateRoute;
        public bool CanCreateRoute
        { 
            get => _canCreateRoute;
            set
            {
                SetProperty(ref _canCreateRoute, value);
            }
        }

        private Users _user;
        public Users User
        {
            get => _user;
            set
            {
                SetProperty(ref _user, value);
            }
        }

        private ObservableCollection<Routes> _routes;
        public ObservableCollection<Routes> Routes
        {
            get => _routes;
            set
            {
                SetProperty(ref _routes, value);
            }
        }
        
        private Routes _selectedRoute;
        public Routes SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                SetProperty(ref _selectedRoute, value);
            }
        }

        public ICommand EditCommand => new Command(async () => await EditController());
        public ICommand GoToRouteCreateCommand => new Command(async () => await GoToRouteCreateController());
        public ICommand GoToRouteDetailsCommand => new Command(async () => await GoToRouteDetailsController());
        #endregion

        #region Method
        private async Task EditController()
        {
            switch ((RolesEnum)User.Role)
            {
                case RolesEnum.Worker:
                    await AlertService.ShowAlert(new WarningMessage("No tienes permiso para editar."));
                    break;
                case RolesEnum.Admin:
                    await AlertService.ShowAlert(new SuccessMessage("No tienes permiso para editar."));
                    break;
            }
        }
        private async Task GoToRouteCreateController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.User, User);

            await NavigationService.NavigateToAsync<Route.Create.CreateRoutePage>(parameters: parameters);

            IsBusy = false;
        }
        private async Task GoToRouteDetailsController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Route, SelectedRoute);

            await NavigationService.NavigateToAsync(PagesRoutes.Route.Details);

            SelectedRoute = null;

            IsBusy = false;
        }
        #endregion

        #region Constructor
        public UserDetails()
        {
            ViewsParamaters = new NavigationParameters();
            Routes = new ObservableCollection<Routes>();
            CanCreateRoute = false;
            User = default;
            _userService = new Service.User(new Repository.User());
            _routeService = new Route.Service.Route(new Route.Repository.Route());
        }
        #endregion

        #region Overrides
        public override void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad(parameters);
        }

        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);

            var obj = parameters;
        }

        #endregion

        #region OnLoad
        private async void OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            ResultBase<Users> result = await _userService.GetByIdAsync();

            if (result.IsSuccess)
            {
                CanCreateRoute = true;
                User = result.Data;
                ViewsParamaters.Add(ArgKeys.User, User);

                if (User.Role == (int)RolesEnum.Worker)
                    CanCreateRoute = false;
            }
            else
            {
                await ShowErrorResult(result.Message);
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
