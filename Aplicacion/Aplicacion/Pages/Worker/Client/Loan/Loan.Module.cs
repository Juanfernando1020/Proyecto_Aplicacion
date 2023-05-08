using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Worker.Client.Loan.Module
{
    internal static class Loan
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Create.Module.CreateLoan.Initialize();
        }
    }
}
