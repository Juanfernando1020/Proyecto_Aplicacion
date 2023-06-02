using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Contracts;
using Aplicacion.Pages.User.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Pages.User.Enums;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Aplicacion.Config.Messages;
using Aplicacion.Config.Routes;
using Aplicacion.Pages.Route.Channels;

namespace Aplicacion.Pages.Route.Details.ViewModel
{
    internal class RouteDetails : PageViewModelBase, IRouteBudgetsChangedChannel, ILoadBudgetListFromRouteDetailsChannel
    {
        #region Variables
        private readonly IRouteService _routeService;
        private readonly List<Budgets> budgets = new List<Budgets>();
        #endregion

        #region Properties
        private Routes _route;
        public Routes Route
        { 
            get => _route;
            set
            {
                SetProperty(ref _route, value);
            }
        }

        private Users _worker;
        public Users Worker
        {
            get => _worker;
            set => SetProperty(ref _worker, value);
        }
        
        private bool _isCreate;
        public bool IsCreate
        {
            get => _isCreate;
            set => SetProperty(ref _isCreate, value);
        }

        private INavigationParameters _budgetListParameters;
        public INavigationParameters BudgetListParameters
        {
            get => _budgetListParameters;
            set => SetProperty(ref _budgetListParameters, value);
        }

        public ICommand OpenUserBySpecificationPopupCommand => new Command(async () => await OpenUserBySpecificationPopupController());
        public ICommand OpenBudgetCreatePopupCommand => new Command(async () => await OpenBudgetCreatePopupController());
        public ICommand GoToBasisDetailsCommand => new Command(async () => await GoToBasisDetailsController());

        public ICommand CreateCommand => new Command(async () => await CreateController());
        #endregion

        #region Methods
        private async Task GoToBasisDetailsController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Route, Route);

            await NavigationService.NavigateToAsync(PagesRoutes.Route.Basis.Details, parameters: parameters);

            IsBusy = false;
        }
        private async Task OpenUserBySpecificationPopupController()
        {
            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Specification, new UserByRoleSpecification(RolesEnum.Worker));

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.User.UserBySpecification, parameters: parameters);
        }
        private async Task CreateController()
        {
            IsBusy = true;
            ResultBase result = await _routeService.CreateAsync(Route);

            if (result.IsSuccess)
            {
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Route, Route);

                await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.InformationMessage));
                await NavigationService.PopAsync(parameters: parameters);
            }
            else
            {
                await AlertService.ShowAlert(new WarningMessage(result.Message));
            }
            IsBusy = false;
        }
        private async Task OpenBudgetCreatePopupController()
        {
            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Route.Budget.BudgetCreate);
        }

        private void OnRouteBudgetsChanged(object sender, INavigationParameters parameters)
        {
            if (parameters == null)
                return;

            if (parameters[ArgKeys.Budget] is Budgets budget)
            {
                if (parameters["IsDeleted"] is bool isDeleted)
                {
                    if (isDeleted)
                    {
                        budgets.Remove(budget);
                    }
                    else
                    {
                        AddBudget(budget);
                    }
                }
                else
                {
                    AddBudget(budget);
                }
            }

            Route.Budgets = budgets.ToArray();
        }

        private void AddBudget(Budgets budget)
        {
            if (budget != null)
            {
                if (budgets.Find(b => b.Admin.Id == budget.Admin.Id) is Budgets budgetOld)
                {
                    int index = budgets.IndexOf(budgetOld);

                    budgets[index] = new Budgets()
                    {
                        Id = budgetOld.Id,
                        Admin = budgetOld.Admin,
                        Amount = budgetOld.Amount + budget.Amount
                    };
                }
                else
                {
                    budgets.Add(budget);
                }
            }
        }
        #endregion

        #region Constructor
        public RouteDetails()
        {
            IsCreate = true;
            Route = new Routes(Guid.NewGuid(), string.Empty, string.Empty, true, null, null);

            _routeService = new Service.Route(new Repository.Route());

            MessagingCenter.Subscribe<IRouteBudgetsChangedChannel, INavigationParameters>(this, nameof(IRouteBudgetsChangedChannel), OnRouteBudgetsChanged);
        }
        #endregion

        #region Overrides
        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }
        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);
            OnCallBack(parameters);
        }


        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Worker):
                    Route.Worker = Worker;
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            MessagingCenter.Unsubscribe<IRouteBudgetsChangedChannel, INavigationParameters>(this, nameof(IRouteBudgetsChangedChannel));
        }

        #endregion

        #region OnLoad
        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    IsCreate = false;
                    Route = route;
                    Worker = route.Worker;

                    INavigationParameters budgetListViewParameters = new NavigationParameters();
                    budgetListViewParameters.Add(ArgKeys.Budgets, route.Budgets);

                    MessagingCenter.Send<ILoadBudgetListFromRouteDetailsChannel, INavigationParameters>(this, nameof(ILoadBudgetListFromRouteDetailsChannel), budgetListViewParameters);
                }
                else if (parameters[ArgKeys.User] is Users user)
                {
                    Route.Manager = user;
                }
                else
                {
                    await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(ArgKeys.User)), CommonMessages.Error.InformationMessage);
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(ArgKeys.User)), CommonMessages.Error.InformationMessage);
            }
            IsBusy = false;
        }

        #endregion

        #region OnCallBack
        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.User] is Users user)
                    Worker = user;

                if (parameters[ArgKeys.Budget] is Budgets budget)
                {
                    INavigationParameters budgetListParameters = new NavigationParameters();
                    budgetListParameters.Add(ArgKeys.Budget, budget);

                    BudgetListParameters = budgetListParameters;
                }
            }
        }
        #endregion
    }
}
