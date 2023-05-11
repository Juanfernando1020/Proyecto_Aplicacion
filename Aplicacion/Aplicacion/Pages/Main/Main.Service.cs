using Aplicacion.Models;
using Aplicacion.Pages.Main.Contracts;
using Aplicacion.Pages.User.Roles.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Service
{
    internal class Main : IMainService
    {
        private readonly IMainRepository _repository;

        public Main(IMainRepository repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<Menu>> GetMenuAsync(RolesEnum role)
        {
            return await _repository.GetMenuAsync(role);
        }
    }
}
