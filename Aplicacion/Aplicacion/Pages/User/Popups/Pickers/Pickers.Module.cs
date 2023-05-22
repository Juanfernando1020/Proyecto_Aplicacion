namespace Aplicacion.Pages.User.Popups.Pickers.Module
{
    internal static class Pickers
    {
        internal static void Initialize()
        {
            InitializeDependencyPages();
        }

        private static void InitializeDependencyPages()
        {
            UserBySpecification.Module.UserBySpecification.Initialize();
        }
    }
}
