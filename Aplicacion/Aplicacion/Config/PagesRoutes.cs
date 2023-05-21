using Aplicacion.Pages.Client.Create;
using Aplicacion.Pages.Client.Details;
using Aplicacion.Pages.Client.List;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Config
{
    internal static class RoutePages
    {
        internal static string Main => "MainPage";

        internal static class Account
        {
            internal static string Login => "LoginPage";
        }

        internal static class User
        {
            internal static string Create => "UserCreatePage";
            internal static string Details => "UserDetailsPage";
            internal static string List => "UserListPage";
        }

        internal static class Billing
        {
            internal static string Create => "BillingCreatePage";
            internal static string Delay => "BillingDelayPage";
            internal static string Details => "BillingDetailsPage";
            internal static string List => "BillingListPage";
            internal static string ListPlan => "ListPlanPage";
            
        }

        internal static class Loan
        {
            internal static string Create => "LoanCreatePage";
            internal static string Details => "LoanDetailsPage";
            internal static string List => "LoanListPage";

        }

        internal static class Client
        {
            internal static string Create => "ClientCreatePage";
            internal static string Details => "ClientDetailsPage";
            internal static string List => "ClientListPage";
        }

        internal static class Expense
        {
            internal static string List => "ExpenseListPage";
            internal static string Create => "ExpenseCreatePage";

        }

        internal static class Day
        {
            internal static string Summary => "DaySummaryPage";
        }

        internal static class Route
        {
            internal static string List => "ListRoutesPage";
            internal static string Details => "DetailRoutePage";

            internal static string Create => "CreateRoutePage";
            
        }

        internal static class Basis
        {
            internal static string Add => "AddBasisPage";
        }
    }
}
