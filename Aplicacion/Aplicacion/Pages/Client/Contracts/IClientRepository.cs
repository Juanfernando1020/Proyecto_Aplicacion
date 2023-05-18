using Aplicacion.Common.Result;
using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Client.Contracts
{
    public interface IClientRepository
    {
        Task<ResultBase<Clients>> InsertAsync(Clients client);
        Task<ResultBase<Clients>> GetByIdAsync(Guid clientId);
    }
}
