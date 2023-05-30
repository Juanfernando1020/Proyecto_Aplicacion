﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;
using Xamarin.Forms;

namespace Aplicacion.Pages.User.List.UserBySpecification.ViewModel
{
    internal class UserBySpecification : PopupViewModelBase
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
            parameters.Add(ArgKeys.User, SelectedUser);

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

            if (parameters != null)
            {
                await OnLoad(parameters);
            }
        }
        #endregion

        #region OnLoad

        private async Task OnLoad(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters.ContainsKey(ArgKeys.Specification))
            {
                if (parameters[ArgKeys.Specification] is SpecificationBase<Users> specification)
                {
                    ResultBase<IEnumerable<Users>> result = await _userService.GetAllBySpecificationAsync(specification);

                    if (result.IsSuccess)
                    {
                        foreach (Users user in result.Data)
                        {
                            UsersCollection.Add(user);
                        }
                    }
                    else
                    {
                        await ShowError(result.Message);
                    }
                }
            }
            else
            {
                await ShowError();
            }

            IsBusy = false;
        }

        private async Task ShowError(string consoleMessage = null)
        {
            Console.WriteLine(consoleMessage ?? string.Format(CommonMessages.Console.MissingKey, ArgKeys.Specification));
            await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            await NavigationPopupService.PopPopupAsync(this);
        }

        #endregion
    }
}