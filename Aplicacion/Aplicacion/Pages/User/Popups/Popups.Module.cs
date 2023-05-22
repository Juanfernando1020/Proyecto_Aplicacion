namespace Aplicacion.Pages.User.Popups.Module
{
    internal static class Popups
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            Pickers.Module.Pickers.Initialize();
        }
    }
}
