using Aplicacion.Models;
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
	public partial class Informacion_Admin : ContentPage
	{
		public Informacion_Admin ()
		{
			InitializeComponent ();


            // Deserializar la cadena JSON en un objeto Administrador
            var usuarioLogueadoJson = Application.Current.Properties["usuario"] as string;
            var usuarioLogueado = JsonConvert.DeserializeObject<Administrador>(usuarioLogueadoJson);

            // Establecer el objeto Administrador como el contexto de enlace de la página
            BindingContext = usuarioLogueado;
        }
        private void Deseas_Editar_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Warning", "AQUI PASO A OTRA PAGINA", "Ok");
        }
    }
}