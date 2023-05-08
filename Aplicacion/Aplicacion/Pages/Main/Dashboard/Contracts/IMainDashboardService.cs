using Aplicacion.Pages.Main.Dashboard.Models;
using Aplicacion.Pages.Main.Enums;
using System.Collections.Generic;

namespace Aplicacion.Pages.Main.Dashboard.Contracts
{
    public interface IMainDashboardService
    {
        IEnumerable<MainDashboardItem> GetAllItems(MainTypesEnum mainType);
    }
}
