using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Client.Specifications;
using Aplicacion.Pages.Loan.Config;
using Aplicacion.Pages.Loan.Installment.Enums;
using Aplicacion.Pages.Loan.Installment.Models;
using Aplicacion.Pages.Loan.Models;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Aplicacion.Pages.Loan.Create.ViewModel
{
    internal class LoanCreate : PopupViewModelBase
    {
        #region Variables

        private Clients clientInfo;
        private readonly IGenericService<Loans, Guid> _genericService;
        private readonly IGenericService<Installments, Guid> _genericInstallmentService;

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

            if (await IsValid(LoanExtension.Loan))
            {
                LoanExtension.Loan.InstallmentType = (int)SelectedInstallmentOptions.InstallmentType;
                if (await AddInstallments(LoanExtension.Loan, LoanExtension.FirstInstallmentDate))
                {
                    LoanExtension.Loan.CanSurcharge = CanHaveASurcharge;
                    ResultBase result = await _genericService.InsertAsync(LoanExtension.Loan);
                    if (result.IsSuccess)
                    {
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

                    //ResultBase result = await _genericInstallmentService.InsertAsync(installment);

                    //if (result.IsSuccess)
                    //{
                    //    installments.Add(installment);
                    //}
                    //else
                    //{
                    //    await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                    //    return false;
                    //}

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
            _genericInstallmentService = GetGenericService<Installments, Guid>();
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
            }
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters != null)
            {
                if (parameters[ArgKeys.Client] is Clients client)
                {
                    clientInfo = client;
                    LoanExtension.Loan.ClientId = client.Id;
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