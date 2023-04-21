using Aplicacion.Vistas.VistasAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Views.VistasTrabajador
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanelTrabajador : ContentPage
    {
        public PanelTrabajador()
        {
            InitializeComponent();
        }

        private void Boton_Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new Informacion_Trabajador()));
        }
    }
}