using Xamarin.CommonToolkit.Result;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Models;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Account.Login.Contracts
{
    public interface ILoginRepository
    {
        Task<ResultBase<Users>> LoginAsync(Credentials credentials);
    }
}
