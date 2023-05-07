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

namespace Aplicacion.Common.MVVM.Navigation.Services
{
    internal class NavigationService : INavigationService
    {
        #region NavigateTo
        public async Task<NavigationResult> NavigateTo<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            where TView : Page
            => await NavigateTo(typeof(TView), pageBase, args);

        public async Task<NavigationResult> NavigateTo(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            => await NavigateTo(page?.GetType(), pageBase, args);

        private async Task<NavigationResult> NavigateTo(Type page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
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
                return new NavigationResult(false, ex);
            }
        }
        #endregion

        #region NavigateToRoot
        public async Task<NavigationResult> NavigateToRoot<TView>(PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            where TView : Page
            => await NavigateToRoot(typeof(TView), pageBase, args);

        public async Task<NavigationResult> NavigateToRoot(Page page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
            => await NavigateToRoot(page?.GetType(), pageBase, args);

        private Task<NavigationResult> NavigateToRoot(Type page, PagesBaseEnum pageBase = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    switch (pageBase)
                    {
                        case PagesBaseEnum.ContentPage:
                            ContentPageBase contentPage = ViewsManager.CreateView(page, args) as ContentPageBase;

                            Application.Current.MainPage = new NavigationPage(contentPage);
                            break;
                    }
                });

                return Task<NavigationResult>.FromResult(new NavigationResult(true));
            }
            catch (Exception ex)
            {
                return Task<NavigationResult>.FromResult(new NavigationResult(false, ex));
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
                return new NavigationResult(false, ex);
            }
        }
        #endregion
    }
}
