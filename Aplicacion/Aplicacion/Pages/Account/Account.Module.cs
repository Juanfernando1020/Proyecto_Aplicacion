using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pages.Account.Module
{
    internal static class Account
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Test.Module.Test.Initialize();
            Login.Module.Login.Initialize();
        }
    }
}
