using System;

namespace Aplicacion.Pages.Main.Module
{
    internal static class Main
    {
        internal static void Initialize()
        {
            InitializeDependecyPages();
        }

        private static void InitializeDependecyPages()
        {
            Dashboard.Module.MainDashboard.Initialize();
        }
    }
}
