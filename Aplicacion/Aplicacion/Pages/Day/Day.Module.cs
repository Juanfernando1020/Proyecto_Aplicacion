using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Day.Module
{
    internal static class Day
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Summary.Module.DaySummary.Initialize();
        }
    }
}
