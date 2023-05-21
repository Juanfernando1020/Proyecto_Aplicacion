using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Route.Contracts;
using Aplicacion.Pages.Route.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Helpers;
using Xamarin.CommonToolkit.Helpers.Firebase;
using Xamarin.CommonToolkit.Result;

namespace Aplicacion.Pages.Route.Repository
{
    internal class Route : IRouteRepository
    {
        public async Task<ResultBase> CreateAsync(Routes route)
        {
            try
            {
                Routes result = await FirebaseHelper.Instance[FirebaseEntities.Routes]
                    .CreateDataAsync(route);

                if (!result.Id.Equals(route.Id))
                    return new ResultBase("Repository.Route.CreateAsync", false, CommonMessages.Error.InformationMessage);

                return new ResultBase("Repository.Route.CreateAsync", true);
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handler(ex);
                return new ResultBase("Repository.Route.CreateAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }

        public async Task<ResultBase<IEnumerable<Routes>>> GetAllByUserId(Guid userId)
        {
			try
			{
                IEnumerable<Routes> list = await FirebaseHelper.Instance[FirebaseEntities.Routes]
                .GetAllBySpecificationAsync(new RoutesByUserIdSpecification(userId));

                if (list == null)
                {
                    return new ResultBase<IEnumerable<Routes>>("Repository.Route.GetAllRoutesByUserId", false, CommonMessages.Error.InformationMessage);
                }

                return new ResultBase<IEnumerable<Routes>>("Repository.Route.GetAllRoutesByUserId", true, null, list);
            }
			catch (Exception ex)
			{
                ExceptionHelper.Handler(ex);
                return new ResultBase<IEnumerable<Routes>>("Repository.Route.GetAllRoutesByUserId", false, CommonMessages.Exception.ResultMessage);
            }
        }
    }
}
