using System;
using System.Linq;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Common;

namespace Aplicacion.Pages.Client.Arrangings
{
    internal class ClientsArragangingByInstallmentDate : ArrangingBase<Clients>
    {

        public ClientsArragangingByInstallmentDate()
        {
            this.IsDescendent = true;
        }

        protected override Expression<Func<Clients, object>> ToExpression()
            => client => client.Loans == null || client.Loans.Length == 0 ? (object)client.Name : client.Loans
                .FirstOrDefault(loan => loan.IsActive && 
                                        loan.Installments.Any(installment => installment.IsActive) && 
                                                                            loan.Date.Day == DateTime.Now.Day).Date;
    }
}
