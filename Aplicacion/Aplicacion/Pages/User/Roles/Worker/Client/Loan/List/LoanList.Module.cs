﻿using Aplicacion.Common.Helpers;

namespace Aplicacion.Pages.User.Roles.Worker.Client.Loan.List.Module
{
    internal static class LoanList
    {
        internal static void Initialize()
        {
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<LoanListPage, ViewModel.LoanList>();
        }
    }
}
