using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Fee.Channels;
using Aplicacion.Pages.Loan.Installment.Fee.Config;
using Aplicacion.Pages.Loan.Specifications;
using Aplicacion.Pages.Route.Basis.Cashflow.Enum;
using Aplicacion.Pages.Route.Basis.Config;
using Aplicacion.Pages.Route.Basis.Specifications;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Installment.Fee.Create.ViewModel
{
    internal class FeeCreate : PopupViewModelBase, IFeeCreatedChannel
    {
        #region Variables
        private Installments _installmentInfo;
        private Loans _loanInfo;
        private Basises _basisInfo;
        private readonly IGenericService<Fees, Guid> _genericFeeService;
        private readonly IGenericService<Loans, Guid> _genericLoanService;
        private readonly IGenericService<Basises, Guid> _genericBasisService;

        #endregion

        #region Properties

        private Fees _fee;
        public Fees Fee
        {
            get => _fee;
            set => SetProperty(ref _fee, value);
        }



        public ICommand CreateFeeCommand => new AsyncCommand(CreateFeeController);

        #endregion

        #region Methods

        private async Task CreateFeeController()
        {
            IsBusy = true;

            if (await IsValid(Fee))
            {
                List<Installments> installments = _loanInfo.Installments.ToList();
                Fee.Date = DateTime.Now;
                Fee.InstallmentId = _installmentInfo.Id;

                if (installments.Any(i => i == _installmentInfo))
                {
                    ResultBase result = await _genericFeeService.InsertAsync(Fee);

                    if (result.IsSuccess)
                    {
                        if (Fee.Amount == _installmentInfo.Amount)
                        {
                            installments.Remove(_installmentInfo);
                            _installmentInfo.IsActive = false;
                            installments.Add(_installmentInfo);
                            _loanInfo.IsActive = installments.Any(installment => installment.IsActive);
                            _loanInfo.Installments = installments.ToArray();

                            ResultBase resultUpdate = await _genericLoanService
                                .UpdateAsync(new LoansFirebaseObjectByClientIdSpecification(_loanInfo.ClientId), _loanInfo.Id, _loanInfo);

                            if (resultUpdate.IsSuccess)
                            {
                                Users user = Aplicacion.Module.App.UserInfo;
                                Routes route = Aplicacion.Module.App.RouteInfo;

                                List<Cashflows> cashflows = _basisInfo.CashFlows.ToList();
                                cashflows.Add(new Cashflows()
                                {
                                    Description = string.Format(BasisDescriptions.ADD_FEE, user.Name),
                                    Type = (int)CashflowTypes.Collection,
                                    Amount = Fee.Amount,
                                });

                                _basisInfo.CashFlows = cashflows.ToArray();

                                INavigationParameters parameters = new NavigationParameters();
                                parameters.Add(ArgKeys.Fee, Fee);

                                MessagingCenter.Send<IFeeCreatedChannel, INavigationParameters>(this, nameof(IFeeCreatedChannel), parameters);
                                await _genericBasisService
                                    .UpdateAsync(new BasisFirebaseObjectByRouteIdAndDateSpecification(route.Id, DateTime.Now), _basisInfo.Id, _basisInfo);
                                await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                                await NavigationPopupService.PopPopupAsync(this);
                            }
                        }
                        else
                        {
                            await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                            await NavigationPopupService.PopPopupAsync(this);
                        }
                    }
                    else
                    {
                        await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                    }
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                }

                
            }

            IsBusy = false;
        }

        private async Task<bool> IsValid(Fees fee)
        {
            if (fee.Amount < FeeConfig.MINIMUM_AMOUNT)
            {
                await AlertService.ShowAlert(new WarningMessage(string.Format(CommonMessages.Warning.LessThanMinimum,
                    FeeConfig.MINIMUM_AMOUNT)));
                return false;
            }
            
            if (fee.Amount > _installmentInfo.Amount)
            {
                await AlertService.ShowAlert(new WarningMessage(string.Format(CommonMessages.Warning.GreatherThanMaximum,
                    _installmentInfo.Amount)));
                return false;
            }

            return true;
        }

        #endregion

        #region Constructor

        public FeeCreate()
        {
            Fee = new Fees()
            {
                Id = Guid.NewGuid(),
            };
            _genericFeeService = GetGenericService<Fees, Guid>();
            _genericLoanService = GetGenericService<Loans, Guid>();
            _genericBasisService = GetGenericService<Basises, Guid>();
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
            IsBusy = true;
            if (parameters != null)
            {
                if (parameters[ArgKeys.Installment] is Installments installment &&
                    parameters[ArgKeys.Loan] is Loans loan &&
                    Aplicacion.Module.App.RouteInfo is Routes route)
                {
                    if (loan.Installments.Any(i => i.Id == installment.Id))
                    {
                        _installmentInfo = installment;
                        _loanInfo = loan;

                        ResultBase<Basises> result =
                            await _genericBasisService.GetBySpecificacionAsync(
                                new BasisByRouteIdAndDateSpecification(route.Id, DateTime.Now));

                        if (result.IsSuccess && result.Data is Basises basis)
                        {
                            _basisInfo = basis;
                        }
                        else
                        {
                            await ShowErrorResultPopup("The data doesn't contains the correct information.", CommonMessages.Error.InformationMessage);
                        }

                    }
                    else
                    {
                        await ShowErrorResultPopup("The data doesn't contains the correct information.", CommonMessages.Error.InformationMessage);
                    }
                }
                else
                {
                    await ShowErrorResultPopup(string.Format(CommonMessages.Console.MissingKey, ArgKeys.Installment + "-" + ArgKeys.Loan), CommonMessages.Error.InformationMessage);
                }
            }
            else
            {
                await ShowErrorResultPopup(string.Format(CommonMessages.Console.NullKey, nameof(parameters)),
                    CommonMessages.Error.InformationMessage);
            }
            IsBusy = false;
        }

        #endregion
    }
}
