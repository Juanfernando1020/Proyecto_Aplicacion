using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Channels;
using Aplicacion.Pages.Loan.Installment.Channels;
using Aplicacion.Pages.Loan.Installment.Models;
using Aplicacion.Pages.Loan.Specifications;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Installment.Details.ViewModel
{
    internal class InstallmentDetails : PageViewModelBase, ILoadFeeListView, ILoanUpdatedChannel
    {
        #region MyRegion

        private readonly IGenericService<Loans, Guid> _genericLoanService;

        #endregion

        #region Properties

        private InstallmentExtension _installmentExtension;
        public InstallmentExtension InstallmentExtension
        {
            get => _installmentExtension;
            set => SetProperty(ref _installmentExtension, value);
        }

        #endregion

        #region Constructor

        public InstallmentDetails()
        {
            _genericLoanService = GetGenericService<Loans, Guid>();
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
                if (parameters[ArgKeys.InstallmentExtension] is InstallmentExtension installment &&
                    parameters[ArgKeys.Loan] is Loans loan)
                {
                   

                    INavigationParameters feeListParameters = new NavigationParameters();
                    feeListParameters.Add(ArgKeys.InstallmentExtension, installment);
                    feeListParameters.Add(ArgKeys.Loan, loan);

                    DateTime limitDate = installment.Installment.PaymenDate.AddDays(loan.SurchargeDays).Date;
                    DateTime now = DateTime.Now.Date;
                    if (loan.CanSurcharge && limitDate < now)
                    {
                        decimal surchageAmount = loan.Surcharge;
                        TimeSpan time = now.Subtract(limitDate);

                        decimal value = time.Days * surchageAmount;

                        //switch ((InstallmentTypeEnum)loan.InstallmentType)
                        //{
                        //    case InstallmentTypeEnum.Daily:
                        //        value = time.Days * surchageAmount;
                        //        break;
                        //    case InstallmentTypeEnum.Weekly:
                        //        value = (int)Math.Floor(time.TotalDays / 7) * surchageAmount;
                        //        break;
                        //    case InstallmentTypeEnum.Biweekly:
                        //        value = (int)Math.Floor(time.TotalDays / 14) * surchageAmount;
                        //        break;
                        //    case InstallmentTypeEnum.Monthly:
                        //        value = (int)Math.Floor(time.TotalDays / 14)
                        //        break;
                        //}

                        List<Installments> installments = loan.Installments.ToList();
                        installments.Remove(installment.Installment);
                        installment.Installment.DiferenceAmount += value;
                        installments.Add(installment.Installment);

                        loan.Installments = installments.ToArray();


                        ResultBase result = await _genericLoanService.UpdateAsync(
                            new LoansFirebaseObjectByClientIdSpecification(loan.ClientId), loan.Id, loan);

                        if (result.IsSuccess)
                        {
                            INavigationParameters loanDetailsParameters = new NavigationParameters();
                            loanDetailsParameters.Add(ArgKeys.Loan, loan);

                            MessagingCenter.Send<ILoanUpdatedChannel, INavigationParameters>(this, nameof(ILoanUpdatedChannel), loanDetailsParameters);
                        }
                        else
                        {
                            await ShowErrorResult("Update loan failed", CommonMessages.Error.InformationMessage);
                        }
                    }
                    InstallmentExtension = installment;

                    MessagingCenter.Send<ILoadFeeListView, INavigationParameters>(this, nameof(ILoadFeeListView), feeListParameters);
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.NullKey, ArgKeys.InstallmentExtension + " - " + ArgKeys.Loan),
                    CommonMessages.Error.InformationMessage);
            }

            IsBusy = false;
        }

        #endregion
    }
}
