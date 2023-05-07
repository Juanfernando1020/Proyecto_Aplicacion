using Aplicacion.Common.MVVM.Navigation.Models;
using Aplicacion.Common.PagesBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aplicacion.Common.MVVM.Navigation.Interfaces
{
    public interface INavigationService
    {
        Task<NavigationResult> NavigateTo<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null) where TView : Page;
        Task<NavigationResult> NavigateTo(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);

        Task<NavigationResult> NavigateToRoot<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null) where TView : Page;
        Task<NavigationResult> NavigateToRoot(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);
        
        Task<NavigationResult> PopAsync(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);
        Task<NavigationResult> PopToRootAsync(Dictionary<string, object> args = null);
    }
}
