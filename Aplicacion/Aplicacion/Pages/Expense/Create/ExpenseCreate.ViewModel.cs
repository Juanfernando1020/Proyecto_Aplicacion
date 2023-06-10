using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Expense.Channels;
using Aplicacion.Pages.Expense.Config;
using Aplicacion.Pages.Route.Basis.Specifications;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Expense.Create.ViewModel
{
    internal class ExpenseCreate : PopupViewModelBase, IExpenseCreatedChannel
    {
        #region Variables

        private Routes _routeInfo;
        private Basises _basisInfo;
        private readonly IGenericService<Expenses, Guid> _genericService;
        private readonly IGenericService<Basises, Guid> _genericBasisService;

        #endregion

        #region Properties

        private Expenses _expense;
        public Expenses Expense
        {
            get => _expense;
            set => SetProperty(ref _expense, value);
        }

        public ICommand CreateExpenseCommand => new AsyncCommand(CreateExpenseController);

        #endregion

        #region Methods
        
        private async Task CreateExpenseController()
        {
            IsBusy = true;

            if (await IsValid())
            {
                Expense.RouteId = _routeInfo.Id;
                Expense.Date = DateTime.Now;
                ResultBase result = await _genericService.InsertAsync(Expense);

                if (result.IsSuccess)
                {
                    INavigationParameters parameters = new NavigationParameters();
                    parameters.Add(ArgKeys.Expense, Expense);

                    MessagingCenter.Send<IExpenseCreatedChannel, INavigationParameters>(this, nameof(IExpenseCreatedChannel), parameters);
                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                    await NavigationPopupService.PopPopupAsync(this, parameters: parameters);
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                }
            }

            IsBusy = false;
        }

        private async Task<bool> IsValid()
        {
            if (string.IsNullOrEmpty(Expense.Name)
                || string.IsNullOrEmpty(Expense.Description))
            {
                await AlertService.ShowAlert(new WarningMessage(CommonMessages.Form.NullOrEmptyInfo));

                return false;
            }

            if (Expense.Amount > _basisInfo.CashFlows.Sum(cashflow => cashflow.Amount))
            {
                await AlertService.ShowAlert(new WarningMessage(string.Format(CommonMessages.Warning.GreatherThanMaximum, _basisInfo.CashFlows.Sum(cashflow => cashflow.Amount))));

                return false;
            }

            return true;
        }

        #endregion

        #region Constructor

        public ExpenseCreate()
        {
            Expense = new Expenses()
            {
                Id = Guid.NewGuid()
            };

            _genericService = GetGenericService<Expenses, Guid>();
            _genericBasisService = GetGenericService<Basises, Guid>();
        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);

            await OnLoad(parameters);
        }

        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    _routeInfo = route;

                    ResultBase<Basises> result =
                        await _genericBasisService.GetBySpecificacionAsync(
                            new BasisByRouteIdAndDateSpecification(route.Id, DateTime.Now));

                    if (result.IsSuccess)
                    {
                        if (result.Data is Basises basis)
                        {
                            _basisInfo = basis;

                            if (!basis.IsActive)
                            {
                                await ShowErrorResultPopup(string.Empty, "La base del día ya ha sido cerrada.");
                            }
                        }
                        else
                        {
                            await AlertService.ShowAlert(new InfoMessage("No se te ha asignado una base del día."));
                            await NavigationPopupService.PopPopupAsync(this);
                        }
                    }
                    else
                    {
                        await AlertService.ShowAlert(new InfoMessage("No se te ha asignado una base del día."));
                        await NavigationPopupService.PopPopupAsync(this);
                    }
                }
            }
            else
            {
                await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                await NavigationPopupService.PopPopupAsync(this);
            }
            IsBusy = false;
        }

        #endregion
    }
}
