using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Common.Result;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Vistas.VistasAdmin;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.Account.Login.ViewModel
{
    internal class Login : ViewModelBase
    {
        #region Variables
        private bool isLoaded = false;

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
            if(string.IsNullOrEmpty(Credentials.Username) || string.IsNullOrEmpty(Credentials.Password))
            {
                await AlertsManager.ShowAlert(new ErrorMessage("Los campos no pueden estar vacíos."));
                return;
            }

            ResultBase<string> result = await _loginService.LoginAsync(Credentials);

            if (!result.IsSuccess)
            {
                //Credentials = new Credentials();
                await AlertsManager.ShowAlert(new ErrorMessage(result.Message));
                return;
            }

            Application.Current.Properties["usuario"] = JsonConvert.SerializeObject(result.Data);

            App.Current.MainPage = new NavigationPage(new PanelAdmin());
        }
        #endregion

        #region Constructor
        public Login()
        {
            _loginService = new Service.Login(new Repository.Login());

            Credentials = new Credentials();
        }
        #endregion

        #region Overrides
        public override void OnViewAppearing()
        {
            base.OnViewAppearing();

            if (!isLoaded)
            {
                OnLoad();
            }
        }
        #endregion

        #region OnLoad
        private void OnLoad()
        {
            isLoaded = true;
        }
        #endregion
    }
}
