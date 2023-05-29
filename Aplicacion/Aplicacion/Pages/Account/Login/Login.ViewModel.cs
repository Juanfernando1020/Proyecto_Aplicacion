using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Config;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Models;
using Xamarin.Forms;
using Aplicacion.Pages.Main;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;

namespace Aplicacion.Pages.Account.Login.ViewModel
{
    internal class Login : PageViewModelBase
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

            ResultBase<Users> result = await _loginService.LoginAsync(Credentials);

            if (!result.IsSuccess)
            {
                IsBusy = false;
                await AlertService.ShowAlert(new ErrorMessage(result.Message));
                return;
            }

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.User, result.Data);

            await NavigationService.NavigateToRootAsync<MainPage>(parameters: parameters);

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
