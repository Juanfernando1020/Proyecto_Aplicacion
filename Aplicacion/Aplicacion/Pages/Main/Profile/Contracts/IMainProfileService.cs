using Aplicacion.Models;
using Aplicacion.Pages.Main.Enums;
using Aplicacion.Pages.Main.Profile.Models;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Profile.Contracts
{
    public interface IMainProfileService
    {
        ProfileInformation GetProfileInformationAsync(MainTypesEnum mainType, Administrador administrador = null, Trabajador trabajador = null);
    }
}
