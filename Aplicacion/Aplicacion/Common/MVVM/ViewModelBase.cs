using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Aplicacion.Common.MVVM
{
    public abstract class ViewModelBase : BindableBase
    {
        #region Properties
        private bool _isBusy;
        public bool IsBusy
        { 
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
            } 
        }
        
        private string _loadingTitle;
        public string LoadingTitle
        { 
            get => _loadingTitle;
            set
            {
                SetProperty(ref _loadingTitle, value);
            } 
        }
        #endregion

        #region Constructor
        public ViewModelBase()
        {
            IsBusy = false;
        }
        #endregion

        #region Methods

        #endregion

        #region Virtual
        public virtual void OnViewAppearing()
        {

        }
        public virtual void OnViewDisappearing()
        {

        }
        #endregion

        #region Overrides
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(IsBusy):
                    if (IsBusy)
                    {
                        UserDialogs.Instance.Loading(LoadingTitle);
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                    break;
            }
        }
        #endregion
    }
}
