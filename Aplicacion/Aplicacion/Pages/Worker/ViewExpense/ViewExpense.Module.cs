using Aplicacion.Common.MVVM;
using Aplicacion.Helpers;
using Aplicacion.Pages.Account.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.ViewExpense.Module
{
    internal static class ViewExpense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }

        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ViewExpensePage, ViewModel.ViewExpense>();
        }

        internal static Page CreateViewExpensePage()
        {
            return ViewsManager.CreateView<ViewExpensePage>();
        }
        private static void InitializeDependencyPages()
        {
        }


    }
}
