using Xamarin.CommonToolkit.PagesBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommonToolkit.Mvvm.Navigation.Interfaces;

namespace Aplicacion.Models
{
    public class Modules
    {
        public Modules(string name, string page, PagesBaseEnum pageType, INavigationParameters args = null)
        {
            Name = name;
            Page = page;
            PageType = pageType;
            Args = args;
        }

        public string Name { get; set; }
        public string Page { get; set; }
        public PagesBaseEnum PageType { get; set; }
        public INavigationParameters Args { get; set; }
    }
}
