namespace Aplicacion.Pages.User.Roles.Worker.Module
{
    internal static class Worker
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Client.Module.Client.Initialize();
            Finance.Module.Finance.Initialize();
        }
    }
}
