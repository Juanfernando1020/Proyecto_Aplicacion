using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Contracts;
using Aplicacion.Pages.Main.Specifications;
using Aplicacion.Pages.User.Roles.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Repository
{
    internal class Main : IMainRepository
    {
        public async Task<ResultBase<IEnumerable<Menu>>> GetMenuAsync(RolesEnum role)
        {
            try
            {
                IEnumerable<Menu> menu = await FirebaseHelper.Instance[FirebaseEntities.Menu]
                .GetAllBySpecificationAsync(new MenuByRoleSpecification(role));

                return new ResultBase<IEnumerable<Menu>>("Repository.Main.GetMenuAsync", true, null, menu);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<IEnumerable<Menu>>("Repository.Main.GetMenuAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }
    }
}
