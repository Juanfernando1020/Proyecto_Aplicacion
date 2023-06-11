using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Client.Specifications;
using Aplicacion.Pages.Expense.Specifications;
using Aplicacion.Pages.Loan.Specifications;
using Aplicacion.Pages.Route.Basis.Cashflow.Enum;
using Aplicacion.Pages.Route.Basis.Specifications;
using Aplicacion.Pages.Route.Budget.Config;
using Aplicacion.Pages.Route.Budget.Enums;
using Aplicacion.Pages.Route.Specifications;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Day.Summary.ViewModel
{
    internal class DaySummary : PageViewModelBase
    {
        #region Variables

        private Users _userInfo;
        private Routes _routeInfo;
        private Routes[] _routes;

        private readonly IGenericService<Routes, Guid> _genericRoutesService;
        private readonly IGenericService<Basises, Guid> _genericBasisService;
        private readonly IGenericService<Expenses, Guid> _genericExpenseService;
        private readonly IGenericService<Clients, Guid> _genericClientService;
        private readonly IGenericService<Loans, Guid> _genericLoanService;
        private readonly IGenericService<Fees, Guid> _genericFeeService;

        #endregion

        #region Properties

        private decimal _expensesAmount;
        public decimal ExpensesAmount
        {
            get => _expensesAmount;
            set => SetProperty(ref _expensesAmount, value);
        }

        private decimal _balance;
        public decimal Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }
        
        private decimal _feesAmount;
        public decimal FeesAmount
        {
            get => _feesAmount;
            set => SetProperty(ref _feesAmount, value);
        }
        
        private decimal _feesQuantity;
        public decimal FeesQuantity
        {
            get => _feesQuantity;
            set => SetProperty(ref _feesQuantity, value);
        }

        private int _loansQuantity;
        public int LoansQuantity
        {
            get => _loansQuantity;
            set => SetProperty(ref _loansQuantity, value);
        }

        private Basises _basis;
        public Basises Basis
        {
            get => _basis;
            set => SetProperty(ref _basis, value);
        }

        private decimal _loansAmount;
        public decimal LoansAmount
        {
            get => _loansAmount;
            set => SetProperty(ref _loansAmount, value);
        }
        
        private bool _canCloseDay;
        public bool CanCloseDay
        {
            get => _canCloseDay;
            set => SetProperty(ref _canCloseDay, value);
        }

        public ICommand CloseDayCommand => new AsyncCommand(CloseDayController);

        #endregion

        #region Methods

        private async Task CloseDayController()
        {
            IsBusy = true;
            Basis.IsActive = false;
            ResultBase result = await _genericBasisService.UpdateAsync(new BasisFirebaseObjectByRouteIdAndDateSpecification(_routeInfo.Id, DateTime.Now), Basis.Id, Basis);

            if (result.IsSuccess)
            {
                List<Budgets> budgets = _routeInfo.Budgets.ToList();
                budgets.Add(new Budgets()
                {
                    Id = Guid.NewGuid(),
                    Type = (int)BudgetTypes.Deposit,
                    User = _userInfo,
                    Description = string.Format(BudgetDescriptions.CLOSE_DAY, DateTime.Now.ToString("MM/dd/yyyy"), _userInfo.Name),
                    Amount = Balance
                });
                _routeInfo.Budgets = budgets.ToArray();
                await _genericRoutesService.UpdateAsync(new RoutesByIdAndActiveStateSpecification(_routeInfo.Id, true),
                    _routeInfo.Id, _routeInfo);

                await AlertService.ShowAlert(new SuccessMessage("Se ha cerrado correctamente el día."));
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

        public DaySummary()
        {
            _genericRoutesService = GetGenericService<Routes, Guid>();
            _genericBasisService = GetGenericService<Basises, Guid>();
            _genericExpenseService = GetGenericService<Expenses, Guid>();
            _genericClientService = GetGenericService<Clients, Guid>();
            _genericLoanService = GetGenericService<Loans, Guid>();
            _genericFeeService = GetGenericService<Fees, Guid>();
        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);

            await OnLoad();
        }

        #endregion

        #region OnLoad

        private async Task OnLoad()
        {
            IsBusy = true;

            if (Aplicacion.Module.App.UserInfo is Users user && Aplicacion.Module.App.RouteInfo is Routes route)
            {
                _userInfo = user;
                _routeInfo = route;

                await GetRouteInfo(route);
                await Task.Yield();
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey,
                    $"{nameof(Aplicacion.Module.App.UserInfo)}-{Aplicacion.Module.App.RouteInfo}"), CommonMessages.Error.InformationMessage);
            }

            IsBusy = false;
        }

        private async Task GetRouteInfo(Routes route)
        {
            Budgets[] budgets = route.Budgets;
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Task.WhenAll(
                    GetExpensesInfo(route.Id),
                    GetLoansInfo(route.Id),
                    GetBasisInfo(route.Id)
                );
            });
            Balance = Basis.CashFlows.Sum(cashflow => cashflow.Amount);
            FeesAmount = Basis.CashFlows.Where(cashflow => cashflow.Type == (int)CashflowTypes.Collection).Sum(cashflow => cashflow.Amount);
            FeesQuantity = Basis.CashFlows.Count(cashflow => cashflow.Type == (int)CashflowTypes.Collection);
        }

        private async Task GetLoansInfo(Guid routeId)
        {
            Clients[] clients = await GetClientsInfo(routeId);

            decimal totalLoansAmount = 0;

            await Task.Yield();
            if (clients != null)
            {
                foreach (Clients client in clients)
                {
                    ResultBase<IEnumerable<Loans>> result = await _genericLoanService.GetAllAsync(new LoansByClientIdSpecification(client.Id));

                    if (result.IsSuccess && result.Data is IEnumerable<Loans> loans)
                    {
                        LoansQuantity = loans.Count(new LoansByClientIdAndDateSpecification(client.Id, DateTime.Now));

                        totalLoansAmount += loans.Sum(loan => loan.Amount);
                    }
                    else
                    {
                        await ShowErrorResult(string.Format(CommonMessages.Console.ResultIsNotSuccess, nameof(_genericExpenseService), "GetAllAsync"), CommonMessages.Error.InformationMessage);
                        break;
                    }
                }
            }

            LoansAmount = totalLoansAmount;
        }

        private async Task<Clients[]> GetClientsInfo(Guid routeId)
        {
            ResultBase<IEnumerable<Clients>> result = await _genericClientService.GetAllAsync(new ClientsByRouteIdSpecification(routeId));

            if (result.IsSuccess && result.Data is IEnumerable<Clients> clients)
            {
                return clients.ToArray();
            }
            else
            {
                return null;
            }
        }

        private async Task GetExpensesInfo(Guid routeId)
        {
            await Task.Yield();
            ResultBase<IEnumerable<Expenses>> result = await _genericExpenseService.GetAllAsync(new ExpensesByRouteAndDateSpecification(routeId,DateTime.Now.Date));

            if (result.IsSuccess && result.Data is IEnumerable<Expenses> expenses)
            {
                ExpensesAmount = expenses.Sum(expense => expense.Amount);
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.ResultIsNotSuccess, nameof(_genericExpenseService), "GetAllAsync"), CommonMessages.Error.InformationMessage);
            }
        }

        private async Task GetBasisInfo(Guid routeId)
        {
            ResultBase<Basises> result = await _genericBasisService.GetBySpecificacionAsync(new BasisByRouteIdAndDateSpecification(routeId, DateTime.Now));

            if (result.IsSuccess && result.Data is Basises basis)
            {
                Basis = basis;
                CanCloseDay = basis.IsActive;
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.ResultIsNotSuccess, nameof(_genericBasisService), "GetBySpecificacionAsync"), "No se ha generado base del día.");
            }
        }

        #endregion
    }
}
