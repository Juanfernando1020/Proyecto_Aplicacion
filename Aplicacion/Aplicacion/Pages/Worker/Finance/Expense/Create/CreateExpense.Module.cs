using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Billing.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Finance.Expense.Create.Module
{
    internal static class CreateExpense
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateExpensePage, ViewModel.CreateExpense>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<CreateExpensePage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
