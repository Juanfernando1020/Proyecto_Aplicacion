using Aplicacion.Models;
using Aplicacion.Pages.User.Roles.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Contracts
{
    public interface IMainService
    {
        Task<IEnumerable<Menu>> GetMenuAsync(RolesEnum role);
    }
}
