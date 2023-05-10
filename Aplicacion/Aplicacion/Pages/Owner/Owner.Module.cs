using Aplicacion.Pages.Worker.Client.Module;
using Aplicacion.Pages.Worker.Finance.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Owner.Module
{
    internal static class Owner
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Admin.Module.Admin.Initialize();
        }
    }
}
