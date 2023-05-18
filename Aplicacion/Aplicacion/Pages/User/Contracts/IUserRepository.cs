using Xamarin.CommonToolkit.Result;
using Aplicacion.Models;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Pages.User.Contracts
{
    public interface IUserRepository
    {
        Task<ResultBase<Users>> InsertAsync(Users user);
        Task<ResultBase<Users>> GetByIdAsync(Guid userId);
    }
}
