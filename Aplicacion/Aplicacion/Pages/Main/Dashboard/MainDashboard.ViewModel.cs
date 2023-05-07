using Aplicacion.Common.MVVM;
using Aplicacion.Pages.Main.Dashboard.Contracts;
using Aplicacion.Pages.Main.Dashboard.Enums;
using Aplicacion.Pages.Main.Dashboard.Models;
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
            await NavigationService.NavigateTo(item.Page, item.PageType, item.Args);
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

            if (Args.ContainsKey(ArgsKeys.DashboardType))
            {
                MainDashboardTypeEnum type = (MainDashboardTypeEnum)Args[ArgsKeys.DashboardType];

                MenuItems = service
                    .GetAllItems(type)
                    .ToList();
            }
        }
        #endregion

        #region Private classes
        private static class ArgsKeys
        {
            internal static string DashboardType => "DashboardType";
        }
        #endregion
    }
}
