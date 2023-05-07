using Acr.UserDialogs;
using Aplicacion.Common.Helpers;
using Aplicacion.Common.MVVM.Navigation.Interfaces;
using Aplicacion.Common.MVVM.Navigation.Services;
using Aplicacion.Common.PagesBase;
using Aplicacion.Common.PagesBase.Enums;
using Aplicacion.Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.Common.MVVM
{
    public abstract class ViewModelBase : BindableBase
    {
        #region Properties
        protected bool LockUIWhenBusy { get; set; } = true;

        protected INavigationService NavigationService { get; }

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
        
        private Dictionary<string, object> _args;
        public virtual Dictionary<string, object> Args
        {
            get => _args;
            set => _args = value;
        }

        Element _parent;
        public virtual Element Parent
        {
            get => _parent;
            set
            {
                SetProperty(ref _parent, value);
            }
        }
        #endregion

        #region Events
        public AsyncAction<Dictionary<string, object>> CallBack;
        #endregion

        #region Constructor
        public ViewModelBase()
        {
            NavigationService = new NavigationService();
            Args = new Dictionary<string, object>();
            IsBusy = false;
            LoadingTitle = "Loading";

            Module.App.ViewModel = this;
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
        public virtual void OnInitialize()
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
                    if (LockUIWhenBusy)
                    {
                        if (IsBusy)
                            UserDialogs.Instance.ShowLoading(LoadingTitle);
                        else
                            UserDialogs.Instance.HideLoading();
                    }
                    break;
            }
        }
        #endregion
    }
}
