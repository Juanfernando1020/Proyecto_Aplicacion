using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Result;

namespace Aplicacion.Pages.Route.Contracts
{
    public interface IRouteService
    {
        Task<ResultBase<IEnumerable<Routes>>> GetAllByUserId(Guid userId);
        Task<ResultBase> CreateAsync(Routes route);
    }
}
