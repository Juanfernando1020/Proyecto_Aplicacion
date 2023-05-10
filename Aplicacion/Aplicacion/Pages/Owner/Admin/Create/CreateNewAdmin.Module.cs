using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Owner.Admin.Create.Module
{
    internal static class CreateNewAdmin
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView < CreateNewAdminPage, ViewModel.CreateNewAdmin>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<CreateNewAdminPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
