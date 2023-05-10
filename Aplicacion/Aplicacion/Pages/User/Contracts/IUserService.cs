using Aplicacion.Common.Result;
using Aplicacion.Models;
using System.Threading.Tasks;

namespace Aplicacion.Pages.User.Contracts
{
    public interface IUserService
    {
        Task<ResultBase> InsertAsync(Users user, string confirmPassword);
    }
}
