using System.Threading.Tasks;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Channels;
using Aplicacion.Pages.Loan.Installment.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Installment.Details.ViewModel
{
    internal class InstallmentDetails : PageViewModelBase, ILoadFeeListView
    {
        #region Properties

        private InstallmentExtension _installmentExtension;
        public InstallmentExtension InstallmentExtension
        {
            get => _installmentExtension;
            set => SetProperty(ref _installmentExtension, value);
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
                    InstallmentExtension = installment;

                    INavigationParameters feeListParameters = new NavigationParameters();
                    feeListParameters.Add(ArgKeys.InstallmentExtension, installment);
                    feeListParameters.Add(ArgKeys.Loan, loan);

                    MessagingCenter.Send<ILoadFeeListView, INavigationParameters>(this, nameof(ILoadFeeListView), feeListParameters);
                }
            }
            else
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.NullKey, nameof(parameters)),
                    CommonMessages.Error.InformationMessage);
            }

            IsBusy = false;
        }

        #endregion
    }
}
