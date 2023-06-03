using System;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm;

namespace Aplicacion.Pages.Loan.Models
{
    internal class LoansExtension : ObservableObject
    {
        public LoansExtension()
        {
            Loan = new Loans
            {
                Id = Guid.NewGuid(),
                Installments = new Installments[] { }
            };
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        
        private DateTime _firtsInstallmentDate;
        public DateTime FirtsInstallmentDate
        {
            get => _firtsInstallmentDate; 
            set => SetProperty(ref _firtsInstallmentDate, value);
        }
        
        private Loans _loan;
        public Loans Loan
        { get => _loan; 
            set => SetProperty(ref _loan, value);
        }
    }
}
