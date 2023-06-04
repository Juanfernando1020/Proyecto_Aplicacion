using System;
using Aplicacion.Config;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config.Routes;
using Aplicacion.Pages.Loan.Models;
using Aplicacion.Pages.Loan.Specifications;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Aplicacion.Pages.Client.Channels;

namespace Aplicacion.Pages.Loan.List.ViewModel
{
    internal class LoanList : ViewModelBase, ILoadLoanListViewChannel
    {
        #region Variables

        private Clients clientInfo;
        private List<Loans> loanList = new List<Loans>();

        #endregion

        #region Propeties

        private LoanListFilters _selectedFilter;
        public LoanListFilters SelectedFilter
        {
            get => _selectedFilter;
            set => SetProperty(ref _selectedFilter, value);
        }
        
        private List<LoanListFilters> _filters;
        public List<LoanListFilters> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        private LoansExtension _selectedLoan;
        public LoansExtension SelectedLoan
        {
            get => _selectedLoan;
            set => SetProperty(ref _selectedLoan, value);
        }
        
        private ObservableCollection<LoansExtension> _filterLoansCollections;
        public ObservableCollection<LoansExtension> FilterLoansCollections
        {
            get => _filterLoansCollections;
            set => SetProperty(ref _filterLoansCollections, value);
        }

        public ICommand GoToCreateLoanCommand => new AsyncCommand(GoToCreateLoanController);
        public ICommand SelectLoanCommand => new AsyncCommand(SelectLoanController);
        #endregion

        #region Methods
        private async Task GoToCreateLoanController()
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Client, clientInfo);

            await NavigationPopupService.PushPopupAsync(this, PopupsRoutes.Loan.LoanCreate, parameters: parameters);
            
            IsBusy = false;
        }
        private async Task SelectLoanController()
        {
            if (SelectedLoan != null)
            {
                IsBusy = true;

                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.LoanExtension, SelectedLoan);

                await NavigationService.NavigateToAsync(PagesRoutes.Loan.Details, parameters: parameters);

                IsBusy = false;

                SelectedLoan = null;
            }
        }
        private void RefreshFilterLoansCollection()
        {
            if (SelectedFilter != null)
            {
                if (loanList.Count > 0)
                {
                    FilterLoansCollections.Clear();

                    if (SelectedFilter.Specification != null)
                    {
                        foreach (Loans loan in loanList.Where(SelectedFilter.Specification).OrderByDescending(l => l.Date).ToList())
                        {
                            FilterLoansCollections.Add(new LoansExtension()
                            {
                                Date = loan.Date,
                                Loan = loan
                            });
                        }
                    }
                    else
                    {
                        foreach (Loans loan in loanList)
                        {
                            FilterLoansCollections.Add(new LoansExtension()
                            {
                                Date = loan.Date,
                                Loan = loan
                            });
                        }
                    }
                }
            }
        }

        private void OnLoadLoanListView(ILoadLoanListViewChannel sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Client] is Clients client)
                {
                    clientInfo = client;
                }
                
                if (parameters[ArgKeys.Loans] is Loans[] loans)
                {
                    loanList = loans.ToList();
                }
            }
        }

        #endregion

        #region Constructor

        public LoanList()
        {
            FilterLoansCollections = new ObservableCollection<LoansExtension>();

            MessagingCenter.Subscribe<ILoadLoanListViewChannel, INavigationParameters>(this, nameof(ILoadLoanListViewChannel), OnLoadLoanListView);
        }

        #endregion

        #region Overrides

        public override void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad(parameters);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            MessagingCenter.Unsubscribe<ILoadLoanListViewChannel, INavigationParameters>(this, nameof(ILoadLoanListViewChannel));
        }

        #endregion

        #region OnLoad

        private void OnLoad(INavigationParameters parameters)
        {
            Filters = new List<LoanListFilters>()
            {
                new LoanListFilters()
                {
                    Title = "Todos",
                    Specification = null
                },
                new LoanListFilters()
                {
                    Title = "Activos",
                    Specification = new LoansByActiveSpecifications(true)
                },
                new LoanListFilters()
                {
                    Title = "Cobrar Hoy",
                    Specification = new LoansByDateSpecifications(DateTime.Now)
                },
                new LoanListFilters()
                {
                    Title = "Cobrar Mañana",
                    Specification = new LoansByDateSpecifications(DateTime.Now.AddDays(1))
                },
                new LoanListFilters()
                {
                    Title = "Vencidos",
                    Specification = new LoansByActiveSpecifications(false)
                }
            };
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SelectedFilter):
                    RefreshFilterLoansCollection();
                    break;
            }
        }

        #endregion
    }
}
