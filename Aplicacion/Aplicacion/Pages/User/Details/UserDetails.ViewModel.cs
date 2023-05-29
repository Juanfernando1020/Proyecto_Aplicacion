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
        private Users _user;
        public Users User
        {
            get => _user;
            set
            {
                SetProperty(ref _user, value);
            }
        }
        public ICommand EditCommand => new Command(async () => await EditController());
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
        #endregion

        #region Constructor
        public UserDetails()
        {
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
        #endregion

        #region OnLoad
        private async void OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            ResultBase<Users> result = await _userService.GetByIdAsync();

            if (result.IsSuccess)
            {
                User = result.Data;
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
