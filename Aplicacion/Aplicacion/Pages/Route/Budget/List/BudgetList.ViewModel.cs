using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Aplicacion.Config;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;

namespace Aplicacion.Pages.Route.Budget.List.ViewModel
{
    internal class BudgetList : ViewModelBase
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
        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad(parameters);
        }
        #endregion

        #region OnLoad

        private void OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters != null && parameters.ContainsKey(ArgKeys.Budgets))
            {
                if (parameters[ArgKeys.Budgets] is Budgets[] budgets)
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
    }
}
