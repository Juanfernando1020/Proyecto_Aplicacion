using Aplicacion.Common.Helpers;
using Aplicacion.Common.MVVM;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using Aplicacion.Common.PagesBase.Enums;

namespace Aplicacion.Common.PagesBase
{
    public abstract class ContentPageBase : ContentPage
    {
        #region Properties
        //internal Dictionary<string, object> Args;
        public ViewModelBase ViewModel
        {
            get => BindingContext as ViewModelBase;
        }
        #endregion

        #region Methods
        #endregion

        #region Constructor
        public ContentPageBase()
        {
            base.SetBinding(Page.IsBusyProperty, new Binding("IsBusy", 0, null, null, null, null));
        }
        #endregion

        #region Overrides
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(ViewModel != null)
            {
                ViewModel.Parent = this;
                ViewModel.OnInitialize();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel?.OnViewAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel?.OnViewDisappearing();
        }
        #endregion
    }
}
