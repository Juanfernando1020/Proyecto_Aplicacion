using Xamarin.CommonToolkit.Helpers.Firebase;
using Xamarin.CommonToolkit.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using Aplicacion.Pages.User.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommonToolkit.Specifications;
using Aplicacion.Config.Messages;

namespace Aplicacion.Pages.User.Repository
{
    internal class User : IUserRepository
    {
        public async Task<ResultBase<Users>> GetByIdAsync(Guid userId)
        {
            try
            {
                Users user = await FirebaseHelper.Instance[FirebaseEntities.Users]
                    .GetBySpecificationAsync(new UserByIdSpecification(userId));

                if (user == null)
                {
                    return new ResultBase<Users>("Repository.User.GetByIdAsync", false, CommonMessages.Error.InformationMessage);
                }

                return new ResultBase<Users>("Repository.User.GetByIdAsync", true, null, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<Users>("Repository.User.GetByIdAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }

        public async Task<ResultBase<IEnumerable<Users>>> GetAllBySpecificationAsync(SpecificationBase<Users> specification)
        {
            try
            {
                IEnumerable<Users> users = await FirebaseHelper.Instance[FirebaseEntities.Users]
                    .GetAllBySpecificationAsync(specification);

                if (users == null)
                {
                    return new ResultBase<IEnumerable<Users>>("Repository.User.GetAllBySpecificationAsync", false, CommonMessages.Error.InformationMessage);
                }

                return new ResultBase<IEnumerable<Users>>("Repository.User.GetAllBySpecificationAsync", true, null, users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<IEnumerable<Users>>("Repository.User.GetAllBySpecificationAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }

        public async Task<ResultBase<Users>> InsertAsync(Users user)
        {
            try
            {

                Users result = await FirebaseHelper.Instance[FirebaseEntities.Users]
                    .CreateDataAsync(user);

                bool isSuccess = result != null;

                return new ResultBase<Users>("Repository.User.InsertAsync", isSuccess, isSuccess ? null : "No se ha podido crear el usuario.", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<Users>("Repository.User.InsertAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }
    }
}
