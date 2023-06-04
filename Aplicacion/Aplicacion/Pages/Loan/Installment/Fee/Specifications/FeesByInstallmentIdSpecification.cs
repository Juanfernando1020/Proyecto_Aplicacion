using System;
using System.Linq.Expressions;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Installment.Fee.Specifications
{
    internal class FeesByInstallmentIdSpecification : SpecificationBase<Fees>
    {
        private readonly Guid _installmentId;

        public FeesByInstallmentIdSpecification(Guid installmentId)
        {
            _installmentId = installmentId;
        }

        public override Expression<Func<Fees, bool>> ToExpression()
            => fee => fee.InstallmentId == _installmentId;
    }
}
