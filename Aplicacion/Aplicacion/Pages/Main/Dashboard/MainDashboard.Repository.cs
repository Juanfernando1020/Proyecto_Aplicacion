using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.Result;
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
        public async Task<ResultBase<IEnumerable<Menu>>> GetMenuAsync(RolesEnum role)
        {
			try
			{
				IEnumerable<Menu> menu = await FirebaseHelper.Instance[FirebaseEntities.Menu]
                .GetAllBySpecificationAsync(new MenuByRoleSpecification(role));

				return new ResultBase<IEnumerable<Menu>>("Repository.MainDashboard.GetMenuAsync", true, null, menu);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return new ResultBase<IEnumerable<Menu>>("Repository.MainDashboard.GetMenuAsync", false, CommonMessages.Exception.ResultMessage);
			}
        }
    }
}
