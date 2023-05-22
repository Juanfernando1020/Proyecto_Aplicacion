using Xamarin.CommonToolkit.Result;
using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.User.Contracts
{
    public interface IUserService
    {
        Task<Guid> GetUserId();
        Task<ResultBase<IEnumerable<Users>>> GetAllBySpecificationAsync(SpecificationBase<Users> specification);
        Task<ResultBase> InsertAsync(Users user, string confirmPassword);
        Task<ResultBase<Users>> GetByIdAsync(Guid? user);
    }
}
