using Aplicacion.Common.PagesBase.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Aplicacion.Pages.Main.Dashboard.Models
{
    public class MainDashboardItem
    {
        public string Title { get; set; }
        public Page Page { get; set; }
        public PagesBaseEnum PageType { get; set; }
        public Dictionary<string, object> Args { get; set; }

        public MainDashboardItem(string title, Page page, PagesBaseEnum pageType = PagesBaseEnum.ContentPage, Dictionary<string, object> args = null)
        {
            Title = title;
            Page = page;
            PageType = pageType;
            Args = args;
        }
    }
}
