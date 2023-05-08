using Aplicacion.Pages.Worker.Client.Billing.Module;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Aplicacion.Pages.Worker.Finance.Base.Module
{
    internal static class Base
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            list.Module.ListBase.Initialize();
        }
    }
}
