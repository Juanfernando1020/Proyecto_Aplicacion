using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Basis.Module 
{
    internal static class Basis
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }
        private static void InitializeDependencyPages()
        {
            Add.Module.AddBasis.Initialize();
        }
    }
}
