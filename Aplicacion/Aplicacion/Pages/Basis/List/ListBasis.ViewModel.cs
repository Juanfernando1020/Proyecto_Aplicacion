using System;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm.Services.Interfaces;
using Xamarin.CommonToolkit.Mvvm.ViewModels;

namespace Aplicacion.Pages.Basis.List.ViewModel
{
    internal class ListBasis : PageViewModelBase
    {
        #region Variables
        private IGenericService<Basises, Guid> _genericService;
        #endregion

        #region Constructor
        public ListBasis()
        {
            _genericService = GetGenericService<Basises, Guid>();
        }
        #endregion
    }
}
