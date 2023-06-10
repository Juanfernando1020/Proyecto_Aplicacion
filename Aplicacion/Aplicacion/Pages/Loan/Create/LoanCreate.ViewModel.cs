using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Channels;
using Aplicacion.Pages.Loan.Config;
using Aplicacion.Pages.Loan.Installment.Enums;
using Aplicacion.Pages.Loan.Installment.Models;
using Aplicacion.Pages.Loan.Models;
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

namespace Aplicacion.Pages.Loan.Create.ViewModel
{
    internal class LoanCreate : PopupViewModelBase, ILoanCreatedChannel
    {
        #region Variables

        private Clients _clientInfo;
        private Basises _basisInfo;
        private readonly IGenericService<Loans, Guid> _genericService;
        private readonly IGenericService<Basises, Guid> _genericBasisService;

        #endregion

        #region Properties

        private InstallmentOptions _selectedInstallmentOptions;

        public InstallmentOptions SelectedInstallmentOptions
        {
            get => _selectedInstallmentOptions;
            set => SetProperty(ref _selectedInstallmentOptions, value);
        }

        private LoansExtension _loanExtension;
        public LoansExtension LoanExtension
        {
            get => _loanExtension;
            set => SetProperty(ref _loanExtension, value);
        }

        private List<InstallmentOptions> _installmentOptionsList;
        public List<InstallmentOptions> InstallmentOptionsList
        {
            get => _installmentOptionsList;
            set => SetProperty(ref _installmentOptionsList, value);
        }

        private bool _canHaveASurcharge;
        public bool CanHaveASurcharge
        {
            get => _canHaveASurcharge;
            set => SetProperty(ref _canHaveASurcharge, value);
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

        public ICommand CancelCommand => new AsyncCommand(CancelController);

        public ICommand CreateLoanCommand => new AsyncCommand(CreateLoanController);

        #endregion

        #region Methods
        private async Task CancelController()
        {
            IsBusy = true;

            await NavigationPopupService.PopPopupAsync(this);

            IsBusy = false;
        }

        private async Task CreateLoanController()
        {
            IsBusy = true;

            LoanExtension.Loan.Amount = Amount;

            if (await IsValid(LoanExtension.Loan))
            {
                LoanExtension.Loan.InstallmentType = (int)SelectedInstallmentOptions.InstallmentType;
                if (await AddInstallments(LoanExtension.Loan, LoanExtension.FirstInstallmentDate))
                {
                    LoanExtension.Loan.CanSurcharge = CanHaveASurcharge;
                    ResultBase result = await _genericService.InsertAsync(LoanExtension.Loan);
                    if (result.IsSuccess)
                    {
                        Users user = Aplicacion.Module.App.UserInfo;
                        List<Cashflows> cashFlows = _basisInfo.CashFlows.ToList();

                        INavigationParameters parameters = new NavigationParameters();
                        parameters.Add(ArgKeys.Loan, LoanExtension.Loan);

                        cashFlows.Add(new Cashflows()
                        {
                            Description = string.Format(BasisDescriptions.ADD_LOAN, user.Name),
                            Type = (int)CashflowTypes.Expense,
                            Amount = -LoanExtension.Loan.Amount,
                        });
                        _basisInfo.CashFlows = cashFlows.ToArray();

                        await _genericBasisService.UpdateAsync(
                            new BasisFirebaseObjectByRouteIdAndDateSpecification(_basisInfo.Route, DateTime.Now),
                            _basisInfo.Id, _basisInfo);

                        MessagingCenter.Send<ILoanCreatedChannel, INavigationParameters>(this, nameof(ILoanCreatedChannel), parameters);
                        await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                        await NavigationPopupService.PopPopupAsync(this);
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

        private async Task<bool> AddInstallments(Loans loan, DateTime firstPaymentDate)
        {
            if (SelectedInstallmentOptions != null)
            {
                int quantity = loan.InstallmentsQuantity;
                decimal installmentQuantity = loan.InstallmentsQuantity;
                decimal interest = loan.InterestRate;
                decimal totalAmount = loan.Amount;
                decimal totalInterest = (totalAmount * (100 + interest)) / 100;


                List<Installments> installments = new List<Installments>();

                for (int i = 0; i < quantity; i++)
                {
                    Installments installment = new Installments()
                    {
                        Id = Guid.NewGuid(),
                        PaymenDate = firstPaymentDate.AddDays(i * SelectedInstallmentOptions.Days),
                        Amount = totalInterest / installmentQuantity,
                        IsActive = true
                    };

                    installments.Add(installment);
                }

                LoanExtension.Loan.Installments = installments.ToArray();

                return installments.Count == quantity;
            }

            return false;
        }

        private async Task<bool> IsValid(Loans loan)
        {
            if (loan.Id == Guid.Empty)
            {
                await ShowErrorResultPopup(string.Format(CommonMessages.Console.MissingKey, nameof(loan.Id)),
                    CommonMessages.Error.InformationMessage);

                return false;
            }

            if (string.IsNullOrEmpty(loan.Name) 
                || loan.Amount <= LoanConfig.MINIMUM_AMOUNT
                || loan.InterestRate <= LoanConfig.MINIMUM_INTEREST_RATE
                || SelectedInstallmentOptions == null
                || loan.Date.Date < DateTime.Now.Date
                || (CanHaveASurcharge && 
                    (loan.Surcharge <= LoanConfig.MINIMUM_SURCHARGE_AMOUNT
                     || loan.SurchargeDays <= LoanConfig.MINIMUM_SURCHARGE_DAYS)))
            {
                await AlertService.ShowAlert(new WarningMessage(CommonMessages.Form.NullOrEmptyInfo));

                return false;
            }

            return true;
        }

        #endregion

        #region Constructor

        public LoanCreate()
        {
            LoanExtension = new LoansExtension()
            {
                Date = DateTimeNow
            };

            _genericService = GetGenericService<Loans, Guid>();
            _genericBasisService = GetGenericService<Basises, Guid>();
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
                case nameof(SelectedInstallmentOptions):

                    if (SelectedInstallmentOptions != null)
                    {
                        LoanExtension.Loan.InstallmentType = (int)SelectedInstallmentOptions.InstallmentType;
                    }

                    break;
                case nameof(Amount):

                    if (Amount < LoanConfig.MINIMUM_AMOUNT)
                    {
                        AlertService.ShowAlert(
                            new WarningMessage($"No puedes ingresar un valor inferior a {LoanConfig.MINIMUM_AMOUNT}"));
                    }
                    else if (Amount > _basisInfo.CashFlows.Sum(cashflow => cashflow.Amount))
                    {
                        AlertService.ShowAlert(
                            new WarningMessage("No puedes ingresar un valor superior a tu base del día."));
                    }
                    else
                    {
                        AvailableAmount = _basisInfo.CashFlows.Sum(cashflow => cashflow.Amount) - Amount;
                    }

                    break;
            }
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters != null)
            {
                if (parameters[ArgKeys.Client] is Clients client && Aplicacion.Module.App.RouteInfo is Routes route)
                {
                    _clientInfo = client;
                    LoanExtension.Loan.ClientId = client.Id;

                    ResultBase<Basises> result =
                        await _genericBasisService.GetBySpecificacionAsync(
                            new BasisByRouteIdAndDateSpecification(route.Id, DateTime.Now));

                    if (result.IsSuccess && result.Data is Basises basis)
                    {
                        AvailableAmount = basis.CashFlows.Sum(cashflow => cashflow.Amount);
                        _basisInfo = basis;

                        if (!basis.IsActive)
                        {
                            await ShowErrorResultPopup(string.Empty, "La base del día ya ha sido cerrada.");
                        }
                    }
                    else
                    {
                        await ShowErrorResultPopup(string.Format(CommonMessages.Console.MissingKey, ArgKeys.Client), CommonMessages.Error.InformationMessage);
                    }

                }
                else
                {
                    await ShowErrorResultPopup(string.Format(CommonMessages.Console.MissingKey, ArgKeys.Client), CommonMessages.Error.InformationMessage);
                }
            }
            else
            {
                await ShowErrorResultPopup(string.Format(CommonMessages.Console.NullKey, nameof(parameters)), CommonMessages.Error.InformationMessage);
            }

            InstallmentOptionsList = new List<InstallmentOptions>()
            {
                new InstallmentOptions()
                {
                    Title = "Diario",
                    InstallmentType = InstallmentTypeEnum.Daily,
                    Days = 1
                },
                new InstallmentOptions()
                {
                    Title = "Semanal",
                    InstallmentType = InstallmentTypeEnum.Weekly,
                    Days = 7
                },
                new InstallmentOptions()
                {
                    Title = "Quincenal",
                    InstallmentType = InstallmentTypeEnum.Biweekly,
                    Days = 14
                },
                new InstallmentOptions()
                {
                    Title = "Mensual",
                    InstallmentType = InstallmentTypeEnum.Monthly,
                    Days = 30
                },
            };

            IsBusy = false;
        }

        #endregion
    }
}