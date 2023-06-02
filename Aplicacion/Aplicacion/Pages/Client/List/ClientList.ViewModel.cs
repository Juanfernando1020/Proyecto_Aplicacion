using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Aplicacion.Config;
using Aplicacion.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Pages.Client.Arrangings;
using Aplicacion.Pages.Client.Specifications;
using Xamarin.CommonToolkit.Common;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;
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

        private ObservableCollection<Clients> _filterClientsCollection;

        public ObservableCollection<Clients> FilterClientsCollection
        {
            get => _filterClientsCollection;
            set => SetProperty(ref _filterClientsCollection, value);
        }

        public ICommand GoToCreateClientCommand => new Command(async () => await GoToCreateClientController());
        public ICommand SelectOptionCommand => new Command<Clients>(async (Clients client) => await SelectOptionController(client));
        #endregion

        #region Methods
        private async Task GoToCreateClientController()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<Create.ClientCreatePage>();
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
        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    SpecificationBase<Clients> specification = new ClientsByRouteIdSpecification(route.Id);
                    ArrangingBase<Clients> arranging = new ClientsArragangingByInstallmentDate();

                    ResultBase<IEnumerable<Clients>> result = await _genericService.GetAllAsync(specification, arranging);

                    if (result.IsSuccess)
                    {
                        if (result.Data is IEnumerable<Clients> clients)
                        {
                            _clients = clients.ToList();
                            foreach (Clients client in clients)
                            {
                                FilterClientsCollection.Add(client);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
