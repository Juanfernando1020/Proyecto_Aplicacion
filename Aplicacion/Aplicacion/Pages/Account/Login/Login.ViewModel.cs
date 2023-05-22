using Xamarin.CommonToolkit.Mvvm;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Config;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.Main.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Aplicacion.Pages.Main;
using Aplicacion.Pages.User.Enums;

namespace Aplicacion.Pages.Account.Login.ViewModel
{
    internal class Login : ViewModelBase
    {
        #region Variables
        private readonly ILoginService _loginService;
        #endregion

        #region Properties
        private Credentials credentials;
        public Credentials Credentials 
        { 
            get => credentials;
            set 
            {
                SetProperty(ref credentials, value);
            } 
        }

        public ICommand LoginCommand => new Command(async () => await LoginController());
        #endregion

        #region Methods
        private async Task LoginController()
        {
            IsBusy = true;
            if(string.IsNullOrEmpty(Credentials.Username) || string.IsNullOrEmpty(Credentials.Password))
            {
                IsBusy = false;
                await AlertService.ShowAlert(new ErrorMessage("Los campos no pueden estar vacíos."));
                return;
            }

            ResultBase<RolesEnum> result = await _loginService.LoginAsync(Credentials);

            if (!result.IsSuccess)
            {
                IsBusy = false;
                //Credentials = new Credentials();
                await AlertService.ShowAlert(new ErrorMessage(result.Message));
                return;
            }

            await NavigationService.NavigateToRootAsync<MainPage>(args: new Dictionary<string, object>()
            {
                { ArgKeys.Role, result.Data }
            });

            IsBusy = false;
        }
        #endregion

        #region Constructor
        public Login()
        {
            _loginService = new Service.Login(new Repository.Login());

            Credentials = new Credentials();
        }
        #endregion
    }
}
