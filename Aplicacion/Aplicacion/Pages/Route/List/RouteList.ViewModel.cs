using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Channels;
using Aplicacion.Pages.Route.Contracts;
using Aplicacion.Pages.Route.Specifications;
using Aplicacion.Pages.User.Enums;
using Xamarin.CommonToolkit.Common;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.List.ViewModel
{
    internal class RouteList : PageViewModelBase, IRouteCreatedChannel
    {
        #region Variable

        private Users _userInfo;
        private readonly IGenericService<Routes, Guid> _genericService;
        #endregion

        #region Propeties
        private Routes _selectedRoute;
        public Routes SelectedRoute
        {
            get => _selectedRoute;
            set => SetProperty(ref _selectedRoute, value);
        }

        private bool _canRouteDetails;
        public bool CanRouteDetails
        {
            get => _canRouteDetails;
            set
            {
                SetProperty(ref _canRouteDetails, value);
            }
        }

        private ObservableCollection<Routes> _routesCollection;
        public ObservableCollection<Routes> RoutesCollection
        {
            get => _routesCollection;
            set => SetProperty(ref _routesCollection, value);
        }
        public ICommand GoToRouteDashboardCommand => new Command(async () => await GoToRouteDashboardController());
        public ICommand GoToRouteCreateCommand => new Command(async () => await GoToRouteCreateController());
        #endregion

        #region Methods
        private async Task GoToRouteDashboardController()
        {
            IsBusy = true;

            if (SelectedRoute != null)
            {
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.User, _userInfo);

                Aplicacion.Module.App.RouteInfo = SelectedRoute;

                await NavigationService.NavigateToAsync(PagesRoutes.Main, parameters: parameters);

                SelectedRoute = null;
            }
            IsBusy = false;
        }
        private async Task GoToRouteCreateController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.User, _userInfo);

            await NavigationService.NavigateToAsync(PagesRoutes.Route.Details, parameters: parameters);

            IsBusy = false;
        }

        private void OnRouteCreated(IRouteCreatedChannel sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    RoutesCollection.Add(route);
                }
            }
        }

        #endregion

        #region Constructor

        public RouteList()
        {
            CanRouteDetails = false;
            RoutesCollection = new ObservableCollection<Routes>();

            _genericService = GetGenericService<Routes, Guid>();

            MessagingCenter.Subscribe<IRouteCreatedChannel, INavigationParameters>(this, nameof(IRouteCreatedChannel), OnRouteCreated);
        }

        #endregion

        #region Overrides

        public override void OnViewAppearing()
        {
            base.OnViewAppearing();

            Aplicacion.Module.App.RouteInfo = null;
        }

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }

        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);
            OnCallBack(parameters);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            MessagingCenter.Unsubscribe<IRouteCreatedChannel, INavigationParameters>(this, nameof(IRouteCreatedChannel));
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
                    _userInfo = user;
                    RoutesByUserIdSpecification specification = new RoutesByUserIdSpecification(user.Id);
                    CanRouteDetails = user.Role == (int)RolesEnum.Admin;

                    if (parameters[ArgKeys.Arranging] is ArrangingBase<Routes> arranging)
                    {
                        ResultBase<IEnumerable<Routes>> result = await _genericService.GetAllAsync(specification, arranging);

                        if (result.IsSuccess)
                        {
                            foreach (Routes route in result.Data)
                            {
                                RoutesCollection.Add(route);
                            }
                        }
                    }
                    else
                    {
                        ResultBase<IEnumerable<Routes>> result = await _genericService.GetAllAsync(specification);

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

        #region OnCallBack

        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    RoutesCollection.Add(route);
                }
            }
        }
        #endregion
    }
}
