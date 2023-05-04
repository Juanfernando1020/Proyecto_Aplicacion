namespace Aplicacion.Pages.Admin.Module
{
    internal static class Admin
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Panel.Module.AdminDashboard.Initialize();
        }
    }
}
