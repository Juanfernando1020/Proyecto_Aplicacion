using Xamarin.CommonToolkit.Helpers.Firebase;
using Xamarin.CommonToolkit.Helpers;
using Xamarin.CommonToolkit.MVVM.Navigation.Interfaces;
using Xamarin.CommonToolkit.MVVM;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Aplicacion.Module
{
    internal static class App
    {
        public static ViewModelBase ViewModel { get; set; }
        public static INavigationService NavigationService { get; set; }
        public static string setting = "value";

        public static Page Initialize()
        {
            FirebaseHelper.Initialize("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
            Pages.Module.Pages.Initialize();

            return GetInitPage();
        }

        private static Page GetInitPage()
        {
            Page page = ViewsManager.CreateView<Pages.Account.Login.LoginPage>();
            //Page page = ViewsManager.CreateView<Pages.Client.Details.ClientDetailsPage>();
            return new NavigationPage(page);
        }
    }
}
