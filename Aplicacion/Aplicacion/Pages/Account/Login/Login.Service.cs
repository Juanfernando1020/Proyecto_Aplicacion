using Aplicacion.Common.Helpers.SecureStorage.Interfaces;
using Aplicacion.Common.Helpers.SecureStorage.Service;
using Aplicacion.Common.Result;
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

        public async Task<ResultBase<RolesEnum>> LoginAsync(Credentials credentials)
        {
            ResultBase<Users> result = default;
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                result = await _loginRepository.LoginAsync(credentials);
            });

            await _secureStorage.DeleteAllAsync();
            await _secureStorage.SaveAsync(ArgKeys.User, result.Data?.Id ?? Guid.Empty);

            return new ResultBase<RolesEnum>(result.Code, result.IsSuccess, result.Message, result.Data == null ? default(RolesEnum) : (RolesEnum)result.Data.Role);
        }
    }
}
