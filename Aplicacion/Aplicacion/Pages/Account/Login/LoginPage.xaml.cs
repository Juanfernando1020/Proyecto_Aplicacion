using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Aplicacion.Vistas.VistasAdmin;
using Firebase.Database;
using Aplicacion.Models;
using Newtonsoft.Json;
using Firebase.Database.Query;
using Aplicacion.Views.VistasTrabajador;
using Aplicacion.Common.PagesBase;

namespace Aplicacion.Pages.Account.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPageBase
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        //FirebaseClient firebaseClient = new FirebaseClient("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");

        //private async void Boton_Inicio(object sender, EventArgs e)
        //{
        //    var usuariosTrabajadores = await firebaseClient
        //        .Child("Trabajadores")
        //        .OrderByKey()
        //        .OnceAsync<Trabajador>();

        //    var usuariosAdministradores = await firebaseClient
        //        .Child("Administradores")
        //        .OrderByKey()
        //        .OnceAsync<Administrador>();

        //    bool user_exist = false;
        //    foreach (var user in usuariosTrabajadores)
        //    {
        //        user_exist = user.Object.Phone.Equals(txtUsuario.Text) && user.Object.Password.Equals(txtPassword.Text);
        //        if (user_exist)
        //        {
        //            // Crear un objeto Usuario con los datos del usuario que inició sesión
        //            var Usuario_Logueado = new Trabajador
        //            {
        //                Name = user.Object.Name,
        //                Phone = user.Object.Phone,
        //                Location= user.Object.Location,
        //                Admin = user.Object.Admin,
                        
        //            };

        //            // Almacenar el objeto Usuario en las propiedades de la aplicación
        //            Application.Current.Properties["usuario"] = JsonConvert.SerializeObject(Usuario_Logueado);

        //            // Navegar a la página PanelTrabajador
        //            App.Current.MainPage = new NavigationPage(new PanelTrabajador());
        //            return;
        //        }
        //    }

        //    foreach (var user in usuariosAdministradores)
        //    {
        //        user_exist = user.Object.Phone.Equals(txtUsuario.Text) && user.Object.Password.Equals(txtPassword.Text);
        //        if (user_exist)
        //        {
        //            // Crear un objeto Usuario con los datos del usuario que inició sesión
        //            var Usuario_Logueado = new Administrador
        //            {
        //                Name = user.Object.Name,
        //                Phone= user.Object.Phone,
        //                Location = user.Object.Location,
                        
        //            };

        //            // Almacenar el objeto Usuario en las propiedades de la aplicación
        //            Application.Current.Properties["usuario"] = JsonConvert.SerializeObject(Usuario_Logueado);

        //            // Navegar a la página PanelAdmin
        //            App.Current.MainPage = new NavigationPage(new PanelAdmin());
        //            return;
        //        }
        //    }

        //    txtUsuario.Text = string.Empty; // Limpia el Entry de usuario
        //    txtPassword.Text = string.Empty; // Limpia el Entry de contraseña
        //}
    }
}