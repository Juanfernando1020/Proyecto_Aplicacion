using Aplicacion.Helpers;
using Aplicacion.Pages.Account.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.AddNewExpense.Module
{
    internal static class AddNewExpense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<AddNewExpensePage, ViewModel.AddNewExpens>();
        }
        internal static Page CreateAddNewExpensePage()
        {
            return ViewsManager.CreateView<AddNewExpensePage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
