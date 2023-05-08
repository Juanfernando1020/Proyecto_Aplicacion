using Aplicacion.Common.MVVM;
using Aplicacion.Common.MVVM.Alerts.Messages;
using Aplicacion.Config;
using Aplicacion.Models;
using Aplicacion.Pages.Main.Enums;
using Aplicacion.Pages.Main.Profile.Contracts;
using Aplicacion.Pages.Main.Profile.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Profile.ViewModel
{
    internal class MainProfile : ViewModelBase
    {
        #region Variables
        private MainTypesEnum mainTypesEnum;

        private readonly IMainProfileService _mainProfileService;
        #endregion

        #region Property
        private ProfileInformation _information;
        public ProfileInformation Information
        {
            get => _information;
            set
            {
                SetProperty(ref _information, value);
            }
        }

        public ICommand EditCommand => new Command(async () => await Editcontroller());
        #endregion

        #region Method
        private async Task Editcontroller()
        {
            switch (mainTypesEnum)
            {
                case MainTypesEnum.Worker:
                    await AlertService.ShowAlert(new WarningMessage("No tienes permiso para editar."));
                    break;
                case MainTypesEnum.Admin:
                    await AlertService.ShowAlert(new SuccessMessage("Soon."));
                    break;
            }
        }
        #endregion

        #region Constructor
        public MainProfile()
        {
            _mainProfileService = new Service.MainProfile();
        }
        #endregion

        #region Overrides
        public override void OnInitialize()
        {
            IsBusy = true;
            base.OnInitialize();
            OnLoad();
            IsBusy = false;
        }
        #endregion

        #region OnLoad
        private async void OnLoad()
        {
            if (!Args.ContainsKey(ArgKeys.MainType))
            {
                Console.WriteLine("It was missing the 'MainType' key.");
                await AlertService.ShowAlert(new ErrorMessage("No se puede cargar la información. Intentalo más tarde."));
                await NavigationService.PopAsync();
                return;
            }

            mainTypesEnum = (MainTypesEnum)Args[ArgKeys.MainType];
            Trabajador worker = JsonConvert.DeserializeObject<Trabajador>((string)Application.Current.Properties[ArgKeys.User]) ?? default;
            Administrador admin = JsonConvert.DeserializeObject<Administrador>((string)Application.Current.Properties[ArgKeys.User]) ?? default;

            Information = _mainProfileService.GetProfileInformationAsync(mainTypesEnum, admin, worker);
        }
        #endregion
    }
}
