using Xamarin.Forms;

namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Module.App.Initialize();

            // MainPage = new NavigationPage(Pages.Account.Login.Module.Login.CreatePage());
            //MainPage = new NavigationPage(Pages.Worker.Client.Loan.List.Module.ListLoan.CreatePage());
            MainPage = new NavigationPage(Pages.Worker.Client.Create.Module.CreateNewClient.CreatePage());
            //MainPage = new NavigationPage(Pages.Worker.Client.Billing.List.Module.ListBillings.CreatePage());
            //MainPage = new NavigationPage(Pages.Worker.Client.Detail.Module.DetailClient.CreatePage());
          //MainPage = Pages.Account.Test.Module.Test.CreateTestPage();
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
