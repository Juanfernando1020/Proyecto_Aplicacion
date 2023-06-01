using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Channels;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Budget.List.ViewModel
{
    internal class BudgetList : ViewModelBase, IRouteBudgetsChangedChannel, ILoadBudgetListFromRouteDetailsChannel
    {
        #region Properties
        private decimal _total;
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        private ObservableCollection<Budgets> _budgetsCollection;
        public ObservableCollection<Budgets> BudgetsCollection
        {
            get => _budgetsCollection;
            set => SetProperty(ref _budgetsCollection, value);
        }
        #endregion

        #region Methods

        private void OnLoadBudgetListFromRouteDetails(object sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Budgets] is Budgets[] budgets)
                {
                    Total = 0;
                    BudgetsCollection = new ObservableCollection<Budgets>();
                    foreach (Budgets budget in budgets)
                    {
                        Total += budget.Amount;
                        BudgetsCollection.Add(budget);
                    }
                }
            }
        }

        #endregion

        #region Constructor

        public BudgetList()
        {
            BudgetsCollection = new ObservableCollection<Budgets>();

            MessagingCenter.Subscribe<ILoadBudgetListFromRouteDetailsChannel, INavigationParameters>(this, nameof(ILoadBudgetListFromRouteDetailsChannel), OnLoadBudgetListFromRouteDetails);
        }
        #endregion

        #region Overrides
        public override void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad(parameters);
        }

        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);

            OnCallBack(parameters);
        }

        #endregion

        #region OnLoad
        private void OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters != null)
            {
                if (parameters[ArgKeys.Budgets] is List<Budgets> budgets)
                {
                    Total = 0;
                    BudgetsCollection = new ObservableCollection<Budgets>();
                    foreach (Budgets budget in budgets)
                    {
                        Total += budget.Amount;
                        BudgetsCollection.Add(budget);
                    }
                }
            }

            IsBusy = false;
        }
        #endregion

        #region OnCallBack

        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Budget] is Budgets budget)
                {
                    Total += budget.Amount;
                    BudgetsCollection.Add(budget);

                    INavigationParameters budgetListViewParameters = new NavigationParameters();
                    budgetListViewParameters.Add(ArgKeys.Budget, budget);

                    MessagingCenter.Send<IRouteBudgetsChangedChannel, INavigationParameters>(this, nameof(IRouteBudgetsChangedChannel),
                        budgetListViewParameters);
                }
            }
        }
        #endregion
    }
}
