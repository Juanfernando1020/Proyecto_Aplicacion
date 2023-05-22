using Aplicacion.Models;
using Xamarin.CommonToolkit.PagesBase;
using Xamarin.Forms.Xaml;

namespace Aplicacion.Pages.User.Views.UserBySpecification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserBySpecificationPopup : CustomPopupBase
    {
        public UserBySpecificationPopup()
        {
            var obj = this.BindingContext;
            InitializeComponent();
        }
    }
}