﻿using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Mvvm;

namespace Aplicacion.Pages.Loan.Installment.Models
{
    internal class InstallmentExtension : ObservableObject
    {
        private Installments _installments;

        public Installments Installment
        {
            get => _installments; 
            set => SetProperty(ref _installments, value);
        }

        public List<Fees> Fees { get; set; }
    }
}