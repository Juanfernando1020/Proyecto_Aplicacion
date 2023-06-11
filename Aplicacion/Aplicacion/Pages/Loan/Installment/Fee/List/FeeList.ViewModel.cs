using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Channels;
using Aplicacion.Pages.Loan.Installment.Enums;
using Aplicacion.Pages.Loan.Installment.Fee.Channels;
using Aplicacion.Pages.Loan.Installment.Fee.Specifications;
using Aplicacion.Pages.Loan.Installment.Models;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Installment.Fee.List.ViewModel
{
    internal class FeeList : ViewModelBase, ILoadFeeListView, IFeeCreatedChannel
    {
        #region Variables

        private InstallmentExtension _installment;
        private Loans _loan;
        private readonly IGenericService<Fees, Guid> _genericFeeService;

        #endregion

        #region Properties

        private ObservableRangeCollection<Fees> _feesCollection;
        public ObservableRangeCollection<Fees> FeesCollection
        {
            get => _feesCollection;
            set => SetProperty(ref _feesCollection, value);
        }

        private bool _canCreateFee;
        public bool CanCreateFee
        {
            get => _canCreateFee;
            set => SetProperty(ref _canCreateFee, value);
        }

        public ICommand OpenFeeCreatePopupCommand => new AsyncCommand(OpenFeeCreatePopupController);

        #endregion

        #region Methods
        private async Task OpenFeeCreatePopupController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.InstallmentExtension, _installment);
            parameters.Add(ArgKeys.Loan, _loan);

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Loan.Installment.Fee.FeeCreate, parameters: parameters);

            IsBusy = false;
        }
        private async void OnLoadFeeListView(ILoadFeeListView sender, INavigationParameters parameters)
        {
            await OnLoad(parameters);
            
        }

        private void OnFeeCreated(IFeeCreatedChannel sender, INavigationParameters parameters)
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

        #region Constructor

        public FeeList()
        {
            CanCreateFee = true;
            FeesCollection = new ObservableRangeCollection<Fees>();
            MessagingCenter.Subscribe<ILoadFeeListView, INavigationParameters>(this, nameof(ILoadFeeListView), OnLoadFeeListView);
            MessagingCenter.Subscribe<IFeeCreatedChannel, INavigationParameters>(this, nameof(IFeeCreatedChannel), OnFeeCreated);

            _genericFeeService = GetGenericService<Fees, Guid>();
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            MessagingCenter.Unsubscribe<ILoadFeeListView, INavigationParameters>(this, nameof(ILoadFeeListView));
            MessagingCenter.Unsubscribe<IFeeCreatedChannel, INavigationParameters>(this, nameof(IFeeCreatedChannel));
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {

                if (parameters[ArgKeys.InstallmentExtension] is InstallmentExtension installmentExtension &&
                    parameters[ArgKeys.Loan] is Loans loan)
                {
                    _installment = installmentExtension;
                    _loan = loan;

                    CanCreateFee = _installment.Installment.Status == (int)InstallmentStatusEnum.Complete;

                    ResultBase<IEnumerable<Fees>> result = await _genericFeeService.GetAllAsync(new FeesByInstallmentIdSpecification(_installment.Installment.Id));

                    FeesCollection.Clear();
                    if (result.IsSuccess && result.Data is IEnumerable<Fees> fees)
                    {
                        foreach (Fees fee in fees)
                        {
                            FeesCollection.Add(fee);
                        }
                    } 
                }
            }
        }

        #endregion
    }
}
