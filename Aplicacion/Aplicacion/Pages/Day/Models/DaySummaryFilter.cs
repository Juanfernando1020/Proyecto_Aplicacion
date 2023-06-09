using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Models;
using Aplicacion.Pages.Day.Enums;
using Xamarin.CommonToolkit.Specifications;

namespace Aplicacion.Pages.Day.Models
{
    class DaySummaryFilter
    {
        public string Title { get; set; }
        public DaySummaryFilterTypes Type { get; set; }
    }
}
