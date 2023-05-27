﻿using Xamarin.CommonToolkit.Helpers.SecureStorage.Interfaces;
using Xamarin.CommonToolkit.Helpers.SecureStorage.Service;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommonToolkit.Utils;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Specifications;
using Aplicacion.Config.Messages;

namespace Aplicacion.Pages.User.Service
{
    internal class User : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecureStorageService _secureStorageService;

        public User(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _secureStorageService = new SecureStorageService();
        }

        public async Task<ResultBase<IEnumerable<Users>>> GetAllBySpecificationAsync(SpecificationBase<Users> specification)
        {
            return await _userRepository.GetAllBySpecificationAsync(specification);
        }

        public async Task<ResultBase> InsertAsync(Users user, string confirmPassword)
        {
            VerifyResponse userVerified = UserValidation(user, confirmPassword);

            if (!userVerified.Verified)
            {
                return new ResultBase("Service.User.InsertAsync", false, userVerified.Message);
            }

            ResultBase<Users> result =  await _userRepository.InsertAsync(user);

            if (result.IsSuccess)
            {
                if (!result.Data.Equals(user))
                {
                    result.IsSuccess = false;
                    result.Message = CommonMessages.Error.InformationMessage;
                }
            }

            return result;
        }

        public async Task<ResultBase<Users>> GetByIdAsync()
        {
            Guid userId = await GetUserId();

            return await GetByIdAsync(userId);
        }

        public async Task<ResultBase<Users>> GetByIdAsync(Guid user)
        {
            if(user.Equals(Guid.Empty))
            {
                Console.WriteLine("The parameter 'user' cannot be null or Guid.Empty.");
                return new ResultBase<Users>("Service.User.GetByIdAsync", false, CommonMessages.Error.InformationMessage);
            }

            return await _userRepository.GetByIdAsync(user);
        }

        public async Task<Guid> GetUserId()
        {
            Guid id = Guid.Empty;
            string userId = await _secureStorageService.ReadAsync(ArgKeys.User);

            if (!Guid.TryParse(userId, out id))
            {
                return Guid.Empty;
            }

            return id;
        }

        #region Properties Validation
        private VerifyResponse UserValidation(Users user, string confirmPassword)
        {
            VerifyResponse response = new VerifyResponse();

            bool allTrue = MultipleValuesValidation.AllTrueValidation(
                NameValidation(user.Name, ref response),
                PhoneValidation(user.Phone, ref response),
                PasswordValidation(user.Password, confirmPassword, ref response)
                );

            response.Verified = allTrue;

            return response;
        }
        private bool NameValidation(string name, ref VerifyResponse response)
        {
            bool isNull = !string.IsNullOrEmpty(name);

            response.Message = "No hay información en el campo 'Nombre'.";
            return isNull;
        }
        private bool PhoneValidation(string phone, ref VerifyResponse response)
        {
            bool isNull = !string.IsNullOrEmpty(phone);

            response.Message = "No hay información en el campo 'Teléfono'.";
            return isNull;
        }
        private bool PasswordValidation(string password, string confirmPassword, ref VerifyResponse response)
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
