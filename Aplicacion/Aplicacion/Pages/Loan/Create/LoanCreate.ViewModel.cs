using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Config;
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
        private readonly IGenericService<Clients, Guid> _genericService;

        #endregion

        #region Properties

        private List<InstallmentOptions> _installmentOptionsList;

        public List<InstallmentOptions> InstallmentOptionsList
        {
            get => _installmentOptionsList;
            set => SetProperty(ref _installmentOptionsList, value);
        }

        private LoansExtension _loanExtension;
        public LoansExtension LoanExtension
        {
            get => _loanExtension;
            set => SetProperty(ref _loanExtension, value);
        }

        private bool _canHaveASurcharge;
        public bool CanHaveASurcharge
        {
            get => _canHaveASurcharge;
            set => SetProperty(ref _canHaveASurcharge, value);
        }

        public ICommand CreateLoanCommand => new AsyncCommand(CreateLoanController);

        #endregion

        #region Methods

        private async Task CreateLoanController()
        {
            IsBusy = true;

            if (await IsValid(LoanExtension.Loan))
            {
                List<Loans> loans = clientInfo.Loans.ToList();
                loans.Add(LoanExtension.Loan);

                clientInfo.Loans = loans.ToArray();

                if (await AddInstallments(LoanExtension.Loan))
                {
                    ResultBase result = await _genericService.UpdateAsync(clientInfo.Id, clientInfo);

                    if (result.IsSuccess)
                    {
                        await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                    }
                    else
                    {
                        await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                    }
                }
            }

            IsBusy = false;
        }

        private async Task<bool> AddInstallments(Loans loan)
        {
            int quantity = loan.InstallmentsQuantity;
            decimal installmentQuantity = loan.InstallmentsQuantity;
            decimal interest = loan.InterestRate;
            decimal totalAmount = loan.Amount;
            decimal totalInterest = (totalAmount * (100 + interest)) / 100;


            List<Installments> installments = new List<Installments>();

            await Task.Run(() =>
            {
                for (int i = 0; i < quantity; i++)
                {
                    installments.Add(new Installments()
                    {
                        Id = Guid.NewGuid(),
                        Amount = totalInterest / installmentQuantity,
                    });
                }
            });

            return installments.Count == quantity;
        }

        private async Task<bool> IsValid(Loans loan)
        {
            if (loan.Id == Guid.Empty)
            {
                await ShowErrorResultPopup(string.Format(CommonMessages.Console.MissingKey, nameof(loan.Id)),
                    CommonMessages.Error.InformationMessage);

                return false;
            }
            
            if (string.IsNullOrEmpty(loan.Name))
            {
                await AlertService.ShowAlert(new WarningMessage(CommonMessages.Form.NullOrEmptyInfo));

                return false;
            }
            
            if (string.IsNullOrEmpty(loan.Name) 
                || loan.Amount <= LoanConfig.MINIMUM_AMOUNT
                || loan.InterestRate <= LoanConfig.MINIMUM_INTEREST_RATE
                || loan.Date < DateTime.Now
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
            LoanExtension = new LoansExtension();

            _genericService = GetGenericService<Clients, Guid>();
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
                if (parameters[ArgKeys.Client] is Clients client)
                {
                    clientInfo = client;
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
                    Days = 1
                },
                new InstallmentOptions()
                {
                    Title = "Semanal",
                    Days = 7
                },
                new InstallmentOptions()
                {
                    Title = "Quincenal",
                    Days = 14
                },
                new InstallmentOptions()
                {
                    Title = "Mensual",
                    Days = 30
                },
            };

            IsBusy = false;
        }

        #endregion
    }
}