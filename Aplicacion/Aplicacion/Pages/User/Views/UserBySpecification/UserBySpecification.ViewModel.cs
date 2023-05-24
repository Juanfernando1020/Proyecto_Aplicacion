using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using Xamarin.CommonToolkit.Common;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;
using Xamarin.Forms;

namespace Aplicacion.Pages.User.Views.UserBySpecification.ViewModel
{
    internal class UserBySpecification : PopupViewModelBase, IPopupEvents
    {
        #region Variables

        private readonly IUserService _userService;
        #endregion

        #region Properties

        private Users _selectedUser;

        public Users SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        private ObservableCollection<Users> _usersCollection;
        public ObservableCollection<Users> UsersCollection
        {
            get => _usersCollection;
            set => SetProperty(ref _usersCollection, value);
        }

        public ICommand SelectUserCommand => new Command(async () => await SelectUserController());
        #endregion

        #region Methods
        private async Task SelectUserController()
        {
            if (SelectedUser == null) return;

            INavigationParameters parameters = new NavigationParameters();
            parameters.Add(ArgKeys.Worker, SelectedUser);

            await NavigationPopupService.PopPopupAsync(this, parameters: parameters);

            SelectedUser = null;
        }
        #endregion

        #region Constructor
        public UserBySpecification()
        {
            UsersCollection = new ObservableCollection<Users>();
            _userService = new Service.User(new Repository.User());
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
            IsBusy = true;
            if (!parameters.ContainsKey(ArgKeys.Specification))
            {
                await ShowError();
            }

            SpecificationBase<Users> specification = (SpecificationBase<Users>)parameters[ArgKeys.Specification];

            ResultBase<IEnumerable<Users>> result = await _userService.GetAllBySpecificationAsync(specification);

            if (!result.IsSuccess)
            {
                await ShowError(result.Message);
            }

            foreach (Users user in result.Data)
            {
                UsersCollection.Add(user);
            }

            IsBusy = false;
        }

        private async Task ShowError(string consoleMessage = null)
        {
            Console.WriteLine(consoleMessage ?? string.Format(CommonMessages.Console.MissingKey, ArgKeys.Specification));
            await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            await NavigationService.PopAsync();
        }

        #endregion
    }
}
