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
            Navigation.PushAsync( new CrearUsuario() );
        }
    }
}