using Aplicacion.Config.Routes;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Client.Models;
using Aplicacion.Pages.User.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using System.ComponentModel;
using Aplicacion.Pages.Client.Specifications;
using Xamarin.CommonToolkit.Result;
using static Aplicacion.Config.Routes.PagesRoutes;
using Aplicacion.Pages.User.Enums;
using Firebase.Auth;
using Aplicacion.Pages.User.Service;
using Aplicacion.Pages.User.Specifications;
using Xamarin.Forms;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;

namespace Aplicacion.Pages.User.List.ViewModel
{
    internal class UserList : PageViewModelBase
    {
        #region Variable
        private List<Users> _users;
        private readonly IGenericService<Users, Guid> _genericService;
        #endregion

        #region Properties

        private UserListFilter _selectedFilter;
        public UserListFilter SelectedFilter
        {
            get => _selectedFilter;
            set => SetProperty(ref _selectedFilter, value);
        }

        private Users _selectedUser;
        public Users SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        private List<UserListFilter> _filters;
        public List<UserListFilter> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }

        private ObservableCollection<Users> _filterUsersCollection;
        public ObservableCollection<Users> FilterUsersCollection
        {
            get => _filterUsersCollection;
            set => SetProperty(ref _filterUsersCollection, value);
        }



        #endregion

        #region Commands
        public ICommand DeactivateUserCommand { get; }
        public ICommand UpdatePaymentCommand { get; }
        #endregion

        #region Methods
        private void RefreshUserCollection()
        {
            if (_users != null)
            {
                FilterUsersCollection.Clear();
                foreach (Users user in _users)
                {
                    switch (SelectedFilter.Filter)
                    {
                        case UserListFilterType.All:
                            if (user.Role != 0)
                            {
                                FilterUsersCollection.Add(user);
                            }
                            break;
                        case UserListFilterType.ExpiredWorker:
                            if (DateTime.Now.Date > user.CreateDate.AddMonths(1) && user.Role == 2 )
                            {
                                FilterUsersCollection.Add(user);
                            }
                            break;
                        case UserListFilterType.InactiveWorker:
                            if (user.IsActive == false && user.Role == 2)
                            {
                                FilterUsersCollection.Add(user);
                            }
                            break;
                        case UserListFilterType.Today:
                            if(user.CreateDate == DateTime.Now)
                            {
                                FilterUsersCollection.Add(user);
                            }
                            break;
                        case UserListFilterType.Admin:
                            if (user.Role == 1)
                            {
                                FilterUsersCollection.Add(user);
                            }
                            break;
                    }
                }
            }
        }
        private void DeactivateUserTapped()
        {
            AlertService.ShowAlert(new ErrorMessage("Disable User Logic"));
        }
        private void UpdatePaymentTapped()
        {
            AlertService.ShowAlert(new ErrorMessage("Update Payment Logic"));
        }
        #endregion

        #region Constructor
        public UserList()
        {
            FilterUsersCollection = new ObservableCollection<Users>();
            _genericService = GetGenericService<Users, Guid>();
            DeactivateUserCommand = new Command(DeactivateUserTapped);
            UpdatePaymentCommand = new Command(UpdatePaymentTapped);
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
                        RefreshUserCollection();
                    }
                    break;
            }
        }

        #endregion

        #region OnLoad
        private async Task OnLoad(INavigationParameters pararameters)
        {
            IsBusy = true;
            ResultBase<IEnumerable<Users>> result = await _genericService.GetAllAsync(new AllUsersSpecification());
            if (result.Data is IEnumerable<Users> userList)
            {
                _users = userList.ToList();
            }

            Filters = new List<UserListFilter>()
            {
                new UserListFilter()
                {
                     Name = "Todos",
                     Filter = UserListFilterType.All
                },
                new UserListFilter()
                {
                    Name = "Trabajadores Vencidos",
                    Filter = UserListFilterType.ExpiredWorker
                },
                 new UserListFilter()
                {
                    Name = "Trabajadores Desactivados",
                    Filter = UserListFilterType.InactiveWorker
                },
                  new UserListFilter()
                {
                    Name = "Vencen Hoy",
                    Filter = UserListFilterType.Today
                },
                    new UserListFilter()
                {
                    Name = "Administradores",
                    Filter = UserListFilterType.Admin
                }
            };

            IsBusy= false;
        }

        #endregion
    }
}
