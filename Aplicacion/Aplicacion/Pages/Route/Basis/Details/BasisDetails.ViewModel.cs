using System.Threading.Tasks;
using Aplicacion.Config;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;

namespace Aplicacion.Pages.Route.Basis.Details.ViewModel
{
    internal class BasisDetails : ViewModelBase
    {
        #region Properties

        private Basises _basis;
        public Basises Basis
        {
            get => _basis;
            set => SetProperty(ref _basis, value);
        }

        #endregion

        #region Overrides

        public override void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Basis] is Basises basis)
                {

                }
            }
        }

        #endregion
    }
}
