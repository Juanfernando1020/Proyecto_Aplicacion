using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Application = Xamarin.Forms.Application;
using System.Globalization;

namespace Aplicacion
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var culture = new CultureInfo("es-CO");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

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
