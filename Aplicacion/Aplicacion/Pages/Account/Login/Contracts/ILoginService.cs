using Aplicacion.Common.Result;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.User.Enums;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Account.Login.Contracts
{
    public interface ILoginService
    {
        Task<ResultBase<RolesEnum>> LoginAsync(Credentials credentials);
    }
}
