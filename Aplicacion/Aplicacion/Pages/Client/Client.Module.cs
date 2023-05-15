namespace Aplicacion.Pages.Client.Module
{
    internal static class Client
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Create.Module.ClientCreate.Initialize();
            Details.Module.ClientDetails.Initialize();
            List.Module.ClientList.Initialize();
        }
    }
}
