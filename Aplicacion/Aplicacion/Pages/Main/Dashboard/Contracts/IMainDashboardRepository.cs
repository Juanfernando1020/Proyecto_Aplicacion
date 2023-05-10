using Aplicacion.Enums;
using Aplicacion.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Dashboard.Contracts
{
    public interface IMainDashboardRepository
    {
        Task<IEnumerable<Menu>> GetMenuAsync(RolesEnum role);
    }
}
