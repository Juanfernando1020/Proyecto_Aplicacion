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

                if(user == null)
                {
                    return new ResultBase<Users>("GetByIdAsync", false, CommonMessages.Error.InformationMessage);
                }

                return new ResultBase<Users>("GetByIdAsync", true, null, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<Users>("InsertAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }

        public async Task<ResultBase<IEnumerable<Users>>> GetAllBySpecificationAsync(SpecificationBase<Users> specification)
        {
            try
            {
                IEnumerable<Users> user = await FirebaseHelper.Instance[FirebaseEntities.Users]
                    .GetAllBySpecificationAsync(specification);

                if (user == null)
                {
                    return new ResultBase<IEnumerable<Users>>("GetByIdAsync", false, CommonMessages.Error.InformationMessage);
                }

                return new ResultBase<IEnumerable<Users>>("GetByIdAsync", true, null, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<IEnumerable<Users>>("InsertAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }

        public async Task<ResultBase<Users>> InsertAsync(Users user)
        {
            try
            {

                Users result = await FirebaseHelper.Instance[FirebaseEntities.Users]
                    .CreateDataAsync(user);

                bool isSuccess = result != null;

                return new ResultBase<Users>("InsertAsync", isSuccess, isSuccess ? null : "No se ha podido crear el usuario.", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResultBase<Users>("InsertAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }
    }
}
