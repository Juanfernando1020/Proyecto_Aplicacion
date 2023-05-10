using Aplicacion.Enums;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Dashboard.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Main.Dashboard.Service
{
    internal class MainDashboard : IMainDashboardService
    {
        private readonly IMainDashboardRepository _repository;

        public MainDashboard(IMainDashboardRepository repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<Menu>> GetMenuAsync(RolesEnum role)
        {
            return await _repository.GetMenuAsync(role);
        }
    }
}
