using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Expense.Channels;
using Aplicacion.Pages.Expense.Specifications;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Expense.List.ViewModel
{
    internal class ExpenseList : PageViewModelBase, IExpenseCreatedChannel
    {
        #region Variables

        private Routes _routeInfo;
        private readonly IGenericService<Expenses, Guid> _genericService;

        #endregion

        #region Properties

        private Expenses _selectedExpense;
        public Expenses SelectedExpense
        {
            get => _selectedExpense;
            set => SetProperty(ref _selectedExpense, value);
        }
        
        private ObservableCollection<Expenses> _expensesCollection;

        public ObservableCollection<Expenses> ExpensesCollection
        {
            get => _expensesCollection;
            set => SetProperty(ref _expensesCollection, value);
        }

        public ICommand GoToExpenseCreateCommand => new AsyncCommand(GoToExpenseCreateController);

        #endregion

        #region Methods

        private async Task GoToExpenseCreateController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Route, _routeInfo);

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Expense.Create, parameters: parameters);

            IsBusy = false;
        }
        private void OnExpenseCreated(IExpenseCreatedChannel sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Expense] is Expenses expense)
                {
                    ExpensesCollection.Add(expense);
                }
            }
        }

        #endregion

        #region Constructor

        public ExpenseList()
        {
            ExpensesCollection = new ObservableCollection<Expenses>();

            _genericService = GetGenericService<Expenses, Guid>();

            MessagingCenter.Subscribe<IExpenseCreatedChannel, INavigationParameters>(this, nameof(IExpenseCreatedChannel), OnExpenseCreated);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            MessagingCenter.Unsubscribe<IExpenseCreatedChannel, INavigationParameters>(this, nameof(IExpenseCreatedChannel));
        }

        #endregion

        #region OnLoan

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    _routeInfo = route;

                    ResultBase<IEnumerable<Expenses>> result = await _genericService.GetAllAsync(new ExpensesByRouteSpecification(route.Id));

                    if (result.IsSuccess)
                    {
                        if (result.Data is IEnumerable<Expenses> expenses)
                        {
                            foreach (Expenses expense in expenses)
                            {
                                ExpensesCollection.Add(expense);
                            }
                        }
                    }
                }
                else
                {
                    await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, ArgKeys.Route),
                        CommonMessages.Error.InformationMessage);
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.NullKey, nameof(parameters)),
                    CommonMessages.Error.InformationMessage);
            }
        }

        #endregion

        #region OnCallBack

        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Expense] is Expenses expense)
                {
                    ExpensesCollection.Add(expense);
                }
            }
        }

        #endregion
    }
}
