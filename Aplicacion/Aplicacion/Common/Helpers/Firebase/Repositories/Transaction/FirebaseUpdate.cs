using Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Repositories.Transaction
{
    internal class FirebaseUpdate : IFirebaseUpdate
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly string _node;

        public FirebaseUpdate(FirebaseClient firebaseClient, string node)
        {
            _firebaseClient = firebaseClient;
            _node = node;
        }

        public async Task UpdateDataAsync<T>(T newObj)
        {
            await _firebaseClient
                .Child(_node)
                .PutAsync(JsonConvert.SerializeObject(newObj));
        }
    }
}
