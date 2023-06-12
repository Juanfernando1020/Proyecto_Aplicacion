using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Aplicacion.Config.Routes;
using Aplicacion.Models;
using Aplicacion.Pages.Loan.Installment.Enums;
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
                IsActive = true
            };
            Fees = new List<Fees>();
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        
        private DateTime _firstInstallmentDate;
        public DateTime FirstInstallmentDate
        {
            get => _firstInstallmentDate; 
            set => SetProperty(ref _firstInstallmentDate, value);
        }
        
        private Loans _loan;
        public Loans Loan
        { get => _loan; 
            set => SetProperty(ref _loan, value);
        }
        
        private List<Fees> _fees;
        public List<Fees> Fees
        {
            get => _fees; 
            set => SetProperty(ref _fees, value);
        }

        public int CompletedPayments { get; set; }
        public decimal PayedInstallments { get; set; }
        public decimal PayedInstallmentsPercentage { get; set; }
        public decimal UnpayedInstallments { get; set; }
        public decimal UnpayedInstallmentsPercentage { get; set; }
        public decimal TotalToPay { get; set; }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Date):
                    Loan.Date = Date;
                    break;
                case nameof(Loan):
                    Refresh();
                    break;
                case nameof(Fees):
                    Refresh();
                    break;
            }
        }

        private void Refresh()
        {
            if (Loan != null)
            {
                Date = Loan.Date;
                TotalToPay = 0;
                CompletedPayments = 0;
                if (Loan.Installments is Installments[] installments)
                {
                    CompletedPayments = installments.Count(installment => installment.Status == (int)InstallmentStatusEnum.Complete);
                    TotalToPay = installments.Sum(installment => installment.Amount);
                    UnpayedInstallments = installments.Sum(installment => installment.DiferenceAmount);
                    PayedInstallments = TotalToPay - UnpayedInstallments;
                    PayedInstallmentsPercentage = PayedInstallments / TotalToPay;
                    UnpayedInstallmentsPercentage = UnpayedInstallments / TotalToPay;
                }
            }
        }
    }
}
