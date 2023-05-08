using Aplicacion.Models;
using Aplicacion.Pages.Main.Enums;
using Aplicacion.Pages.Main.Profile.Contracts;
using Aplicacion.Pages.Main.Profile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Profile.Service
{
    internal class MainProfile : IMainProfileService
    {
        public ProfileInformation GetProfileInformationAsync(MainTypesEnum mainType, Administrador administrador = null, Trabajador trabajador = null)
        {
            ProfileInformation profileInformation;
            switch (mainType)
            {
                case MainTypesEnum.Worker:
                    profileInformation = new ProfileInformation(trabajador.Name, trabajador.Phone, trabajador.Location, false, trabajador.Admin);
                    break;
                case MainTypesEnum.Admin:
                    profileInformation = new ProfileInformation(administrador.Name, administrador.Phone, administrador.Location, true);
                    break;
                default:
                    throw new InvalidNavigationException("You have to send and option on 'mainType' parameter.");
            }

            return profileInformation;
        }
    }
}
