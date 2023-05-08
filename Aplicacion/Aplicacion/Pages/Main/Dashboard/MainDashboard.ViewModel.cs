using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Config;
using Aplicacion.Pages.Main.Dashboard.Contracts;
using Aplicacion.Pages.Main.Dashboard.Models;
using Aplicacion.Pages.Main.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Dashboard.ViewModel
{
    internal class MainDashboard : ViewModelBase
    {
        #region Variables
        private readonly IMainDashboardService service;
        #endregion

        #region Properties
        private List<MainDashboardItem> _menuItems;
        public List<MainDashboardItem> MenuItems
        {
            get => _menuItems;
            set
            {
                SetProperty(ref _menuItems, value);
            }
        }

        public ICommand SelectOptionCommand => new Command<MainDashboardItem>(async (MainDashboardItem item) => await SelectOptionController(item));
        #endregion

        #region Methods
        private async Task SelectOptionController(MainDashboardItem item)
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync(item.Page, item.PageType, item.Args);
            IsBusy = false;
        }
        #endregion

        #region Constructor
        public MainDashboard()
        {
            service = new Service.MainDashboard();

            MenuItems = new List<MainDashboardItem>();
        }
        #endregion

        #region Overrides
        public override void OnInitialize()
        {
            base.OnInitialize();
            OnLoad();
        }
        #endregion

        #region OnLoad
        private async void OnLoad()
        {
            if (!Args.ContainsKey(ArgKeys.MainType))
            {
                Console.WriteLine($"It was missing the '{ArgKeys.MainType}' key.");
                await AlertService.ShowAlert(new ErrorMessage("No se puede cargar la información. Intentalo más tarde."));
                await NavigationService.PopAsync();
                return;
            }

            MainTypesEnum type = (MainTypesEnum)Args[ArgKeys.MainType];

            MenuItems = service
                .GetAllItems(type)
                .ToList();
        }
        #endregion
    }
}
