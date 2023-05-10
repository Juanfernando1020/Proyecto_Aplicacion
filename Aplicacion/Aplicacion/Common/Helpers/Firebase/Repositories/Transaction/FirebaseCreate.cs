using Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Repositories.Transaction
{
    internal class FirebaseCreate : IFirebaseCreate
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly string _node;

        public FirebaseCreate(FirebaseClient firebaseClient, string node)
        {
            _firebaseClient = firebaseClient;
            _node = node;
        }

        public async Task<T> CreateDataAsync<T>(T entity)
        {
            FirebaseObject<T> data = await _firebaseClient
                .Child(_node)
                .PostAsync<T>(entity);

            return data.Object;
        }
    }
}
