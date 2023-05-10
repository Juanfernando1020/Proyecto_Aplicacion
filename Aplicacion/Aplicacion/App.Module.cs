using Aplicacion.Common.Helpers;
using Aplicacion.Common.Helpers.Firebase;
using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Navigation.Interfaces;
using Aplicacion.Common.MVVM.Navigation.Services;
using System;
using Xamarin.Forms;

namespace Aplicacion.Module
{
    internal static class App
    {
        public static ViewModelBase ViewModel { get; set; }
        public static INavigationService NavigationService { get; set; }

        public static Page Initialize()
        {
            FirebaseHelper.Initialize("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
            Pages.Module.Pages.Initialize();

            return GetInitPage();
        }

        private static Page GetInitPage()
        {
            Page page = ViewsManager.CreateView<Pages.Account.Login.LoginPage>();
            
            return new NavigationPage(page);
        }
    }
}
