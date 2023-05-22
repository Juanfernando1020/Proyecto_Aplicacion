using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using Xamarin.CommonToolkit.Mvvm;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Views.UserBySpecification.ViewModel
{
    internal class UserBySpecification : ViewModelBase
    {
        #region Variables

        private readonly IUserService _userService;
        #endregion

        #region Properties
        private ObservableCollection<Users> _usersCollection;
        public ObservableCollection<Users> UsersCollection
        {
            get => _usersCollection;
            set => SetProperty(ref _usersCollection, value);
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
