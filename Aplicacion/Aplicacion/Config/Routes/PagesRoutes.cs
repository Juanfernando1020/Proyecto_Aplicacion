using Aplicacion.Pages.Client.Create;
using Aplicacion.Pages.Client.Details;
using Aplicacion.Pages.Client.List;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Config.Routes
{
    internal static class PagesRoutes
    {
        internal const string Main = "MainPage";

        internal static class Account
        {
            internal const string Login = "LoginPage";
        }

        internal static class User
        {
            internal const string Create = "UserCreatePage";
            internal const string Details = "UserDetailsPage";
            internal const string List = "UserListPage";
        }

        internal static class Billing
        {
            internal const string Create = "BillingCreatePage";
            internal const string Delay = "BillingDelayPage";
            internal const string Details = "BillingDetailsPage";
            internal const string List = "BillingListPage";
            internal const string ListPlan = "ListPlanPage";

        }

        internal static class Loan
        {
            internal const string Create = "LoanCreatePage";
            internal const string Details = "LoanDetailsPage";
            internal const string List = "LoanListPage";

        }

        internal static class Client
        {
            internal const string Create = "ClientCreatePage";
            internal const string Details = "ClientDetailsPage";
            internal const string List = "ClientListPage";
        }

        internal static class Expense
        {
            internal const string List = "ExpenseListPage";
            internal const string Create = "ExpenseCreatePage";

        }

        internal static class Day
        {
            internal const string Summary = "DaySummaryPage";
        }

        internal static class Route
        {
            internal const string List = "RouteListView";
            internal const string Details = "RouteDetailsPage";

            internal const string Create = "CreateRoutePage";

        }

        internal static class Basis
        {
            internal const string Add = "AddBasisPage";
        }
    }
}
