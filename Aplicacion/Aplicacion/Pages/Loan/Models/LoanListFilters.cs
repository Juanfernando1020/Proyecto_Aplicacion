using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Loan.Models
{
    internal class LoanListFilters
    {
        public string Title { get; set; }
        public SpecificationBase<Loans> Specification { get; set; }
    }
}
