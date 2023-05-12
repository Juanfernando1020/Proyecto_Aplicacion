using Aplicacion.Pages.User.Roles.Worker.Client.Create;
using Aplicacion.Pages.User.Roles.Worker.Client.Details;
using Aplicacion.Pages.User.Roles.Worker.Client.List;
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
        }

        internal static class Billing
        {
            internal static string Create => "BillingCreatePage";
            internal static string Delay => "BillingDelayPage";
            internal static string Details => "BillingDetailsPage";
            internal static string List => "BillingListPage";
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
    }
}
