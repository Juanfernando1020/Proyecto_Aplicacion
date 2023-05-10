using Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Repositories.Transaction
{
    internal class FirebaseDelete : IFirebaseDelete
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly string _node;

        public FirebaseDelete(FirebaseClient firebaseClient, string node)
        {
            _firebaseClient = firebaseClient;
            _node = node;
        }

        public async Task DeleteAsync(string key)
        {
            await _firebaseClient
            .Child(_node)
            .Child(key)
            .DeleteAsync();
        }
    }
}
