using Aplicacion.Pages.Client.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Basis.Add.Module 
{
    class AddBasis
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<AddBasisPage, ViewModel.AddBasis>();
        }
    }
}
