using Firebase.Database;

namespace Aplicacion.Helpers
{
    internal class FirebaseHelper
    {
        private static readonly FirebaseClient _firebaseClient = new FirebaseClient("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");

        internal static FirebaseClient GetClient()
        {
            return _firebaseClient;
        }
    }
}
