using Aplicacion.Common.Result;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.Main.Enums;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Account.Login.Service
{
    internal class Login : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public Login(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<ResultBase<MainTypesEnum>> LoginAsync(Credentials credentials)
        {
            return await _loginRepository.LoginAsync(credentials);
        }
    }
}
