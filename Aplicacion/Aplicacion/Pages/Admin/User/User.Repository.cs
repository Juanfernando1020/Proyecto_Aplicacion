using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Admin.User.Contracts;
using Aplicacion.Pages.Admin.User.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Admin.User.Repository
{
    internal class User : IUserRepository
    {
        public async Task<ResultBase> InsertAsync(UserModel user, string adminName)
        {
			try
			{
                if (user.IsAdmin)
                {
                    Administrador administrador = new Administrador()
                    {
                        Name = user.Name,
                        Location = user.Location,
                        Password = user.Password,
                        Phone = user.Phone,
                    };

                    Administrador adminResult = await FirebaseHelper
                        .Instance["Administradores"]
                        .CreateDataAsync(administrador);

                    bool adminSuccess = adminResult != null;

                    return new ResultBase("InsertAsync", adminSuccess, adminSuccess ? null : "No se puedo crear el usuario. Intentalo más tarde.");
                }

                Trabajador trabajador = new Trabajador()
                {
                    Name = user.Name,
                    Location = user.Location,
                    Password = user.Password,
                    Phone = user.Phone,
                    Admin = adminName
                };

                Trabajador workerResult = await FirebaseHelper
                        .Instance["Trabajadores"]
                        .CreateDataAsync(trabajador);

                bool workerSuccess = workerResult != null;

                return new ResultBase("InsertAsync", workerSuccess, workerSuccess ? null : "No se puedo crear el usuario. Intentalo más tarde.");
			}
			catch (Exception)
			{
                return new ResultBase("InsertAsync", false, CommonMessages.Exception.ResultMessage);
			}
        }
    }
}
