using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Result;

namespace Aplicacion.Pages.Route.Service
{
    internal class Route : IRouteService
    {
        private readonly IRouteRepository _repository;

        public Route(IRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultBase> CreateAsync(Routes route)
        {
            string message = string.Empty;
            if (!CanRouteDetails(out message, route))
            {
                return new ResultBase("Service.Route.CreateAsync", false, message);
            }
            return await _repository.CreateAsync(route);
        }

        private bool CanRouteDetails(out string message, Routes route)
        {
            if (route == null || string.IsNullOrEmpty(route.Name) || route.Worker == null || route.Manager == null || route.Zone == null)
            {
                message = CommonMessages.Form.NullOrEmptyInfo;
                return false;
            }

            if (route.Budgets == null || route.Budgets.Length <= 0)
            {
                message = "Debe asignarse un presupuesto general.";
                return false;
            }

            message = null;

            return true;
        }

        public Task<ResultBase<IEnumerable<Routes>>> GetAllByUserId(Guid userId)
        {
            return _repository.GetAllByUserId(userId);
        }
    }
}
