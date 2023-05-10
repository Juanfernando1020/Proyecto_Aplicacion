using Aplicacion.Common.Helpers.Firebase.Interfaces;
using Aplicacion.Common.Helpers.Firebase.Repositories;
using Aplicacion.Common.Specifications;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase
{
    public class FirebaseHelper
    {
        private static FirebaseClient _firebaseClient;

        public static readonly FirebaseHelper Instance = new FirebaseHelper();

        public IFirebaseHelperRepository this[string node] => Config(node);

        private IFirebaseHelperRepository Config(string node)
        {
            return new FirebaseRepository(_firebaseClient, node);
        }

        internal static void Initialize(string connection)
        {
            _firebaseClient = new FirebaseClient(connection);
        }
    }
}
