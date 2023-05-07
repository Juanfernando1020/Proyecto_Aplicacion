﻿using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Common.MVVM.Navigation.Models;
using Aplicacion.Common.Result;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.Main.Dashboard.Enums;
using Aplicacion.Vistas.VistasAdmin;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
                await AlertsManager.ShowAlert(new ErrorMessage("Los campos no pueden estar vacíos."));
                return;
            }

            ResultBase<MainDashboardTypeEnum> result = await _loginService.LoginAsync(Credentials);

            if (!result.IsSuccess)
            {
                IsBusy = false;
                //Credentials = new Credentials();
                await AlertsManager.ShowAlert(new ErrorMessage(result.Message));
                return;
            }

            await NavigationService.NavigateToRoot<Main.Dashboard.MainDashboardPage>(args: new Dictionary<string, object>()
            {
                {"DashboardType", result.Data}
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

        #region Overrides
        #endregion
    }
}
