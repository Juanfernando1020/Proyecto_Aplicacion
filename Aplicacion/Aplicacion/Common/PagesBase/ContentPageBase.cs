using Aplicacion.Common.MVVM;
using Aplicacion.Helpers;
using Xamarin.Forms;

namespace Aplicacion.Common.PagesBase
{
    public abstract class ContentPageBase : ContentPage
    {
        protected ViewModelBase ViewModel { get; private set; }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            ViewModel = this.BindingContext as ViewModelBase;
        }
    }
}
