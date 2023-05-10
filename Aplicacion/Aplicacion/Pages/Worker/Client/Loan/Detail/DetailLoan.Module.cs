using Aplicacion.Common.Helpers;
using Aplicacion.Pages.Worker.Client.Loan.Create;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Aplicacion.Pages.Worker.Client.Loan.Detail.Module
{
    internal static class DetailLoan
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
            RegisterPage();
        }
        private static void RegisterPage()
        {
            ViewsManager.RegisterView<DetailLoanPage, ViewModel.DetailLoan>();
        }
        internal static Page CreatePage()
        {
            return ViewsManager.CreateView<DetailLoanPage>();
        }
        private static void InitializeDependencyPages()
        {
        }
    }
}
