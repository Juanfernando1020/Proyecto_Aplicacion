﻿namespace Aplicacion.Pages.Loan.Module
{
    internal static class Loan
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Create.Module.LoanCreate.Initialize();
            Details.Module.LoanDetails.Initialize();
            List.Module.LoanList.Initialize();
        }
    }
}
