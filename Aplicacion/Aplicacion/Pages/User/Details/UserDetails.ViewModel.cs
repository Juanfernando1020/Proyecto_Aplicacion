using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using Aplicacion.Pages.User.Roles.Enums;
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
            if (!Args.ContainsKey(ArgKeys.Role))
            {
                Console.WriteLine("It was missing the 'MainType' key.");
                await AlertService.ShowAlert(new ErrorMessage("No se puede cargar la información. Intentalo más tarde."));
                await NavigationService.PopAsync();
                return;
            }

            rolesEnum = (RolesEnum)Args[ArgKeys.Role];
            Users user = JsonConvert.DeserializeObject<Users>((string)Application.Current.Properties[ArgKeys.User]) ?? default;
          

            var result = _userService.GetByIdAsync(user.Id);    
        }
        #endregion

    }
}
