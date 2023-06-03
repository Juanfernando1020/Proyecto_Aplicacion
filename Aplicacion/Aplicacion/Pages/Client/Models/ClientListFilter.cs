using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Client.Models
{
    internal class ClientListFilter
    {
        public string Name { get; set; }
        public SpecificationBase<Clients> Specification { get; set; }
    }
}
