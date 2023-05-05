using System;
using System.Collections.Generic;
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
    }
}
