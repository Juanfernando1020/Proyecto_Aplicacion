using Aplicacion.Models;
using Aplicacion.Vistas.VistasAdmin;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.VistasAdmin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreaRuta : ContentPage
	{
		public CreaRuta ()
		{
			InitializeComponent ();
		}

        FirebaseClient firebaseClient = new FirebaseClient("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
        private async void Boton_Registro_Ruta_Clicked(object sender, EventArgs e)
        {
			string lugar = LugarTxt.Text;
			string admin = AdminTxt.Text;
			string trabajador = TrabajadorTxt.Text;
			string base_ruta = BaseTxt.Text;

			if (string.IsNullOrEmpty(lugar) )
			{
                DisplayAlert("Atención", "No hay informacion en el lugar", "Ok");
                return;
            }
			if (string.IsNullOrEmpty(admin) )
			{
                DisplayAlert("Atención", "No hay informacion en el administrador", "Ok");
                return;
            }
			if (string.IsNullOrEmpty(trabajador) )
			{
                DisplayAlert("Atención", "No hay informacion en el trabajador", "Ok");
                return;
            }	
			if (string.IsNullOrEmpty(base_ruta) )
			{
                DisplayAlert("Atención", "No hay informacion en la base", "Ok");
                return;

            }
            await firebaseClient.Child("Rutas").PostAsync(JsonConvert.SerializeObject(new Ruta()
            {
                Lugar = lugar,
                Trabajador_a_cargo = trabajador,
                Administrador = admin,
                Base = base_ruta

            }));

            DisplayAlert("Creacion", "La ruta ha sido creado correctamente", "Ok");

            Navigation.PushAsync(new NavigationPage(new PanelAdmin()));





        }

    }
}