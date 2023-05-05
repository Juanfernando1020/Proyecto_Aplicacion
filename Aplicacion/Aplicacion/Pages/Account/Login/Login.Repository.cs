using Aplicacion.Common.Result;
using Aplicacion.Helpers;
using Aplicacion.Models;
using Aplicacion.Pages.Account.Login.Contracts;
using Aplicacion.Pages.Account.Login.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Pages.Account.Login.Repository
{
    internal class Login : ILoginRepository
    {
        public async Task<ResultBase<string>> LoginAsync(Credentials credentials)
        {
            FirebaseClient client = FirebaseHelper.GetClient();

            IReadOnlyCollection<FirebaseObject<Trabajador>> workers = 
                await client
                .Child("Trabajadores")
                .OrderByKey()
                .OnceAsync<Trabajador>();
            
            IReadOnlyCollection<FirebaseObject<Administrador>> admins = 
                await client
                .Child("Administradores")
                .OrderByKey()
                .OnceAsync<Administrador>();

            IEnumerable<Trabajador> worker = from w in workers
                    where w.Object.Phone.Equals(credentials.Username) &&
                    w.Object.Password.Equals(credentials.Password)
                    select w.Object;
            
            IEnumerable<Administrador> admin = from a in admins
                    where a.Object.Phone.Equals(credentials.Username) &&
                    a.Object.Password.Equals(credentials.Password)
                    select a.Object;


            bool userAuthenticated = worker.Any() || admin.Any();

            return new ResultBase<string>("LoginAsync", 
                userAuthenticated, 
                userAuthenticated ? null : "El usuario o la contraseña no son válidos.",
                JsonConvert.SerializeObject((object)worker.FirstOrDefault() ?? (object)admin.FirstOrDefault()));
        }
    }
}
