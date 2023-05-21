using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Application = Xamarin.Forms.Application;

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
            Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
