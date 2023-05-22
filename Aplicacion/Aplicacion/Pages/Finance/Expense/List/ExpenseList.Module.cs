﻿using Xamarin.CommonToolkit.Helpers;

namespace Aplicacion.Pages.Finance.Expense.List.Module
{
    internal static class ExpenseList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterPage<ExpenseListPage, ViewModel.ExpenseList>();
        }
    }
}
