using Aplicacion.Config;
using Aplicacion.Pages.Main.Dashboard.Contracts;
using Aplicacion.Pages.Main.Dashboard.Models;
using Aplicacion.Pages.Main.Enums;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Dashboard.Service
{
    internal class MainDashboard : IMainDashboardService
    {
        public IEnumerable<MainDashboardItem> GetAllItems(MainTypesEnum mainType)
        {
            List<MainDashboardItem> items = new List<MainDashboardItem>()
            {
                new MainDashboardItem("Mi información", Profile.Module.MainProfile.CreatePage(), args: new Dictionary<string, object>()
                {
                    { ArgKeys.MainType, mainType }
                }),
            };

            switch (mainType)
            {
                case MainTypesEnum.Worker:
                    items.Add(new MainDashboardItem("Clientes", null));
                    items.Add(new MainDashboardItem("Mis gastos", null));
                    items.Add(new MainDashboardItem("Agregar base", null));
                    break;
                case MainTypesEnum.Admin:
                    items.Add(new MainDashboardItem("Mis rutas", null));
                    items.Add(new MainDashboardItem("Crear rutas", null));
                    items.Add(new MainDashboardItem("Crear usuario", Admin.User.Create.Module.CreateUser.CreatePage()));
                    break;
                default:
                    throw new InvalidNavigationException("You have to send and option on 'mainType' parameter.");
            }

            return items;
        }
    }
}
