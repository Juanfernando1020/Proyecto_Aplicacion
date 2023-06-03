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

        private ObservableCollection<Clients> _filterClientsCollection;
        public ObservableCollection<Clients> FilterClientsCollection
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

        #endregion

        #region Constructor

        public ClientList()
        {
            FilterClientsCollection = new ObservableCollection<Clients>();
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
                    try
                    {
                        if (SelectedFilter != null)
                        {
                            FilterClientsCollection.Clear();
                            foreach (Clients client in _clients.Where(SelectedFilter.Specification).ToList())
                            {
                                FilterClientsCollection.Add(client);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
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
                SpecificationBase<Clients> specification = new ClientsByInstallmentDateNowSpecification();
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
