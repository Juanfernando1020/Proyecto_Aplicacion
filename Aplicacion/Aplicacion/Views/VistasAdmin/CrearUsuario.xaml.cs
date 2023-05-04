using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Aplicacion.Models;
using Firebase.Database;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Firebase.Database.Query;

namespace Aplicacion.Vistas.VistasAdmin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearUsuario : ContentPage
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private async void OnBackButtonTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        FirebaseClient firebaseClient = new FirebaseClient("https://app-cobranzas-4a3dc-default-rtdb.firebaseio.com");
        private async void Registrar_Nuevo_Usuario(object sender, EventArgs e)
        {
            string nombre = UsuarioTxt.Text;
            string telefono = TelefonoTxt.Text;
            string ubicacion = UbicacionTxt.Text;
            string contrasena = ContraseñaTxt.Text;
            string confirmacion = ConfirmacionTxt.Text;

            if (String.IsNullOrEmpty(nombre))
            {
                    DisplayAlert("Warning", "No hay informacion en el nombre", "Ok");
                    return;
            }
            if (String.IsNullOrEmpty(telefono))
            {
                DisplayAlert("Warning", "No hay informacion en el teléfono", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(ubicacion))
            {
                DisplayAlert("Warning", "No hay informacion en la ubicación", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(contrasena))
            {
                DisplayAlert("Warning", "No hay informacion en la contraseña", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(confirmacion))
            {
                DisplayAlert("Warning", "No hay informacion en la confirmacion", "Ok");
                return;
            }
            if (contrasena != confirmacion)
            {
                DisplayAlert("Warning", "Las contraseñas no coinciden", "Ok");
                return;
            }

            if (AdminCheckBox.IsChecked)
            {
                await firebaseClient.Child("Administradores").PostAsync(JsonConvert.SerializeObject(new Administrador()
                {
                    Name = nombre,
                    Phone = telefono,
                    Password = contrasena,
                    Location = ubicacion,
                    

                }));
            }

            else
            {
             string adminName = await DisplayPromptAsync("Nombre del administrador", "Ingrese el nombre del administrador:");
                await firebaseClient.Child("Trabajadores").PostAsync(JsonConvert.SerializeObject(new Trabajador()
                {
                    Name = nombre,
                    Phone = telefono,
                    Location = ubicacion,
                    Password = contrasena,
                    Admin = adminName,

                }));
            }
            DisplayAlert("Creacion", "El usuario ha sido creado correctamente", "Ok");
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}