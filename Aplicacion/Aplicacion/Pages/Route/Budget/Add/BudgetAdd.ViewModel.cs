using System;
using Aplicacion.Config;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Common;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Budget.Add.ViewModel
{
    internal class BudgetAdd : PopupViewModelBase
    {
        #region Properties

        private Budgets _budget;

        public Budgets Budget
        {
            get => _budget;
            set => SetProperty(ref _budget, value);
        }

        public ICommand SendBudgetCommand => new Command(async () => await SendBudgetController());
        #endregion

        #region Methods
        private async Task SendBudgetController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Budget, Budget);

            await NavigationPopupService.PopPopupAsync(this, parameters: parameters);

            IsBusy = false;
        }
        #endregion

        #region Overrides
        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);

            await OnLoad(parameters);
        }
        #endregion

        #region OnLoad
        private async Task OnLoad(INavigationParameters parameters)
        {
        }
        #endregion
    }
}
