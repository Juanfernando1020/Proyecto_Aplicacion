using Aplicacion.Common.Helpers;
using Aplicacion.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Admin.Panel.Module
{
    internal static class AdminDashboard
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<AdminDashboardPage, ViewModel.AdminDashboard>();
        }

        internal static Page CreateAdminDashboardPage()
        {
            return ViewsManager.CreateView<AdminDashboardPage>();
        }

        private static void InitializeDependencyPages()
        {
        }
    }
}
