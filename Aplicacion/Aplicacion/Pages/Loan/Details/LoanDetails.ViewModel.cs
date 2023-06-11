using System;
using System.Collections.Generic;
using System.Linq;
using Aplicacion.Config;
using Aplicacion.Models;
using System.Threading.Tasks;
using Aplicacion.Config.Messages;
using Aplicacion.Pages.Loan.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Pages.Loan.Channels;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Details.ViewModel
{
    internal class LoanDetails : PageViewModelBase, ILoadInstallmentListView, ILoanUpdatedChannel
    {
        #region Variables

        private List<Fees> _fees = new List<Fees>();
        private readonly IGenericService<Fees, Guid> _genericFeesService;

        #endregion

        #region Properties

        private LoansExtension _loan;
        public LoansExtension Loan
        {
            get => _loan;
            set => SetProperty(ref _loan, value);
        }

        #endregion

        #region Methods

        private void OnLoanUpdated(ILoanUpdatedChannel sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Loan] is Loans loan)
                {
                    Loan.Loan = loan;
                }
            }
        }

        #endregion

        #region Constructor

        public LoanDetails()
        {
            _genericFeesService = GetGenericService<Fees, Guid>();

            MessagingCenter.Subscribe<ILoanUpdatedChannel, INavigationParameters>(this, nameof(ILoanUpdatedChannel), OnLoanUpdated);
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
                if (parameters[ArgKeys.LoanExtension] is LoansExtension loan)
                {
                    Loan = loan;

                    ResultBase<IEnumerable<Fees>> result = await _genericFeesService.GetAllAsync();

                    if (result.IsSuccess)
                    {
                        if (result.Data is IEnumerable<Fees> fees)
                        {
                            foreach (Fees fee in fees)
                            {
                                if (Loan.Loan.Installments.Any(installment => installment.Id == fee.InstallmentId))
                                {
                                    _fees.Add(fee);
                                }
                            }

                            Loan.Fees = _fees;
                        }
                        else
                        {
                            await ShowErrorResult(string.Format(CommonMessages.Console.NullDataWhenIsSuccess), CommonMessages.Error.InformationMessage);
                        }
                    }

                    INavigationParameters installmentListParameters = new NavigationParameters();
                    installmentListParameters.Add(ArgKeys.LoanExtension, Loan);

                    MessagingCenter.Send<ILoadInstallmentListView, INavigationParameters>(this, nameof(ILoadInstallmentListView), installmentListParameters);
                }
                else
                {
                    await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(parameters)), CommonMessages.Error.InformationMessage);
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.NullKey, nameof(parameters)), CommonMessages.Error.InformationMessage);
            }

            IsBusy = false;
        }

        #endregion
    }
}
