using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Common.MVVM
{
    public abstract class ViewModelBase : BindableBase
    {
        private bool _isBusy;
        public bool IsBusy
        { 
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
            } 
        }

        public ViewModelBase()
        {
            IsBusy = false;
        }
    }
}
