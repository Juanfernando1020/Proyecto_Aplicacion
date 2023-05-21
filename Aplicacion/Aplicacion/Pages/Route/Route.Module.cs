using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Route.Module
{
    internal static class Route
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.CreateRoute.Initialize();
            Detail.Module.DetailRoute.Initialize();
            List.Module.ListRoutes.Initialize();
        }
    }
}
