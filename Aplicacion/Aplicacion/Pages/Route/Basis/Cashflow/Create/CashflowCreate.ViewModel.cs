using Aplicacion.Config.Routes;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Enums;
using Aplicacion.Pages.User.Specifications;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Linq;
using Aplicacion.Config.Messages;
using Aplicacion.Pages.Route.Basis.Cashflow.Enum;
using Aplicacion.Pages.Route.Basis.Config;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Basis.Cashflow.Create.ViewModel
{
    public class CashflowCreate : PopupViewModelBase
    {
        #region Variables

        private Routes _routeInfo;
        private decimal _total;

        #endregion

        #region Properties

        private Cashflows _cashflow;
        public Cashflows Cashflow
        {
            get => _cashflow;
            set => SetProperty(ref _cashflow, value);
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }
        
        private decimal _availableAmount;
        public decimal AvailableAmount
        {
            get => _availableAmount;
            set => SetProperty(ref _availableAmount, value);
        }

        public ICommand SendCashflowCommand => new Command(async () => await SendCashflowController());
        #endregion

        #region Methods
        private async Task SendCashflowController()
        {
            IsBusy = true;

            Cashflow.Amount = Amount;
            Cashflow.Date = DateTime.Now;

            if (await Validate(Cashflow))
            {
                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Cashflow, Cashflow);

                await NavigationPopupService.PopPopupAsync(this, parameters: parameters);
            }

            IsBusy = false;
        }

        private async Task<bool> Validate(Cashflows cashflow)
        {
            if (cashflow.Amount < BasisConfig.MIN_BASIS)
            {
                await AlertService.ShowAlert(new WarningMessage(string.Format(CommonMessages.Warning.LessThanMinimum,
                    BasisConfig.MIN_BASIS)));

                return false;
            }

            return true;
        }
        #endregion

        #region Constructor

        public CashflowCreate()
        {
            Cashflow = new Cashflows
            {
                Description = string.Format(BasisDescriptions.ADD_BASIS, Aplicacion.Module.App.UserInfo.Name),
                Type = (int)CashflowTypes.Deposit
            };
        }
        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            await OnLoad(parameters);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Amount):
                    if (Amount < BasisConfig.MIN_BASIS)
                    {
                        Amount = BasisConfig.MIN_BASIS;
                    }else if (Amount > _total)
                    {
                        Amount = _total;
                    }
                    else
                    {
                        AvailableAmount = _total - Amount;
                    }
                    break;
            }

        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (Aplicacion.Module.App.RouteInfo is Routes route)
            {
                _routeInfo = route;
                _total = route.Budgets.Sum(budget => budget.Amount);
                AvailableAmount = _total;
            }
            else
            {
                await ShowErrorResultPopup(string.Format(CommonMessages.Console.NullKey, nameof(Aplicacion.Module.App.RouteInfo)), CommonMessages.Error.InformationMessage);
            }
        }

        #endregion
    }
}
