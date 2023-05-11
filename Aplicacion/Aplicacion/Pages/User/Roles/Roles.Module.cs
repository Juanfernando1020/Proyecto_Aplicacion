namespace Aplicacion.Pages.User.Roles.Module
{
    internal static class Roles
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Admin.Module.Admin.Initialize();
            Owner.Module.Owner.Initialize();
            Worker.Module.Worker.Initialize();
        }
    }
}
