﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Config;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;

namespace Aplicacion.Pages.Route.Details.ViewModel
{
    internal class RouteDetails : PageViewModelBase
    {
        #region Properties
        private Routes _route;

        public Routes Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
        }
        #endregion

        #region Overrides

        public override void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
        }

        #endregion

        #region OnLoad

        private void OnLoad(INavigationParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters[ArgKeys.Route] is Routes route)
                {
                    Route = route;
                }
            }
        }

        #endregion
    }
}
