using Aplicacion.Common.Result;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Models;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Account.Login.Contracts
{
    public interface ILoginService
    {
        Task<ResultBase<string>> LoginAsync(Credentials credentials);
    }
}
