using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Common.PagesBase.Enums;
using Aplicacion.Config;
using Aplicacion.Pages.Main.Contracts;
using Aplicacion.Pages.User.Roles.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.ViewModel
{
    internal class Main : ViewModelBase
    {
        #region Variables
        private RolesEnum role;
        private readonly IMainService service;
        #endregion

        #region Properties
        private List<Models.Menu> _menuItems;
        public List<Models.Menu> MenuItems
        {
            get => _menuItems;
            set
            {
                SetProperty(ref _menuItems, value);
            }
        }

        public ICommand SelectOptionCommand => new Command<Models.Menu>(async (item) => await SelectOptionController(item));
        #endregion

        #region Methods
        private async Task SelectOptionController(Models.Menu item)
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync(item.Page, (PagesBaseEnum)item.PageType, new Dictionary<string, object>()
            {
                { ArgKeys.Role, role }
            });
            IsBusy = false;
        }
        #endregion

        #region Constructor
        public Main()
        {
            service = new Service.Main(new Repository.Main());

            MenuItems = new List<Models.Menu>();
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
            if (!Args.ContainsKey(ArgKeys.Role))
            {
                Console.WriteLine($"It was missing the '{ArgKeys.Role}' key.");
                await AlertService.ShowAlert(new ErrorMessage("No se puede cargar la información. Intentalo más tarde."));
                await NavigationService.PopAsync();
                return;
            }
            IsBusy = true;
            role = (RolesEnum)Args[ArgKeys.Role];

            IEnumerable<Models.Menu> menu = await service.GetMenuAsync(role);

            MenuItems = menu.ToList();
            IsBusy = false;
        }
        #endregion
    }
}
