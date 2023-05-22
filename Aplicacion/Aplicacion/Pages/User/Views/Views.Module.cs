namespace Aplicacion.Pages.User.Views.Module
{
    internal static class Views
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
