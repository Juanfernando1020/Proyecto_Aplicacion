using System;
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
        public IEnumerable<Modules> GetModulesAsync(Users user, Routes route = null)
        {
            List<Modules> response = new List<Modules>();

            INavigationParameters routesParameters = new NavigationParameters();
            routesParameters.Add(ArgKeys.User, user);

            if (route != null)
            {
                routesParameters.Add(ArgKeys.Route, route);

                response.Add(new Modules("Ver Ruta", PagesRoutes.Route.Details, PagesBaseEnum.ContentPage, routesParameters));
                response.Add(new Modules("Base del Día", PagesRoutes.Route.Basis.Details, PagesBaseEnum.ContentPage, routesParameters));
                response.Add(new Modules("Clientes", PagesRoutes.Client.List, PagesBaseEnum.ContentPage, routesParameters));
            }
            else
            {
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
                        response.Add(new Modules("Mis Rutas", PagesRoutes.Route.List, PagesBaseEnum.ContentPage, routesParameters));
                        break;
                }
            }

            return response;
        }
    }
}
