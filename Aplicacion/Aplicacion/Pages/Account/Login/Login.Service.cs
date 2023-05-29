using Xamarin.CommonToolkit.Helpers.SecureStorage.Interfaces;
using Xamarin.CommonToolkit.Helpers.SecureStorage.Service;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.User.Enums;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Account.Login.Service
{
    internal class Login : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ISecureStorageService _secureStorage;

        public Login(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _secureStorage = new SecureStorageService();
        }

        public async Task<ResultBase<Users>> LoginAsync(Credentials credentials)
        {
            ResultBase<Users> result = await _loginRepository.LoginAsync(credentials); ;

            await _secureStorage.DeleteAllAsync();
            await _secureStorage.SaveAsync(ArgKeys.User, result.Data?.Id ?? Guid.Empty);

            return result;
        }
    }
}
