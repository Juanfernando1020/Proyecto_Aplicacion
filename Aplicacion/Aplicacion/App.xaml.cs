using Xamarin.Forms;

namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = Module.App.Initialize();
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
