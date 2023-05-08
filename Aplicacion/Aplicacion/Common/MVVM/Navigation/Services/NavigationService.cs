using Aplicacion.Common.Helpers;
using Aplicacion.Common.MVVM.Navigation.Interfaces;
using Aplicacion.Common.MVVM.Navigation.Models;
using Aplicacion.Common.PagesBase.Enums;
using Aplicacion.Common.PagesBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace Aplicacion.Common.MVVM.Navigation.Services
{
    internal class NavigationService : INavigationService
    {
        #region NavigateTo
        public async Task<NavigationResult> NavigateToAsync<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            where TView : Page
            => await GoToAsync(typeof(TView), pageBase, args);

        public async Task<NavigationResult> NavigateToAsync(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            => await GoToAsync(page?.GetType(), pageBase, args);

        private async Task<NavigationResult> GoToAsync(Type page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
        {
            try
            {
                switch (pageBase)
                {
                    case PagesBaseEnum.ContentPage:
                        ContentPageBase contentPage = ViewsManager.CreateView(page, args) as ContentPageBase;

                        await Application.Current.MainPage.Navigation.PushAsync(contentPage);
                        break;
                }
                return new NavigationResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new NavigationResult(false, ex);
            }
        }
        #endregion

        #region NavigateToRoot
        public async Task<NavigationResult> NavigateToRootAsync<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            where TView : Page
            => await GoToRootAsync(typeof(TView), pageBase, args);

        public async Task<NavigationResult> NavigateToRootAsync(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            => await GoToRootAsync(page?.GetType(), pageBase, args);

        private async Task<NavigationResult> GoToRootAsync(Type page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
        {
            try
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    switch (pageBase)
                    {
                        case PagesBaseEnum.ContentPage:
                            ContentPageBase contentPage = ViewsManager.CreateView(page, args) as ContentPageBase;

                            Application.Current.MainPage = new NavigationPage(contentPage);
                            break;
                    }
                });

                return new NavigationResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new NavigationResult(false, ex);
            }
        }
        #endregion

        #region Pop
        public async Task<NavigationResult> PopAsync(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
        {
            try
            {
                switch (pageBase)
                {
                    case PagesBaseEnum.ContentPage:
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Module.App.ViewModel.CallBack?.Invoke(args);
                        break;
                }

                return new NavigationResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new NavigationResult(false, ex);
            }
        }
        #endregion

        #region PopToRoot
        public async Task<NavigationResult> PopToRootAsync(Dictionary<string, object> args = null)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                await Module.App.ViewModel.CallBack?.Invoke(args);

                return new NavigationResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new NavigationResult(false, ex);
            }
        }
        #endregion
    }
}
