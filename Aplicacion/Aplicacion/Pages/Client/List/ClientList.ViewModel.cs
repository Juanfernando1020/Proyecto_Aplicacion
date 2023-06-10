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
using Aplicacion.Pages.Client.Models;
using Aplicacion.Pages.Client.Specifications;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;   
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Aplicacion.Pages.Client.Channels;

namespace Aplicacion.Pages.Client.List.ViewModel
{
    internal class ClientList : PageViewModelBase
    {
        #region Variables

        private List<Clients> _clients;
        private List<Loans> _loans = new List<Loans>();
        private readonly IGenericService<Clients, Guid> _genericService;
        private readonly IGenericService<Loans, Guid> _genericLoansService;

        #endregion

        #region Propeties

        private ClientListFilter _selectedFilter;
        public ClientListFilter SelectedFilter
        {
            get => _selectedFilter;
            set => SetProperty(ref _selectedFilter, value);
        }
        
        private Clients _selectedClient;
        public Clients SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        private List<ClientListFilter> _filters;
        public List<ClientListFilter> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        private ObservableCollection<Clients> _filterClientsCollection;
        public ObservableCollection<Clients> FilterClientsCollection
        {
            get => _filterClientsCollection;
            set => SetProperty(ref _filterClientsCollection, value);
        }

        public ICommand GoToCreateClientCommand => new AsyncCommand(GoToCreateClientController);
        public ICommand SelectClientCommand => new AsyncCommand(SelectClientController);
        #endregion

        #region Methods
        private async Task GoToCreateClientController()
        {
            IsBusy = true;

            await NavigationService.NavigateToAsync(PagesRoutes.Client.Create);
            
            IsBusy = false;
        }
        private async Task SelectClientController()
        {
            if (SelectedClient != null)
            {
                IsBusy = true;

                INavigationParameters parameters = new NavigationParameters();
                parameters.Add(ArgKeys.Client, SelectedClient);

                await NavigationService.NavigateToAsync(PagesRoutes.Client.Details, parameters: parameters);
                IsBusy = false;

                SelectedClient = null;
            }
        }
        private void RefreshClientsCollection()
        {
            if (_clients != null)
            {
                FilterClientsCollection.Clear();
                foreach (Clients client in _clients)
                {
                    List<Loans> loans = _loans.Where(loan => loan.ClientId == client.Id).ToList();
                    switch (SelectedFilter.Filter)
                    {
                        case ClientListFilterType.All:
                            FilterClientsCollection.Add(client);
                            break;
                        case ClientListFilterType.CollectToday:
                            if (loans.Any(loan => loan.Date.Date == DateTime.Now.Date))
                            {
                                FilterClientsCollection.Add(client);
                            }
                            break;
                    }
                }
            }
        }
        private void OnClientCreated(IClientCreatedChannel sender, INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Client] is Clients client)
                {
                    _clients.Add(client);
                    RefreshClientsCollection();
                }
            }
        }

        #endregion

        #region Constructor

        public ClientList()
        {
            FilterClientsCollection = new ObservableCollection<Clients>();
            _genericService = GetGenericService<Clients, Guid>();
            _genericLoansService = GetGenericService<Loans, Guid>();

            MessagingCenter.Subscribe<IClientCreatedChannel, INavigationParameters>(this, nameof(IClientCreatedChannel), OnClientCreated);
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
            IsBusy = true;
            if (Aplicacion.Module.App.RouteInfo is Routes route)
            {
                ResultBase<IEnumerable<Clients>> result = await _genericService.GetAllAsync(new ClientsByRouteIdSpecification(route.Id));

                if (result.IsSuccess)
                {
                    if (result.Data is IEnumerable<Clients> clientList)
                    {
                        _clients = clientList.ToList();

                        ResultBase<IEnumerable<Loans>> resultLoan = await _genericLoansService.GetAllAsync();

                        if (resultLoan.IsSuccess)
                        {
                            if (resultLoan.Data is IEnumerable<Loans> loans)
                            {
                                _loans = loans.ToList();
                            }
                        }
                    }
                }

                Filters = new List<ClientListFilter>()
                {
                    new ClientListFilter()
                    {
                        Name = "Todos",
                        Filter = ClientListFilterType.All
                    },
                    new ClientListFilter()
                    {
                        Name = "Cobrar Hoy",
                        Filter = ClientListFilterType.CollectToday
                    }
                };
            }

            IsBusy = false;
        }

        #endregion
    }
}
