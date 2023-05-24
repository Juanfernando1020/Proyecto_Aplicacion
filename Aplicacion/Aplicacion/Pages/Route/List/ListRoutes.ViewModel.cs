using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.List.ViewModel
{
    class ListRoutes : PageViewModelBase
    {
        #region Propeties
        public ICommand GoToCreaterouteCommand => new Command(async () => await GoToCreateRouteController());

        #endregion

        #region Methods
        private async Task GoToCreateRouteController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Create.CreateRoutePage>();
            IsBusy = false;
        }

        #endregion
    }
}
