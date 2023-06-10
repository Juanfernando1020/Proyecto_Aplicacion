using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Config.Routes
{
    internal static class PopupsRoutes
    {
        internal static class User
        {
            internal const string UserBySpecification = "UserBySpecificationPopup";
        }
        internal static class Expense
        {
            internal const string Create = "ExpenseCreatePopup";
        }
        internal static class Route
        {
            internal static class Basis
            {
                internal const string BasisCreate = "BasisCreatePopup";

                internal static class Cashflow
                {
                    internal const string CashflowCreate = "CashflowCreatePopup";
                }
            }

            internal static class Budget
            {
                internal const string BudgetCreate = "BudgetCreatePopup";
            }
        }

        internal static class Loan
        {
            internal const string LoanCreate = "LoanCreatePopup";

            internal static class Installment
            {
                internal static class Fee
                {
                    internal const string FeeCreate = "FeeCreatePopup";
                }
            }
        }
    }
}
