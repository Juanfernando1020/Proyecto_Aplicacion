using Xamarin.Forms;
namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Module.App.Initialize();

           //MainPage = new NavigationPage(Pages.Account.Login.Module.Login.CreatePage());
           MainPage = new NavigationPage(Pages.Worker.Finance.Base.list.Module.ListBase.CreatePage());
           //MainPage = new NavigationPage(Pages.Worker.Client.Detail.Module.DetailClient.CreatePage());
           //MainPage = Pages.Worker.ViewExpense.Module.ViewExpense.CreateViewExpensePage();
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
