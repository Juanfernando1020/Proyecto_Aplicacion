using Aplicacion.Common.Helpers.Firebase;

namespace Aplicacion.Module
{
    internal static class App
    {
        public static void Initialize()
        {
            FirebaseHelper.Initialize("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
            Pages.Module.Pages.Initialize();
        }
    }
}
