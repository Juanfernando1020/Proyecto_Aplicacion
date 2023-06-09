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
using Aplicacion.Pages.Loan.Specifications;
using Aplicacion.Config.Messages;

namespace Aplicacion.Pages.User.List.ViewModel
{
    internal class UserList : PageViewModelBase
    {
        #region Variable
        Users _userInfo;
        private List<Users> _users;
        private readonly IGenericService<Users, Guid> _genericUsersService;
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
        private async Task DeactivateUserTapped()
        {
            IsBusy = true;

            if (SelectedUser != null)
            {
               

                Users newUser = new Users(SelectedUser.Id, SelectedUser.Name, SelectedUser.Phone, SelectedUser.Password, SelectedUser.Address, SelectedUser.Role, false, SelectedUser.Admin, SelectedUser.CreateDate, SelectedUser.NextPaymentDate);
                Guid userId = SelectedUser.Id;
                SelectedUser.IsActive = false;
                ResultBase resultUpdate = await _genericUsersService
                                    .UpdateAsync(new UsersFirebaseObjectByUserIdSpecification(userId), SelectedUser.Id, newUser);
                await AlertService.ShowAlert(new SuccessMessage("Usuario Desactivado Correctamente"));
                RefreshUserCollection();
            }

            else
            {
                AlertService.ShowAlert(new ErrorMessage("Selecciona el usuario"));
            }

            IsBusy = false;
        }
        private async Task UpdatePaymentTapped()
        {
            IsBusy = true;

            if (SelectedUser != null)
            {

                DateTime updateNextPaymentDay = DateTime.Now.AddMonths(1);
                Users updateUser = new Users(SelectedUser.Id, SelectedUser.Name, SelectedUser.Phone, SelectedUser.Password, SelectedUser.Address, SelectedUser.Role, true, SelectedUser.Admin, SelectedUser.CreateDate, updateNextPaymentDay);
                Guid userId = SelectedUser.Id;
                SelectedUser.IsActive = false;
                ResultBase resultUpdate = await _genericUsersService
                                    .UpdateAsync(new UsersFirebaseObjectByUserIdSpecification(userId), SelectedUser.Id, updateUser);
                await AlertService.ShowAlert(new SuccessMessage("Pago Actualizado Correctamente"));
                RefreshUserCollection();
            }

            else
            {
                AlertService.ShowAlert(new ErrorMessage("Selecciona el usuario"));
            }

            IsBusy = false;
        }
        #endregion

        #region Constructor
        public UserList()
        {
            FilterUsersCollection = new ObservableCollection<Users>();
            _genericUsersService = GetGenericService<Users, Guid>();
            DeactivateUserCommand = new Command(async () => await DeactivateUserTapped());
            UpdatePaymentCommand = new Command(async () => await UpdatePaymentTapped());
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
            ResultBase<IEnumerable<Users>> result = await _genericUsersService.GetAllAsync(new AllUsersSpecification());
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
