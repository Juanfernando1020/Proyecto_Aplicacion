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

namespace Aplicacion.Pages.User.Details.ViewModel
{
    internal class UserDetails : ViewModelBase
    {
        #region Variables
        private RolesEnum rolesEnum;

        private readonly IUserService _userService;
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

        public ICommand EditCommand => new Command(async () => await Editcontroller());
        #endregion

        #region Method
        private async Task Editcontroller()
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
        #endregion

        #region Constructor
        public UserDetails()
        {
            User = default;
            _userService = new Service.User(new Repository.User());


        }
        #endregion

        #region Overrides
        public override void OnInitialize()
        {
            IsBusy = true;
            base.OnInitialize();
            OnLoad();
            IsBusy = false;
        }
        #endregion

        #region OnLoad
        private async void OnLoad()
        {
            Guid userId = await _userService.GetUserId();

            if (userId == null || userId == Guid.Empty)
            {
                Console.WriteLine("It was missing the 'userId' key.");
                await AlertService.ShowAlert(new ErrorMessage("No se puede cargar la información. Intentalo más tarde."));
                await NavigationService.PopAsync();
                return;
            }

            ResultBase<Users> result = await _userService.GetByIdAsync(userId);

            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Message);
                await AlertService.ShowAlert(new ErrorMessage("No se puede cargar la información. Intentalo más tarde."));
                await NavigationService.PopAsync();
            }

            User = result.Data;
        }
        #endregion

    }
}
