﻿using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Contracts;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommonToolkit.MVVM;
using Xamarin.CommonToolkit.MVVM.Alerts.Messages;
using Xamarin.Forms;

namespace Aplicacion.Pages.Route.Create.ViewModel
{
    internal class CreateRoute : ViewModelBase
    {
        #region Variables
        private IRouteService _routeService;
        #endregion

        #region Properties
        private Routes route;
        public Routes Route
        { 
            get => route;
            set
            {
                SetProperty(ref route, value);
            }
        }

        public ICommand CreateCommand => new Command(async () => await CreateController());
        #endregion

        #region Methods
        private async Task CreateController()
        {
            IsBusy = true;
            await _routeService.CreateAsync(Route);
            IsBusy = false;
        }
        #endregion

        #region Constructor
        public CreateRoute()
        {
            Route = default;
            _routeService = new Service.Route(new Repository.Route());
        }
        #endregion

        #region Overrides
        public override async void OnInitialize()
        {
            base.OnInitialize();

            await OnLoad();
        }
        #endregion

        #region OnLoad
        private async Task OnLoad()
        {
            if (!Args.ContainsKey(ArgKeys.User))
            {
                await ShowErrorResult(string.Format(CommonMessages.Console.MissingKey, nameof(ArgKeys.User)));
            }
        }

        private async Task ShowErrorResult(string message)
        {
            Console.WriteLine(message);
            await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
            await NavigationService.PopAsync();
        }
        #endregion
    }
}
