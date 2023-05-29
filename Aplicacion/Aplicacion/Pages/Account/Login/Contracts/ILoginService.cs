using Xamarin.CommonToolkit.Result;
using Aplicacion.Pages.Account.Login.Models;
using System.Threading.Tasks;
using Aplicacion.Models;

namespace Aplicacion.Pages.Account.Login.Contracts
{
    public interface ILoginService
    {
        Task<ResultBase<Users>> LoginAsync(Credentials credentials);
    }
}
