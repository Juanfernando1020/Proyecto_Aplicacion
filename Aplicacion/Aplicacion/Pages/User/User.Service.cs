using Aplicacion.Common.Result;
using Aplicacion.Common.Utils;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using System.Threading.Tasks;

namespace Aplicacion.Pages.User.Service
{
    internal class User : IUserService
    {
        private readonly IUserRepository _userRepository;

        public User(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultBase> InsertAsync(Users user, string confirmPassword)
        {
            VerifyResponse userVerified = UserVerification(user, confirmPassword);

            if (!userVerified.Verified)
            {
                return new ResultBase("InsertAsync", false, userVerified.Message);
            }

            ResultBase<Users> result =  await _userRepository.InsertAsync(user);

            if (result.IsSuccess)
            {
                if (!result.Data.Equals(user))
                {
                    result.IsSuccess = false;
                    result.Message = "Ha ocurrido algo en la creación de ";
                }
            }

            return result;
        }

        private VerifyResponse UserVerification(Users user, string confirmPassword)
        {
            VerifyResponse response = new VerifyResponse();

            bool allTrue = MultipleValuesVerification.AllTrueVerification(
                NameVerification(user.Name, ref response),
                PhoneVerification(user.Phone, ref response),
                PasswordVerification(user.Password, confirmPassword, ref response)
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
