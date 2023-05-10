using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Config;
using Aplicacion.Enums;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Dashboard.Contracts;
using Aplicacion.Pages.Main.Dashboard.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Dashboard.Repository
{
    internal class MainDashboard : IMainDashboardRepository
    {
        public async Task<IEnumerable<Menu>> GetMenuAsync(RolesEnum role)
        {
            return await FirebaseHelper.Instance[FirebaseEntities.Menu]
                .GetAllBySpecificationAsync(new MenuByRoleSpecification(role));
        }
    }
}
