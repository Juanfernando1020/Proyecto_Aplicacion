using Aplicacion.Views.VistasAdmin;
using Aplicacion.Views.VistasTrabajador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Vistas.VistasAdmin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanelAdmin : ContentPage
    {
        public PanelAdmin()
        {
            InitializeComponent();
        }
        private void Boton_CrearUsuario(object sender, EventArgs e)
        {
            //Navigation.PushAsync(Pages.Admin.User.Create.Module.CreateUser.CreatePage());
        }

        private void Boton_Informacion (object sender, EventArgs e)
        {
            Navigation.PushAsync(new Informacion_Admin());
        }

        private void Boton_CreaRuta(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreaRuta());
        }

        private void Boton_Mis_Rutas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisRutasAdmin());
        }
    }
}