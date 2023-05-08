using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Billing.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Loan.Create.Module
{
    internal static class CreateLoan
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<CreateLoanPage, ViewModel.CreateLoan>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<CreateLoanPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
