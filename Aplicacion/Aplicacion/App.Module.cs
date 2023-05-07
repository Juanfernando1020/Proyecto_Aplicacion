using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.MVVM;

namespace Aplicacion.Module
{
    internal static class App
    {
        public static ViewModelBase ViewModel { get; set; }

        public static void Initialize()
        {
            FirebaseHelper.Initialize("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
            Pages.Module.Pages.Initialize();
        }
    }
}
