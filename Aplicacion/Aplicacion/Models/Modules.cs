using Aplicacion.Common.PagesBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Models
{
    public class Modules
    {
        public Modules(string name, string page, PagesBaseEnum pageType, Dictionary<string, object> args = null)
        {
            Name = name;
            Page = page;
            PageType = pageType;
            Args = args;
        }

        public string Name { get; set; }
        public string Page { get; set; }
        public PagesBaseEnum PageType { get; set; }
        public Dictionary<string,object> Args { get; set; }
    }
}
