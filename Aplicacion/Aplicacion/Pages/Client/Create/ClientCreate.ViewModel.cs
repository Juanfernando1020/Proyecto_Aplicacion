using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Aplicacion.Config.Messages;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Alerts.Messages;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;
using Xamarin.CommonToolkit.Result;
using Xamarin.CommunityToolkit.ObjectModel;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Firebase.Storage;
using System.IO;
using Aplicacion.Config;
using Aplicacion.Pages.Client.Channels;
using Xamarin.CommonToolkit.Mvvm.Navigation.Services;


namespace Aplicacion.Pages.Client.Create.ViewModel
{
    internal class ClientCreate : PageViewModelBase, IClientCreatedChannel
    {
        #region Variables

        private readonly IGenericService<Clients, Guid> _genericService;
        private FirebaseStorage _firebaseStorage;
        private string _imageId;

        #endregion
        
        #region Properties

        private Clients _client;
        public Clients Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }

        public ICommand CreateClientCommand => new AsyncCommand(CreateClientController);

        private ImageSource _photoSource;
        public ImageSource PhotoSource
        {
            get => _photoSource;
            set => SetProperty(ref _photoSource, value);
        }

        public ICommand TakePhotoCommand => new AsyncCommand(TakePhotoController);


        private bool _isIconLabelVisible = true;
        public bool IsIconLabelVisible
        {
            get => _isIconLabelVisible;
            set => SetProperty(ref _isIconLabelVisible, value);
        }


        #endregion

        #region Methods

        private async Task CreateClientController()
        {
            IsBusy = true;

            bool isValid = await ValidateClient();

            if (isValid)
            {
                // Subir la imagen a Firebase Storage
                string imageId = null;
                if (PhotoSource != null)
                {
                    Stream imageStream = await (PhotoSource as StreamImageSource).Stream.Invoke(default);
                    string imageName = $"{Guid.NewGuid()}.jpg";

                    // Configurar Firebase Storage
                    var storage = new FirebaseStorage("app-cobranzas-4a3dc.appspot.com")
                        .Child("Ids") // Ruta dentro del bucket de almacenamiento
                        .Child(imageName); // Nombre de archivo en Firebase Storage

                    // Subir la imagen
                    var task = storage.PutAsync(imageStream);

                    // Esperar a que se complete la carga
                    imageId = await task;
                }

                // Guardar el ID de la imagen en el modelo de cliente
                Client.IDImage = imageId;

                // Insertar el cliente en Firebase Realtime Database
                ResultBase result = await _genericService.InsertAsync(Client);

                if (result.IsSuccess)
                {
                    INavigationParameters parameters = new NavigationParameters();
                    parameters.Add(ArgKeys.Client, Client);

                    MessagingCenter.Send<IClientCreatedChannel, INavigationParameters>(this, nameof(IClientCreatedChannel), parameters);
                    await AlertService.ShowAlert(new SuccessMessage(CommonMessages.Success.Create));
                    await NavigationService.PopAsync();
                }
                else
                {
                    await AlertService.ShowAlert(new ErrorMessage(CommonMessages.Error.InformationMessage));
                }
            }

            IsBusy = false;
        }


        private async Task<bool> ValidateClient()
        {
            if (string.IsNullOrEmpty(Client.Name))
            {
                await AlertService.ShowAlert(new WarningMessage("Debes asignarle un nombre."));
                return false;
            }

            if (string.IsNullOrEmpty(Client.Phone))
            {
                await AlertService.ShowAlert(new WarningMessage("Debes asignarle un teléfono."));
                return false;
            }

            if (string.IsNullOrEmpty(Client.Address))
            {
                await AlertService.ShowAlert(new WarningMessage("Debes asignarle una dirección."));
                return false;
            }

            if (PhotoSource == null)
            {
                await AlertService.ShowAlert(new WarningMessage("Debes tomar una foto."));
                return false;
            }

            return true;
        }

        private async Task TakePhotoController()
        {
            MediaFile photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                SaveToAlbum = false,
                RotateImage = true,
                DefaultCamera = CameraDevice.Rear,
                AllowCropping = false,
                ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
            });

            if (photo != null)
            {
                PhotoSource = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();
                });

                IsIconLabelVisible = false;
            }
        }



        #endregion

        #region Constructor

        public ClientCreate()
        {
            Client = new Clients()
            {
                Id = Guid.NewGuid(),
                IsActive = true
            };

            _genericService = GetGenericService<Clients, Guid>();

            _firebaseStorage = new FirebaseStorage("app-cobranzas-4a3dc.appspot.com", new FirebaseStorageOptions
            {
                ThrowOnCancel = true,
            });


        }

        #endregion

        #region Overrides

        public override async void OnInitialize(INavigationParameters parameters)
        {
            base.OnInitialize(parameters);
            OnLoad(parameters);
        }

        #endregion

        #region OnLoad

        private void OnLoad(INavigationParameters parameters)
        {
            if (Aplicacion.Module.App.RouteInfo is Routes route)
            {
                Client.Route = route.Id;
            }
        }

        #endregion
    }
}
