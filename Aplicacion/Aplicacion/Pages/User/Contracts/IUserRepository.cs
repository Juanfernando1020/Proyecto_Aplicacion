using Xamarin.CommonToolkit.Result;
using Aplicacion.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Contracts
{
    public interface IUserRepository
    {
        Task<ResultBase<Users>> InsertAsync(Users user);
        Task<ResultBase<Users>> GetByIdAsync(Guid userId);
        Task<ResultBase<IEnumerable<Users>>> GetAllBySpecificationAsync(SpecificationBase<Users> specification);
    }
}
