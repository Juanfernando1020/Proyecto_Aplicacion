using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Worker.Module
{
    internal static class Worker
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            ViewExpense.Module.ViewExpense.Initialize();
            AddNewExpense.Module.AddNewExpense.Initialize();
            NewClient.Module.NewClient.Initialize();
        }
    }
}
