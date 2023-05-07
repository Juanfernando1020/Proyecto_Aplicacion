using Aplicacion.Pages.Main.Dashboard.Enums;
using Aplicacion.Pages.Main.Dashboard.Models;
using System.Collections.Generic;

namespace Aplicacion.Pages.Main.Dashboard.Contracts
{
    public interface IMainDashboardService
    {
        IEnumerable<MainDashboardItem> GetAllItems(MainDashboardTypeEnum type);
    }
}
