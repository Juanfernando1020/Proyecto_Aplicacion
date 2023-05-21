using Xamarin.CommonToolkit.MVVM;
using Xamarin.CommonToolkit.MVVM.Alerts.Messages;
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
using Aplicacion.Pages.Route.Contracts;
using System.Linq;

namespace Aplicacion.Pages.User.Details.ViewModel
{
    internal class UserDetails : ViewModelBase
    {
        #region Variables
        private RolesEnum rolesEnum;

        private readonly IUserService _userService;
        private readonly IRouteService _routeService;
        #endregion

        #region Property
        private bool canCreateRoute;
        public bool CanCreateRoute
        { 
            get => canCreateRoute;
            set
            {
                SetProperty(ref canCreateRoute, value);
            }
        }

        private Users user;
        public Users User
        {
            get => user;
            set
            {
                SetProperty(ref user, value);
            }
        }

        private List<Routes> routes;
        public List<Routes> Routes
        {
            get => routes;
            set
            {
                SetProperty(ref routes, value);
            }
        }

        public ICommand EditCommand => new Command(async () => await EditController());
        public ICommand GoToCreateRouteCommand => new Command(async () => await GoToCreateRouteController());
        #endregion

        #region Method
        private async Task EditController()
        {
            switch (rolesEnum)
            {
                case RolesEnum.Worker:
                    await AlertService.ShowAlert(new WarningMessage("No tienes permiso para editar."));
                    break;
                case RolesEnum.Admin:
                    await AlertService.ShowAlert(new SuccessMessage("No tienes permiso para editar."));
                    break;
            }
        }
        private async Task GoToCreateRouteController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Route.Create.CreateRoutePage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.User, User }
            });
            IsBusy = false;
        }
        #endregion

        #region Constructor
        public UserDetails()
        {
            CanCreateRoute = false;
            User = default;
            _userService = new Service.User(new Repository.User());
            _routeService = new Route.Service.Route(new Route.Repository.Route());
        }
        #endregion

        #region Overrides
        public override void OnInitialize()
        {
            base.OnInitialize();
            OnLoad();
        }
        #endregion

        #region OnLoad
        private async void OnLoad()
        {
            IsBusy = true;
            Guid userId = await _userService.GetUserId();

            if (userId != null && userId != Guid.Empty)
            {
                ResultBase<Users> result = await _userService.GetByIdAsync(userId);

                if (result.IsSuccess)
                {
                    CanCreateRoute = true;
                    User = result.Data;

                    if (User.Role == (int)RolesEnum.Worker)
                        CanCreateRoute = false;
                }
                else
                {
                    await ShowErrorResult(result.Message);
                }

                if (User.Role != (int)RolesEnum.Owner)
                {
                    ResultBase<IEnumerable<Routes>> routesResult = await _routeService.GetAllByUserId(userId);

                    if (routesResult.IsSuccess)
                    {
                        Routes = routesResult.Data.ToList();
                    }
                    else
                    {
                        await ShowErrorResult(routesResult.Message);
                    }
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(userId)));
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
