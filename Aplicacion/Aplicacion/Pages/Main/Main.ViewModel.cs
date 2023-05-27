using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.PagesBase.Enums;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Contracts;
using Aplicacion.Pages.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Aplicacion.Config.Messages;

namespace Aplicacion.Pages.Main.ViewModel
{
    internal class Main : PageViewModelBase
    {
        #region Variables
        private RolesEnum role;
        private readonly IMainService service;
        #endregion

        #region Properties
        private List<Modules> _menuItems;
        public List<Modules> MenuItems
        {
            get => _menuItems;
            set
            {
                SetProperty(ref _menuItems, value);
            }
        }

        public ICommand SelectOptionCommand => new Command<Modules>(async (item) => await SelectOptionController(item));
        #endregion

        #region Methods
        private async Task SelectOptionController(Modules item)
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Role, role);

            await NavigationService.NavigateToAsync(item.Page, (PagesBaseEnum)item.PageType, parameters);
            IsBusy = false;
        }
        #endregion

        #region Constructor
        public Main()
        {
            service = new Service.Main();

            MenuItems = new List<Modules>();
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
            if (!parameters.ContainsKey(ArgKeys.Role))
            {
                Console.WriteLine(string.Format(CommonMessages.Console.MissingKey, ArgKeys.Role));
                await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                return;
            }

            IsBusy = true;

            role = (RolesEnum)parameters[ArgKeys.Role];
            IEnumerable<Modules> result = service.GetModulesAsync(role);
            MenuItems = result.ToList();

            IsBusy = false;
        }
        #endregion
    }
}
