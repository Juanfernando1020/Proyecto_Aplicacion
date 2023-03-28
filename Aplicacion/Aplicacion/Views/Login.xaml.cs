using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Aplicacion.Vistas.VistasAdmin;
using Firebase.Database;
using Aplicacion.Models;
using Newtonsoft.Json;
using Firebase.Database.Query;
using Aplicacion.Views.VistasTrabajador;

namespace Aplicacion.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }


        private void TxtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Limpia el Entry de contraseña si el usuario cambia su nombre de usuario
            txtPassword.Text = string.Empty;
        }

        FirebaseClient firebaseClient = new FirebaseClient("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
        private async void Boton_Inicio(object sender, EventArgs e)
        {
            var usuariosTrabajadores = await firebaseClient
                .Child("Trabajadores")
                .OrderByKey()
                .OnceAsync<Trabajador>();

            var usuariosAdministradores = await firebaseClient
                .Child("Administradores")
                .OrderByKey()
                .OnceAsync<Administrador>();

            bool user_exist = false;
            foreach (var user in usuariosTrabajadores)
            {
                user_exist = user.Object.Phone.Equals(txtUsuario.Text) && user.Object.Password.Equals(txtPassword.Text);
                if (user_exist)
                {
                    App.Current.MainPage = new NavigationPage(new PanelTrabajador());
                    return;
                }
            }

            foreach (var user in usuariosAdministradores)
            {
                user_exist = user.Object.Phone.Equals(txtUsuario.Text) && user.Object.Password.Equals(txtPassword.Text);
                if (user_exist)
                {
                    App.Current.MainPage = new NavigationPage(new PanelAdmin());
                    return;
                }
            }

            txtUsuario.Text = string.Empty; // Limpia el Entry de usuario
            txtPassword.Text = string.Empty; // Limpia el Entry de contraseña
        }
    }
}
