using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using System.Runtime.CompilerServices;
using Aplicacion.Config.Routes;
using Aplicacion.Config;
using Aplicacion.Pages.User.Enums;
using Aplicacion.Pages.User.Specifications;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using System;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacion.Pages.User.Create.ViewModel
{
    internal class UserCreate : PageViewModelBase
    {
        #region Variables
        private string adminName = null;
        private readonly IGenericService<Users, Guid> _genericService;
        private readonly IUserService _userService;

        #endregion

        #region Properties
        private Users _user;
        public Users User
        {
            get => _user;
            set
            {
                SetProperty(ref _user, value);
            }
        }

        private bool _isWorker;
        public bool IsWorker
        {
            get => _isWorker;
            set
            {
                SetProperty(ref _isWorker, value); 
            }
        }

        private Users _admin;
        public Users Admin
        {
            get => _admin;
            set
            {
                SetProperty(ref _admin, value);
            }
        }

        public ICommand UserCreateCommand => new Command(async () => await UserCreateController());
        public ICommand OpenUserBySpecificationPopupCommand => new Command(async () => await OpenUserBySpecificationPopupController());
        #endregion

        #region Methods
        private async Task UserCreateController()
        {
 
            if (string.IsNullOrEmpty(User.Name) ||
                string.IsNullOrEmpty(User.Phone) ||
                string.IsNullOrEmpty(User.Address) ||
                string.IsNullOrEmpty(User.Password))
            {
                await AlertService.ShowAlert(new ErrorMessage("Por favor, complete todos los campos requeridos."));
                return;
            }

            IsBusy = true;
            User.Admin = Admin;
            User.CreateDate = DateTime.UtcNow.AddHours(-5); ;
            User.NextPaymentDate= DateTime.Now.AddMonths(1);
            User.Role = IsWorker ? (int)RolesEnum.Worker : (int)RolesEnum.Admin;

            UserByPhoneSpecification specification = new UserByPhoneSpecification(User.Phone);
            ResultBase<IEnumerable<Users>> existingUserResult = await _userService.GetAllBySpecificationAsync(specification);

            if (existingUserResult.IsSuccess && existingUserResult.Data.Any())
            {
                await AlertService.ShowAlert(new ErrorMessage("El número de teléfono ya está asignado a otro usuario."));
                IsBusy = false;
                return;
            }
            ResultBase result = await _genericService.InsertAsync(User);

            if (!result.IsSuccess)
            {
                await AlertService.ShowAlert(new ErrorMessage(result.Message));
                return;
            }
            await AlertService.ShowAlert(new SuccessMessage("El usuario ha sido creado correctamente"));

            await NavigationService.PopAsync();
            IsBusy = false;
        }
        private async Task OpenUserBySpecificationPopupController()
        {
            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Specification, new UserByRoleSpecification(RolesEnum.Admin));

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.User.UserBySpecification, parameters: parameters);
        }

        private void OnCallback(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.User] is Users user)
                {
                    Admin = user;
                }
            }
        }
        #endregion

        #region Contructor
        public UserCreate()
        {
            User = new Users(Guid.NewGuid(),string.Empty,string.Empty,string.Empty,string.Empty,1);
            _genericService = GetGenericService<Users, Guid>();
            _userService = new Service.User(new Repository.User());
        }
        #endregion

        #region Overrides
        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);
            OnCallback(parameters);
        }
        #endregion
    }
}
