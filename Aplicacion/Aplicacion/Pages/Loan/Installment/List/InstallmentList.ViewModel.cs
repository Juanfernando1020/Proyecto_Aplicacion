using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Channels;
using Aplicacion.Pages.Loan.Installment.Arranging;
using Aplicacion.Pages.Loan.Installment.Fee.Specifications;
using Aplicacion.Pages.Loan.Installment.Models;
using Aplicacion.Pages.Loan.Installment.Specifications;
using Aplicacion.Pages.Loan.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Utils.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Loan.Installment.List.ViewModel
{
    internal class InstallmentList : ViewModelBase, ILoadInstallmentListView
    {
        #region Variables

        private LoansExtension _loan = new LoansExtension();
        private readonly List<InstallmentExtension> _installments = new List<InstallmentExtension>();

        #endregion

        #region Methods

        private InstallmentExtension _selectedInstallment;
        public InstallmentExtension SelectedInstallment
        {
            get => _selectedInstallment;
            set => SetProperty(ref _selectedInstallment, value);
        }
        
        private InstallmentListFilter _selectedFilter;
        public InstallmentListFilter SelectedFilter
        {
            get => _selectedFilter;
            set => SetProperty(ref _selectedFilter, value);
        }

        private ObservableCollection<InstallmentExtension> _installmentsCollection;
        public ObservableCollection<InstallmentExtension> InstallmentsCollection
        {
            get => _installmentsCollection; 
            set => SetProperty(ref _installmentsCollection, value);
        }
        
        private List<InstallmentListFilter> _filters;
        public List<InstallmentListFilter> Filters
        {
            get => _filters; 
            set => SetProperty(ref _filters, value);
        }

        public ICommand SelectInstallmentCommand => new AsyncCommand(SelectInstallmentController);

        #endregion

        #region Methods
        private async Task SelectInstallmentController()
        {
            if (SelectedInstallment != null)
            {
                IsBusy = true;

                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.InstallmentExtension, SelectedInstallment);
                parameters.Add(ArgKeys.Loan, _loan.Loan);

                await NavigationService.NavigateToAsync(PagesRoutes.Installment.Details, parameters: parameters);

                IsBusy = false;
                SelectedInstallment = null;
            }
        }
        private void OnLoadInstallmentListView(ILoadInstallmentListView sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.LoanExtension] is LoansExtension loan)
                {
                    _loan = loan;
                    foreach (Installments installment in _loan.Loan.Installments.OrderByArranging(new InstallmentByDateArranging()))
                    {
                        _installments.Add(new InstallmentExtension()
                        {
                            Installment = installment,
                            Fees = _loan.Fees.FilterBySpecification(new FeesByInstallmentIdSpecification(installment.Id)).ToList()
                        });
                    }
                }
            }
        }

        private void RefreshInstallmentCollection()
        {
            if (SelectedFilter != null)
            {
                IEnumerable<InstallmentExtension> filteredInstallments = _installments.FilterBySpecification(SelectedFilter.Specification);

                InstallmentsCollection.Clear();
                foreach (InstallmentExtension installment in filteredInstallments)
                {
                    InstallmentsCollection.Add(installment);
                }
            }
        }

        #endregion

        #region Constructor

        public InstallmentList()
        {
            InstallmentsCollection = new ObservableCollection<InstallmentExtension>();
            MessagingCenter.Subscribe<ILoadInstallmentListView, INavigationParameters>(this, nameof(ILoadInstallmentListView), OnLoadInstallmentListView);
        }

        #endregion

        #region Overrides

        public override void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SelectedFilter):
                    RefreshInstallmentCollection();
                    break;
            }
        }

        #endregion

        #region OnLoad

        private void OnLoad()
        {
            Filters = new List<InstallmentListFilter>()
            {
                new InstallmentListFilter()
                {
                    Title = "Todos",
                    Specification = null
                },
                new InstallmentListFilter()
                {
                    Title = "Cuota de Hoy",
                    Specification = new InstallmentsByPaymentDateAndDateNowSpecification()
                }
            };
        }

        #endregion
    }
}
