using Xamarin.CommonToolkit.Result;
using Aplicacion.Models;
using Aplicacion.Pages.User.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Contracts
{
    public interface IMainService
    {
        IEnumerable<Modules> GetModulesAsync(RolesEnum role);
    }
}
