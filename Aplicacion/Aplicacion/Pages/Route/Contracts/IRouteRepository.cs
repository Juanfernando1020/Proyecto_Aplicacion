using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Route.Contracts
{
    public interface IRouteRepository
    {
        Task<ResultBase<IEnumerable<Routes>>> GetAllByUserId(Guid userId);
        Task<ResultBase> CreateAsync(Routes route);
    }
}
