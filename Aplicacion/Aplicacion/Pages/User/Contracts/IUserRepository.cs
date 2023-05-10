using Aplicacion.Common.Result;
using Aplicacion.Models;
using System.Threading.Tasks;

namespace Aplicacion.Pages.User.Contracts
{
    public interface IUserRepository
    {
        Task<ResultBase<Users>> InsertAsync(Users user)
    }
}
