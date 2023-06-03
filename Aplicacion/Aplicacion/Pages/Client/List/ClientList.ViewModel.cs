using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Aplicacion.Config;
using Aplicacion.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config.Routes;
using Aplicacion.Pages.Client.Arrangings;
using Aplicacion.Pages.Client.Models;
using Aplicacion.Pages.Client.Specifications;
using Xamarin.CommonToolkit.Common;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Aplicacion.Pages.Client.List.ViewModel
{
    internal class ClientList : PageViewModelBase
    {
        #region Variables

        private List<Clients> _clients;
        private readonly IGenericService<Clients, Guid> _genericService;

        #endregion

        #region Propeties

        private ClientListFilter _selectedFilter;

        public ClientListFilter SelectedFilter
        {
            get => _selectedFilter;
            set => SetProperty(ref _selectedFilter, value);
        }

        private List<ClientListFilter> _filters;
        public List<ClientListFilter> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        private ObservableCollection<ClientExtended> _filterClientsCollection;
        public ObservableCollection<ClientExtended> FilterClientsCollection
        {
            get => _filterClientsCollection;
            set => SetProperty(ref _filterClientsCollection, value);
        }

        public ICommand GoToCreateClientCommand => new AsyncCommand(GoToCreateClientController);
        public ICommand SelectOptionCommand => new Command<Clients>(async (Clients client) => await SelectOptionController(client));
        #endregion

        #region Methods
        private async Task GoToCreateClientController()
        {
            IsBusy = true;

            await NavigationService.NavigateToAsync(PagesRoutes.Client.Create);
            
            IsBusy = false;
        }
        private async Task SelectOptionController(Clients client)
        {
            IsBusy = true;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Client, client);

            await NavigationService.NavigateToAsync<Details.ClientDetailsPage>(parameters: parameters);
            IsBusy= false;
        }
        private void RefreshClientsCollection()
        {
            FilterClientsCollection.Clear();
            foreach (Clients client in _clients.Where(SelectedFilter.Specification).ToList())
            {
                decimal totalAmount = 0;
                decimal payedAmount = 0;

                ClientExtended clientExtended = new ClientExtended()
                {
                    Client = client,
                    LoansQuantity = client.Loans?.Count() ?? 0
                };

                if (client.Loans != null)
                {
                    foreach (Loans loan in client.Loans.Where(l => l.IsActive).ToList())
                    {
                        totalAmount += loan.Amount;

                        if (loan.Installments != null)
                        {
                            foreach (Installments installment in loan.Installments)
                            {
                                if (!installment.IsActive)
                                {
                                    payedAmount += installment.Amount;
                                }

                                if (installment.Fees != null)
                                {
                                    foreach (Fees fee in installment.Fees)
                                    {
                                        payedAmount += fee.Amount;
                                    }
                                }
                            }
                        }
                    }
                }

                clientExtended.TotalAmount = totalAmount;
                clientExtended.PayedAmount = payedAmount;
                clientExtended.PayedPercentage = totalAmount > 0 ? (int)Math.Round((payedAmount * 100) / totalAmount) : 0;
                clientExtended.RestAmount = totalAmount - payedAmount;
                clientExtended.RestPercentage = totalAmount > 0 ? (int)Math.Round((clientExtended.RestAmount * 100) / totalAmount) : 0;

                FilterClientsCollection.Add(clientExtended);
            }
        }
        #endregion

        #region Constructor

        public ClientList()
        {
            FilterClientsCollection = new ObservableCollection<ClientExtended>();
            _genericService = GetGenericService<Clients, Guid>();
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
                case nameof(SelectedFilter):
                    if (SelectedFilter != null)
                    {
                        RefreshClientsCollection();
                    }
                    break;
            }
        }

        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (Aplicacion.Module.App.RouteInfo is Routes route)
            {
                SpecificationBase<Clients> specification = new ClientsByRouteIdSpecification(route.Id);
                ClientListFilter defaultFilter = new ClientListFilter()
                {
                    Name = "Todos",
                    Specification = specification
                };
                ArrangingBase<Clients> arranging = new ClientsArragangingByInstallmentDate();

                ResultBase<IEnumerable<Clients>> result = await _genericService.GetAllAsync(specification, arranging);

                if (result.IsSuccess)
                {
                    _clients = result.Data?.ToList();
                }

                SelectedFilter = defaultFilter;
                Filters = new List<ClientListFilter>()
                {
                    defaultFilter,
                    new ClientListFilter()
                    {
                        Name = "Cobrar Hoy",
                        Specification = new ClientsByInstallmentDateNowSpecification()
                    }
                };
            }
        }

        #endregion
    }
}
