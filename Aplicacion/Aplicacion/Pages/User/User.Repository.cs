using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.User.Contracts;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Pages.User.Repository
{
    internal class User : IUserRepository
    {
        public async Task<ResultBase<Users>> InsertAsync(Users user)
        {
            try
            {
                user.AuditCreationDate = DateTime.Now;
                user.AuditUpdateDate = DateTime.Now;

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
