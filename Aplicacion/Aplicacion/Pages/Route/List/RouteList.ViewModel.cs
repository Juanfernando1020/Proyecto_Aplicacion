using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Contracts;
using Aplicacion.Pages.User.Enums;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.List.ViewModel
{
    class RouteList : ViewModelBase
    {
        #region Variable
        private readonly IRouteService _routeService;
        #endregion

        #region Propeties

        private Routes _selectedRoute;
        public Routes SelectedRoute
        {
            get => _selectedRoute;
            set => SetProperty(ref _selectedRoute, value);
        }

        private ObservableCollection<Routes> _routesCollection;
        public ObservableCollection<Routes> RoutesCollection
        {
            get => _routesCollection;
            set => SetProperty(ref _routesCollection, value);
        }
        public ICommand GoToRouteDetails => new Command(async () => await GoToRouteDetailsController());

        #endregion

        #region Methods
        private async Task GoToRouteDetailsController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Detail.RouteDetailsPage>();
            IsBusy = false;
        }

        #endregion

        #region Constructor

        public RouteList()
        {
            RoutesCollection = new ObservableCollection<Routes>();

            _routeService = new Service.Route(new Repository.Route());
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
            IsBusy = true;

            if (parameters != null)
            {
                if (parameters[ArgKeys.User] is Users user)
                {
                    if (user.Role != (int)RolesEnum.Worker)
                    {
                        ResultBase<IEnumerable<Routes>> result = await _routeService.GetAllByUserId(user.Id);

                        if (result.IsSuccess)
                        {
                            foreach (Routes route in result.Data)
                            {
                                RoutesCollection.Add(route);
                            }
                        }
                    }
                }
            }

            IsBusy = false;
        }
        #endregion
    }
}
