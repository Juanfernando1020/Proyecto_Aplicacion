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
            Budget.Module.Budget.Initialize();
            Create.Module.CreateRoute.Initialize();
            Detail.Module.RouteDetails.Initialize();
            List.Module.RouteList.Initialize();
        }
    }
}
