using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Client.Specifications;
using Aplicacion.Pages.Day.Enums;
using Aplicacion.Pages.Day.Models;
using Aplicacion.Pages.Expense.Specifications;
using Aplicacion.Pages.Loan.Installment.Fee.Specifications;
using Aplicacion.Pages.Loan.Specifications;
using Aplicacion.Pages.Route.Basis.Specifications;
using Aplicacion.Pages.Route.Budget.Enums;
using Aplicacion.Pages.Route.Budget.Specifications;
using Aplicacion.Pages.Route.Specifications;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
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

                //ResultBase<IEnumerable<Routes>> result = await _genericRoutesService.GetAllAsync(new RoutesByUserIdSpecification(user.Id));

                //if (result.IsSuccess && result.Data is IEnumerable<Routes> routes)
                //{
                //    _routes = routes.ToArray();
                //}
                //else
                //{
                //    await ShowErrorResult(string.Format(CommonMessages.Console.ResultIsNotSuccess, nameof(_genericRoutesService), "GetAllAsync"), CommonMessages.Error.InformationMessage);
                //}

                await GetRouteInfo(route);
                await Task.Yield();
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey,
                    $"{nameof(Aplicacion.Module.App.UserInfo)}-{Aplicacion.Module.App.RouteInfo}"), CommonMessages.Error.InformationMessage);
            }

            //Filters = new List<DaySummaryFilter>()
            //{
            //    new DaySummaryFilter()
            //    {
            //        Title = "Todas las Rutas",
            //        Type = DaySummaryFilterTypes.AllRoutes
            //    },
            //    new DaySummaryFilter()
            //    {
            //        Title = "Por Ruta",
            //        Type = DaySummaryFilterTypes.ByRoute,
            //    },
            //    new DaySummaryFilter()
            //    {
            //        Title = "Por Trabajador",
            //        Type = DaySummaryFilterTypes.ByWorker,
            //    }
            //};

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
            Balance = (Basis.Amount + FeesAmount)-(ExpensesAmount + LoansAmount);
            FeesAmount = budgets.Where(new BudgetsByDateAndTypeSpecification(DateTime.Now, BudgetTypes.Collection)).Sum(budget => budget.Amount);
            FeesQuantity = budgets.Where(new BudgetsByDateAndTypeSpecification(DateTime.Now, BudgetTypes.Collection)).Count();
        }

        private async Task GetLoansInfo(Guid routeId)
        {
            Clients[] clients = await GetClientsInfo(routeId);

            decimal totalLoansAmount = 0; 

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
            ResultBase<IEnumerable<Expenses>> result = await _genericExpenseService.GetAllAsync(new ExpensesByRouteSpecification(routeId));

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
            ResultBase<Basises> result = await _genericBasisService.GetBySpecificacionAsync(new BasisByRouteIdAndDateNowSpecification(routeId));

            if (result.IsSuccess && result.Data is Basises basis)
            {
                Basis = basis;
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.ResultIsNotSuccess, nameof(_genericBasisService), "GetBySpecificacionAsync"), "No se ha generado base del día.");
            }
        }

        #endregion
    }
}
