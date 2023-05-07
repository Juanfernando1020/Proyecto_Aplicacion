using Aplicacion.Common.Result;
using Aplicacion.Models;
using Aplicacion.Pages.Admin.User.Models;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Admin.User.Contracts
{
    public interface IUserService
    {
        Task<ResultBase> InsertAsync(UserModel user, string adminName);
    }
}
