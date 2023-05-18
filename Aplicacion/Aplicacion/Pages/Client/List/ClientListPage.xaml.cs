using Aplicacion.Common.MVVM.Navigation.Services;
using Aplicacion.Common.PagesBase;
using Aplicacion.Pages.Client.Create;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Pages.Client.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientListPage : ContentPageBase
	{
		public ClientListPage ()
		{
			InitializeComponent ();
		}

        private void AgregarNuevoClienteButton_Clicked(object sender, System.EventArgs e)
        {
			var CreatePage = new NavigationPage(new ClientCreatePage());
			Navigation.PushAsync(CreatePage);
        }
    }
}