using Aplicacion.Pages.Main.Profile.Models;
using System;

namespace Aplicacion.Pages.Main.Profile.Contracts
{
    public interface IMainProfileService
    {
        ProfileInformation GetProfileInformationAsync(Guid userId);
    }
}
