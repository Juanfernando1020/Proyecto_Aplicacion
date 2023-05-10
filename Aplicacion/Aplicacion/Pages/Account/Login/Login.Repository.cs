using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.Result;
using Aplicacion.Config;
using Aplicacion.Enums;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.Account.Login.Specifications;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.Pages.Account.Login.Repository
{
    internal class Login : ILoginRepository
    {
        public async Task<ResultBase<Users>> LoginAsync(Credentials credentials)
        {
            try
            {
                Users user = await FirebaseHelper.Instance[FirebaseEntities.Users].GetBySpecificationAsync(new CredentialsForUsersSpecifications(credentials));

                bool userAuthenticated = user != null;

                Application.Current.Properties[ArgKeys.User] = JsonConvert.SerializeObject(user);

                return new ResultBase<Users>("LoginAsync",
                    userAuthenticated,
                    userAuthenticated ? null : "El usuario o la contraseña no son válidos.",
                    user);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new ResultBase<Users>("LoginAsync", false, CommonMessages.Exception.ResultMessage);
            }
        }
    }
}
