using Aplicacion.Enums;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Profile.Models;
using Aplicacion.Pages.User.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.User.Details.Service
{
    internal class UserDetail 
    {
   public ProfileInformation GetProfileInformationAsync(RolesEnum mainType, Users administrador = null, Users trabajador = null)
        {
            ProfileInformation profileInformation;
            switch (mainType)
            {
                case RolesEnum.Worker:
                    profileInformation = new ProfileInformation(trabajador.Name, trabajador.Phone, "Ubicacion", false, "Admin"); ///Hacer consulta ruta y admin 
                    break;
                case RolesEnum.Admin:
                    profileInformation = new ProfileInformation(administrador.Name, administrador.Phone, "Ubicacion" , true); // hacer consulta de ubicacion teniendo en cuenta que tiene muchas rutas a cargo<
                    break;
                default:
                    throw new InvalidNavigationException("You have to send and option on 'mainType' parameter.");
            }

            return profileInformation;
        }
    }
}

