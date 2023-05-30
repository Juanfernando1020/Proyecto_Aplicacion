using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Basis.Specifications;
using Aplicacion.Pages.User.Contracts;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Basis.Details.ViewModel
{
    internal class BasisDetails : ViewModelBase
    {
        #region Variables

        private readonly IUserService _userService;
        private readonly IGenericService<Basises, Guid> _genericService;
        private Users _userInfo;

        #endregion

        #region Properties

        private Basises _basis;
        public Basises Basis
        {
            get => _basis;
            set => SetProperty(ref _basis, value);
        }

        public ICommand GoToCreateBasis => new AsyncCommand(GoToCreateBasisController);

        #endregion

        #region Methods

        private async Task GoToCreateBasisController()
        {
            IsBusy = true;
            if (Basis != null)
            {
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Basis, Basis);

                await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Route.Basis.BasisCreate, parameters: parameters);
            }
        }

        #endregion

        #region Constructor

        public BasisDetails()
        {
            _userService = new User.Service.User(new User.Repository.User());
            _genericService = GetGenericService<Basises, Guid>();
        }

        #endregion

        #region Overrides
        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Guid routeId)
                {
                    ResultBase<Basises> result = await _genericService.GetBySpecificacionAsync(new BasisByRouteIdSpecification(routeId));

                    if (result != null && result.Data is Basises basis)
                    {
                        Basis = basis;
                    }
                }

                if (parameters[ArgKeys.User] is Users user)
                {
                    _userInfo = user;
                }
            }
        }

        #endregion
    }
}
