using Aplicacion.Views.VistasTrabajador;
using Aplicacion.Vistas.VistasAdmin;
using Xamarin.Forms;
namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Pages.Module.Pages.Initialize();

           //MainPage = new NavigationPage(new PanelTrabajador());
           MainPage = Pages.Worker.AddNewExpense.Module.AddNewExpense.CreateAddNewExpensePage();
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
