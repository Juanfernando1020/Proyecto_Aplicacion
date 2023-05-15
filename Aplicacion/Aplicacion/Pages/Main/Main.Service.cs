using Aplicacion.Common.PagesBase.Enums;
using Aplicacion.Common.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Contracts;
using Aplicacion.Pages.User.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Main.Service
{
    internal class Main : IMainService
    {
        public Main()
        {
        }

        public IEnumerable<Modules> GetModulesAsync(RolesEnum role)
        {
            List<Modules> response = new List<Modules>();

            response.Add(new Modules("Mi información", RoutePages.User.Details, PagesBaseEnum.ContentPage));

            switch (role)
            {
                case RolesEnum.Owner:
                    response.Add(new Modules("Usuarios", RoutePages.User.List, PagesBaseEnum.ContentPage));
                    break;
                case RolesEnum.Admin:
                    response.Add(new Modules("Rutas", null, PagesBaseEnum.ContentPage));
                    break;
                case RolesEnum.Worker:
                    response.Add(new Modules("Cobros", null, PagesBaseEnum.ContentPage));
                    break;
            }

            return response;
        }
    }
}
