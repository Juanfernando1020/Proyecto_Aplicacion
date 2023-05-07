using Aplicacion.Pages.Main.Dashboard.Contracts;
using Aplicacion.Pages.Main.Dashboard.Enums;
using Aplicacion.Pages.Main.Dashboard.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Dashboard.Service
{
    internal class MainDashboard : IMainDashboardService
    {
        public IEnumerable<MainDashboardItem> GetAllItems(MainDashboardTypeEnum type)
        {
            switch (type)
            {
                case MainDashboardTypeEnum.Worker:
                    List<MainDashboardItem> workerItems = new List<MainDashboardItem>()
                    {
                        new MainDashboardItem("Mi información", null),
                        new MainDashboardItem("Clientes", null),
                        new MainDashboardItem("Mis gastos", null),
                        new MainDashboardItem("Agregar base", null),
                    };

                    return workerItems;
                case MainDashboardTypeEnum.Admin:
                    List<MainDashboardItem> adminItems = new List<MainDashboardItem>()
                    {
                        new MainDashboardItem("Mi información", null),
                        new MainDashboardItem("Mis rutas", null),
                        new MainDashboardItem("Crear rutas", null),
                        new MainDashboardItem("Crear usuario", Admin.User.Create.Module.CreateUser.CreatePage()),
                    };

                    return adminItems;
                default:
                    throw new InvalidNavigationException("You have to send and option on 'type' parameter.");
            }

            
        }
    }
}
