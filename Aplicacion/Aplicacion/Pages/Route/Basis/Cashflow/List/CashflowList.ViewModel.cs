using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Basis.Cashflow.Channels;
using Aplicacion.Pages.Route.Channels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Basis.Cashflow.List.ViewModel
{
    internal class CashflowList : ViewModelBase, ILoadCashflowListToBasisDetailsChannel, IBasisCashflowsChangedChannel
    {
        #region Properties
        private decimal _total;
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        private ObservableCollection<Cashflows> _budgetsCollection;
        public ObservableCollection<Cashflows> CashflowsCollection
        {
            get => _budgetsCollection;
            set => SetProperty(ref _budgetsCollection, value);
        }
        #endregion

        #region Methods

        private void OnLoadCashflowListToBasisDetails(ILoadCashflowListToBasisDetailsChannel sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Cashflows] is Cashflows[] budgets)
                {
                    Total = 0;
                    CashflowsCollection.Clear();
                    foreach (Cashflows budget in budgets)
                    {
                        Total += budget.Amount;
                        CashflowsCollection.Add(budget);
                    }
                }
            }
        }

        #endregion

        #region Constructor

        public CashflowList()
        {
            CashflowsCollection = new ObservableCollection<Cashflows>();

            MessagingCenter.Subscribe<ILoadCashflowListToBasisDetailsChannel, INavigationParameters>(this, nameof(ILoadCashflowListToBasisDetailsChannel), OnLoadCashflowListToBasisDetails);
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
                if (parameters[ArgKeys.Cashflows] is List<Cashflows> budgets)
                {
                    Total = 0;
                    CashflowsCollection = new ObservableCollection<Cashflows>();
                    foreach (Cashflows budget in budgets)
                    {
                        Total += budget.Amount;
                        CashflowsCollection.Add(budget);
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
                if (parameters[ArgKeys.Cashflow] is Cashflows budget)
                {
                    Total += budget.Amount;
                    CashflowsCollection.Add(budget);

                    INavigationParameters budgetListViewParameters = new NavigationParameters();
                    budgetListViewParameters.Add(ArgKeys.Cashflow, budget);

                    MessagingCenter.Send<IBasisCashflowsChangedChannel, INavigationParameters>(this, nameof(IBasisCashflowsChangedChannel),
                        budgetListViewParameters);
                }
            }
        }
        #endregion
    }
}
