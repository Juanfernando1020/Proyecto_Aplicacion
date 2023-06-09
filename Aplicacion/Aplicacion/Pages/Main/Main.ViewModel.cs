using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Aplicacion.Config.Messages;

namespace Aplicacion.Pages.Main.ViewModel
{
    internal class Main : PageViewModelBase
    {
        #region Variables
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

            await NavigationService.NavigateToAsync(item.Page, item.PageType, item.Args);

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
            if (parameters != null)
            {
                if (parameters[ArgKeys.User] is Users user)
                {
                    IEnumerable<Modules> result = service.GetModulesAsync(user, Aplicacion.Module.App.RouteInfo);
                    MenuItems = result.ToList();
                }
            }
            else
            {
                Console.WriteLine(string.Format(CommonMessages.Console.MissingKey, ArgKeys.Role));
                await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            }
        }
        #endregion
    }
}
