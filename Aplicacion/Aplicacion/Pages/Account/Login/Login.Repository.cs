using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.Result;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Aplicacion.Pages.Account.Login.Specifications;
using Aplicacion.Pages.Main.Enums;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.Pages.Account.Login.Repository
{
    internal class Login : ILoginRepository
    {
        public async Task<ResultBase<MainTypesEnum>> LoginAsync(Credentials credentials)
        {
			try
			{
                Trabajador worker = await FirebaseHelper.Instance["Trabajadores"].GetBySpecificationAsync(new CredentialsForWorkerSpecifications(credentials));
                Administrador admin = await FirebaseHelper.Instance["Administradores"].GetBySpecificationAsync(new CredentialsForAdministratorSpecifications(credentials));

                bool userAuthenticated = worker != null || admin != null;

                Application.Current.Properties[ArgKeys.User] = JsonConvert.SerializeObject((object)worker ?? (object)admin);

                return new ResultBase<MainTypesEnum>("LoginAsync", 
                    userAuthenticated, 
                    userAuthenticated ? null : "El usuario o la contraseña no son válidos.",
                    worker != null ? MainTypesEnum.Worker : MainTypesEnum.Admin);
			}
			catch (Exception ex)
			{
                Debug.WriteLine(ex);
                return new ResultBase<MainTypesEnum>("LoginAsync", false, CommonMessages.Exception.ResultMessage);
			}
        }
    }
}
