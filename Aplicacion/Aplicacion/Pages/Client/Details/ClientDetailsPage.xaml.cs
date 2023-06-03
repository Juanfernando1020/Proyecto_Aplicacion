using Plugin.Media.Abstractions;
using Xamarin.CommonToolkit.PagesBase;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Pages.Client.Details
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientDetailsPage : ContentPageBase
	{
		public ClientDetailsPage()
		{
			InitializeComponent();

			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

			TakePhoto.GestureRecognizers.Add(tapGestureRecognizer);
		}

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
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
                    Photo.Source = ImageSource.FromStream(() =>
                    {
                        return photo.GetStream();
                    });

                    // Ocultar el icono
                    IconLabel.IsVisible = false;
                }
            });
        }


    }
}