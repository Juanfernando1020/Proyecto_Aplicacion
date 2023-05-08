using Xamarin.Essentials;
using Xamarin.Forms;

namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Module.App.Initialize();
            MainPage = new NavigationPage(Pages.Account.Login.Module.Login.CreatePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
