﻿namespace Aplicacion.Pages.User.Module
{
    internal static class User
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.UserCreate.Initialize();
            Roles.Module.Roles.Initialize();
        }
    }
}
