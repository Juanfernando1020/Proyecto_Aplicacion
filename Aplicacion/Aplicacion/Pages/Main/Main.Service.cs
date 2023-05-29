﻿using System;
using Xamarin.CommonToolkit.PagesBase.Enums;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Contracts;
using Aplicacion.Pages.User.Enums;
using System.Collections.Generic;
using Aplicacion.Config;
using Xamarin.Essentials;
using Aplicacion.Config.Routes;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;
using Aplicacion.Pages.Route.Specifications;
using Aplicacion.Pages.User.Contracts;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;

namespace Aplicacion.Pages.Main.Service
{
    internal class Main : IMainService
    {
        public IEnumerable<Modules> GetModulesAsync(Users user)
        {
            List<Modules> response = new List<Modules>();

            INavigationParameters routesParameters = new NavigationParameters();

            routesParameters.Add(ArgKeys.User, user);

            response.Add(new Modules("Mi información", PagesRoutes.User.Details, PagesBaseEnum.ContentPage));

            switch ((RolesEnum)user.Role)
            {
                case RolesEnum.Owner:
                    response.Add(new Modules("Usuarios", PagesRoutes.User.List, PagesBaseEnum.ContentPage));
                    response.Add(new Modules("Crear usuario", PagesRoutes.User.Create, PagesBaseEnum.ContentPage));
                    break;
                case RolesEnum.Admin:
                    response.Add(new Modules("Mis Rutas", PagesRoutes.Route.List, PagesBaseEnum.ContentPage, routesParameters));
                    break;
                case RolesEnum.Worker:
                    response.Add(new Modules("Clientes", PagesRoutes.Client.List, PagesBaseEnum.ContentPage));
                    response.Add(new Modules("Mis Rutas", PagesRoutes.Route.List, PagesBaseEnum.ContentPage, routesParameters));
                    response.Add(new Modules("Mis gastos del día", PagesRoutes.Expense.List, PagesBaseEnum.ContentPage));
                    response.Add(new Modules("Resumen del día", PagesRoutes.Day.Summary, PagesBaseEnum.ContentPage));
                    break;
            }

            return response;
        }
    }
}
