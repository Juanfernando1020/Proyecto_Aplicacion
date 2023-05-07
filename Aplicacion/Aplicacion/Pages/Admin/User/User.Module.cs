using System;

namespace Aplicacion.Pages.Admin.User.Module
{
    internal static class User
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.CreateUser.Initialize();
        }
    }
}
