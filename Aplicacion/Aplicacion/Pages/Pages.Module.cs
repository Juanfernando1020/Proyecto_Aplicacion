using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Module
{
    internal static class Pages
    {
        internal static void Initialize()
        {
            InitializeDependecyPages();
        }

        private static void InitializeDependecyPages()
        {
            Account.Module.Account.Initialize();
            Admin.Module.Admin.Initialize();
        }
    }
}
