using Aplicacion.Common.Result;
using Aplicacion.Common.Utils;
using Aplicacion.Models;
using Aplicacion.Pages.Admin.User.Contracts;
using Aplicacion.Pages.Admin.User.Models;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Admin.User.Service
{
    internal class User : IUserService
    {
        private readonly IUserRepository _userRepository;

        public User(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultBase> InsertAsync(UserModel user, string adminName)
        {
            VerifyResponse userVerified = UserVerification(user);

            if (!userVerified.Verified)
            {
                return new ResultBase("InsertAsync", false, userVerified.Message);
            }

            return await _userRepository.InsertAsync(user, adminName);
        }

        private VerifyResponse UserVerification(UserModel user)
        {
            VerifyResponse response = new VerifyResponse();

            bool allTrue = MultipleValuesVerification.AllTrueVerification(
                NameVerification(user.Name, ref response),
                PhoneVerification(user.Phone, ref response),
                PasswordVerification(user.Password, user.ConfirmPassword, ref response),
                LocationVerification(user.Location, ref response)
                );

            response.Verified = allTrue;

            return response;
        }

        #region Properties Verification
        private bool NameVerification(string name, ref VerifyResponse response)
        {
            bool isNull = !string.IsNullOrEmpty(name);

            response.Message = "No hay información en el campo 'Nombre'.";
            return isNull;
        }
        private bool PhoneVerification(string phone, ref VerifyResponse response)
        {
            bool isNull = !string.IsNullOrEmpty(phone);

            response.Message = "No hay información en el campo 'Teléfono'.";
            return isNull;
        }
        private bool PasswordVerification(string password, string confirmPassword, ref VerifyResponse response)
        {
            if (string.IsNullOrEmpty(password))
            {
                response.Message = "No hay información en el campo 'Contraseña'.";

                return false;
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                response.Message = "No hay información en el campo 'Confirmar Contraseña'.";

                return false;
            }

            if (!confirmPassword.Equals(password))
            {
                response.Message = "Las contraseñas no coinciden.";

                return false;
            }

            return true;
        }
        private bool LocationVerification(string location, ref VerifyResponse response)
        {
            bool isNull = !string.IsNullOrEmpty(location);

            response.Message = "No hay información en el campo 'Dirección'.";
            return isNull;
        }
        #endregion

        #region Private Class
        private class VerifyResponse
        {
            public bool Verified { get; set; }
            public string Message { get; set; }
        }
        #endregion
    }
}
