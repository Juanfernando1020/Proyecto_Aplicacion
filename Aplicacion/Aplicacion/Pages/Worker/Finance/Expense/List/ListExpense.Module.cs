using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Finance.Expense.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Finance.Expense.List.Module
{
    internal static class ListExpense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListExpensePage, ViewModel.ListExpense>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<ListExpensePage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
