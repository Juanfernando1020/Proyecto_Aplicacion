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
    public partial class Informacion_Trabajador : ContentPage
    {
        public Informacion_Trabajador()
        {
            InitializeComponent();
        }
        private void Deseas_Editar_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Warning", "NO TIENES PERMISOS PARA EDITAR", "Ok");
        }
    }
}