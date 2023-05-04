using Aplicacion.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Account.Test.Module
{
    internal static class Test
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<TestPage, ViewModel.Test>();
        }

        internal static Page CreateTestPage()
        {
            return ViewsManager.CreateView<TestPage>();
        }

        private static void InitializeDependencyPages()
        {
        }
    }
}
