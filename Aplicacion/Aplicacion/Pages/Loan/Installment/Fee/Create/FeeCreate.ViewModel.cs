using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Enums;
using Aplicacion.Pages.Loan.Installment.Fee.Channels;
using Aplicacion.Pages.Loan.Installment.Fee.Config;
using Aplicacion.Pages.Loan.Installment.Models;
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
        private InstallmentExtension _installmentInfo;
        private Loans _loanInfo;
        private Basises _basisInfo;
        private Users _userInfo;
        private Routes _routeInfo;
        private List<Fees> _fees;
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
                
                Fee.Date = DateTime.Now;
                Fee.InstallmentId = _installmentInfo.Installment.Id;

                if (await UpdateInfo())
                {
                    INavigationParameters parameters = new NavigationParameters();
                    parameters.Add(ArgKeys.Fee, Fee);

                    MessagingCenter.Send<IFeeCreatedChannel, INavigationParameters>(this, nameof(IFeeCreatedChannel), parameters);
                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                    await NavigationPopupService.PopPopupAsync(this);
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                }
            }

            IsBusy = false;
        }

        private async Task<bool> UpdateInfo()
        {
            ResultBase result = await _genericFeeService.InsertAsync(Fee);

            return result.IsSuccess && (await UpdateInstallments() && await UpdateBasis());
        }

        private async Task<bool> UpdateBasis()
        {
            List<Cashflows> cashflows = _basisInfo.CashFlows.ToList();
            cashflows.Add(new Cashflows()
            {
                Description = string.Format(BasisDescriptions.ADD_FEE, _userInfo.Name),
                Type = (int)CashflowTypes.Collection,
                Amount = Fee.Amount,
            });
            _basisInfo.CashFlows = cashflows.ToArray();

            ResultBase result = await _genericBasisService
                .UpdateAsync(new BasisFirebaseObjectByRouteIdAndDateSpecification(_routeInfo.Id, DateTime.Now), _basisInfo.Id, _basisInfo);

            return result.IsSuccess;
        }

        private async Task<bool> UpdateInstallments()
        {
            Installments installmentInfo = _installmentInfo.Installment;
            List<Installments> installments = _loanInfo.Installments.ToList();
            
            if (installments.All(installment => installment != installmentInfo))
                return false;

            installments.Remove(installmentInfo);

            installmentInfo.Status = Fee.Amount == installmentInfo.DiferenceAmount
                ? (int)InstallmentStatusEnum.Complete
                : Fee.Amount > 0 && installmentInfo.Status == (int)InstallmentStatusEnum.Backlog
                    ? (int)InstallmentStatusEnum.Progress
                    : installmentInfo.Status;
            _installmentInfo.Installment = installmentInfo;
            installments.Add(installmentInfo);

            _loanInfo.IsActive = installments.All(installment => installment.Status == (int)InstallmentStatusEnum.Complete);
            _loanInfo.Installments = installments.ToArray();

            ResultBase result = await _genericLoanService
                .UpdateAsync(new LoansFirebaseObjectByClientIdSpecification(_loanInfo.ClientId), _loanInfo.Id, _loanInfo);

            return result.IsSuccess;
        }

        private async Task<bool> IsValid(Fees fee)
        {
            Installments installmentInfo = _installmentInfo.Installment;
            if (fee.Amount < 0)
            {
                await AlertService.ShowAlert(new WarningMessage(string.Format(CommonMessages.Warning.LessThanMinimum,
                    0)));
                return false;
            }
            
            if (fee.Amount > installmentInfo.DiferenceAmount)
            {
                await AlertService.ShowAlert(new WarningMessage(string.Format(CommonMessages.Warning.GreatherThanMaximum,
                    installmentInfo.DiferenceAmount)));
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
                if (parameters[ArgKeys.InstallmentExtension] is InstallmentExtension installmentExtension &&
                    parameters[ArgKeys.Loan] is Loans loan &&
                    Aplicacion.Module.App.RouteInfo is Routes route)
                {
                    if (loan.Installments.Any(i => i.Id == installmentExtension.Installment.Id))
                    {
                        _fees = installmentExtension.Fees;
                        _installmentInfo = installmentExtension;
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
