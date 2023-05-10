using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Common.Result;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.User.Create.ViewModel
{
    internal class CreateUser : ViewModelBase
    {
        #region Variables
        private string adminName = null;

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

        public ICommand CreateUserCommand => new Command(async () => await CreateUserController());
        #endregion

        #region Methods
        private async Task CreateUserController()
        {
            IsBusy = true;
            ResultBase result = await _userService.InsertAsync(User, adminName);

            if (!result.IsSuccess)
            {
                await AlertService.ShowAlert(new ErrorMessage(result.Message));
                return;
            }

            await AlertService.ShowAlert(new SuccessMessage("El usuario ha sido creado correctamente"));

            await NavigationService.PopAsync();
            IsBusy = false;
        }
        #endregion

        #region Contructor
        public CreateUser()
        {
            User = default;

            _userService = new Service.User(new Repository.User());
        }
        #endregion

        #region Overrides
        #endregion
    }
}
