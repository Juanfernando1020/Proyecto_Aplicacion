using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Channels;
using Aplicacion.Pages.Route.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Budget.List.ViewModel
{
    internal class BudgetList : ViewModelBase, IRouteBudgetsChanged
    {
        #region Properties
        private ObservableCollection<Budgets> _budgetsCollection;

        public ObservableCollection<Budgets> BudgetsCollection
        {
            get => _budgetsCollection;
            set => SetProperty(ref _budgetsCollection, value);
        }
        #endregion

        #region Constructor

        public BudgetList()
        {
            BudgetsCollection = new ObservableCollection<Budgets>();
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

            if (parameters != null && parameters.ContainsKey(ArgKeys.Budgets))
            {
                BudgetsCollection = new ObservableCollection<Budgets>();
                if (parameters[ArgKeys.Budget] is List<Budgets> budgets)
                {
                    foreach (Budgets budget in budgets)
                    {
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
                    BudgetsCollection.Add(budget);

                    MessagingCenter.Send<IRouteBudgetsChanged, RouteBudgetsChangedArgs>(this, nameof(IRouteBudgetsChanged), 
                        new RouteBudgetsChangedArgs(false, budget));
                }
            }
        }
        #endregion
    }
}
