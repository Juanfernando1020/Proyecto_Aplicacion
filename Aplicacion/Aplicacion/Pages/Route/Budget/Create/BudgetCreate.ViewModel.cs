using System;
using Aplicacion.Config;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Budget.Create.Contracts;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;
using Aplicacion.Config.Routes;
using Aplicacion.Pages.User.Contracts;
using Aplicacion.Pages.User.Enums;
using Aplicacion.Pages.User.Specifications;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;

namespace Aplicacion.Pages.Route.Budget.Create.ViewModel
{
    internal class BudgetCreate : PopupViewModelBase
    {
        #region Variables
        private readonly IBudgetCreateService _budgetCreateService;
        private readonly IUserService _userService;
        private Guid _userId;
        #endregion

        #region Properties

        private Budgets _budget;
        public Budgets Budget
        {
            get => _budget;
            set => SetProperty(ref _budget, value);
        }

        private Users _admin;
        public Users Admin
        {
            get => _admin;
            set => SetProperty(ref _admin, value);
        }

        public ICommand SendBudgetCommand => new Command(async () => await SendBudgetController());
        public ICommand OpenUserBySpecificationPopupCommand => new Command(async () => await OpenUserBySpecificationPopupController());
        #endregion

        #region Methods
        private async Task SendBudgetController()
        {
            IsBusy = true;

            string message = string.Empty;

            if (_budgetCreateService.Validate(Budget, out message))
            {
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Budget, Budget);

                await NavigationPopupService.PopPopupAsync(this, parameters: parameters);
            }
            else
            {
                if (!string.IsNullOrEmpty(message))
                {
                    await AlertService.ShowAlert(new WarningMessage(message));
                }
            }

            IsBusy = false;
        }
        private async Task OpenUserBySpecificationPopupController()
        {
            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Specification, new UserByRoleAndNotIdSpecification(_userId, RolesEnum.Admin));

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.User.UserBySpecification, parameters: parameters);
        }
        #endregion

        #region Constructor

        public BudgetCreate()
        {
            Budget = new Budgets
            {
                Id = Guid.NewGuid()
            };
            _budgetCreateService = new Service.BudgetCreate();
            _userService = new User.Service.User(new User.Repository.User());
            _userId = Guid.NewGuid();
        }
        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);

            await OnLoad(parameters);
        }

        public override async void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);
            await OnCallBack(parameters);
        }
        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;
            _userId = await _userService.GetUserId();
            IsBusy = false;
        }
        #endregion

        #region OnCallBack
        private async Task OnCallBack(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters != null)
            {
                if (parameters[ArgKeys.User] is Users user)
                {
                    if (user.Id != _userId)
                    {
                        Admin = user;
                    }
                }
            }

            IsBusy = false;
        }
        #endregion
    }
}
