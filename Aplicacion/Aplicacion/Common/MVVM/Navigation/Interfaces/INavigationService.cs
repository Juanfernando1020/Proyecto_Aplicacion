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
        Task<NavigationResult> NavigateToAsync<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null) where TView : Page;
        Task<NavigationResult> NavigateToAsync(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);
        
        Task<NavigationResult> NavigateToAsync(string page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);

        Task<NavigationResult> NavigateToRootAsync<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null) where TView : Page;
        Task<NavigationResult> NavigateToRootAsync(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);
        Task<NavigationResult> NavigateToRootAsync(string page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);
        
        Task<NavigationResult> PopAsync(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null);
        Task<NavigationResult> PopToRootAsync(Dictionary<string, object> args = null);
    }
}
