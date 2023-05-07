using Aplicacion.Common.Helpers.Firebase.Interfaces;
using Aplicacion.Common.Helpers.Firebase.Services;
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

        public IFirebaseHelperService this[string node] => Config(node);

        private IFirebaseHelperService Config(string node)
        {
            return new FirebaseHelperService(_firebaseClient, node);
        }

        internal static void Initialize(string connection)
        {
            _firebaseClient = new FirebaseClient(connection);
        }
    }
}
