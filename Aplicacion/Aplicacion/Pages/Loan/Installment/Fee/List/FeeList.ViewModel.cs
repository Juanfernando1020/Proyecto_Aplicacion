using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Channels;
using Aplicacion.Pages.Loan.Installment.Models;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Installment.Fee.List.ViewModel
{
    internal class FeeList : ViewModelBase, ILoadFeeListView
    {
        #region Variables

        private Installments _installment;
        private Loans _loan;

        #endregion

        #region Properties

        private ObservableRangeCollection<Fees> _feesCollection;
        public ObservableRangeCollection<Fees> FeesCollection
        {
            get => _feesCollection;
            set => SetProperty(ref _feesCollection, value);
        }

        public ICommand OpenFeeCreatePopupCommand => new AsyncCommand(OpenFeeCreatePopupController);

        #endregion

        #region Methods
        private async Task OpenFeeCreatePopupController()
        {
            IsBusy = true;

            if (_installment.IsActive)
            {
                if (_loan.Installments.Any(i =>
                        i.IsActive && i.PaymenDate.Date <= DateTime.Now.Date && i.Id != _installment.Id))
                {
                    await AlertService.ShowAlert(new InfoMessage("Aún hay cuotas por pagar."));
                }
                else
                {
                    INavigationParameters parameters = new NavigationParameters();
                    parameters.Add(ArgKeys.Installment, _installment);
                    parameters.Add(ArgKeys.Loan, _loan);

                    await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Loan.Installment.Fee.FeeCreate, parameters: parameters);
                }
            }
            else
            {
                await AlertService.ShowAlert(new InfoMessage("No puedes agregarle un pago a una cuota cancelada"));
            }

            IsBusy = false;
        }
        private void OnLoadFeeListView(ILoadFeeListView sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.InstallmentExtension] is InstallmentExtension installmentExtension &&
                    parameters[ArgKeys.Loan] is Loans loan)
                {
                    _installment = installmentExtension.Installment;
                    _loan = loan;
                }
            }
        }

        #endregion

        #region Constructor

        public FeeList()
        {
            FeesCollection = new ObservableRangeCollection<Fees>();
            MessagingCenter.Subscribe<ILoadFeeListView, INavigationParameters>(this, nameof(ILoadFeeListView), OnLoadFeeListView);
        }

        #endregion

        #region Overrides

        public override void CallBack(INavigationParameters parameters)
        {
            base.CallBack(parameters);

            OnCallBack(parameters);
        }

        #endregion

        #region OnLoad

        

        #endregion

        #region OnCallBack

        private void OnCallBack(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Fee] is Fees fee)
                {
                    FeesCollection.Add(fee);
                }
            }
        }

        #endregion
    }
}
