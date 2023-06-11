using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Basis.Cashflow.Channels;
using Aplicacion.Pages.Route.Basis.Cashflow.Enum;
using Aplicacion.Pages.Route.Basis.Config;
using Aplicacion.Pages.Route.Basis.Specifications;
using Aplicacion.Pages.Route.Budget.Config;
using Aplicacion.Pages.Route.Budget.List.ViewModel;
using Aplicacion.Pages.Route.Channels;
using Aplicacion.Pages.Route.Config;
using Aplicacion.Pages.Route.Specifications;
using Aplicacion.Pages.User.Enums;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using static Aplicacion.Config.Routes.PopupsRoutes.Route;

namespace Aplicacion.Pages.Route.Basis.Details.ViewModel
{
    internal class BasisDetails : PageViewModelBase, IRouteBudgetsChangedChannel, ILoadCashflowListToBasisDetailsChannel
    {
        #region Variables
        private List<Cashflows> _cashflows = new List<Cashflows>();
        private List<Cashflows> _updateCashFlow = new List<Cashflows>();
        private Users _userInfo;
        private Routes _routeInfo;
        private readonly IGenericService<Basises, Guid> _genericService;
        private readonly IGenericService<Routes, Guid> _genericRouteService;
        #endregion

        #region Properties
        private bool _canCreate;
        public bool CanCreate
        {
            get => _canCreate;
            set => SetProperty(ref _canCreate, value);
        }
        
        private bool _canUpdate;
        public bool CanUpdate
        {
            get => _canUpdate;
            set => SetProperty(ref _canUpdate, value);
        }

        private Basises _basis;
        public Basises Basis
        {
            get => _basis;
            set => SetProperty(ref _basis, value);
        }

        public ICommand CreateBasisCommand => new AsyncCommand(CreateBasisController);
        public ICommand OpenCashflowCreateCommand => new AsyncCommand(OpenCashflowCreateController);
        public ICommand UpdateBasisCommand => new AsyncCommand(UpdateBasisController);

        #endregion

        #region Methods

        private async Task CreateBasisController()
        {
            decimal amount = _cashflows.Sum(c => c.Amount);
            List<Cashflows> cashFlows = new List<Cashflows>();
            List<Budgets> budgetList = _routeInfo.Budgets.ToList();
            IsBusy = true;

            Basis.Date = DateTime.Now;
            cashFlows.Add(new Cashflows()
            {
                Description = string.Format(BasisDescriptions.ADD_BASIS, _userInfo.Name),
                Type = (int)CashflowTypes.Deposit,
                Amount = amount
            });
            Basis.CashFlows = cashFlows.ToArray();

            ResultBase result = await _genericService.InsertAsync(Basis);

            if (result.IsSuccess)
            {
                Budgets newBudget = new Budgets()
                {
                    Id = Guid.NewGuid(),
                    Description = string.Format(BudgetDescriptions.ADD_BASIS, _userInfo.Name),
                    User = _userInfo,
                    Amount = -amount
                };

                budgetList.Add(newBudget);
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Budget, newBudget);

                _routeInfo.Budgets = budgetList.ToArray();

                ResultBase routeResult = await _genericRouteService
                    .UpdateAsync(new RoutesByIdAndActiveStateSpecification(_routeInfo.Id, true), _routeInfo.Id, _routeInfo);

                if (routeResult.IsSuccess)
                {
                    Aplicacion.Module.App.RouteInfo = _routeInfo;
                    MessagingCenter.Send<IRouteBudgetsChangedChannel, INavigationParameters>(this, nameof(IRouteBudgetsChangedChannel), parameters);
                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                    await NavigationService.PopAsync();
                }
                else
                {
                    await _genericService.DeleteAsync(Basis.Id);
                    await AlertService.ShowAlert(new ErrorMessage(routeResult.Message));
                }
            }
            else
            {
                await AlertService.ShowAlert(new ErrorMessage(result.Message));
            }

            IsBusy = false;
        }

        private async Task OpenCashflowCreateController()
        {
            IsBusy = true;

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Route.Basis.Cashflow.CashflowCreate);

            IsBusy = false;
        }

        private async Task UpdateBasisController()
        {
            decimal amount = _updateCashFlow.Sum(c => c.Amount);
            List<Budgets> budgetList = _routeInfo.Budgets.ToList();
            Budgets newBudget = new Budgets()
            {
                Id = Guid.NewGuid(),
                Description = string.Format(BudgetDescriptions.ADD_BASIS, _userInfo.Name),
                User = _userInfo,
                Amount = -amount
            };
            budgetList.Add(newBudget);
            _routeInfo.Budgets = budgetList.ToArray();

            IsBusy = true;
            Basis.CashFlows = _cashflows.ToArray();

            ResultBase result = await _genericService.UpdateAsync(new BasisFirebaseObjectByRouteIdAndDateSpecification(_routeInfo.Id, DateTime.Now), Basis.Id, Basis);

            if (result.IsSuccess)
            {
                await _genericRouteService
                    .UpdateAsync(new RoutesByIdAndActiveStateSpecification(_routeInfo.Id, true), _routeInfo.Id, _routeInfo);
                await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                await NavigationService.PopAsync();
            }
            else
            {
                await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            }

            IsBusy = false;
        }

        #endregion

        #region Constructor

        public BasisDetails()
        {
            CanUpdate = false;
            CanCreate = false;
            Basis = new Basises()
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Date = DateTime.Now
            };
            _genericService = GetGenericService<Basises, Guid>();
            _genericRouteService = GetGenericService<Routes, Guid>();
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

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (Aplicacion.Module.App.UserInfo is Users user)
            {
                _userInfo = user;
            }

            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    _routeInfo = route;
                    ResultBase<Basises> result = await _genericService.GetBySpecificacionAsync(new BasisByRouteIdAndDateSpecification(route.Id, DateTime.Now));

                    if (result.IsSuccess)
                    {
                        if (result.Data is Basises basis)
                        {
                            Basis = basis;

                            _cashflows = basis.CashFlows.ToList();

                            INavigationParameters cashflowListParameters = new NavigationParameters();
                            cashflowListParameters.Add(ArgKeys.Cashflows, basis.CashFlows);

                            MessagingCenter.Send<ILoadCashflowListToBasisDetailsChannel, INavigationParameters>(this, nameof(ILoadCashflowListToBasisDetailsChannel), cashflowListParameters);
                        }
                        else
                        {
                            await ShowErrorInit(CommonMessages.Console.NullDataWhenIsSuccess);
                        }
                    }
                    else
                    {
                        if (_userInfo.Role == (int)RolesEnum.Admin)
                        {
                            Basis.Route = route.Id;
                            CanCreate = true;
                        }
                        else
                        {
                            await NavigationService.PopAsync();
                        }

                        await AlertService.ShowAlert(new InfoMessage("No se ha encontrado una base para el día de hoy."));
                    }
                }
                else
                {
                    await ShowErrorInit(String.Format(CommonMessages.Console.MissingKey, ArgKeys.Route));
                }
            }
            else
            {
                await ShowErrorInit(String.Format(CommonMessages.Console.NullKey, nameof(parameters)));
            }
        }

        private async Task ShowErrorInit(string consoleError)
        {
            Console.WriteLine(consoleError);
            await AlertService.ShowAlert(new InfoMessage(CommonMessages.Error.InformationMessage));
            await NavigationService.PopAsync();
        }
        #endregion

        #region OnCallBack

        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Cashflow] is Cashflows cashflow)
                {
                    CanUpdate = !CanCreate;
                    _cashflows.Add(cashflow);
                    if (CanUpdate)
                    {
                        _updateCashFlow.Add(cashflow);
                    }

                    INavigationParameters cashflowListParameters = new NavigationParameters();
                    cashflowListParameters.Add(ArgKeys.Cashflows, _cashflows.ToArray());

                    MessagingCenter.Send<ILoadCashflowListToBasisDetailsChannel, INavigationParameters>(this, nameof(ILoadCashflowListToBasisDetailsChannel), cashflowListParameters);
                }
            }
        }

        #endregion
    }
}
