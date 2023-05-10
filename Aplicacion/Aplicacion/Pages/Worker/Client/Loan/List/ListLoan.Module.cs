using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Loan.Detail;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Loan.List.Module
{
    internal static class ListLoan
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<ListLoanPage, ViewModel.ListLoan>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
