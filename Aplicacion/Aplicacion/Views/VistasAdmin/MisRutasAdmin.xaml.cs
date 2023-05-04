using Aplicacion.Models;
using Aplicacion.Vistas.VistasAdmin;
using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class MisRutasAdmin : ContentPage
    {
        public MisRutasAdmin()
        {
            InitializeComponent();
            CargarRutas();
        }
        private async void OnBackButtonTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public static List<Ruta> ObtenerRutas()
        {
            var firebase = new FirebaseClient("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");

            var rutas = firebase
                .Child("Rutas")
                .OnceAsync<Ruta>().Result
                .Select(x => x.Object)
                .ToList();

            return rutas;
        }

        public string ObtenerNumeroUsuario()
        {
            var usuarioLogueadoJson = Application.Current.Properties["usuario"] as string;
            var usuarioLogueado = JsonConvert.DeserializeObject<Administrador>(usuarioLogueadoJson);

            return usuarioLogueado.Phone;
        }

        private async void CargarRutas()
        {
            var rutas = ObtenerRutas();
            var numeroUsuario = ObtenerNumeroUsuario();

            List<Ruta> rutasMostradas = new List<Ruta>();

            foreach (var ruta in rutas)
            {
                if (ruta.Administrador == numeroUsuario)
                {
                    rutasMostradas.Add(ruta);
                }
            }

            listView.ItemsSource = rutasMostradas;
        }
    }
}
