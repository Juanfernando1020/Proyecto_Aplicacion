using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Config
{
    static class PagesRoutes
    {
        #region "User Pages"
        static string UserCreatePage => UserCreatePage;
        static string UserDetailsPage => UserDetailsPage;
        #endregion

        #region "Billing Pages"
        static string BillingCreatePage => BillingCreatePage;
        static string BillingDelayPage => BillingDelayPage;
        static string BillingDetailsPage => BillingDetailsPage;
        static string BillingListPage => BillingListPage;
        #endregion

        #region "Loan Pages"
        static string LoanCreatePage => LoanCreatePage;
        static string LoanDetailsPage => LoanDetailsPage;
        static string LoanListPage => LoanListPage;

        #endregion

        #region "Client Pages"
        static string ClientcreatePage => ClientcreatePage;
        static string ClientDetailsPage => ClientDetailsPage;
        static string ClientListPage => ClientListPage;
        #endregion
    }
}
