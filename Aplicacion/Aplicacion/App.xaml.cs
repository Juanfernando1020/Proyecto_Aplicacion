using Xamarin.Forms;
namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Pages.Module.Pages.Initialize();

           //MainPage = new NavigationPage(new Login());
           MainPage = Pages.Admin.Panel.Module.AdminDashboard.CreateAdminDashboardPage();
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
