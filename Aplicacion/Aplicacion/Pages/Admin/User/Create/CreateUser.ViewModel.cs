using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Common.Result;
using Aplicacion.Models;
using Aplicacion.Pages.Admin.User.Contracts;
using Aplicacion.Pages.Admin.User.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.Admin.User.Create.ViewModel
{
    internal class CreateUser : ViewModelBase
    {
        #region Variables
        private string adminName = null;

        private readonly IUserService _userService;
        #endregion

        #region Properties
        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set
            {
                SetProperty(ref _user, value);
            }
        }

        public ICommand CreateUserCommand => new Command(async () => await CreateUserController());
        #endregion

        #region Methods
        private async Task CreateUserController()
        {
            IsBusy = true;
            if (!User.IsAdmin && string.IsNullOrEmpty(adminName))
            {
                adminName = await AlertsManager.ShowPrompt(new PromptMessage("Nombre del administrador", "Ingrese el nombre del administrador:"));

                if (string.IsNullOrEmpty(adminName))
                {
                    await AlertsManager.ShowConfirmAlert(new ConfirmationMessage("Todos los trabajadores deben relacionarse con un administrador."));

                    await CreateUserController();
                    return;
                }
            }

            ResultBase result = await _userService.InsertAsync(User, adminName);

            if (!result.IsSuccess)
            {
                await AlertsManager.ShowAlert(new ErrorMessage(result.Message));
                return;
            }

            await AlertsManager.ShowAlert(new SuccessMessage("El usuario ha sido creado correctamente"));

            await NavigationService.PopAsync();
            IsBusy = false;
        }
        #endregion

        #region Contructor
        public CreateUser()
        {
            User = new UserModel();

            _userService = new Service.User(new Repository.User());
        }
        #endregion

        #region Overrides
        #endregion
    }
}
